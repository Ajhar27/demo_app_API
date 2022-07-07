using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_app_API.Models
{
    public class UserData
    {
       
    }
}

namespace demo_app_API
{
    public class UserModel
    {
        public int Id { get; set; }
        public DateTime? C_date { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Payment { get; set; }
        public string R_payment { get; set; }
        public string Workdone { get; set; }
        public DateTime? A_date { get; set; }
    }
}
