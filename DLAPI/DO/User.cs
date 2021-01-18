using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class User
    {
        public string UserName { get; set; }//שם משתמש
        public string passCode { get; set; }//סיסמה של המשתמש
        public bool managaccount { get; set; }// גישה למנהל
        public bool IsDeleted { get; set; }// האם מחוק
    }
}
