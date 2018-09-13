using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Calc_Assignment21
{
    [ServiceContract(Namespace = "urn:myNamespace")]
    public interface ITempConverter
    {
        [OperationContract]
        double CtoF(double c);
        [OperationContract]
        double FtoC(double f);
    }
}
