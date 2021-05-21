using Newtonsoft.Json;
using StockAnalyzer.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzer.Core.Services
{
    public interface IStockService
    {
        Task<IEnumerable<StockPrice>> GetStockPricesFor(string stockIdentifier,
            CancellationToken cancellationToken);
    }

    public class StockService : IStockService
    {
        private static string API_URL = "https://ps-async.fekberg.com/api/stocks";
        private int i = 0;

        public async Task<IEnumerable<StockPrice>>
            GetStockPricesFor(string stockIdentifier,
                              CancellationToken cancellationToken)
        {
            // Simulate that each time this method is called
            // it takes a little bit longer.
            //
            // DO NOT DO THIS IN PRODUCTION...
            await Task.Delay((i++) * 1000);

            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{API_URL}/{stockIdentifier}",
                    cancellationToken);

                result.EnsureSuccessStatusCode();

                var content = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<StockPrice>>(content);
            }
        }
    }

    public class MockStockService : IStockService
    {
        private int i = 0;
        public async Task<IEnumerable<StockPrice>> GetStockPricesFor(string stockIdentifier, CancellationToken cancellationToken)
        {

            await Task.Delay((i++) * 2000);

            var stocks = new List<StockPrice>
            {
                new StockPrice
                {
                    Identifier = "MSFT",
                    Change = 0.5m,
                    ChangePercent = 0.75m
                },
                new StockPrice
                {
                    Identifier = "GOOGL",
                    Change = 0.4m,
                    ChangePercent = 0.75m
                },
                new StockPrice
                {
                    Identifier = "AAPL",
                    Change = 0.3m,
                    ChangePercent = 0.75m
                },
                new StockPrice
                {
                    Identifier = "CAT",
                    Change = 0.2m,
                    ChangePercent = 0.75m
                }
            };

            var task = Task.FromResult(stocks.Where(stock => stock.Identifier == stockIdentifier));

            if (cancellationToken.IsCancellationRequested)
            {
                throw new System.Exception("Search was canceled.");
            }

            if (!task.Result.Any())
            {
                throw new System.Exception("Search has returned no results.");
            }

            return task.Result;
        }
    }
}
