using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricing
{
    class Program
    {
        static void Main(string[] args)
        {
            double spotPrice = 100;
            double exercisePrice = 100;
            double riskFreeRate = 0.1;
            double volatility = 0.3;
            double time = 0.5;
            char option = 'c';

            BinomialPricer a = new BinomialPricer(spotPrice, exercisePrice, riskFreeRate, volatility, time, option, 200);
            BinomialGreeks b = new BinomialGreeks(spotPrice, exercisePrice, riskFreeRate, volatility, time, option, 200);
            BSPricer c = new BSPricer(spotPrice, exercisePrice, riskFreeRate, volatility, time, option);
            BSGreeks d = new BSGreeks(spotPrice, exercisePrice, riskFreeRate, volatility, time, option);


            double price1 = a.Pricing();
            Console.WriteLine(price1);
            double delta1 = b.delta();
            Console.WriteLine(delta1);

            double price2 = c.Pricing();
            Console.WriteLine(price2);
            double delta2 = d.delta();
            Console.WriteLine(delta2);

            Console.ReadKey();
        }
    }
}
