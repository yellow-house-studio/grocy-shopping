namespace YellowHouseStudio.GrocyShopping.Models;

public interface Product
{
    public string Name { get; }

    public string Brand { get; }

    public double Price { get; }

    public string SingleAmount { get; set; }

    public int Amount { get; set; }
}