using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc_Assignment21
{
    public class TempConverter : ITempConverter
    {
        public double CtoF(double c)
        { return (c * 9.0 / 5.0) + 32; }

        public double FtoC(double f)
        { return (f - 32) * 5.0 / 9.0; }
    }

}
