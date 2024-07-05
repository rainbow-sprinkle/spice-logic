namespace BusinessDomain.BusinessLogic.Payment;

public class PaymentSession
{
    public string Id { get; set; } = "";
    public decimal AmountSubtotal { get; set; }
    public decimal AmountTotal { get; set; }
    public string Currency { get; set; } = "";
    public string CustomerId { get; set; } = "";
    public string CustomerEmail { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public string CustomerCountry { get; set; } = "";
    public string CustomerPostalCode { get; set; } = "";
    public bool LiveMode { get; set; }
    public string Mode { get; set; } = "";
    public string PaymentIntent { get; set; } = "";
    public string PaymentStatus { get; set; } = "";
    public string Status { get; set; } = "";
    public string CheckoutUrl { get; set; } = "";
}
 