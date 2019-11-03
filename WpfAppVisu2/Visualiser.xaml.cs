
using System.Collections.ObjectModel;


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WpfAppVisu
{
    /// <summary>
    /// Logique d'interaction pour Visualiser.xaml
    /// à remettre dans des view models
    /// </summary>
    public partial class Visualiser : Window
    {
        private ColdPlate coldplate;
        private ObservableCollection<LossElementInstance> lossElementInstances;
        private Collection<LossElement> lossElements;
        private Collection<LossElementDetail> leDetails;
        private Rectangle selectedRectangle;
        private LossElementDetail selectedLe;
        private LossElementDetail previousLe;

        public ObservableCollection<LossElementInstance> LossElementInstances { get; private set; }

        public Visualiser(ColdPlate coldplate, ObservableCollection<LossElementInstance> lossElementInstances, Collection<LossElement> lossElements)
        {
            this.coldplate = coldplate;
            this.LossElementInstances = lossElementInstances;
            this.lossElements = lossElements;


            InitializeComponent();

            int offsetMarginX = 10;
            int offsetMarginY = 10;

            leDetails = new Collection<LossElementDetail>();

            // create coldplates sides Upper plate  / Lower plate
            CreateRectangle(coldplate.Name.Key + " Upper plate", offsetMarginX, offsetMarginY, (int)coldplate.Dimension.X, (int)coldplate.Dimension.Y, Brushes.Gray, Brushes.DarkGray, 1);
            CreateRectangle(coldplate.Name.Key + " Lower plate", offsetMarginX * 2 + (int)coldplate.Dimension.X, offsetMarginY, (int)coldplate.Dimension.X, (int)coldplate.Dimension.Y, Brushes.Gray, Brushes.DarkGray, 1);


            for (int i = 0; i < lossElementInstances.Count(); i++)
            {
                // Create the rectangle
                var nval = lossElementInstances.ElementAt(i).Name;
                var xval = (int)lossElementInstances.ElementAt(i).Position.X;
                var yval = (int)lossElementInstances.ElementAt(i).Position.Y;
                var wval = (int)lossElements.ElementAt(i).Dimension.X;
                var hval = (int)lossElements.ElementAt(i).Dimension.Y;


                var tuple = CreateRectangle(
                         nval,
                       xval
                        ,
                      yval
                        ,
                      wval,
                     hval
                      , Brushes.WhiteSmoke, Brushes.DarkGray, 2);


                var ldetail = new LossElementDetail()
                {
                    LossElementName = nval,
                    LeRectangle = tuple.Item1,
                    LeTextBloc = tuple.Item2,
                    X = xval,
                    Y = yval,
                    Width = wval,
                    Height = hval,
                  
                    Type = "le"
                };


                leDetails.Add(ldetail);
                // distinct position for upper and lower plate
            }
            selectedLe = leDetails.First();
            selectedLe.LeRectangle.Opacity = 1;
            selectedLe.LeRectangle.Fill = new SolidColorBrush(Colors.Yellow);

            previousLe = selectedLe;
            DataContext = selectedLe;

        }

        public Tuple<Rectangle,TextBlock> CreateRectangle(string name, int x, int y, int width, int height, Brush fill, Brush Stroke, int thickness)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = name;
            textBlock.Foreground = new SolidColorBrush(Colors.Black);
            Canvas.SetLeft(textBlock, x + 5);
            Canvas.SetTop(textBlock, y);

            Rectangle rec = new Rectangle()
            {
                Name = name.Replace(" ", "_"),
                Width = width,
                Height = height,
                Fill = fill,
                Stroke = Stroke,
                StrokeThickness = thickness,
               
            };

            rec.MouseDown += new MouseButtonEventHandler(shape_MouseDown);
            Canvas.SetLeft(rec, x);
            Canvas.SetTop(rec, y);

            canvas.Children.Add(rec);
            canvas.Children.Add(textBlock);
            var tuple = new  Tuple<Rectangle,TextBlock>(rec,textBlock);
            return tuple;
        }

        private void shape_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            selectedRectangle = (Rectangle)e.Source;
            var recname = selectedRectangle.Name;

            var previousLe = selectedLe;
                previousLe.LeRectangle.Opacity = 0.5;
                previousLe.LeRectangle.Fill = new SolidColorBrush(Colors.White);

            var le = GetSelectedLE(selectedRectangle.Name);
            if (le == null) {
                selectedLe = previousLe;
                return;
                    };
            le.LeRectangle.Opacity = 1;
            le.LeRectangle.Fill = new SolidColorBrush(Colors.Yellow);

            DataContext = le;
            
        }

        

        private LossElementDetail GetSelectedLE(string name)
        {
            selectedLe = leDetails.FirstOrDefault(l => l.LossElementName == name.Replace("_", " "));
            return selectedLe;
        }

        private void TextBox_XChanged(object sender, TextChangedEventArgs e)
        {
 
            Console.WriteLine(selectedLe.X.ToString());
            Canvas.SetLeft(selectedLe.LeRectangle, 0);

        }

        private void TextBox_YChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine(selectedLe.Y.ToString());
         
        }

        private void TextBox_HChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine(selectedLe.Height.ToString());
            //selectedLe.LeRectangle.Width = Width;
        }
        private void TextBox_WChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine(selectedLe.Width.ToString());
            //selectedLe.LeRectangle.Height = Height;
        }
    }

  
    public class LossElementDetail
    {
        public TextBlock LeTextBloc
        {
            get;
            set;
        }

        public Rectangle LeRectangle
        {
            get;
            set;
        }
        public string LossElementName
        {
            get;
            set;
        }

        public int x;
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                Canvas.SetLeft(this.LeRectangle, (int)x);
                Canvas.SetLeft(this.LeTextBloc, (int)x + 5);
            }
        }

        public int y;
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                Canvas.SetTop(this.LeRectangle, (int)y);
                Canvas.SetTop(this.LeTextBloc, (int)y + 5);
            }
        }

        public int height;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                this.LeRectangle.Height= height;
          
            }
        }


        public int width;
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                this.LeRectangle.Width=width;
            
            }
        }
        
        public string Type
        {
            get;
            set;
        }
        
    }

}

