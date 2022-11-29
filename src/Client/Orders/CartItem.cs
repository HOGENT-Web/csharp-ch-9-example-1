namespace BogusStore.Client.Orders;

public class CartItem
{
    public int ProductId { get; init; }
    public string Name { get; init; }
    public int Amount { get; set; }
    public decimal Price { get; init; }
    public decimal Total => Price * Amount;

    public CartItem(int productId, string name, decimal price, int amount)
    {
        ProductId = productId;
        Name = name;
        Price = price;
        Amount = amount;
    }
}
