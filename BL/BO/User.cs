using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class User
    {
        public string UserName { get; set; } //user name
        public string passCode { get; set; }//user passcode
        public bool managaccount { get; set; } //access for admin
    }
}
