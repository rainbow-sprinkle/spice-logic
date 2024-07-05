namespace FrameworkUtilities.ConfigNames;

public static class PaymentSettingsNames
{
    public const string PayMode = "PaymentGateway:Mode";

    public static class Test
    {
        public const string PublishableKey = "PaymentGateway:Stripe-Test:PublishableKey";
        public const string SecretKey = "PaymentGateway:Stripe-Test:SecretKey";
        public const string WebHookSecret = "PaymentGateway:Stripe-Test:WebHookSecret";
        public const string PricingTableId = "PaymentGateway:Stripe-Test:PricingTableId";
    }
    
    public static class Live
    {
        public const string PublishableKey = "PaymentGateway:Stripe-Live:PublishableKey";
        public const string SecretKey = "PaymentGateway:Stripe-Live:SecretKey";
        public const string WebHookSecret = "PaymentGateway:Stripe-Live:WebHookSecret";
        public const string PricingTableId = "PaymentGateway:Stripe-Live:PricingTableId";
    }
}

