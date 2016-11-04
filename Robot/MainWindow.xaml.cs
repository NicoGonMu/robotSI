using System;
using System.Collections.Generic;
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

namespace Robot {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        Tablero tablero;

        public MainWindow() {
            InitializeComponent();
            Show();

            tablero = new Tablero(18);
            paint();


        }

        void paint()
        {
            TableroUI.Children.Clear();
            for (int i = 0; i < tablero.Length; i++)
            {
                for (int j = 0; j < tablero.Length; j++) {
                    Image image = new Image();
                    int cell = tablero.getCell(i, j);
                    if (cell == 0){
                        var uri = new Uri("pack://application:,,,/Textures/floor.png");
                        image.Source = new BitmapImage(uri);
                    }else if (cell == 1){
                        var uri = new Uri("pack://application:,,,/Textures/wall.png");
                        image.Source = new BitmapImage(uri);
                    }
                    image.Width = (TableroUI.ActualHeight < TableroUI.ActualWidth - 100) ? TableroUI.ActualHeight / tablero.Length : (TableroUI.ActualWidth - 100 )/ tablero.Length;
                    image.Height = image.Width;
                    image.Margin = new Thickness(image.Width * i, image.Height * j, image.Width * (i + 1), image.Height * (j + 1));
                    TableroUI.Children.Add(image);
                }
            }
            
        }

        void Click(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(this);
            double size = ((TableroUI.ActualHeight < TableroUI.ActualWidth - 100) ? TableroUI.ActualHeight / tablero.Length : (TableroUI.ActualWidth - 100) / tablero.Length);
            tablero.clickInTablero((int) (point.X / size), (int)(point.Y / size), false);
            paint();
        }
    }
}
