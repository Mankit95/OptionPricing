using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Princing
{
    public interface PricerInterface
    {
        /*
         *  The interface implemetation method
         *  Pricing()  : method for option price calculation
         *  
         *  The interface implemetation property
         *  S : Initial stock price
         *  K : Strike price
         *  R : Risk free rate
         *  Vol : Stock volatility
         *  T : Time to maturity
         *  Option : option type : call 'c', put 'p'
         */

        double S { get; set; }
        double K { get; set; }
        double R { get; set; }
        double Vol { get; set; }
        double T { get; set; }
        char Option { get; set; }
        double Pricing();
    }
}
