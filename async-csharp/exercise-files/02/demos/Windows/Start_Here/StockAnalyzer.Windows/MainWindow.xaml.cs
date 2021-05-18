using Newtonsoft.Json;
using StockAnalyzer.Core;
using StockAnalyzer.Core.Domain;
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

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BeforeLoadingStockData();

                var loadLinesTask = Task.Run(() =>
                {
                    try 
                    { 
                        var lines = File.ReadAllLines("StockPrices_Small.csv");

                        return lines;
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                });

                var processStocksTask = loadLinesTask.ContinueWith((completedTask) =>
                {
                    try
                    {
                        var data = new List<StockPrice>();

                        foreach (var line in completedTask.Result.Skip(1))
                        {
                            var price = StockPrice.FromCSV(line);

                            data.Add(price);
                        }

                        var x = Dispatcher.Invoke(() => { return StockIdentifier.Text; });


                        // Código relacionado a Exception Swallow com ContinueWith
                        // Problema com defex
                        /* 
                        var dataFiltered3 = data.Where(sp => sp.Identifier == x).ToList();

                        var dataFiltered = Dispatcher.Invoke(() => { 
                                   return data.Where(sp => sp.Identifier == 
                                   StockIdentifier.Text).ToList(); });
                        

                        if (!dataFiltered.Any())
                        {
                            throw new Exception($"Could not find any stocks.");
                        }
                        */

                        Dispatcher.Invoke(() =>
                        {
                            Stocks.ItemsSource = data.Where(sp => sp.Identifier == StockIdentifier.Text);
                        });
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                });

                // Código relacionado a teste de Thread de ContinueWith
                /* var processStocksTask2 = loadLinesTask.ContinueWith((completedTask) =>
                {
                    var data2 = new List<StockPrice>();

                    foreach (var line in completedTask.Result.Skip(1))
                    {
                        var price = StockPrice.FromCSV(line);

                        data2.Add(price);
                    }
                });
                */

                processStocksTask.ContinueWith( _ => {
                    Dispatcher.Invoke(() => AfterLoadingStockData());
                });
            }
            catch (Exception ex)
            {
                Notes.Text = ex.Message;
            }
        }

        private async Task GetStocks()
        {
            try
            {
                var store = new DataStore();

                var responseTask = store.GetStockPrices(StockIdentifier.Text);

                var data = await responseTask;



                /* Dois cavaleiros do Debug
                var data2 = responseTask.Result;
                var data3 = responseTask;
                var data4 = await responseTask;
                */

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
