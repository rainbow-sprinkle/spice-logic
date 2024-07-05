namespace BusinessDomain.BusinessLogic.Payment;

public class PaymentInstruction
{
    public string ExistingStripeCustomerId { get; set; } = "";
    
    public decimal UnitPrice { get; set; }
    
    public int Quantity { get; set; }

    public string Currency { get; set; } = "";
    
    public string ProductName { get; set; } = "";
    
    public string Description { get; set; } = "";
    
    public string ImageUrl { get; set; } = "";
    
    public string CustomerEmail { get; set; } = "";
    
    public string BaseUrl { get; set; } = "";
}