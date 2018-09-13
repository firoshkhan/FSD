using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Calc_Assignment21
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.   
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EmpService : IEmployeeService
    {

        public string SayHello(string name)

        {
            string msg;

            if (DateTime.Now.Hour < 12)

            {

                msg = "Good Morning " + name;

                // lblDate.Text = Convert.ToString(DateTime.Now);

            }

            else if (DateTime.Now.Hour < 17)

            {

                //  lblGreeting.Text = "Good Afternoon";

                msg = "Good Afternoon " + name;

            }

            else

            {

                //  lblGreeting.Text = "Good Evening";

                msg = "Good Evening " + name;

            }
            return msg;
        }

        public string GetData(int value)
        {
            return Convert.ToString(value);
        }

        //C- Add Employee Record   
        public string AddEmployyee(Employee emp)
        {
            string result = "";
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["getconn"].ConnectionString;
                SqlConnection con = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand();

                string Query = @"INSERT INTO EmpAssesm22 (EmpID,Name,Email,Phone,Gender)   
                                               Values(@EmpID,@Name,@Email,@Phone,@Gender)";

                cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@EmpID", emp.EmpID);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Phone", emp.Phone);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                result = "Record Added Successfully !";
            }
            catch (FaultException fex)
            {
                result = "Error";
            }

            return result;
        }

        //Retrieve Data   
        //Retrive Record   
        public DataSet RetrieveEmployees()
        {
            DataSet ds = new DataSet();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["getconn"].ConnectionString;
                SqlConnection con = new SqlConnection(connString);
                string Query = "SELECT * FROM EmpAssesm22";

                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                sda.Fill(ds);
            }
            catch (FaultException fex)
            {
                throw new FaultException<string>("Error: " + fex);
            }

            return ds;
        }

        //Delete Record   
        public string DeleteEmployee(Employee emp)
        {
            string result = "";
            string connString = ConfigurationManager.ConnectionStrings["getconn"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            string Query = "DELETE FROM EmpAssesm22 Where EmpID=@EmpID";
            cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@EmpID", emp.EmpID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            result = "Record Deleted Successfully!";
            return result;
        }

        //Search Employee Record   
        public DataSet RetreiveEmployeeByID(Employee emp)
        {
            DataSet ds = new DataSet();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["getconn"].ConnectionString;
                SqlConnection con = new SqlConnection(connString);
                string Query = "SELECT * FROM EmpAssesm22 WHERE EmpID=@EmpID";

                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                sda.SelectCommand.Parameters.AddWithValue("@EmpID", emp.EmpID);
                sda.Fill(ds);
            }
            catch (FaultException fex)
            {
                throw new FaultException<string>("Error:  " + fex);
            }
            return ds;
        }

        //UPDATE RECORDS   
        //Update by Phone Roll    
        public string UpdateEmployee(Employee emp)
        {
            string result = "";
            string connString = ConfigurationManager.ConnectionStrings["getconn"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();

            string Query = "UPDATE EmpAssesm22 SET Email=@Email,Phone=@Phone WHERE EmpID=@EmpID";

            cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@EmpID", emp.EmpID);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@Phone", emp.Phone);
            con.Open();
            cmd.ExecuteNonQuery();
            result = "Record Updated Successfully !";
            con.Close();

            return result;
        }
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.   
    [DataContract]
    public class Employee
    {

        string _empID = "";
        string _name = "";
        string _email = "";
        string _phone = "";
        string _gender = "";

        [DataMember]
        public string EmpID
        {
            get { return _empID; }
            set { _empID = value; }
        }

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [DataMember]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [DataMember]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        [DataMember]
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
    }
}

