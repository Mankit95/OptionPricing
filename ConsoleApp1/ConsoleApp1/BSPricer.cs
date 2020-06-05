using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Princing
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
        public static double CND(double z)
        {
            double p = 0.3275911;
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;

            int sign;
            if (z < 0.0)
                sign = -1;
            else
                sign = 1;

            double x = Math.Abs(z) / Math.Sqrt(2.0);
            double t = 1.0 / (1.0 + p * x);
            double erf = 1.0 - (((((a5 * t + a4) * t) + a3)
              * t + a2) * t + a1) * t * Math.Exp(-x * x);
            return 0.5 * (1.0 + sign * erf);
        }


        // calculating d1 and d2
        public double d1()
        {
            double D1 = (Math.Log10(S / K) + (R + Vol * Vol * 0.5) * T) / (Vol * Math.Sqrt(T));
            return D1;
        }

        public double d2()
        {
            double D2 = (Math.Log10(S / K) + (R - Vol * Vol * 0.5) * T) / (Vol * Math.Sqrt(T));
            return D2;
        }


        // black scholes price calculation
        public double Pricing()
        {
            if (Option.Equals('c'))
            {
                double price = S * CND(d1()) - CND(d2() * K * Math.Exp(-R * T));
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