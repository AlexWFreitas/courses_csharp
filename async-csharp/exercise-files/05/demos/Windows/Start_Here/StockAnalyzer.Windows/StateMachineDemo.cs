using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.Windows
{
    class StateMachineDemo
    {
        public Task<string> Run()
        {
            var result = Compute();

            return result;
        }

        public Task<string> Compute()
        {
            var result = Load();

            return result;
        }

        public Task<string> Load()
        {
            return Task.Run(() => "PluralSight");
        }
    }
}
