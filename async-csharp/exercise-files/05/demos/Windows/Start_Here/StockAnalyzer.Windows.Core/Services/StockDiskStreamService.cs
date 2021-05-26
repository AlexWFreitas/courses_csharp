using StockAnalyzer.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzer.Windows.Core.Services
{
    public class StockDiskStreamService : IStockStreamService
    {
        public async IAsyncEnumerable<StockPrice> 
            GetAllStockPrices([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using var stream = new StreamReader(File.OpenRead("StockPrices_small.csv"));

            await stream.ReadLineAsync(); // Skips header row in the file.

            string line;
            while((line = await stream.ReadLineAsync()) != null)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }

                yield return StockPrice.FromCSV(line);
            }
        }
    }
}
