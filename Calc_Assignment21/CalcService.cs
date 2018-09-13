using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Calc_Assignment21
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CalcService : ICalcService

    {
        List<Jobs> lstJobs = new List<Jobs>();
        public CalcService()
        {
            lstJobs.Add(new Jobs { Name = "Firosh", Role = "Manager" });
            lstJobs.Add(new Jobs { Name = "Joby", Role = "SA" });
            lstJobs.Add(new Jobs { Name = "Reshma", Role = "SA" });
            lstJobs.Add(new Jobs { Name = "Jiss", Role = "SA" });
            lstJobs.Add(new Jobs { Name = "Ajay", Role = "Manager" });
          
        }

        public double Add(double n1, double n2)

        {

            return n1 + n2;

        }

        public double Subtract(double n1, double n2)

        {

            return n1 - n2;

        }

        public double Multiply(double n1, double n2)

        {

            return n1 * n2;

        }

        public double Divide(double n1, double n2)

        {

            return n1 / n2;

        }

        public string SayHello(string  name)

        {
            string msg;

            if (DateTime.Now.Hour < 12)

            {

                msg = "Good Morning " + name ;

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

        public string TodayProgram(string name)

        {
            string msg;
            DayOfWeek today = DateTime.Today.DayOfWeek;

            if ((today == DayOfWeek.Saturday)   ||  (today == DayOfWeek.Sunday))

            {

                msg = "Happy Weekend  " + name;

                // lblDate.Text = Convert.ToString(DateTime.Now);

            }

                       else

            {

                //  lblGreeting.Text = "Good Evening";

                msg = "Enjoy working day  " + name;

            }
            return msg;

        }

       
        

        IEnumerable<Jobs> ICalcService.OpeningJobs()
        {
            
            return lstJobs;
        }

        IEnumerable<Jobs> ICalcService.OpeningJobsByRole( string role)
        {
            List<Jobs> jobs = lstJobs;
            var filtered = from Jobs p in jobs
                           where p.Role ==role
                           select p;

            return filtered;
        }
    }

}
