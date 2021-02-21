using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public class Station//תחנה
    {
        public int Code { get; set; }//קוד התחנה
        public string Name { get; set; }// שם התחנה
        public double Longitude { get; set; } //קווי אורך
        public double Latitude { get; set; }// קווי רוחב
        public string Address { get; set; }//כתובת
        public bool DisabledAccess { get; set; }//גישה לנכים
        public bool IsDeleted { get; set; }// האם מחוק

    }
}
