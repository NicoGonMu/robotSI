using System;
using System.Threading;
using System.ComponentModel;
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

namespace PracticaRobot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Worker for async painting
        BackgroundWorker bg = new BackgroundWorker();

        Tablero tablero;
        List<Robot> robotList = new List<Robot>();
        int type = 0;
        int velocitat = 200;
        bool paused = true;


        public MainWindow()
        {
            InitializeComponent();
            Show();

            tablero = new Tablero(18);
            paint();
            vel.Text = 7 - velocitat / 100 + "";
            bg.DoWork += runAllRobots;
            bg.ProgressChanged += asyncPaint;
            bg.WorkerReportsProgress = true;
        }

        void paint() {
            TableroUI.Children.Clear();
            double size =  (TableroUI.ActualHeight < TableroUI.ActualWidth - 100) ? TableroUI.ActualHeight / tablero.Length : (TableroUI.ActualWidth - 100) / tablero.Length;
            for (int i = 0; i < tablero.Length; i++) {
                for (int j = 0; j < tablero.Length; j++) {
                    Image image = new Image();
                    int cell = tablero.getCell(i, j);
                    if (cell == 0) {
                        var uri = new Uri("pack://application:,,,/Textures/floor.png");
                        image.Source = new BitmapImage(uri);
                    }
                    else if (cell == 1) {
                        var uri = new Uri("pack://application:,,,/Textures/wall.png");
                        image.Source = new BitmapImage(uri);
                    }
                    else if (cell == 2) {
                        var uri = new Uri("pack://application:,,,/Textures/floor.png");
                        image.Source = new BitmapImage(uri);
                    }
                    image.Width = size;
                    image.Height = image.Width;
                    image.Margin = new Thickness(image.Width * i, image.Height * j, image.Width * (i + 1), image.Height * (j + 1));
                    TableroUI.Children.Add(image);
                }
            }
            foreach (Robot r in robotList) {

                var uri = new Uri("pack://application:,,,/Textures/"+r.direccio.ToString() + "robot.png");
                Image image = new Image();
                image.Source = new BitmapImage(uri);
                image.Width = size;
                image.Height = image.Width;
                image.Margin = new Thickness(image.Width * r.X, image.Height * r.Y, image.Width * (r.X + 1), image.Height * (r.Y + 1));
                TableroUI.Children.Add(image);
            }
        }

        void Click(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(this);
            double size = ((TableroUI.ActualHeight < TableroUI.ActualWidth - 100) ? TableroUI.ActualHeight / tablero.Length : (TableroUI.ActualWidth - 100) / tablero.Length);

            int x = (int)(point.X / size);
            int y = (int)(point.Y / size);
            tablero.clickInTablero(x, y, type);
            if (type == 2) { robotList.Add(new Robot(x, y, ref tablero)); }
            paint();
        }

        void selectWall(object sender, RoutedEventArgs e)
        {
            if (type == 1)
            {
                Wall.Background = Brushes.LightGray;
                type = 0;
            }
            else {
                Wall.Background = Brushes.Gray;
                Robot.Background = Brushes.LightGray;
                Remove.Background = Brushes.LightGray;
                type = 1;
            }
        }

        void selectRobot(object sender, RoutedEventArgs e)
        {
            if (type == 2)
            {
                Robot.Background = Brushes.LightGray;
                type = 0;
            }
            else {
                Robot.Background = Brushes.Gray;
                Wall.Background = Brushes.LightGray;
                Remove.Background = Brushes.LightGray;
                type = 2;
            }
        }

        void decVel(object sender, RoutedEventArgs e)
        {
            if (velocitat < 600) velocitat += 100;
            vel.Text = 7 - velocitat / 100 + "";
        }


        void incVel(object sender, RoutedEventArgs e)
        {
            if (velocitat > 100) velocitat -= 100;
            vel.Text = 7 - velocitat / 100 + "";
        }


        void startProcess(object sender, RoutedEventArgs e)
        {
            if (paused)
            {
                Start.Content = "Pause";
                paused = false;
                if (robotList.Count == 0)
                {
                    return;
                }

                bg.RunWorkerAsync(0);
            }
            else {
                paused = true;
                Start.Content = "Start";
            }
        }

        void stepProcess(object sender, RoutedEventArgs e)
        {
            if (robotList.Count == 0)
            {
                return;
            }

            foreach (Robot r in robotList)
            {
                r.move(tablero);
            }

            paint();
        }

        void removeSelection(object sender, RoutedEventArgs e)
        {
            type = 0;
            Robot.Background = Brushes.LightGray;
            Wall.Background = Brushes.LightGray;
            Remove.Background = Brushes.LightGray;
        }


        private void runAllRobots(object sender, DoWorkEventArgs e)
        {
            while (!paused)
            {
                if (robotList.Count == 0)
                {
                    continue;
                }

                foreach (Robot r in robotList)
                {
                    r.move(tablero);

                }

                bg.ReportProgress(0);

                Thread.Sleep(velocitat);
            }
        }

        private void asyncPaint(object sender, ProgressChangedEventArgs e)
        {
            paint();
        }

    }
}
