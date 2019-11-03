namespace WpfAppVisu
{
    public class LossElement
    {

        public Dimension Dimension { get;  set; }

        public LossElement()
        {
            Dimension = new Dimension() { X = 0, Y = 0 };
        }
    }
}