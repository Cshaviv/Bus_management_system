using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    public class DataSource
    {
        public static List<Bus> ListBuses;
        public static List<Station> ListStations;
        public static List<AdjacentStations> ListAdjacentStations;
        public static List<Trip> ListTrips;
        public static List<Line> ListLines;
        public static List<LineStation> ListLineStations;
        public static List<LineTrip> ListLineTrips;
        public static List<User> ListUsers;


        static DataSource()
        {
            InitAllLists();
        }
        static void InitAllLists()
        {
            //    #region ListStations 
            //    ListStations = new List<Station>
            //    {
            //        new Station
            //        {
            //            Code = 73,
            //            Name = "שדרות גולדה מאיר/המשורר אצ''ג",
            //            Address = "רחוב:שדרות גולדה מאיר  עיר: ירושלים ",
            //            Latitude = 31.825302,
            //            Longitude = 35.188624,
            //            DisabledAccess = false,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 76,
            //            Name = "בית ספר צור באהר בנות/אלמדינה אלמונוורה",
            //            Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים",
            //            Latitude = 31.738425,
            //            Longitude = 35.228765,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 77,
            //            Name = "בית ספר אבן רשד/אלמדינה אלמונוורה",
            //            Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים ",
            //            Latitude = 31.738676,
            //            Longitude = 35.226704,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 78,
            //            Name = "שרי ישראל/יפו",
            //            Address = "רחוב:שדרות שרי ישראל 15 עיר: ירושלים",
            //            Latitude = 31.789128,
            //            Longitude = 35.206146,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 83,
            //            Name = "בטן אלהווא/חוש אל מרג",
            //            Address = "רחוב:בטן אל הווא  עיר: ירושלים",
            //            Latitude = 31.766358,
            //            Longitude = 35.240417,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 84,
            //            Name = "מלכי ישראל/הטורים",
            //            Address = " רחוב:מלכי ישראל 77 עיר: ירושלים ",
            //            Latitude = 31.790758,
            //            Longitude = 35.209791,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 85,
            //            Name = "בית ספר לבנים/אלמדארס",
            //            Address = "רחוב:אלמדארס  עיר: ירושלים",
            //            Latitude = 31.768643,
            //            Longitude = 35.238509,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 86,
            //            Name = "מגרש כדורגל/אלמדארס",
            //            Address = "רחוב:אלמדארס  עיר: ירושלים",
            //            Latitude = 31.769899,
            //            Longitude = 35.23973,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 88,
            //            Name = "בית ספר לבנות/בטן אלהוא",
            //            Address = " רחוב:בטן אל הווא  עיר: ירושלים",
            //            Latitude = 31.767064,
            //            Longitude = 35.238443,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 89,
            //            Name = "דרך בית לחם הישה/ואדי קדום",
            //            Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
            //            Latitude = 31.765863,
            //            Longitude = 35.247198,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 90,
            //            Name = "גולדה/הרטום",
            //            Address = "רחוב:דרך בית לחם הישנה  עיר: ירושלים",
            //            Latitude = 31.799804,
            //            Longitude = 35.213021,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 91,
            //            Name = "דרך בית לחם הישה/ואדי קדום",
            //            Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
            //            Latitude = 31.765717,
            //            Longitude = 35.247102,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 93,
            //            Name = "חוש סלימה 1",
            //            Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
            //            Latitude = 31.767265,
            //            Longitude = 35.246594,
            //            DisabledAccess = false,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 94,
            //            Name = "דרך בית לחם הישנה ב",
            //            Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
            //            Latitude = 31.767084,
            //            Longitude = 35.246655,
            //            DisabledAccess = false,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 95,
            //            Name = "דרך בית לחם הישנה א",
            //            Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
            //            Latitude = 31.768759,
            //            Longitude = 31.768759,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 97,
            //            Name = "שכונת בזבז 2",
            //            Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
            //            Latitude = 31.77002,
            //            Longitude = 35.24348,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 102,
            //            Name = "גולדה/שלמה הלוי",
            //            Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
            //            Latitude = 31.8003,
            //            Longitude = 35.208257,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 103,
            //            Name = "גולדה/הרטום",
            //            Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
            //            Latitude = 31.8,
            //            Longitude = 35.214106,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 105,
            //            Name = "גבעת משה",
            //            Address = " רחוב:גבעת משה 2 עיר: ירושלים",
            //            Latitude = 31.797708,
            //            Longitude = 35.217133,
            //            DisabledAccess = false,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 106,
            //            Name = "גבעת משה",
            //            Address = " רחוב:גבעת משה 3 עיר: ירושלים",
            //            Latitude = 31.797535,
            //            Longitude = 35.217057,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        //20
            //        new Station
            //        {
            //            Code = 108,
            //            Name = "עזרת תורה/עלי הכהן",
            //            Address = "  רחוב:עזרת תורה 25 עיר: ירושלים",
            //            Latitude = 31.797535,
            //            Longitude = 35.213728,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 109,
            //            Name = "עזרת תורה/דורש טוב",
            //            Address = "  רחוב:עזרת תורה 21 עיר: ירושלים ",
            //            Latitude = 31.796818,
            //            Longitude = 35.212936,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 110,
            //            Name = "עזרת תורה/דורש טוב",
            //            Address = " רחוב:עזרת תורה 12 עיר: ירושלים",
            //            Latitude = 31.796129,
            //            Longitude = 35.212698,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 111,
            //            Name = "יעקובזון/עזרת תורה",
            //            Address = "  רחוב:יעקובזון 1 עיר: ירושלים",
            //            Latitude = 31.794631,
            //            Longitude = 35.21161,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 112,
            //            Name = "יעקובזון/עזרת תורה",
            //            Address = " רחוב:יעקובזון  עיר: ירושלים",
            //            Latitude = 31.79508,
            //            Longitude = 35.211684,
            //            DisabledAccess = false,
            //            IsDeleted = false
            //        },
            //        //25
            //        new Station
            //        {
            //            Code = 113,
            //            Name = "זית רענן/אוהל יהושע",
            //            Address = "  רחוב:זית רענן 1 עיר: ירושלים",
            //            Latitude = 31.796255,
            //            Longitude = 35.211065,
            //            DisabledAccess = false,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 115,
            //            Name = "זית רענן/תורת חסד",
            //            Address = " רחוב:זית רענן  עיר: ירושלים",
            //            Latitude = 31.798423,
            //            Longitude = 35.209575,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 116,
            //            Name = "זית רענן/תורת חסד",
            //            Address = "  רחוב:הרב סורוצקין 48 עיר: ירושלים ",
            //            Latitude = 31.798689,
            //            Longitude = 35.208878,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 117,
            //            Name = "קרית הילד/סורוצקין",
            //            Address = "  רחוב:הרב סורוצקין  עיר: ירושלים",
            //            Latitude = 31.799165,
            //            Longitude = 35.206918
            //        },
            //        new Station
            //        {
            //            Code = 119,
            //            Name = "סורוצקין/שנירר",
            //            Address = "  רחוב:הרב סורוצקין 31 עיר: ירושלים",
            //            Latitude = 31.797829,
            //            Longitude = 35.205601,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        //30
            //        new Station
            //        {
            //            Code = 1485,
            //            Name = "שדרות נווה יעקוב/הרב פרדס ",
            //            Address = "רחוב: שדרות נווה יעקוב  עיר:ירושלים ",
            //            Latitude = 31.840063,
            //            Longitude = 35.240062,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1486,
            //            Name = "מרכז קהילתי /שדרות נווה יעקוב",
            //            Address = "רחוב:שדרות נווה יעקוב ירושלים עיר:ירושלים ",
            //            Latitude = 31.838481,
            //            Longitude = 35.23972,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1487,
            //            Name = " מסוף 700 /שדרות נווה יעקוב ",
            //            Address = "חוב:שדרות נווה יעקב 7 עיר: ירושלים  ",
            //            Latitude = 31.837748,
            //            Longitude = 35.231598,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1488,
            //            Name = " הרב פרדס/אסטורהב ",
            //            Address = "רחוב:מעגלות הרב פרדס  עיר: ירושלים רציף  ",
            //            Latitude = 31.840279,
            //            Longitude = 35.246272,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1490,
            //            Name = "הרב פרדס/צוקרמן ",
            //            Address = "רחוב:מעגלות הרב פרדס 24 עיר: ירושלים   ",
            //            Latitude = 31.843598,
            //            Longitude = 35.243639,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1491,
            //            Name = "ברזיל ",
            //            Address = "רחוב:ברזיל 14 עיר: ירושלים",
            //            Latitude = 31.766256,
            //            Longitude = 35.173,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1492,
            //            Name = "בית וגן/הרב שאג ",
            //            Address = "רחוב:בית וגן 61 עיר: ירושלים ",
            //            Latitude = 31.76736,
            //            Longitude = 35.184771,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1493,
            //            Name = "בית וגן/עוזיאל ",
            //            Address = "רחוב:בית וגן 21 עיר: ירושלים    ",
            //            Latitude = 31.770543,
            //            Longitude = 35.183999,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1494,
            //            Name = " קרית יובל/שמריהו לוין ",
            //            Address = "רחוב:ארתור הנטקה  עיר: ירושלים    ",
            //            Latitude = 31.768465,
            //            Longitude = 35.178701,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1510,
            //            Name = " קורצ'אק / רינגלבלום ",
            //            Address = "רחוב:יאנוש קורצ'אק 7 עיר: ירושלים",
            //            Latitude = 31.759534,
            //            Longitude = 35.173688,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1511,
            //            Name = " טהון/גולומב ",
            //            Address = "רחוב:יעקב טהון  עיר: ירושלים     ",
            //            Latitude = 31.761447,
            //            Longitude = 35.175929,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1512,
            //            Name = "הרב הרצוג/שח''ל ",
            //            Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
            //            Latitude = 31.761447,
            //            Longitude = 35.199936,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1514,
            //            Name = "פרץ ברנשטיין/נזר דוד ",
            //            Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
            //            Latitude = 31.759186,
            //            Longitude = 35.189336,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1518,
            //            Name = "פרץ ברנשטיין/נזר דוד",
            //            Address = " רחוב:פרץ ברנשטיין 56 עיר: ירושלים ",
            //            Latitude = 31.759121,
            //            Longitude = 35.189178,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1522,
            //            Name = "מוזיאון ישראל/רופין",
            //            Address = "  רחוב:דרך רופין  עיר: ירושלים ",
            //            Latitude = 31.774484,
            //            Longitude = 35.204882,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1523,
            //            Name = "הרצוג/טשרניחובסקי",
            //            Address = "   רחוב:הרב הרצוג  עיר: ירושלים  ",
            //            Latitude = 31.769652,
            //            Longitude = 35.208248,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 1524,
            //            Name = "רופין/שד' הזז",
            //            Address = "    רחוב:הרב הרצוג  עיר: ירושלים   ",
            //            Latitude = 31.769652,
            //            Longitude = 35.208248,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 121,
            //            Name = "מרכז סולם/סורוצקין ",
            //            Address = " רחוב:הרב סורוצקין 13 עיר: ירושלים",
            //            Latitude = 31.796033,
            //            Longitude =35.206094,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 123,
            //            Name = "אוהל דוד/סורוצקין ",
            //            Address = "  רחוב:הרב סורוצקין 9 עיר: ירושלים",
            //            Latitude = 31.794958,
            //            Longitude =35.205216,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        },
            //        new Station
            //        {
            //            Code = 122,
            //            Name = "מרכז סולם/סורוצקין ",
            //            Address = "  רחוב:הרב סורוצקין 28 עיר: ירושלים",
            //            Latitude = 31.79617,
            //            Longitude =35.206158,
            //            DisabledAccess = true,
            //            IsDeleted = false
            //        }
            //    };
            //    #endregion
            //    #region ListBuses
            //    ListBuses = new List<Bus>
            //    {
            //        new Bus//1
            //        {
            //            LicenseNum= 12345678,
            //            StartDate= new DateTime(2018, 12, 1),
            //            TotalKm=10000,
            //            FuelTank=1200,
            //            StatusBus=BusStatus.Available,
            //            DateLastTreat=new DateTime(2020, 12,1 ),
            //            KmLastTreat=8001,
            //            IsDeleted=false
            //        },
            //        new Bus//2
            //        {
            //            LicenseNum= 1524897,
            //            StartDate= new DateTime(2017, 12, 1),
            //            TotalKm=10000,
            //            FuelTank=900,
            //            StatusBus=BusStatus.Available,
            //            DateLastTreat=new DateTime(2020, 12,1 ),
            //            KmLastTreat=9500,
            //            IsDeleted=false
            //        },
            //        new Bus//3
            //        {
            //            LicenseNum= 45698725,
            //            StartDate= new DateTime(2019, 12, 11),
            //            TotalKm=10000,
            //            FuelTank=1000,
            //            StatusBus=BusStatus.Available,
            //            DateLastTreat=new DateTime(2020, 12,1 ),
            //            KmLastTreat=9700,
            //            IsDeleted=false
            //        },
            //         new Bus//4
            //        {
            //            LicenseNum= 47589646,
            //            StartDate= new DateTime(2019, 11, 11),
            //            TotalKm=10000,
            //            FuelTank=800,
            //            StatusBus=BusStatus.Available,
            //            DateLastTreat=new DateTime(2020, 9,2),
            //            KmLastTreat=9600,
            //            IsDeleted=false
            //        },
            //         new Bus//5
            //        {
            //            LicenseNum= 1456982,
            //            StartDate= new DateTime(2016, 11, 2),
            //            TotalKm=10000,
            //            FuelTank=800,
            //            StatusBus=BusStatus.Available,
            //            DateLastTreat=new DateTime(2020, 12,2),
            //            KmLastTreat=9600,
            //            IsDeleted=false
            //        },
            //          new Bus//6
            //        {
            //            LicenseNum= 1458795,
            //            StartDate= new DateTime(2015, 11,3),
            //            TotalKm=20000,
            //            FuelTank=1200,
            //            StatusBus=BusStatus.Available,
            //            DateLastTreat=new DateTime(2020, 8,21),
            //            KmLastTreat=19600,
            //            IsDeleted=false
            //        },
            //            new Bus//7
            //        {
            //            LicenseNum= 65984758,
            //            StartDate= new DateTime(2019, 8,2),
            //            TotalKm=30000,
            //            FuelTank=800,
            //            StatusBus=BusStatus.Available,
            //            DateLastTreat=new DateTime(2020, 5,15),
            //            KmLastTreat=20000,
            //            IsDeleted=false
            //        },
            //          new Bus//8
            //        {
            //            LicenseNum= 4569821,
            //            StartDate= new DateTime(2014, 11,20),
            //            TotalKm=10000,
            //            FuelTank=800,
            //            StatusBus=BusStatus.Available,
            //            DateLastTreat=new DateTime(2020,6,10),
            //            KmLastTreat=9600,
            //            IsDeleted=false
            //        },
            //           new Bus//9
            //        {
            //            LicenseNum= 2564875,
            //            StartDate= new DateTime(2013, 11,2),
            //            TotalKm=50000,
            //            FuelTank=800,
            //            StatusBus=BusStatus.Available,
            //            DateLastTreat=new DateTime(2020,10,1),
            //            KmLastTreat=49700,
            //            IsDeleted=false
            //        },
            //           new Bus//10
            //        {
            //            LicenseNum= 42650314,
            //            StartDate= new DateTime(2019, 1,20),
            //            TotalKm=10000,
            //            FuelTank=1200,
            //            StatusBus=BusStatus.Available,
            //            DateLastTreat=new DateTime(2020,7,1),
            //            KmLastTreat=9900,
            //            IsDeleted=false
            //        },
            //    };
            //    #endregion 
            //    #region ListLines
            //    ListLines = new List<Line>
            //    {
            //           new Line //0
            //        {
            //            LineId=0,
            //            LineNum=32,
            //            Area= Area.Jerusalem,
            //            FirstStation=91, //גולדה/הרטום
            //            LastStation=119, //קרית הילד/סורוצקין
            //            IsDeleted=false
            //        },
            //        new Line //1
            //        {
            //            LineId=1,
            //            LineNum=33,
            //            Area= Area.Jerusalem,
            //            FirstStation=91, //גולדה/הרטום
            //            LastStation=119, //קרית הילד/סורוצקין
            //            IsDeleted=false
            //        },
            //        new Line //2
            //        {
            //            LineId=2,
            //            LineNum=12,
            //            Area= Area.Jerusalem,
            //            FirstStation=84, //מלכי ישראל/הטורים
            //            LastStation=1492, //בית וגן/הרב שאג
            //            IsDeleted=false
            //        },
            //           new Line //3
            //        {
            //            LineId=3,
            //            LineNum=53,
            //            Area= Area.Jerusalem,
            //            FirstStation=78, //שרי ישראל/יפו
            //            LastStation=1511, //טהון/גולומב
            //            IsDeleted=false
            //        },
            //        new Line //4
            //        {
            //            LineId=4,
            //            LineNum=240,
            //            Area= Area.Jerusalem,
            //            FirstStation=102,//גולדה/שלמה הלוי
            //            LastStation=122, //מרכז סולם/סורוצקין
            //            IsDeleted=false
            //        },
            //        new Line //5
            //        {
            //            LineId=5,
            //            LineNum=74,
            //            Area= Area.Jerusalem,
            //            FirstStation=105, //גבעת משה
            //            LastStation=1490, //הרב פרדס/צוקרמן
            //            IsDeleted=false
            //        },
            //        new Line //6
            //        {
            //            LineId=6,
            //            LineNum=9,
            //            Area= Area.Jerusalem,
            //            FirstStation=123, //אוהל דוד/סורוצקין
            //            LastStation=1491, //ברזיל
            //            IsDeleted=false
            //        },
            //        new Line //7
            //        {
            //            LineId=7,
            //            LineNum=139,
            //            Area= Area.Jerusalem,
            //            FirstStation=1518, //פרץ ברנשטיין/נזר דוד
            //            LastStation=116, //זית רענן/תורת חסד
            //            IsDeleted=false
            //        },
            //         new Line //8
            //        {
            //            LineId=8,
            //            LineNum=68,
            //            Area= Area.Jerusalem,
            //            FirstStation=108, //עזרת תורה/עלי הכהן
            //            LastStation=97, //שכונת בזבז 2
            //            IsDeleted=false
            //        },
            //          new Line //9
            //        {
            //            LineId=9,
            //            LineNum=82,
            //            Area= Area.Jerusalem,
            //            FirstStation=111, //יעקובזון/עזרת תורה
            //            LastStation=1493, //בית וגן/עוזיאל
            //            IsDeleted=false
            //        },
            //           new Line //10
            //        {
            //            LineId=10,
            //            LineNum=67,
            //            Area= Area.Jerusalem,
            //            FirstStation=1512, //הרב הרצוג/ שח"ל
            //            LastStation=113, //זית רענן/אוהל יהושע
            //            IsDeleted=false
            //        },
            //    };
            //    #endregion
            //    #region ListLineStation
            //ListLineStations = new List<LineStation>
            //{
            //    //line Id=0
            //    new LineStation
            //    {
            //        LineId=1,
            //        StationCode=91,
            //        LineStationIndex=0,
            //        PrevStationCode=0,
            //        NextStationCode=73,
            //        DistanceFromNext=6,
            //        TimeFromNext=new TimeSpan(0,8,0),
            //        IsDeleted=false,

            //    },
            //     new LineStation
            //    {
            //        LineId=1,
            //        StationCode=73,
            //        LineStationIndex=1,
            //        PrevStationCode=91,
            //        NextStationCode=76,
            //               DistanceFromNext=6,
            //        TimeFromNext=new TimeSpan(0,9,0),
            //        IsDeleted=false,

            //    },
            //    new LineStation
            //    {
            //        LineId=1,
            //        StationCode=76,
            //        LineStationIndex=2,
            //        PrevStationCode=73,
            //        NextStationCode=119,
            //               DistanceFromNext=6,
            //        TimeFromNext=new TimeSpan(0,8,15),
            //        IsDeleted=false,
            //    },
            //    new LineStation
            //    {
            //        LineId=1,
            //        StationCode=119,
            //        LineStationIndex=3,
            //        PrevStationCode=76,
            //        NextStationCode=0,
            //               DistanceFromNext=0,
            //        TimeFromNext=new TimeSpan(0,0,0),
            //        IsDeleted=false,
            //    },
            //};
            //#endregion
            //#region ListAdjacentStations
            //ListAdjacentStations = new List<AdjacentStations>()
            //{
            //    new AdjacentStations
            //    {
            //        StationCode1=91,
            //        StationCode2 = 73,
            //        Distance=4.5,
            //        Time=new TimeSpan(0,5,0),
            //        IsDeleted=false,
            //    },
            //    new AdjacentStations
            //    {
            //        StationCode1=73,
            //        StationCode2 = 76,
            //        Distance=3.5,
            //        Time=new TimeSpan(0,3,0),
            //        IsDeleted=false,
            //    },
            //    new AdjacentStations
            //    {
            //        StationCode1=76,
            //        StationCode2 =119,
            //        Distance=6.5,
            //        Time=new TimeSpan(0,2,0),
            //        IsDeleted=false,
            //    },
            //};
            //#endregion
            #region ListUsers
            ListUsers = new List<User>
            {
                new User //1
                {
                    UserName= "1",
                    passCode="1",
                    managaccount=true,
                    //IsDeleted= false
                },

                new User //2
                {
                    UserName= "ayala6521",
                    passCode= "abc33",
                    managaccount=true,
                   // IsDeleted= false
                },

                new User //3
                {
                    UserName= "tahel87",
                    passCode= "df456",
                    managaccount=true,
                   // IsDeleted= false
                },

                new User //4
                {
                    UserName= "dav983",
                    passCode= "pro865",
                    managaccount=false,
                  //  IsDeleted= false
                },

                new User //5
                {
                    UserName= "duc4569",
                    passCode= "xzxz",
                    managaccount=false,
                 //   IsDeleted= false
                },

                new User //6
                {
                    UserName= "cut765",
                    passCode= "fuyfuy",
                    managaccount=false,
                  //  IsDeleted= false
                },

                new User //7
                {
                    UserName= "dog555",
                    passCode= "digdig",
                    managaccount=false,
               //     IsDeleted= false
                },

                new User //8
                {
                    UserName= "fug897",
                    passCode= "strstr",
                    managaccount=false,
                    //IsDeleted= false
                },

                new User //9
                {
                    UserName= "noa8642",
                    passCode= "ttt456",
                    managaccount=true,
                   // IsDeleted= false
                },

                new User //10
                {
                    UserName= "classb",
                    passCode= "shalom4",
                    managaccount=false,
                 //   IsDeleted= false
                },
            };
            #endregion
            #region ListBuses
            ListBuses = new List<Bus>
            {
                new Bus
                {LicenseNum=1234567 , FuelTank=65.8,StatusBus=BusStatus.Available,TotalKm=3646.9, IsDeleted=true,  DateLastTreat=new DateTime(2020, 12,1 ),StartDate=new DateTime(2019, 12,1 )},
                new Bus
                {LicenseNum=59694581 , FuelTank=65.8,StatusBus=BusStatus.Refueling,TotalKm=3646.9, IsDeleted=false ,DateLastTreat=new DateTime(2020, 12,1 ),StartDate=new DateTime(2019, 12,1 )},
                new Bus
                {LicenseNum=98695485 , FuelTank=65.8,StatusBus=BusStatus.Available,TotalKm=3646.9, IsDeleted=false ,DateLastTreat=new DateTime(2020, 12,1 ),StartDate=new DateTime(2019, 12,1 )},
                new Bus
                {LicenseNum=69874586 , FuelTank=62.8,StatusBus=BusStatus.Available,TotalKm=3646.9, IsDeleted=false ,DateLastTreat=new DateTime(2020, 12,1 ),StartDate=new DateTime(2019, 12,1 )},
                 new Bus
                {LicenseNum=98567845 , FuelTank=60.8,StatusBus=BusStatus.Available,TotalKm=3646.9, IsDeleted=true ,DateLastTreat=new DateTime(2020, 12,1 ),StartDate=new DateTime(2019, 12,1 )},
                new Bus
                {LicenseNum=51194581 , FuelTank=15.8,StatusBus=BusStatus.Refueling,TotalKm=3646.9, IsDeleted=false ,DateLastTreat=new DateTime(2020, 12,1 ),StartDate=new DateTime(2019, 12,1 )},
                new Bus
                {LicenseNum=91195485 , FuelTank=65.8,StatusBus=BusStatus.Available,TotalKm=3646.9, IsDeleted=false ,DateLastTreat=new DateTime(2020, 12,1 ),StartDate=new DateTime(2019, 12,1 )},
                new Bus
                {LicenseNum=61174586 , FuelTank=25.8,StatusBus=BusStatus.Available,TotalKm=3646.9, IsDeleted=false ,DateLastTreat=new DateTime(2020, 12,1 ),StartDate=new DateTime(2019, 12,1 )},
                new Bus
                {LicenseNum=51146598 , FuelTank=61.8 ,StatusBus=BusStatus.InTravel,TotalKm=4898.9, IsDeleted=false ,DateLastTreat=new DateTime(2020, 12,1 ),StartDate=new DateTime(2019, 12,1 )}
            };
            #endregion

            #region ListStations
            ListStations = new List<Station>//50 stations
            {
                                             new Station
                                             {
                                                  Code = 73,
                                                  Name = "שדרות גולדה מאיר/המשורר אצ''ג",
                                                  Address = "רחוב:שדרות גולדה מאיר  עיר: ירושלים ",
                                                  Latitude = 31.825302,
                                                  Longitude = 35.188624,
                                                  IsDeleted=false
                                             },
                                           new Station
                                            {
                                                Code = 76,
                                                Name = "בית ספר צור באהר בנות/אלמדינה אלמונוורה",
                                                Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים",
                                                Latitude = 31.738425,
                                                Longitude = 35.228765,
                                                IsDeleted=false
                                            }
                                           , new Station
                                            {
                                                Code = 77,
                                                Name = "בית ספר אבן רשד/אלמדינה אלמונוורה",
                                                Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים ",
                                                Latitude = 31.738676,
                                                Longitude = 35.226704,
                                                IsDeleted=false
                                            },
                                            new Station
                                            {
                                                Code = 78,
                                                Name = "שרי ישראל/יפו",
                                                Address = "רחוב:שדרות שרי ישראל 15 עיר: ירושלים",
                                                Latitude = 31.789128,
                                                Longitude = 35.206146,
                                                IsDeleted=false
                                            },
                                            new Station
                                            {
                                                Code = 83,
                                                Name = "בטן אלהווא/חוש אל מרג",
                                                Address = "רחוב:בטן אל הווא  עיר: ירושלים",
                                                Latitude = 31.766358,
                                                Longitude = 35.240417,
                                                IsDeleted=false
                                            },
                                            new Station
                                            {
                                                Code = 84,
                                                Name = "מלכי ישראל/הטורים",
                                                Address = " רחוב:מלכי ישראל 77 עיר: ירושלים ",
                                                Latitude = 31.790758,
                                                Longitude = 35.209791,
                                                IsDeleted=false
                                            }
                                          ,  new Station
                                            {
                                                Code = 85,
                                                Name = "בית ספר לבנים/אלמדארס",
                                                Address = "רחוב:אלמדארס  עיר: ירושלים",
                                                Latitude = 31.768643,
                                                Longitude = 35.238509,
                                                IsDeleted=false
                                            },
                                            new Station
                                            {
                                                Code = 86,
                                                Name = "מגרש כדורגל/אלמדארס",
                                                Address = "רחוב:אלמדארס  עיר: ירושלים",
                                                Latitude = 31.769899,
                                                Longitude = 35.23973,
                                                IsDeleted=false
                                            },
                                            new Station
                                            {
                                                Code = 88,
                                                Name = "בית ספר לבנות/בטן אלהוא",
                                                Address = " רחוב:בטן אל הווא  עיר: ירושלים",
                                                Latitude = 31.767064,
                                                Longitude = 35.238443,
                                                IsDeleted=false
                                            },
                                            new Station
                                            {
                                                Code = 89,
                                                Name = "דרך בית לחם הישה/ואדי קדום",
                                                Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
                                                Latitude = 31.765863,
                                                Longitude = 35.247198,

                                            },
                                            new Station
                                            {
                                                Code = 90,
                                                Name = "גולדה/הרטום",
                                                Address = "רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                                                Latitude = 31.799804,
                                                Longitude = 35.213021
                                            },
                                            new Station
                                            {
                                                Code = 91,
                                                Name = "דרך בית לחם הישה/ואדי קדום",
                                                Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
                                                Latitude = 31.765717,
                                                Longitude = 35.247102
                                            },
                                            new Station
                                            {
                                                Code = 93,
                                                Name = "חוש סלימה 1",
                                                Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                                                Latitude = 31.767265,
                                                Longitude = 35.246594
                                            },
                                            new Station
                                            {
                                                Code = 94,
                                                Name = "דרך בית לחם הישנה ב",
                                                Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                                                Latitude = 31.767084,
                                                Longitude = 35.246655
                                            },
                                            new Station
                                            {
                                                Code = 95,
                                                Name = "דרך בית לחם הישנה א",
                                                Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                                                Latitude = 31.768759,
                                                Longitude = 31.768759
                                            },
                                            new Station
                                            {
                                                Code = 97,
                                                Name = "שכונת בזבז 2",
                                                Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                                                Latitude = 31.77002,
                                                Longitude = 35.24348
                                            },
                                            new Station
                                            {
                                                Code = 102,
                                                Name = "גולדה/שלמה הלוי",
                                                Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
                                                Latitude = 31.8003,
                                                Longitude = 35.208257
                                            },
                                            new Station
                                            {
                                                Code = 103,
                                                Name = "גולדה/הרטום",
                                                Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
                                                Latitude = 31.8,
                                                Longitude = 35.214106
                                            },
                                            new Station
                                            {
                                                Code = 105,
                                                Name = "גבעת משה",
                                                Address = " רחוב:גבעת משה 2 עיר: ירושלים",
                                                Latitude = 31.797708,
                                                Longitude = 35.217133
                                            },
                                            new Station
                                            {
                                                Code = 106,
                                                Name = "גבעת משה",
                                                Address = " רחוב:גבעת משה 3 עיר: ירושלים",
                                                Latitude = 31.797535,
                                                Longitude = 35.217057
                                            },
                                            //20
                                            new Station
                                            {
                                                Code = 108,
                                                Name = "עזרת תורה/עלי הכהן",
                                                Address = "  רחוב:עזרת תורה 25 עיר: ירושלים",
                                                Latitude = 31.797535,
                                                Longitude = 35.213728
                                            },
                                            new Station
                                            {
                                                Code = 109,
                                                Name = "עזרת תורה 21 /דורש טוב",
                                                Address = "  רחוב:עזרת תורה 21 עיר: ירושלים ",
                                                Latitude = 31.796818,
                                                Longitude = 35.212936
                                            },
                                            new Station
                                            {
                                                Code = 110,
                                                Name = "עזרת תורה 12 /דורש טוב",
                                                Address = " רחוב:עזרת תורה 12 עיר: ירושלים",
                                                Latitude = 31.796129,
                                                Longitude = 35.212698
                                            },
                                            new Station
                                            {
                                                Code = 111,
                                                Name = "יעקובזון/עזרת תורה",
                                                Address = "  רחוב:יעקובזון 1 עיר: ירושלים",
                                                Latitude = 31.794631,
                                                Longitude = 35.21161
                                            },
                                            new Station
                                            {
                                                Code = 112,
                                                Name = "יעקובזון/עזרת תורה",
                                                Address = " רחוב:יעקובזון  עיר: ירושלים",
                                                Latitude = 31.79508,
                                                Longitude = 35.211684
                                            },
                                            //25
                                            new Station
                                            {
                                                Code = 113,
                                                Name = "זית רענן/אוהל יהושע",
                                                Address = "  רחוב:זית רענן 1 עיר: ירושלים",
                                                Latitude = 31.796255,
                                                Longitude = 35.211065
                                            },
                                            new Station
                                            {
                                                Code = 115,
                                                Name = "זית רענן/תורת חסד",
                                                Address = " רחוב:זית רענן  עיר: ירושלים",
                                                Latitude = 31.798423,
                                                Longitude = 35.209575
                                            },
                                            new Station
                                            {
                                                Code = 116,
                                                Name = "זית רענן/תורת חסד",
                                                Address = "  רחוב:הרב סורוצקין 48 עיר: ירושלים ",
                                                Latitude = 31.798689,
                                                Longitude = 35.208878
                                            },
                                            new Station
                                            {
                                                Code = 117,
                                                Name = "קרית הילד/סורוצקין",
                                                Address = "  רחוב:הרב סורוצקין  עיר: ירושלים",
                                                Latitude = 31.799165,
                                                Longitude = 35.206918
                                            },
                                            new Station
                                            {
                                                Code = 119,
                                                Name = "סורוצקין/שנירר",
                                                Address = "  רחוב:הרב סורוצקין 31 עיר: ירושלים",
                                                Latitude = 31.797829,
                                                Longitude = 35.205601
                                            },

                                            //30
                                            new Station
                                            {
                                                Code = 1485,
                                                Name = "שדרות נווה יעקוב/הרב פרדס ",
                                                Address = "רחוב: שדרות נווה יעקוב  עיר:ירושלים ",
                                                Latitude = 31.840063,
                                                Longitude = 35.240062

                                            },
                                            new Station
                                            {
                                                Code = 1486,
                                                Name = "מרכז קהילתי /שדרות נווה יעקוב",
                                                Address = "רחוב:שדרות נווה יעקוב ירושלים עיר:ירושלים ",
                                                Latitude = 31.838481,
                                                Longitude = 35.23972
                                            },


                                            new Station
                                            {
                                                Code = 1487,
                                                Name = " מסוף 700 /שדרות נווה יעקוב ",
                                                Address = "חוב:שדרות נווה יעקב 7 עיר: ירושלים  ",
                                                Latitude = 31.837748,
                                                Longitude = 35.231598
                                            },
                                            new Station
                                            {
                                                Code = 1488,
                                                Name = " הרב פרדס/אסטורהב ",
                                                Address = "רחוב:מעגלות הרב פרדס  עיר: ירושלים רציף  ",
                                                Latitude = 31.840279,
                                                Longitude = 35.246272
                                            },
                                            new Station
                                            {
                                                Code = 1490,
                                                Name = "הרב פרדס/צוקרמן ",
                                                Address = "רחוב:מעגלות הרב פרדס 24 עיר: ירושלים   ",
                                                Latitude = 31.843598,
                                                Longitude = 35.243639
                                            },
                                            new Station
                                            {
                                                Code = 1491,
                                                Name = "ברזיל ",
                                                Address = "רחוב:ברזיל 14 עיר: ירושלים",
                                                Latitude = 31.766256,
                                                Longitude = 35.173
                                            },
                                            new Station
                                            {
                                                Code = 1492,
                                                Name = "בית וגן/הרב שאג ",
                                                Address = "רחוב:בית וגן 61 עיר: ירושלים ",
                                                Latitude = 31.76736,
                                                Longitude = 35.184771
                                            },
                                            new Station
                                            {
                                                Code = 1493,
                                                Name = "בית וגן/עוזיאל ",
                                                Address = "רחוב:בית וגן 21 עיר: ירושלים    ",
                                                Latitude = 31.770543,
                                                Longitude = 35.183999
                                            },
                                            new Station
                                            {
                                                Code = 1494,
                                                Name = " קרית יובל/שמריהו לוין ",
                                                Address = "רחוב:ארתור הנטקה  עיר: ירושלים    ",
                                                Latitude = 31.768465,
                                                Longitude = 35.178701
                                            },
                                            new Station
                                            {
                                                Code = 1510,
                                                Name = " קורצ'אק / רינגלבלום ",
                                                Address = "רחוב:יאנוש קורצ'אק 7 עיר: ירושלים",
                                                Latitude = 31.759534,
                                                Longitude = 35.173688
                                            },
                                            new Station
                                            {
                                                Code = 1511,
                                                Name = " טהון/גולומב ",
                                                Address = "רחוב:יעקב טהון  עיר: ירושלים     ",
                                                Latitude = 31.761447,
                                                Longitude = 35.175929
                                            },
                                            new Station
                                            {
                                                Code = 1512,
                                                Name = "הרב הרצוג/שח''ל ",
                                                Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
                                                Latitude = 31.761447,
                                                Longitude = 35.199936
                                            },
                                            new Station
                                            {
                                                Code = 1514,
                                                Name = "פרץ ברנשטיין/נזר דוד ",
                                                Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
                                                Latitude = 31.759186,
                                                Longitude = 35.189336
                                            },
                                         new Station
                                         {
                                             Code = 1518,
                                             Name = "פרץ ברנשטיין/נזר דוד",
                                             Address = " רחוב:פרץ ברנשטיין 56 עיר: ירושלים ",
                                             Latitude = 31.759121,
                                             Longitude = 35.189178
                                         },
                                          new Station
                                          {
                                              Code = 1522,
                                              Name = "מוזיאון ישראל/רופין",
                                              Address = "  רחוב:דרך רופין  עיר: ירושלים ",
                                              Latitude = 31.774484,
                                              Longitude = 35.204882
                                          },

                                         new Station
                                         {
                                             Code = 1523,
                                             Name = "הרצוג/טשרניחובסקי",
                                             Address = "   רחוב:הרב הרצוג  עיר: ירושלים  ",
                                             Latitude = 31.769652,
                                             Longitude = 35.208248
                                         },
                                          new Station
                                          {
                                              Code = 1524,
                                              Name = "רופין/שד' הזז",
                                              Address = "    רחוב:הרב הרצוג  עיר: ירושלים   ",
                                              Latitude = 31.769652,
                                              Longitude = 35.208248,
                                          },
                                            new Station
                                            {
                                                Code = 121,
                                                Name = "מרכז סולם/סורוצקין ",
                                                Address = " רחוב:הרב סורוצקין 13 עיר: ירושלים",
                                                Latitude = 31.796033,
                                                Longitude = 35.206094
                                            },
                                            new Station
                                            {
                                                Code = 123,
                                                Name = "אוהל דוד/סורוצקין ",
                                                Address = "  רחוב:הרב סורוצקין 9 עיר: ירושלים",
                                                Latitude = 31.794958,
                                                Longitude = 35.205216
                                            },
                                            new Station
                                            {
                                                Code = 122,
                                                Name = "מרכז סולם/סורוצקין ",
                                                Address = "  רחוב:הרב סורוצקין 28 עיר: ירושלים",
                                                Latitude = 31.79617,
                                                Longitude = 35.206158
                                            }



            };
            #endregion

            #region ListLines
            ListLines = new List<Line>
            {
                new Line//11
                {
                    LineId=1,LineNum=11,Area=Area.Jerusalem,FirstStation=73,LastStation=1493,IsDeleted=false
                },
                 new Line//22
                {
                    LineId=2,LineNum=22,Area=Area.Jerusalem,FirstStation=73,LastStation=106,IsDeleted=false
                },
                 new Line//33
                {
                    LineId=3,LineNum=33,Area=Area.Center,LastStation=77,FirstStation=78,IsDeleted=false
                },
                new Line//44
                {
                    LineId=4,LineNum=44,Area=Area.North,FirstStation=76,LastStation=84,IsDeleted=false
                },
                  new Line//55
                {
                    LineId=5,LineNum=55,Area=Area.Center,FirstStation=89,LastStation=83,IsDeleted=false
                },
                new Line//66
                {
                    LineId=6,LineNum=66,Area=Area.North,LastStation=1524,FirstStation=121,IsDeleted=false
                },
                new Line//77
                {
                    LineId=7,LineNum=77,Area=Area.South,LastStation=89,FirstStation=110,IsDeleted=false
                },
                 new Line//88
                {
                    LineId=8,LineNum=88,Area=Area.South,LastStation=73,FirstStation=1491,IsDeleted=false
                },
                  new Line//99
                {
                    LineId=9,LineNum=99,Area=Area.South,LastStation=116,FirstStation=93,IsDeleted=false
                },
                  new Line//100
                {
                    LineId=10,LineNum=100,Area=Area.South,LastStation=95,FirstStation=111,IsDeleted=false
                }

            };
            #endregion

            #region ListLineStations
            ListLineStations = new List<LineStation>
            {
                #region line 11
                //
                new LineStation
                {/*NextStationCode=76,PrevStationCode=0,*/LineId=1,IsDeleted=false,StationCode=73,LineStationIndex=0},
                new LineStation
                {/*NextStationCode=0,PrevStationCode=86,*/LineId=1,IsDeleted=false,StationCode=76,LineStationIndex=1},
                new LineStation
                {/*NextStationCode=0,PrevStationCode=86,*/LineId=1,IsDeleted=false,StationCode=1491,LineStationIndex=2},
                new LineStation
                {/*NextStationCode=0,PrevStationCode=86,*/LineId=1,IsDeleted=false,StationCode=1510,LineStationIndex=3},
               new LineStation
                {/*NextStationCode=76,PrevStationCode=0,*/LineId=1,IsDeleted=false,StationCode=1512,LineStationIndex=4},
                new LineStation
                {/*NextStationCode=0,PrevStationCode=86,*/LineId=1,IsDeleted=false,StationCode=1522,LineStationIndex=5},
                   new LineStation
                {/*NextStationCode=0,PrevStationCode=86,*/LineId=1,IsDeleted=false,StationCode=78,LineStationIndex=6},
                      new LineStation
                {/*NextStationCode=0,PrevStationCode=86,*/LineId=1,IsDeleted=false,StationCode=89,LineStationIndex=7},
                   new LineStation
                {/*NextStationCode=0,PrevStationCode=86,*/LineId=1,IsDeleted=false,StationCode=1524,LineStationIndex=8},
                      new LineStation
                {/*NextStationCode=0,PrevStationCode=86,*/LineId=1,IsDeleted=false,StationCode=1493,LineStationIndex=9},
                      #endregion
                #region line 22
               
                new LineStation
                {/*NextStationCode=77,PrevStationCode=0,*/LineId=2,IsDeleted=false,StationCode=73,LineStationIndex=0},
                new LineStation
                {/*NextStationCode=83,PrevStationCode=73,*/LineId=2,IsDeleted=false,StationCode=77,LineStationIndex=1},
                 new LineStation
                {/*NextStationCode=78,PrevStationCode=77,*/LineId=2,IsDeleted=false,StationCode=83,LineStationIndex=2},
                new LineStation
                {/*NextStationCode=0,PrevStationCode=83,*/LineId=2,IsDeleted=false,StationCode=78,LineStationIndex=3},
                    new LineStation
                {/*NextStationCode=77,PrevStationCode=0,*/LineId=2,IsDeleted=false,StationCode=110,LineStationIndex=4},
                new LineStation
                {/*NextStationCode=83,PrevStationCode=73,*/LineId=2,IsDeleted=false,StationCode=111,LineStationIndex=5},
                 new LineStation
                {/*NextStationCode=78,PrevStationCode=77,*/LineId=2,IsDeleted=false,StationCode=113,LineStationIndex=6},
                new LineStation
                {/*NextStationCode=0,PrevStationCode=83,*/LineId=2,IsDeleted=false,StationCode=91,LineStationIndex=7},
                new LineStation
                {/*NextStationCode=78,PrevStationCode=77,*/LineId=2,IsDeleted=false,StationCode=121,LineStationIndex=8},
                new LineStation
                {/*NextStationCode=0,PrevStationCode=83,*/LineId=2,IsDeleted=false,StationCode=106,LineStationIndex=9}

                #endregion
                #region line 33
                 ,new LineStation
                {/*NextStationCode=73,*/LineId=3,IsDeleted=false,StationCode=77,LineStationIndex=0},
                new LineStation
                {/*extStationCode=85,*/LineId=3,IsDeleted=false,StationCode=110,LineStationIndex=1},
                 new LineStation
                {/*NextStationCode=75,*/LineId=3,IsDeleted=false,StationCode=111,LineStationIndex=2},
                new LineStation
                {/*NextStationCode=88,*/LineId=3,IsDeleted=false,StationCode=1512,LineStationIndex=3},
                 new LineStation
                {/*NextStationCode=73,*/LineId=3,IsDeleted=false,StationCode=91,LineStationIndex=4},
                new LineStation
                {/*extStationCode=85,*/LineId=3,IsDeleted=false,StationCode=89,LineStationIndex=5},
                 new LineStation
                {/*NextStationCode=75,*/LineId=3,IsDeleted=false,StationCode=116,LineStationIndex=6},
                new LineStation
                {/*NextStationCode=88,*/LineId=3,IsDeleted=false,StationCode=102,LineStationIndex=7},
                      new LineStation
                {/*NextStationCode=75,*/LineId=3,IsDeleted=false,StationCode=83,LineStationIndex=8},
                new LineStation
                {/*NextStationCode=88,*/LineId=3,IsDeleted=false,StationCode=78,LineStationIndex=9},
                #endregion
                #region line 44
               
                new LineStation
                {/*NextStationCode=73,*/LineId=4,IsDeleted=false,StationCode=76,LineStationIndex=0},
                new LineStation
                {/*NextStationCode=85,*/LineId=4,IsDeleted=false,StationCode=1491,LineStationIndex=1},
                   new LineStation
                {/*NextStationCode=73,*/LineId=4,IsDeleted=false,StationCode=91,LineStationIndex=2},
                new LineStation
                {/*NextStationCode=85,*/LineId=4,IsDeleted=false,StationCode=121,LineStationIndex=3},
                   new LineStation
                {/*NextStationCode=73,*/LineId=4,IsDeleted=false,StationCode=83,LineStationIndex=4},
                new LineStation
                {/*NextStationCode=85,*/LineId=4,IsDeleted=false,StationCode=89,LineStationIndex=5},
                   new LineStation
                {/*NextStationCode=73,*/LineId=4,IsDeleted=false,StationCode=1524,LineStationIndex=6},
                new LineStation
                {/*NextStationCode=85,*/LineId=4,IsDeleted=false,StationCode=1493,LineStationIndex=7},
                   new LineStation
                {/*NextStationCode=73,*/LineId=4,IsDeleted=false,StationCode=102,LineStationIndex=8},
                new LineStation
                {/*NextStationCode=85,*/LineId=4,IsDeleted=false,StationCode=84,LineStationIndex=9},
                #endregion
                #region line 55
                //line 472
                new LineStation
                {/*NextStationCode=73,*/LineId=5,IsDeleted=false,StationCode=91,LineStationIndex=0},
                new LineStation
                {/*NextStationCode=85,*/LineId=5,IsDeleted=false,StationCode=121,LineStationIndex=1},
                 new LineStation
                {/*NextStationCode=75,*/LineId=5,IsDeleted=false,StationCode=106,LineStationIndex=2},
                new LineStation
                {/*NextStationCode=88,*/LineId=5,IsDeleted=false,StationCode=76,LineStationIndex=3},
                 new LineStation
                {/*NextStationCode=73,*/LineId=5,IsDeleted=false,StationCode=77,LineStationIndex=4},
                new LineStation
                {/*NextStationCode=85,*/LineId=5,IsDeleted=false,StationCode=110,LineStationIndex=5},
                 new LineStation
                {/*NextStationCode=75,*/LineId=5,IsDeleted=false,StationCode=111,LineStationIndex=6},
                new LineStation
                {/*NextStationCode=88,*/LineId=5,IsDeleted=false,StationCode=1512,LineStationIndex=7},
                  new LineStation
                {/*NextStationCode=75,*/LineId=5,IsDeleted=false,StationCode=1522,LineStationIndex=8},
                new LineStation
                {/*NextStationCode=88,*/LineId=5,IsDeleted=false,StationCode=78,LineStationIndex=9},
#endregion
                #region line 66
                //line 66
                new LineStation
                {/*NextStationCode=73,*/LineId=6,IsDeleted=false,StationCode=121,LineStationIndex=0},
                new LineStation
                {/*NextStationCode=85,*/LineId=6,IsDeleted=false,StationCode=106,LineStationIndex=1},
                 new LineStation
                {/*NextStationCode=75,*/LineId=6,IsDeleted=false,StationCode=76,LineStationIndex=2},
                new LineStation
                {/*NextStationCode=88,*/LineId=6,IsDeleted=false,StationCode=1491,LineStationIndex=3},
                 new LineStation
                {/*NextStationCode=73,*/LineId=6,IsDeleted=false,StationCode=1510,LineStationIndex=4},
                new LineStation
                {/*NextStationCode=85,*/LineId=6,IsDeleted=false,StationCode=1512,LineStationIndex=5},
                 new LineStation
                {/*NextStationCode=75,*/LineId=6,IsDeleted=false,StationCode=1522,LineStationIndex=6},
                new LineStation
                {/*NextStationCode=88,*/LineId=6,IsDeleted=false,StationCode=78,LineStationIndex=7},
                  new LineStation
                {/*NextStationCode=75,*/LineId=6,IsDeleted=false,StationCode=89,LineStationIndex=8},
                new LineStation
                {/*NextStationCode=88,*/LineId=6,IsDeleted=false,StationCode=1524,LineStationIndex=9},
#endregion              
                #region line 77
                //line 77
                new LineStation
                {/*NextStationCode=73,*/LineId=7,IsDeleted=false,StationCode=110,LineStationIndex=0},
                new LineStation
                {/*NextStationCode=85,*/LineId=7,IsDeleted=false,StationCode=111,LineStationIndex=1},
                 new LineStation
                {/*NextStationCode=75,*/LineId=7,IsDeleted=false,StationCode=113,LineStationIndex=2},
                new LineStation
                {/*NextStationCode=88,*/LineId=7,IsDeleted=false,StationCode=91,LineStationIndex=3},
                 new LineStation
                {/*NextStationCode=73,*/LineId=7,IsDeleted=false,StationCode=121,LineStationIndex=4},
                new LineStation
                {/*NextStationCode=85,*/LineId=7,IsDeleted=false,StationCode=112,LineStationIndex=5},
                 new LineStation
                {/*NextStationCode=75,*/LineId=7,IsDeleted=false,StationCode=77,LineStationIndex=6},
                new LineStation
                {/*NextStationCode=88,*/LineId=7,IsDeleted=false,StationCode=83,LineStationIndex=7},
                  new LineStation
                {/*NextStationCode=75,*/LineId=7,IsDeleted=false,StationCode=78,LineStationIndex=8},
                new LineStation
                {/*NextStationCode=88,*/LineId=7,IsDeleted=false,StationCode=89,LineStationIndex=9},
#endregion              
                #region line 88
                //line 88
                new LineStation
                {LineId=8,IsDeleted=false,StationCode=1491,LineStationIndex=0},
                new LineStation
                {LineId=8,IsDeleted=false,StationCode=91,LineStationIndex=1},
                 new LineStation
                {LineId=8,IsDeleted=false,StationCode=121,LineStationIndex=2},
                new LineStation
                {LineId=8,IsDeleted=false,StationCode=106,LineStationIndex=3},
                 new LineStation
                {LineId=8,IsDeleted=false,StationCode=76,LineStationIndex=4},
                new LineStation
                {LineId=8,IsDeleted=false,StationCode=1510,LineStationIndex=5},
                 new LineStation
                {LineId=8,IsDeleted=false,StationCode=1512,LineStationIndex=6},
                new LineStation
                {LineId=8,IsDeleted=false,StationCode=1522,LineStationIndex=7},
                  new LineStation
                {LineId=8,IsDeleted=false,StationCode=78,LineStationIndex=8},
                new LineStation
                {LineId=8,IsDeleted=false,StationCode=73,LineStationIndex=9},
#endregion              
                #region line 99                
                new LineStation
                {LineId=9,IsDeleted=false,StationCode=93,LineStationIndex=0},
                new LineStation
                {LineId=9,IsDeleted=false,StationCode=94,LineStationIndex=1},
                 new LineStation
                {LineId=9,IsDeleted=false,StationCode=95,LineStationIndex=2},
                new LineStation
                {LineId=9,IsDeleted=false,StationCode=102,LineStationIndex=3},
                 new LineStation
                {LineId=9,IsDeleted=false,StationCode=103,LineStationIndex=4},
                new LineStation
                {LineId=9,IsDeleted=false,StationCode=108,LineStationIndex=5},
                 new LineStation
                {LineId=9,IsDeleted=false,StationCode=84,LineStationIndex=6},
                new LineStation
                {LineId=9,IsDeleted=false,StationCode=85,LineStationIndex=7},
                  new LineStation
                {LineId=9,IsDeleted=false,StationCode=86,LineStationIndex=8},
                new LineStation
                {LineId=9,IsDeleted=false,StationCode=116,LineStationIndex=9},
#endregion              
                #region line 100                
                new LineStation
                {LineId=10,IsDeleted=false,StationCode=111,LineStationIndex=0},
                new LineStation
                {LineId=10,IsDeleted=false,StationCode=113,LineStationIndex=1},
                 new LineStation
                {LineId=10,IsDeleted=false,StationCode=91,LineStationIndex=2},
                new LineStation
                {LineId=10,IsDeleted=false,StationCode=121,LineStationIndex=3},
                 new LineStation
                {LineId=10,IsDeleted=false,StationCode=106,LineStationIndex=4},
                new LineStation
                {LineId=10,IsDeleted=false,StationCode=76,LineStationIndex=5},
                 new LineStation
                {LineId=10,IsDeleted=false,StationCode=77,LineStationIndex=6},
                new LineStation
                {LineId=10,IsDeleted=false,StationCode=110,LineStationIndex=7},
                  new LineStation
                {LineId=10,IsDeleted=false,StationCode=94,LineStationIndex=8},
                new LineStation
                {LineId=10,IsDeleted=false,StationCode=95,LineStationIndex=9},
#endregion              

            };
            #endregion

            ListAdjacentStations = new List<AdjacentStations>
            {
                new AdjacentStations{StationCode1=73,StationCode2=77,Distance=5.1,Time=TimeSpan.FromMinutes(3.4),IsDeleted=false},
                new AdjacentStations{StationCode1=83,StationCode2=78,Distance=6.24,Time=TimeSpan.FromMinutes(6.5),IsDeleted=false},
                new AdjacentStations{StationCode1=77,StationCode2=83,Distance=3.24,Time=TimeSpan.FromMinutes(2.5),IsDeleted=false},
                new AdjacentStations{StationCode1=73,StationCode2=76,Distance=2.24,Time=TimeSpan.FromMinutes(1.5),IsDeleted=false},

                new AdjacentStations{StationCode1=76,StationCode2=1491,Distance=5.1,Time=TimeSpan.FromMinutes(3.4),IsDeleted=false},
                new AdjacentStations{StationCode1=1491,StationCode2=1510,Distance=6.24,Time=TimeSpan.FromMinutes(6.5),IsDeleted=false},
                new AdjacentStations{StationCode1=1510,StationCode2=1512,Distance=3.24,Time=TimeSpan.FromMinutes(2.5),IsDeleted=false},
                new AdjacentStations{StationCode1=1512,StationCode2=1522,Distance=2.24,Time=TimeSpan.FromMinutes(1.5),IsDeleted=false},
                new AdjacentStations{StationCode1=1522,StationCode2=78,Distance=5.1,Time=TimeSpan.FromMinutes(3.4),IsDeleted=false},
                new AdjacentStations{StationCode1=78,StationCode2=89,Distance=6.24,Time=TimeSpan.FromMinutes(6.5),IsDeleted=false},
                new AdjacentStations{StationCode1=89,StationCode2=1524,Distance=3.24,Time=TimeSpan.FromMinutes(2.5),IsDeleted=false},
                new AdjacentStations{StationCode1=1524,StationCode2=1493,Distance=2.24,Time=TimeSpan.FromMinutes(1.5),IsDeleted=false},

               new AdjacentStations{StationCode1=78,StationCode2=110,Distance=5.1,Time=TimeSpan.FromMinutes(3.4),IsDeleted=false},
                new AdjacentStations{StationCode1=110,StationCode2=111,Distance=6.24,Time=TimeSpan.FromMinutes(6.5),IsDeleted=false},
                new AdjacentStations{StationCode1=111,StationCode2=113,Distance=3.24,Time=TimeSpan.FromMinutes(2.5),IsDeleted=false},
                new AdjacentStations{StationCode1=113,StationCode2=91,Distance=2.24,Time=TimeSpan.FromMinutes(1.5),IsDeleted=false},
                new AdjacentStations{StationCode1=91,StationCode2=121,Distance=5.1,Time=TimeSpan.FromMinutes(3.4),IsDeleted=false},
                new AdjacentStations{StationCode1=121,StationCode2=106,Distance=6.24,Time=TimeSpan.FromMinutes(6.5),IsDeleted=false},

                new AdjacentStations{StationCode1=77,StationCode2=110,Distance=5.1,Time=TimeSpan.FromMinutes(3.4),IsDeleted=false},
                new AdjacentStations{StationCode1=111,StationCode2=1512,Distance=6.24,Time=TimeSpan.FromMinutes(6.5),IsDeleted=false},
                new AdjacentStations{StationCode1=1512,StationCode2=91,Distance=3.24,Time=TimeSpan.FromMinutes(2.5),IsDeleted=false},
                new AdjacentStations{StationCode1=91,StationCode2=89,Distance=2.24,Time=TimeSpan.FromMinutes(1.5),IsDeleted=false},
                new AdjacentStations{StationCode1=116,StationCode2=102,Distance=5.1,Time=TimeSpan.FromMinutes(3.4),IsDeleted=false},
                new AdjacentStations{StationCode1=102,StationCode2=83,Distance=6.24,Time=TimeSpan.FromMinutes(6.5),IsDeleted=false},
                new AdjacentStations{StationCode1=89,StationCode2=116,Distance=6.24,Time=TimeSpan.FromMinutes(6.5),IsDeleted=false},

                new AdjacentStations{StationCode1=1491,StationCode2=91,Distance=5.1,Time=TimeSpan.FromMinutes(3.4),IsDeleted=false},
                new AdjacentStations{StationCode1=121,StationCode2=83,Distance=6.24,Time=TimeSpan.FromMinutes(6.5),IsDeleted=false},
                new AdjacentStations{StationCode1=83,StationCode2=89,Distance=3.24,Time=TimeSpan.FromMinutes(2.5),IsDeleted=false},
                new AdjacentStations{StationCode1=1493,StationCode2=102,Distance=2.24,Time=TimeSpan.FromMinutes(1.5),IsDeleted=false},
                new AdjacentStations{StationCode1=102,StationCode2=84,Distance=5.1,Time=TimeSpan.FromMinutes(3.4),IsDeleted=false},

                new AdjacentStations{StationCode1=106,StationCode2=76,Distance=3.24,Time=TimeSpan.FromMinutes(2.5),IsDeleted=false},
                new AdjacentStations{StationCode1=76,StationCode2=77,Distance=2.24,Time=TimeSpan.FromMinutes(1.5),IsDeleted=false},

               new AdjacentStations{StationCode1=121,StationCode2=112,Distance=2.74,Time=TimeSpan.FromMinutes(7.5),IsDeleted=false},
               new AdjacentStations{StationCode1=112,StationCode2=77,Distance=2.74,Time=TimeSpan.FromMinutes(7.5),IsDeleted=false},

               new AdjacentStations{StationCode1=76,StationCode2=1510,Distance=2.74,Time=TimeSpan.FromMinutes(7.5),IsDeleted=false},
               new AdjacentStations{StationCode1=78,StationCode2=73,Distance=2.74,Time=TimeSpan.FromMinutes(7.5),IsDeleted=false},

                new AdjacentStations{StationCode1=93,StationCode2=94,Distance=6.24,Time=TimeSpan.FromMinutes(6.5),IsDeleted=false},
                new AdjacentStations{StationCode1=94,StationCode2=95,Distance=3.24,Time=TimeSpan.FromMinutes(2.5),IsDeleted=false},
                new AdjacentStations{StationCode1=95,StationCode2=102,Distance=2.24,Time=TimeSpan.FromMinutes(1.5),IsDeleted=false},
                new AdjacentStations{StationCode1=102,StationCode2=103,Distance=5.1,Time=TimeSpan.FromMinutes(3.4),IsDeleted=false},
                new AdjacentStations{StationCode1=103,StationCode2=108,Distance=6.24,Time=TimeSpan.FromMinutes(6.5),IsDeleted=false},
                new AdjacentStations{StationCode1=108,StationCode2=84,Distance=6.24,Time=TimeSpan.FromMinutes(6.5),IsDeleted=false},
                new AdjacentStations{StationCode1=84,StationCode2=85,Distance=5.1,Time=TimeSpan.FromMinutes(3.4),IsDeleted=false},
                new AdjacentStations{StationCode1=85,StationCode2=86,Distance=6.24,Time=TimeSpan.FromMinutes(6.5),IsDeleted=false},
                new AdjacentStations{StationCode1=86,StationCode2=116,Distance=3.24,Time=TimeSpan.FromMinutes(2.5),IsDeleted=false},

                new AdjacentStations{StationCode1=110,StationCode2=94,Distance=3.24,Time=TimeSpan.FromMinutes(2.5),IsDeleted=false},

            };

            //ListUsers = new List<User>
            //{
            //    new User{UserName="2",Password="2",NickName="Hadar", Admin=true,IsDeleted=false},
            //    new User{UserName="1",Password="1",NickName="Reut", Admin=false,IsDeleted=false}

            //};

            ListLineTrips = new List<LineTrip>
            {
                new LineTrip{LineId=1,StartAt=new TimeSpan(8,30,0)},
                new LineTrip{LineId=1,StartAt=new TimeSpan(9,30,0)},
                new LineTrip{LineId=1,StartAt=new TimeSpan(10,0,0)},
                new LineTrip{LineId=1,StartAt=new TimeSpan(13,30,0)},
                new LineTrip{LineId=1,StartAt=new TimeSpan(15,30,0)},
                new LineTrip{LineId=1,StartAt=new TimeSpan(14,15,0)},
                new LineTrip{LineId=1,StartAt=new TimeSpan(16,30,0)},
                new LineTrip{LineId=1,StartAt=new TimeSpan(23,30,0)},

                new LineTrip{LineId=2,StartAt=new TimeSpan(9,15,0)},
                new LineTrip{LineId=2,StartAt=new TimeSpan(11,30,0)},
                new LineTrip{LineId=2,StartAt=new TimeSpan(13,30,0)},
                new LineTrip{LineId=2,StartAt=new TimeSpan(16,15,0)},
                new LineTrip{LineId=2,StartAt=new TimeSpan(20,30,0)},
                new LineTrip{LineId=2,StartAt=new TimeSpan(22,30,0)},
                new LineTrip{LineId=2,StartAt=new TimeSpan(23,15,0)},

                new LineTrip{LineId=3,StartAt=new TimeSpan(9,15,0)},
                new LineTrip{LineId=3,StartAt=new TimeSpan(11,30,0)},
                new LineTrip{LineId=3,StartAt=new TimeSpan(13,30,0)},
                new LineTrip{LineId=3,StartAt=new TimeSpan(16,15,0)},
                new LineTrip{LineId=3,StartAt=new TimeSpan(18,15,0)},
                new LineTrip{LineId=3,StartAt=new TimeSpan(19,30,0)},
                new LineTrip{LineId=3,StartAt=new TimeSpan(21,30,0)},
                new LineTrip{LineId=3,StartAt=new TimeSpan(23,15,0)},

                new LineTrip{LineId=4,StartAt=new TimeSpan(8,30,0)},
                new LineTrip{LineId=4,StartAt=new TimeSpan(9,30,0)},
                new LineTrip{LineId=4,StartAt=new TimeSpan(11,15,0)},
                new LineTrip{LineId=4,StartAt=new TimeSpan(13,30,0)},
                new LineTrip{LineId=4,StartAt=new TimeSpan(15,30,0)},
                new LineTrip{LineId=4,StartAt=new TimeSpan(16,30,0)},
                new LineTrip{LineId=4,StartAt=new TimeSpan(20,30,0)},
                new LineTrip{LineId=4,StartAt=new TimeSpan(21,15,0)},
                new LineTrip{LineId=4,StartAt=new TimeSpan(22,30,0)},
                new LineTrip{LineId=4,StartAt=new TimeSpan(23,30,0)},

                new LineTrip{LineId=5,StartAt=new TimeSpan(8,30,0)},
                new LineTrip{LineId=5,StartAt=new TimeSpan(9,30,0)},
                new LineTrip{LineId=5,StartAt=new TimeSpan(11,15,0)},
                new LineTrip{LineId=5,StartAt=new TimeSpan(13,30,0)},
                new LineTrip{LineId=5,StartAt=new TimeSpan(15,30,0)},
                new LineTrip{LineId=5,StartAt=new TimeSpan(16,30,0)},
                new LineTrip{LineId=5,StartAt=new TimeSpan(17,30,0)},
                new LineTrip{LineId=5,StartAt=new TimeSpan(19,15,0)},
                new LineTrip{LineId=5,StartAt=new TimeSpan(21,30,0)},
                new LineTrip{LineId=5,StartAt=new TimeSpan(23,30,0)},

                new LineTrip{LineId=6,StartAt=new TimeSpan(8,30,0)},
                new LineTrip{LineId=6,StartAt=new TimeSpan(9,30,0)},
                new LineTrip{LineId=6,StartAt=new TimeSpan(11,15,0)},
                new LineTrip{LineId=6,StartAt=new TimeSpan(13,30,0)},
                new LineTrip{LineId=6,StartAt=new TimeSpan(15,30,0)},
                new LineTrip{LineId=6,StartAt=new TimeSpan(16,30,0)},
                new LineTrip{LineId=6,StartAt=new TimeSpan(17,30,0)},
                new LineTrip{LineId=6,StartAt=new TimeSpan(19,15,0)},
                new LineTrip{LineId=6,StartAt=new TimeSpan(21,30,0)},
                new LineTrip{LineId=6,StartAt=new TimeSpan(23,30,0)},

                new LineTrip{LineId=7,StartAt=new TimeSpan(8,30,0)},
                new LineTrip{LineId=7,StartAt=new TimeSpan(9,30,0)},
                new LineTrip{LineId=7,StartAt=new TimeSpan(11,15,0)},
                new LineTrip{LineId=7,StartAt=new TimeSpan(13,30,0)},
                new LineTrip{LineId=7,StartAt=new TimeSpan(15,30,0)},
                new LineTrip{LineId=7,StartAt=new TimeSpan(16,30,0)},
                new LineTrip{LineId=7,StartAt=new TimeSpan(17,30,0)},
                new LineTrip{LineId=7,StartAt=new TimeSpan(19,15,0)},
                new LineTrip{LineId=7,StartAt=new TimeSpan(21,30,0)},
                new LineTrip{LineId=7,StartAt=new TimeSpan(23,30,0)},

                new LineTrip{LineId=8,StartAt=new TimeSpan(8,30,0)},
                new LineTrip{LineId=8,StartAt=new TimeSpan(9,30,0)},
                new LineTrip{LineId=8,StartAt=new TimeSpan(11,15,0)},
                new LineTrip{LineId=8,StartAt=new TimeSpan(13,30,0)},
                new LineTrip{LineId=8,StartAt=new TimeSpan(15,30,0)},
                new LineTrip{LineId=8,StartAt=new TimeSpan(16,30,0)},
                new LineTrip{LineId=8,StartAt=new TimeSpan(17,30,0)},
                new LineTrip{LineId=8,StartAt=new TimeSpan(19,15,0)},
                new LineTrip{LineId=8,StartAt=new TimeSpan(21,30,0)},
                new LineTrip{LineId=8,StartAt=new TimeSpan(23,30,0)},

                new LineTrip{LineId=9,StartAt=new TimeSpan(8,30,0)},
                new LineTrip{LineId=9,StartAt=new TimeSpan(9,30,0)},
                new LineTrip{LineId=9,StartAt=new TimeSpan(11,15,0)},
                new LineTrip{LineId=9,StartAt=new TimeSpan(13,30,0)},
                new LineTrip{LineId=9,StartAt=new TimeSpan(15,30,0)},
                new LineTrip{LineId=9,StartAt=new TimeSpan(16,30,0)},
                new LineTrip{LineId=9,StartAt=new TimeSpan(17,30,0)},
                new LineTrip{LineId=9,StartAt=new TimeSpan(19,15,0)},
                new LineTrip{LineId=9,StartAt=new TimeSpan(21,30,0)},
                new LineTrip{LineId=9,StartAt=new TimeSpan(23,30,0)},

                new LineTrip{LineId=10,StartAt=new TimeSpan(8,30,0)},
                new LineTrip{LineId=10,StartAt=new TimeSpan(9,30,0)},
                new LineTrip{LineId=10,StartAt=new TimeSpan(11,15,0)},
                new LineTrip{LineId=10,StartAt=new TimeSpan(13,30,0)},
                new LineTrip{LineId=10,StartAt=new TimeSpan(15,30,0)},
                new LineTrip{LineId=10,StartAt=new TimeSpan(16,30,0)},
                new LineTrip{LineId=10,StartAt=new TimeSpan(17,30,0)},
                new LineTrip{LineId=10,StartAt=new TimeSpan(19,15,0)},
                new LineTrip{LineId=10,StartAt=new TimeSpan(21,30,0)},
                new LineTrip{LineId=10,StartAt=new TimeSpan(23,30,0)},
            };



        }
    }
}

