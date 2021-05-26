using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using StockAnalyzer.Core.Domain;

namespace StockAnalyzer.Windows.Core.Services
{
    class MockStockStreamService : IStockStreamService
    {
        public async IAsyncEnumerable<StockPrice>
            GetAllStockPrices([EnumeratorCancellation]CancellationToken cancellationToken = default)
        {
            await Task.Delay(500, cancellationToken);

            yield return new StockPrice { Identifier = "MSFT", Change = 0.5m };

            await Task.Delay(500, cancellationToken);

            yield return new StockPrice { Identifier = "MSFT", Change = 0.5m };

            await Task.Delay(500, cancellationToken);

            yield return new StockPrice { Identifier = "GOOGL", Change = 0.2m };

            await Task.Delay(500, cancellationToken);

            yield return new StockPrice { Identifier = "AAPL", Change = 0.3m };

            await Task.Delay(500, cancellationToken);

            yield return new StockPrice { Identifier = "CAT", Change = 0.6m };
        }
    }
}
