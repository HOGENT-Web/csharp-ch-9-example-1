using System;
namespace BogusStore.Client.Orders;

public class Cart
{
    private readonly List<CartItem> items = new();
    private void NotifyCartChanged() => OnCartChanged?.Invoke();


    public IReadOnlyList<CartItem> Items => items.AsReadOnly();
    public event Action? OnCartChanged;
    public decimal Total => items.Sum(x => x.Total);

    public void AddItem(int productId, string name, decimal price)
    {
        var existingItem = items.SingleOrDefault(x => x.ProductId == productId);

        if (existingItem == null)
        {
            CartItem item = new CartItem(productId, name, price, 1);
            items.Add(item);
        }
        else
        {
            existingItem.Amount++;
        }
        NotifyCartChanged();
    }

    public void RemoveItem(CartItem item)
    {
        items.Remove(item);
        NotifyCartChanged();
    }
}
