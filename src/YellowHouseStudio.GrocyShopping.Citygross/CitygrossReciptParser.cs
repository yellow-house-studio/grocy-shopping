using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace YellowHouseStudio.GrocyShopping.Citygross
{
    public class CitygrossReciptParser
    {
        private Regex _regex = new Regex(@"(\w[^&nbsp;])+");

        public CitygrossReciptParser()
        {

        }

        public async Task<IEnumerable<ReceiptProduct>> ParseEmail(string html)
        {
            var rows = GetRowsWithProducts(html);
            var bought = ParseAllProducts(rows);

            return bought;
        }

        private static List<HtmlNode> GetRowsWithProducts(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var productTable = doc.DocumentNode.Descendants("table")
                .FirstOrDefault(x => Regex.Matches(x.InnerText, @"(\w)+")[0].Value == "Varor");

            var rows = productTable.Descendants("table").First().ChildNodes.Where(x => x.Name == "tr").ToList();
            return rows;
        }

        private List<ReceiptProduct> ParseAllProducts(List<HtmlNode> rows)
        {
            var bought = new List<ReceiptProduct>();

            while (rows.Count > 0)
            {
                var row = ProcessRow(rows);

                if (_regex.IsMatch(row.InnerText))
                {
                    var product = ParseProduct(rows, row);

                    bought.Add(product);
                }
            }

            return bought;
        }

        private static ReceiptProduct ParseProduct(List<HtmlNode> rows, HtmlNode row)
        {
            var firstParts = row.Descendants("p");
            var secondParts = ProcessRow(rows).Descendants("p");
            var thirdPart = secondParts.ElementAt(0).InnerText
                .Split("-", StringSplitOptions.RemoveEmptyEntries);
            var product = new ReceiptProduct
            {
                Name = firstParts.ElementAt(0).InnerText,
                Amount = ParseAmount(firstParts.ElementAt(1).InnerText),
                Brand = thirdPart[0].Trim(),
                Price = ParsePrice(secondParts.ElementAt(1).InnerText),
                SingleAmount = thirdPart[1].Trim()
            };

            return product;
        }

        private static int ParseAmount(string amountText)
        {
            var amountString = Regex.Match(amountText, @"^\d+").Value;

            var amount = Convert.ToInt32(amountString);

            return amount;
        }

        private static double ParsePrice(string priceText)
        {
            var value = Regex.Match(priceText, @"^([\d,])+").Value;
            return Convert.ToDouble(value);
        }

        private static HtmlNode ProcessRow(List<HtmlNode> rows)
        {
            var row = rows.First();
            rows.RemoveAt(0);
            return row;
        }
    }
}
