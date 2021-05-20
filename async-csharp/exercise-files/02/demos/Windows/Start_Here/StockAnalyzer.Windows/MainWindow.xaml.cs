using Newtonsoft.Json;
using StockAnalyzer.Core;
using StockAnalyzer.Core.Domain;
using StockAnalyzer.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace StockAnalyzer.Windows
{
    public partial class MainWindow : Window
    {
        private static string API_URL = "https://ps-async.fekberg.com/api/stocks";
        private Stopwatch stopwatch = new Stopwatch();
        CancellationTokenSource cancellationTokenSource;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            // Cancels the current Search if already searching
            if(cancellationTokenSource != null)
            {
                CancelSearchClick();
                return;
            }

            try
            {
                // Instantiates a CancellationTokenSource object ( Means that the search is running )
                cancellationTokenSource = new CancellationTokenSource();
                CancellationToken token = cancellationTokenSource.Token;

                // Register Callback - Register a delegate to call when a cancellation request is made.
                token.Register(() => Notes.Text = "Cancellation requested.");

                // Set UI Elements to the proper setting at this point in time
                Search.Content = "Cancel"; // Button text
                Stocks.ItemsSource = null; // Clears the table content

                // Displays the loading bar and starts stop watch
                BeforeLoadingStockData();

                var service = new StockService();

                var data = await service.GetStockPricesFor(StockIdentifier.Text, token);

                Stocks.ItemsSource = data;
                

                // Data Store Method
                // LoadStocksFromDataStore(token);

            }
            catch (Exception ex)
            {
                Notes.Text = ex.Message;
            }
            finally
            {
                AfterLoadingStockData();
                cancellationTokenSource = null;
                Search.Content = "Search";
            }
        }

        private void LoadStocksFromDataStore(CancellationToken token)
        {
            // Load Lines Task
            Task<List<string>> loadLinesTask = SearchForStocks(token);

            // Load Lines - Continuations
            var processStocksTask = loadLinesTask.ContinueWith(t => ProcessStocks(t), TaskContinuationOptions.OnlyOnRanToCompletion);
            loadLinesTask.ContinueWith(t => LoadLineFail(t), TaskContinuationOptions.OnlyOnFaulted);

            // Process Stocks - Continuations
            var filterStocksTask = processStocksTask.ContinueWith(t => FilterStocks(t), token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Current);

            // Filter Stocks - Continuations
            var updateStocksTask = filterStocksTask.ContinueWith(t => UpdateStocks(t), token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Current);
            filterStocksTask.ContinueWith(t => FilterDataFail(t), TaskContinuationOptions.OnlyOnFaulted);

            // Update Stocks - Continuations
            // Removes loading animation, restores search button text, assigns null to cancellationTokenSource
            updateStocksTask.ContinueWith(_ => AfterTaskCleanUp(), token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Current);
        }

        // Continuation Methods

        private void AfterTaskCleanUp()
        {
            Dispatcher.Invoke(() =>
            {
                Search.Content = "Search";

                cancellationTokenSource = null;

                AfterLoadingStockData();
            });
        }

        private void CancelSearchClick()
        {
            // Already have an instance of the cancellation token source?
            // This means the button has already been pressed!

            cancellationTokenSource.Cancel();
            cancellationTokenSource = null;

            Search.Content = "Search";
            AfterLoadingStockData();
            return;
        }

        private void FilterDataFail(Task<List<StockPrice>> faultedTask)
        {
            Dispatcher.Invoke(() =>
            {
                AfterLoadingStockData();
                Notes.Text = faultedTask.Exception.InnerException.Message;
                Search.Content = "Search";
            });
        }
        private List<StockPrice> FilterStocks(Task<List<StockPrice>> completedTask)
        {
            var selectionText = Dispatcher.Invoke(() => { return StockIdentifier.Text; });

            var data = completedTask.Result;

            var dataFilteredReadable = data.Where(sp => sp.Identifier == selectionText).ToList();

            if (!dataFilteredReadable.Any())
            {
                throw new Exception($"Could not find any stocks.");
            }

            return data;
        }

        private void LoadLineFail(Task<List<string>> faultedTask)
        {
            Dispatcher.Invoke(() =>
            {
                AfterLoadingStockData();
                Notes.Text = faultedTask.Exception.InnerException.Message;
                Search.Content = "Search";
            });
        }

        private List<StockPrice> ProcessStocks(Task<List<string>> completedTask)
        {
            var data = new List<StockPrice>();

            var lines = completedTask.Result;

            foreach (var line in lines.Skip(1))
            {
                var price = StockPrice.FromCSV(line);

                data.Add(price);
            }

            return data;
        }

        private static Task<List<string>> SearchForStocks(CancellationToken token)
        {
            var resultTask = Task.Run(async () =>
            {
                using (var stream = new StreamReader(File.OpenRead("StockPrices_Small.csv")))
                {
                    var lines = new List<string>();

                    string line;
                    while ((line = await stream.ReadLineAsync()) != null)
                    {
                        if (token.IsCancellationRequested)
                        {
                            break;
                        }
                        lines.Add(line);
                    }

                    return lines;
                }
            }, token);

            return resultTask;
        }

        private void UpdateStocks(Task<List<StockPrice>> completedTask)
        {
            var data = completedTask.Result;

            Dispatcher.Invoke(() =>
            {
                Stocks.ItemsSource = data.Where(sp => sp.Identifier == StockIdentifier.Text);
            });
        }


        private async Task GetStocks()
        {
            try
            {
                var store = new DataStore();

                var responseTask = store.GetStockPrices(StockIdentifier.Text);

                var data = await responseTask;

               Stocks.ItemsSource = data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BeforeLoadingStockData()
        {
            stopwatch.Restart();
            StockProgress.Visibility = Visibility.Visible;
            StockProgress.IsIndeterminate = true;
        }

        private void AfterLoadingStockData()
        {
            StocksStatus.Text = $"Loaded stocks for {StockIdentifier.Text} in {stopwatch.ElapsedMilliseconds}ms";
            StockProgress.Visibility = Visibility.Hidden;
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));

            e.Handled = true;
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
