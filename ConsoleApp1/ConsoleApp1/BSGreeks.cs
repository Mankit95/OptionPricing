using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Princing
{
    class BSGreeks : BSPricer, GreeksInterface
    {
        // constructor calls the Pricer default constructor 
        public BSGreeks()
            : base()
        {

        }

        // constructor calls Pricer defined constructor
        public BSGreeks(double s, double k, double r, double vol, double t, char option)
            : base(s, k, r, vol, t, option)
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

        }

        // method to calculate delta : The first derivative of the option price with respect to the underlying.
        public double delta()
        {
            if (Option.Equals('c'))
            {
                double delta_ = CND(d1());
                return delta_;
            }
            else if (Option.Equals('p'))
            {
                double delta_ = CND(d2());
                return delta_;
            }
            else
            {
                return 0;
            }
        }

        // method to calculate gamma : The second derivative of the option price wrt the underlying stock. These are equal for puts and calls 
        public double gamma()
        {
            if (Option.Equals('c'))
            {
                double gamma_ = CND(d1()) / (S * Vol * Math.Sqrt(T));
                return gamma_;
            }
            else if (Option.Equals('p'))
            {
                double gamma_ = CND(d2()) / (S * Vol * Math.Sqrt(T));
                return gamma_;
            }
            else
            {
                return 0;
            }
        }

        // method to calculate theta : The partial with respect to time-to-maturity. 
        public double theta()
        {
            if (Option.Equals('c'))
            {
                double theta_ = -((CND(d1()) * S * Vol) / (2.0 * Math.Sqrt(T))) - R * K * Math.Exp(-R * T) * CND(d2());
                return theta_;
            }
            else if (Option.Equals('p'))
            {
                double theta_ = -((CND(d1()) * S * Vol) / (2.0 * Math.Sqrt(T))) - R * K * Math.Exp(-R * T) * CND(-d2());
                return theta_;
            }
            else
            {
                return 0;
            }
        }

        // method to calculate vega : The partial with respect to volatility. 
        public double vega()
        {
            if (Option.Equals('c'))
            {
                double vega_ = S * T * CND(d1());
                return vega_;
            }
            else if (Option.Equals('p'))
            {
                double vega_ = S * T * CND(d1());
                return vega_;
            }
            else
            {
                return 0;
            }
        }

        // method to calculate rho : The partial with respect to the interest rate. 
        public double rho()
        {
            if (Option.Equals('c'))
            {
                double rho_ = K * T * Math.Exp(-R * T) * CND(d2());
                return rho_;
            }
            else if (Option.Equals('p'))
            {
                double rho_ = -K * T * Math.Exp(-R * T) * CND(-d2());
                return rho_;
            }
            else
            {
                return 0;
            }
        }



    }

}