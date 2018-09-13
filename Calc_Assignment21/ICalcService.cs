using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace Calc_Assignment21
{
    [ServiceContract]
    public interface ICalcService

    {

        [OperationContract]
        string SayHello(string name );

        [OperationContract]
        string TodayProgram(string name);

        [OperationContract]

        double Add(double n1, double n2);

        [OperationContract]

        double Subtract(double n1, double n2);

        [OperationContract]

        double Multiply(double n1, double n2);

        [OperationContract]

        double Divide(double n1, double n2);


        [OperationContract]

        IEnumerable <Jobs> OpeningJobs();

        [OperationContract]

        IEnumerable<Jobs> OpeningJobsByRole( string role );

    }

    public class Jobs
    {
        public string Name { get; set; }
        public string Role { get; set; }
       
    }

}

