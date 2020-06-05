using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Princing
{
    public interface GreeksInterface
    {
        double delta();
        double gamma();
        double theta();
        double vega();
        double rho();
    }
}
