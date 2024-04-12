namespace TodoApi.Models;
public sealed class Sale
{
    public IEnumerable<Product> Products { get; }
    public string Address { get; }
    public decimal Amount { get; }
    public PaymentMethods.Type PaymentMethod { get; set; }
    public string PurchaseNumber { get; }

    public static string GenerateNextPurchaseNumber()
    {
        Random random = new Random();

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string randomLetters = new string(Enumerable.Repeat(chars, 3)
          .Select(s => s[random.Next(s.Length)]).ToArray());

        int randomNumber = random.Next(100000, 999999);

        string purchaseNumber = $"{randomLetters}-{randomNumber}";

        return purchaseNumber;
    }

    public Sale(IEnumerable<Product> products, string address, decimal amount, PaymentMethods.Type paymentMethod, string purchaseNumber)
    {
        Products = products;
        Address = address;
        Amount = amount;
        PaymentMethod = paymentMethod;
        PurchaseNumber = purchaseNumber;
    }
}