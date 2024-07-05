using BusinessDomain.BusinessLogic.Payment;
using BusinessDomain.Contracts;
using DataAccess.Entities;
using DecisionAnalysis.Web.Client.Components.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace DecisionAnalysis.Web.Apis;

// Until the site is rendered as WASM, the authorization always fails. So, make sure that your site is rendered as WASM.

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PaymentController(
    IPaymentProcessor paymentProcessor,
    UserManager<ApplicationUser> userManager,
    ILogger<PaymentController> logger) : ControllerBase
{
    [HttpGet]
    public string[] Get()
    {
        return ["Apple", "Banana", "Tormuj"];
    }

    [HttpPost("checkout")]
    public async Task<ActionResult> Checkout(PaymentInstruction paymentInstruction)
    {
        PaymentSession sessionInfo = await paymentProcessor.CreateCheckoutSession(
            paymentInstruction, 
            PaymentSuccess.PageUrl, 
            PaymentFailure.PageUrl,
             nameof(PaymentSuccess.CheckoutSessionId));

        return Ok(sessionInfo.CheckoutUrl);
    }

    [HttpPost("create-customer-portal")]
    public async Task<ActionResult> CreateCustomerPortalSession()
    {
        ApplicationUser? user = await userManager.GetUserAsync(User);

        if (user == null)
            return BadRequest();

        string stripeCustomerId = user.StripeCustomerId;

        if (string.IsNullOrEmpty(stripeCustomerId))
            return NoContent();
        
        string returnUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

        try
        {
            string sessionLink = await paymentProcessor.CreateCustomerPortalSession(stripeCustomerId, returnUrl);
            return Ok(sessionLink);
        }
        catch (Exception e)
        {
            logger.LogError("Stripe Portal Session Link Creation Failed. " + e.Message);
            return BadRequest("Could not find Portal Session");
        }
    }

    [HttpGet("session-info")]
    public async Task<ActionResult> SessionInfo(string sessionId)
    {
        PaymentSession sessionInfo = await paymentProcessor.RetrieveSession(sessionId);

        return Ok(sessionInfo);
    }

    [AllowAnonymous]
    [HttpPost("webhook")]
    public async Task<ActionResult> InstantPaymentNotification()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
       
        try
        {
            Event? stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], paymentProcessor.GetWebHookSecret());

            switch (stripeEvent.Type)
            {
                // Handle the event
                case Events.CheckoutSessionCompleted:
                   // stripeEvent.Data.Object.TryGetValue("id", out var sessionId);
                    break;
                case Events.SubscriptionScheduleAborted:
                    
                    break;
                case Events.SubscriptionScheduleCanceled:
                    
                    break;
                case Events.SubscriptionScheduleCompleted:
                    
                    break;
                case Events.SubscriptionScheduleCreated:
                    
                    break;
                case Events.SubscriptionScheduleExpiring:
                    
                    break;
                case Events.SubscriptionScheduleReleased:
                    
                    break;
                case Events.SubscriptionScheduleUpdated:
                    
                    break;
                // ... handle other event types
                default:
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }

            return Ok();
            
        }
        catch (StripeException e)
        {
            return BadRequest();
        }
    }
}