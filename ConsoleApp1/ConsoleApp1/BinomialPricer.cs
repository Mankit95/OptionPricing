using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricing
{
    class BinomialPricer : PricerInterface
    {
        /*
         *  Implemeting the Binomial Tree Model Opton pricing method.(for American type option)
         *  Comumulative normal distribution function is implemented using the Abromowitz and Stegun approximation method.
         *  Parameters are defines as :
         *      s : Initial stock price
         *      k : Strike price
         *      r : Risk free rate
         *    vol : Stock volatility
         *      t : Time to maturity
         * option : option type : call 'c', put 'p'
         *  steps : Steps parameter in binomial model
         */

        // pricing parameter
        private double s;
        private double k;
        private double r;
        private double vol;
        private double t;
        private char option;
        private int steps;

        // constructors
        public BinomialPricer()
        {
        }

        public BinomialPricer(double s, double k, double r, double vol, double t, char option, int steps)
        {
            this.S = s;
            this.K = k;
            this.R = r;
            this.Vol = vol;
            this.T = t;
            this.Option = option;
            this.Steps = steps;
        }

        // properties get and set
        public double S { get => s; set => s = value; }
        public double K { get => k; set => k = value; }
        public double R { get => r; set => r = value; }
        public double Vol { get => vol; set => vol = value; }
        public double T { get => t; set => t = value; }
        public char Option { get => option; set => option = value; }
        public int Steps { get => steps; set => steps = value; }

        // Price calculation
        public double Pricing()
        {
            double u = OptionUp(T, Vol, Steps);
            double d = OptionDown(T, Vol, Steps);
            double p = Probability(T, Vol, Steps, R);
            double q = 1.0 - p;

            double[,] St = new double[Steps + 1, Steps + 1];
            double[,] C = new double[Steps + 1, Steps + 1];

            for (int i = 0; i < Steps + 1; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    St[i, j] = S * Math.Pow(u, (double)(i - j)) * Math.Pow(d, (double)j);
                }
            }
            for (int i = Steps; i >= 0; i--)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    if (i == Steps)
                    {
                        C[i, j] = Payoff(St[i, j], K, Option);
                    }
                    else
                    {
                        C[i, j] = Math.Max(Payoff(St[i, j], K, Option), 1 / Math.Exp(R * (T / Steps)) * (p * C[i + 1, j] + q * C[i + 1, j + 1]));
                    }
                    
                }
            }
            return C[0, 0];

        }

        // Up and Down factors
        public double OptionUp(double t, double s, int n)
        {
            return Math.Exp(s * Math.Sqrt(t / n));
        }
        public double OptionDown(double t, double s, int n)
        {
            return Math.Exp(-s * Math.Sqrt(t / n));
        }

        // Up probability calculation
        public double Probability(double t, double s, int n, double r)
        {
            double d1 = Math.Exp(r * (t / n));
            double d2 = OptionUp(t, s, n);
            double d3 = OptionDown(t, s, n);
            return (d1 - d3) / (d2 - d3);
        }

        // Payoff
        public double Payoff(double S, double K, char CallorPut)
        {
            switch (CallorPut)
            {
                case 'c':
                    return Call(S, K);

                case 'p':
                    return Put(S, K);

                default:
                    return 0.0;
            }
        }

        private double Call(double S, double K)
        {
            return Math.Max(0.0, S - K);
        }

        private double Put(double S, double K)
        {
            return Math.Max(0.0, K - S);
        }

    }
}
