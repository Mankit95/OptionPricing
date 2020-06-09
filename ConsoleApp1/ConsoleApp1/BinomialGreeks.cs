using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricing
{
    class BinomialGreeks : BinomialPricer//, GreeksInterface
    {
        private double Delta;
        private double Gamma;
        private double Vega;
        private double Theta;

        // constructor calls the Pricer default constructor 
        public BinomialGreeks()
            : base()
        {
        }

        // constructor calls Pricer defined constructor
        public BinomialGreeks(double s, double k, double r, double vol, double t, char option, int steps)
             : base(s, k, r, vol, t, option, steps)
        {
            /*
             *  this constructor gives a call to the Pricer defined constructor whichs makes the following variables
             *  usable in the class
             * 
             *  this.S = s;
             *  this.K = k;
             *  this.R = r;
             *  this.Vol = vol;
             *  this.T = t;
             *  this.Option = option;
             */
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
            Delta = (C[1, 0] - C[0, 0]) / (St[1, 0] - St[0, 0]);
            
        }

        public double delta()
        {
            return Delta;
        }
    }
}
