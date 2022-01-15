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
using System.Threading;

namespace BeginnerWPFApp
{

    public static class ExtensionMethods
    {
        private static readonly Action EmptyDelegate = delegate { };
        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, EmptyDelegate);
        }
    }

    public partial class MainWindow : Window
    {

        Brush WhiteBrush;
        Brush fadeBrush;
        Random r = new Random();
        bool todayDone = false;

        public void addRectangles()
        {


        }
        private void rectMouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle activeRec = sender as Rectangle;
            Brush YellowBrush = new SolidColorBrush(Color.FromRgb(255, 255, 0));
            activeRec.Fill = YellowBrush;
        }
        private void rectMouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle activeRec = sender as Rectangle;
            WhiteBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            activeRec.Fill = WhiteBrush;
        }


        public MainWindow()
        {
            InitializeComponent();

            WhiteBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            for (int e = 0; e < 10; e++)
            {
                for (int i = 0; i < 20; i++)
                {

                    Rectangle newRec = new Rectangle();
                    newRec.Width = 30;
                    newRec.Height = 30;
                    newRec.StrokeThickness = 5;
                    newRec.Stroke = Brushes.Black;
                    newRec.Fill = WhiteBrush;
                    newRec.MouseEnter += rectMouseEnter;
                    newRec.MouseLeave += rectMouseLeave;

                    Canvas.SetLeft(newRec, (100 + (i * 30)));
                    Canvas.SetTop(newRec, 100+(e*30));

                    MyCanvas.Children.Add(newRec);
                }
            }
            
        }

        private void removeSquare(object sender, MouseButtonEventArgs e)
        {

            if (e.OriginalSource is Rectangle)
            {
                if (!todayDone)
                {
                    todayDone = true;
                    Rectangle activeRec = (Rectangle)e.OriginalSource;
                    Brush GreenBrush = Brushes.Green;
                    activeRec.Fill = Brushes.Green;
                    activeRec.Refresh();
                    

                    for (int i = 25; i > 0; i--)
                    {
                        Thread.Sleep(10);
                        fadeBrush = new SolidColorBrush(Color.FromRgb((byte)0, (byte)(i * 10), (byte)0));
                        activeRec.Fill = fadeBrush;

                        activeRec.RenderTransformOrigin = new Point(0.5, 0.5);

                        RotateTransform rotateTransform2 = new RotateTransform((25 - i) * 3);

                        double a = Convert.ToDouble(i);
                        ScaleTransform scaleTransform2 = new ScaleTransform((a / 25), (a / 25));

                        TransformGroup myTransformGroup = new TransformGroup();
                        myTransformGroup.Children.Add(scaleTransform2);
                        myTransformGroup.Children.Add(rotateTransform2);

                        activeRec.RenderTransform = myTransformGroup;

                        activeRec.Refresh();
                    }

                    MyCanvas.Children.Remove(activeRec);



                }
                else
                {
                    Rectangle activeRec = (Rectangle)e.OriginalSource;
                    activeRec.Fill = Brushes.Red;
                    activeRec.Refresh();
                    

                    for (int i = 0; i<51; i++)
                    {
                        Thread.Sleep(1);
                        fadeBrush = new SolidColorBrush(Color.FromRgb(255, (byte)(i*5), (byte)(i*5)));
                        activeRec.Fill = fadeBrush;
                        activeRec.Refresh();
                    }

                }

            }
            
        }
    }
}
