
namespace MyApp.Models;
using ChoETL;


// Classes of Json objects  
    public class Flag
    {
        public string? png { get; set; }
        public string? svg { get; set; }
        
    }

    public class Name
    {
        public string? common { get; set; }
        public string? official { get; set; }
        public NativeName? nativeName { get; set; }
    }

    public class NativeName
    {
        public Spa? spa { get; set; }
    }
[ChoXmlRecordObject(XPath = "Contries/Country")]
    public class Root
    {
        public Name? name { get; set; }
        public string[]? capital {get; set;}
        public string? region {get; set;}
        public string? subRegion {get; set;}
        public long? population { get; set; }
        public float? area { get; set; }
        public string[]? timezones { get; set; }
        public Flag? flags {get; set;}
    }

    public class Spa
    {
        public string? official { get; set; }
        public string? common { get; set; }
    }

    

