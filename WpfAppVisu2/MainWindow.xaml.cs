using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppVisu;

namespace WpfAppVisu2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var coldplate = new ColdPlate();
            coldplate.Dimension.X = 300;
            coldplate.Dimension.Y = 300;
            coldplate.Name = new KeyValuePair<string, string>("Coldplate basic","rr");

            var lis = new ObservableCollection<LossElementInstance>();
            var li1 = new LossElementInstance();
            li1.Name = "li 1";
            li1.Position.X = 55;
            li1.Position.Y = 55;

            var li2 = new LossElementInstance();
            li2.Name = "li 2";
            li2.Position.X = 100;
            li2.Position.Y = 50;

            lis.Add(li1);
            lis.Add(li2);



            var les = new Collection<LossElement>();
            var le1 = new LossElement();
       
            le1.Dimension.X = 50;
            le1.Dimension.Y = 50;

            var le2 = new LossElement();
           
            le2.Dimension.X = 50;
            le2.Dimension.Y = 50;

            les.Add(le1);
            les.Add(le2);


      
            Visualiser popup = new Visualiser( 
                coldplate,
                lis, 
                les
                );

            popup.ShowDialog();
        }
    }
}
