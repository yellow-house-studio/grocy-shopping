using NUnit.Framework;
using YellowHouseStudio.GrocyShopping.Citygross;
using YellowHouseStudio.Tests.TestHelpers.TestFactory;

namespace YellowHouseStudio.Tests.UnitTests.GrocyShopping.Citygross;

[TestFixture]
public class CitygrossReciptParser_Specs
{
    private CitygrossReciptParser SUT;
    private readonly TestFactory _testFactory = new();

    [SetUp]
    public void Setup()
    {
        SUT = new CitygrossReciptParser();
    }

    [Test]
    public async Task ParseEmail_should_parse_email()
    {
        // Given
        var file = await File.ReadAllTextAsync("./GrocyShopping.Citygross/TestData/Receipt1.htm");

        // When
        var result = await SUT.ParseEmail(file);

        // Then 
        Assert.AreEqual(41, result.Count());
        Assert.AreEqual("Rysk Yoghurt 17%", result.ElementAt(0).Name);
        Assert.AreEqual(3, result.First().Amount);
        Assert.AreEqual("Salakis", result.First().Brand);
        Assert.AreEqual("500g", result.First().SingleAmount);
        Assert.AreEqual(85.50, result.First().Price);
    }
}