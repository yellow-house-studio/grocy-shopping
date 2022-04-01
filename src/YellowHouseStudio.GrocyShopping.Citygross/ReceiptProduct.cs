namespace YellowHouseStudio.GrocyShopping.Citygross;

public record ReceiptProduct
{
    public string Name { get; init; }

    public string Brand { get; init; }

    public double Price { get; init; }

    public string SingleAmount { get; init; }

    public int Amount { get; init; }
}