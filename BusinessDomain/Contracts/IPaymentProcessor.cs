using BusinessDomain.BusinessLogic.Payment;

namespace BusinessDomain.Contracts;

public interface IPaymentProcessor
{
    Task<PaymentSession> RetrieveSession(string sessionId);

    Task<PaymentSession> CreateCheckoutSession(PaymentInstruction instruction,
        string successRelativeUrl,
        string failureRelativeUrl,
        string sessionIdQueryStr);

    string GetWebHookSecret();
    PricingTableInfo GetPricingTableInfo();
    Task<string> CreateCustomerPortalSession(string customerId, string returnUrl);
}