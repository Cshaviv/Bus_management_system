using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum BusStatus//enum סטטוסים שונים של האוטובוס:זמין, בטיפול, בתדלוק, בנסיעה
    {
        Available, InTravel, OnRefueling, OnTreatment
    }
    public enum Area //enum אזורים שונים: צפון, דרום, מרכז,ירושלים, כללי
    {
        General = 1, North, South, Center, Jerusalem
    }
}

