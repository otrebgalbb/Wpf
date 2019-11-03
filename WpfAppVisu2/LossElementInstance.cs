using System.Collections.Generic;

namespace WpfAppVisu
{
    public class LossElementInstance
    {
        public Position Position;
        public string Name;
        public LossElementInstance()
        {
            Name = "";
            Position = new Position() { X=0,Y=0};
        }
    }

    public class Position
    {
        public double X{
        get;
        set;
    }

        public double Y
        {
            get;
            set;
        }
    }
}