using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Calc_Assignment21
{
    [ServiceContract]
    public interface IEmployeeService
    {

        [OperationContract]
        string SayHello(string name);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        string AddEmployyee(Employee emp);

        [OperationContract]
        DataSet RetrieveEmployees();

        [OperationContract]
        string DeleteEmployee(Employee emp);

        [OperationContract]
        DataSet RetreiveEmployeeByID(Employee emp);

        [OperationContract]
        string UpdateEmployee(Employee emp);


    }


  
}
