using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricing
{
    /*
    *  Implemeting the Black Scholes Opton pricing method.(for European type option)
    *  Comumulative normal distribution function is implemented using the Abromowitz and Stegun approximation method.
    *  Parameters are defines as :
    *      s : Initial stock price
    *      k : Strike price
    *      r : Risk free rate
    *    vol : Stock volatility
    *      t : Time to maturity
    * option : option type : call 'c', put 'p'
    */

    public class BSPricer : PricerInterface
    {
        // pricing parameters

        private double s;
        private double k;
        private double r;
        private double vol;
        private double t;
        private char option;

        // constructors
        public BSPricer()
        {
        }

        public BSPricer(double s, double k, double r, double vol, double t, char option)
        {
            this.S = s;
            this.K = k;
            this.R = r;
            this.Vol = vol;
            this.T = t;
            this.Option = option;

        }

        // properties get and set
        public double S { get => s; set => s = value; }
        public double K { get => k; set => k = value; }
        public double R { get => r; set => r = value; }
        public double Vol { get => vol; set => vol = value; }
        public double T { get => t; set => t = value; }
        public char Option { get => option; set => option = value; }

        // Cumulative normal fuction
        // Abromowitz and Stegun approximation
        public double CND(double X)
        {
            double L = 0.0;
            double K = 0.0;
            double dCND = 0.0;
            const double a1 = 0.31938153;
            const double a2 = -0.356563782;
            const double a3 = 1.781477937;
            const double a4 = -1.821255978;
            const double a5 = 1.330274429;
            L = Math.Abs(X);
            K = 1.0 / (1.0 + 0.2316419 * L);
            dCND = 1.0 - 1.0 / Math.Sqrt(2 * Convert.ToDouble(Math.PI.ToString())) *
                Math.Exp(-L * L / 2.0) * (a1 * K + a2 * K * K + a3 * Math.Pow(K, 3.0) +
                a4 * Math.Pow(K, 4.0) + a5 * Math.Pow(K, 5.0));

            if (X < 0)
            {
                return 1.0 - dCND;
            }
            else
            {
                return dCND;
            }
        }


        // calculating d1 and d2
        public double d1()
        {
            double D1 = (Math.Log(S / K) + (R + Vol * Vol * 0.5) * T) / (Vol * Math.Sqrt(T));
            return D1;
        }

        public double d2()
        {
            double D2 = (Math.Log(S / K) + (R - Vol * Vol * 0.5) * T) / (Vol * Math.Sqrt(T));
            return D2;
        }


        // black scholes price calculation
        public double Pricing()
        {
            if (Option.Equals('c'))
            {
                double price = S * CND(d1()) - CND(d2()) * K * Math.Exp(-R * T);
                return price;
            }
            else if (Option.Equals('p'))
            {
                double price = CND(-d2()) * K * Math.Exp(-R * T) - CND(-d1()) * S;
                return price;
            }
            else
            {
                Console.WriteLine("Option type not valid!");
                return 0;
            }

        }

    }
}