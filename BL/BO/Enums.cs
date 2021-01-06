using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum BusStatus//enum for the bus status
    {
        Available, InTravel, OnRefueling, OnTreatment
    }
    public enum Area //enum for the lines areas
    {
        General = 1, North, South, Center, Jerusalem
    }
}

