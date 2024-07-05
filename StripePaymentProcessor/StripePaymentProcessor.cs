using BusinessDomain.BusinessLogic.Payment;
using BusinessDomain.Contracts;
using FrameworkUtilities.ConfigNames;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace StripePaymentProcessor;

public class StripePaymentProcessor : IPaymentProcessor
{
    private readonly IConfiguration _configuration;
    
    public StripePaymentProcessor(IConfiguration configuration)
    {
        _configuration = configuration;
        
        bool isTestMode = configuration.GetValue<string>(PaymentSettingsNames.PayMode) == "Test";

        string secretKey;

        if (isTestMode)
        {
            secretKey = configuration.GetValue<string>(PaymentSettingsNames.Test.SecretKey) ?? "";
        }
        else
        {
            secretKey = configuration.GetValue<string>(PaymentSettingsNames.Live.SecretKey) ?? "";
        }

        StripeConfiguration.ApiKey = secretKey;
    }

    public PricingTableInfo GetPricingTableInfo()
    {
        bool isTestMode = _configuration.GetValue<string>(PaymentSettingsNames.PayMode) == "Test";

        string publishableKey;
        string pricingTableId;
        
        if (isTestMode)
        {
            publishableKey = _configuration.GetValue<string>(PaymentSettingsNames.Test.PublishableKey) ?? "";
            pricingTableId = _configuration.GetValue<string>(PaymentSettingsNames.Test.PricingTableId) ?? "";
        }
        else
        {
            publishableKey = _configuration.GetValue<string>(PaymentSettingsNames.Live.PublishableKey) ?? "";
            pricingTableId = _configuration.GetValue<string>(PaymentSettingsNames.Live.PricingTableId) ?? "";
        }

        return new PricingTableInfo(publishableKey, pricingTableId);
    }
    
    public string GetWebHookSecret()
    {
        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        bool isTestMode = _configuration.GetValue<string>(PaymentSettingsNames.PayMode) == "Test";

        string secretKey;

        if (isTestMode)
        {
            secretKey = _configuration.GetValue<string>(PaymentSettingsNames.Test.SecretKey) ?? "";
        }
        else
        {
            secretKey = _configuration.GetValue<string>(PaymentSettingsNames.Live.SecretKey) ?? "";
        }
        
        return secretKey;
    }

    public async Task<PaymentSession> RetrieveSession(string sessionId)
    {
        SessionService sessionService = new();
        Session? session = await sessionService.GetAsync(sessionId);

        return CreatePaymentSession(session);
    }

    public async Task<PaymentSession> CreateCheckoutSession(
        PaymentInstruction instruction,
        string successRelativeUrl,
        string failureRelativeUrl,
        string sessionIdQueryStr)
    {
        List<SessionLineItemOptions> lineItems =
        [
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = instruction.Currency,
                    UnitAmountDecimal = instruction.UnitPrice * 100,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = instruction.ProductName,
                        Description = instruction.Description,
                        Images = [instruction.ImageUrl]
                    }
                },
                Quantity = instruction.Quantity
            }
        ];

        SessionCreateOptions options = new()
        {
            PaymentMethodTypes = ["card"],
            LineItems = lineItems,
            Customer = string.IsNullOrWhiteSpace(instruction.ExistingStripeCustomerId) ? null : instruction.ExistingStripeCustomerId,
            Mode = "payment",
            SuccessUrl = $"{instruction.BaseUrl}{successRelativeUrl}?{sessionIdQueryStr}={{CHECKOUT_SESSION_ID}}",
            CancelUrl = $"{instruction.BaseUrl}{failureRelativeUrl}",
            Currency = instruction.Currency,
            CustomerCreation = "always",
            CustomerEmail = string.IsNullOrWhiteSpace(instruction.ExistingStripeCustomerId) ? instruction.CustomerEmail : null
        };

        SessionService service = new();

        if (string.IsNullOrEmpty(StripeConfiguration.ApiKey))
        {
            return new PaymentSession();
        }
        
        Session session = await service.CreateAsync(options);

        return CreatePaymentSession(session);
    }

    public async Task<string> CreateCustomerPortalSession(string customerId, string returnUrl)
    {
        if (string.IsNullOrEmpty(StripeConfiguration.ApiKey))
        {
            return string.Empty;
        }

        var options = new Stripe.BillingPortal.SessionCreateOptions
        { 
            Customer = customerId,
            ReturnUrl = returnUrl
        };
       
        var service = new Stripe.BillingPortal.SessionService();
        var session = await service.CreateAsync(options);
        return session.Url;
    }
    
    private static PaymentSession CreatePaymentSession(Session session)
    {
        return new PaymentSession
        {
            Id = session.Id ?? "",
            AmountSubtotal = session.AmountSubtotal ?? 0,
            AmountTotal = session.AmountTotal ?? 0,
            Currency = session.Currency ?? "",
            CustomerId = session.CustomerId,
            CustomerEmail = session.CustomerEmail ?? "",
            CustomerName = session.CustomerDetails?.Name ?? "",
            CustomerCountry = session.CustomerDetails?.Address?.Country ?? "",
            CustomerPostalCode = session.CustomerDetails?.Address?.PostalCode ?? "",
            LiveMode = session.Livemode,
            Mode = session.Mode ?? "",
            PaymentIntent = session.PaymentIntent?.ToString() ?? "",
            PaymentStatus = session.PaymentStatus ?? "",
            Status = session.Status ?? "",
            CheckoutUrl = session.Url ?? ""
        };
    }
}
