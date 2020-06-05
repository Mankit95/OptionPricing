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
            BinomialPricer a = new BinomialPricer(100, 100, 0.1, 0.3, 0.5, 'c', 100);
            double price = a.Pricing();
            Console.WriteLine(price);
            Console.ReadKey();
        }
    }
}
