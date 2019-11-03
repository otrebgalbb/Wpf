using System.Collections.Generic;

namespace WpfAppVisu
{
    public class ColdPlate
    {
        public KeyValuePair<string,string> Name { get;  set; }
        public Dimension Dimension { get;  set; }
        public ColdPlate()
        {
            Name = new KeyValuePair<string, string>("","");
            Dimension = new Dimension() { X = 0, Y = 0 };
        }
    }

    public class Dimension
    {
        public double X{ get; set; }
        public double Y { get; set; }
    }
}