﻿using Newtonsoft.Json;
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

        public MainWindow()
        {
            InitializeComponent();
        }



        CancellationTokenSource cancellationTokenSource;

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            if(cancellationTokenSource != null)
            {
                // Already have an instance of the cancellation token source?
                // This means the button has already been pressed!

                cancellationTokenSource.Cancel();
                cancellationTokenSource = null;

                Search.Content = "Search";
                AfterLoadingStockData();
                return;
            }

            try
            {
                cancellationTokenSource = new CancellationTokenSource();
                var token = cancellationTokenSource.Token;
                Search.Content = "Cancel"; // Button text
                Stocks.ItemsSource = null;
                Notes.Text = null;

                BeforeLoadingStockData();

                var service = new StockService();

                var identifiers = StockIdentifier.Text
                                                 .Split(',', ' ');

                var loadingTasks = new List<Task<IEnumerable<StockPrice>>>();

                foreach (var identifier in identifiers)
                {
                    var loadTask = service.GetStockPricesFor(identifier, token);
                    loadingTasks.Add(loadTask);
                }

                var timeoutTask = Task.Delay(4000);
                var allStocksLoadingTask = Task.WhenAll(loadingTasks);

                var completedTask = await Task.WhenAny(timeoutTask, allStocksLoadingTask);

                if (completedTask == timeoutTask && cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                    throw new OperationCanceledException("Timeout!");
                }

                if (allStocksLoadingTask.IsCompleted)
                {
                    var data = allStocksLoadingTask.Result;

                    var flattenedData = data.SelectMany(x => x);

                    Stocks.ItemsSource = flattenedData;
                }
            }
            catch (AggregateException ex)
            {
                var comboString = ex.Message;

                if (ex.InnerExceptions != null)
                {
                    foreach (var exception in ex.InnerExceptions)
                    {
                        comboString += $"\n{exception.Message}";
                    }
                }

                Notes.Text = comboString;
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









        private static Task<List<string>> 
            SearchForStocks(CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                using (var stream = new StreamReader(File.OpenRead("StockPrices_Small.csv")))
                {
                    var lines = new List<string>();

                    string line;
                    while ((line = await stream.ReadLineAsync()) != null)
                    {
                        if(cancellationToken.IsCancellationRequested)
                        {
                            break;
                        }

                        lines.Add(line);
                    }

                    return lines;
                }
            }, cancellationToken);
        }

        private async Task GetStocks()
        {
            try
            {
                var store = new DataStore();

                var responseTask = store.GetStockPrices(StockIdentifier.Text);

                Stocks.ItemsSource = await responseTask;
            }
            catch (Exception ex)
            {
                throw ex;
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
