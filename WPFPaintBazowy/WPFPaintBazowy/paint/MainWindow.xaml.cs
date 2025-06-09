using System;
using System.Collections.Generic;
using System.IO;
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

namespace paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ListaDoMaloania.Items.Add("Ellipse");
            ListaDoMaloania.Items.Add("Line");
            ListaDoMaloania.Items.Add("Path");
            ListaDoMaloania.Items.Add("Polygon");
            ListaDoMaloania.Items.Add("Polyline");
            ListaDoMaloania.Items.Add("Rectangle");
        }

        bool isDown = false;

        void PaintCircle(Brush circleColor, Point position)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = circleColor;
            ellipse.Width = 15;
            ellipse.Height = 15;
            Canvas.SetLeft(ellipse, position.X);
            Canvas.SetTop(ellipse, position.Y);
            CanvasTest.Children.Add(ellipse);
        }

        void PaintRectangl(Brush circleColor, Point position)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = circleColor;
            rectangle.Width = 15;
            rectangle.Height = 15;
            Canvas.SetLeft(rectangle, position.X);
            Canvas.SetTop(rectangle, position.Y);
            CanvasTest.Children.Add(rectangle);
        }

        void PaintLine(Point p1, Point p2)
        {
            // Add a Line Element
            Line myLine = new Line();
            myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            myLine.X1 = p1.X;
            myLine.X2 = p2.X;
            myLine.Y1 = p1.Y;
            myLine.Y2 = p2.Y;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            CanvasTest.Children.Add(myLine);
        }

        void PaintPath(Point p1, Point p2)
        {
            System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();
            myPath.Stroke = System.Windows.Media.Brushes.Black;
            myPath.Fill = System.Windows.Media.Brushes.MediumSlateBlue;
            myPath.StrokeThickness = 4;
            myPath.HorizontalAlignment = HorizontalAlignment.Left;
            myPath.VerticalAlignment = VerticalAlignment.Center;
            EllipseGeometry myEllipseGeometry = new EllipseGeometry();
            myEllipseGeometry.Center = new System.Windows.Point(p1.X, p2.Y);
            myEllipseGeometry.RadiusX = 25;
            myEllipseGeometry.RadiusY = 25;
            myPath.Data = myEllipseGeometry;
            CanvasTest.Children.Add(myPath);
        }

        void PaintPolygon(List<Point> listOfPoints)
        {
            //Add the Polygon Element
            Polygon myPolygon = new Polygon();
            myPolygon.Stroke = System.Windows.Media.Brushes.Black;
            myPolygon.Fill = System.Windows.Media.Brushes.LightSeaGreen;
            myPolygon.StrokeThickness = 2;
            myPolygon.HorizontalAlignment = HorizontalAlignment.Left;
            myPolygon.VerticalAlignment = VerticalAlignment.Center;
            PointCollection myPointCollection = new PointCollection();
            foreach (Point point in listOfPoints)
            {
                myPointCollection.Add(point);
            }
            myPolygon.Points = myPointCollection;
            CanvasTest.Children.Add(myPolygon);
        }
        void PaintPolyline(List<Point> listOfPoints)
        {
            Polyline myPolyline = new Polyline();
            myPolyline.Stroke = System.Windows.Media.Brushes.SlateGray;
            myPolyline.StrokeThickness = 2;
            myPolyline.FillRule = FillRule.EvenOdd;
            PointCollection myPointCollection2 = new PointCollection();
            foreach (Point point in listOfPoints)
            {
                myPointCollection2.Add(point);
            }
            myPolyline.Points = myPointCollection2;
            CanvasTest.Children.Add(myPolyline);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.GetPosition(CanvasTest);
            if (ListaDoMaloania.SelectedItem == "Ellipse")
            {
                if (isDown)
                {
                    PaintCircle(Brushes.Black, mousePosition);
                }
            }
            if (ListaDoMaloania.SelectedItem == "Rectangle")
            {
                if (isDown)
                {
                    PaintRectangl(Brushes.Blue, mousePosition);
                    //todo
                }
            }
        }

        //zwolnienie lewy przycisk myszy
        Point mousePositionDown;
        List<Point> listOfPoints = new List<Point>();
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDown = false;
            if (ListaDoMaloania.SelectedItem == "Line")
            {
                PaintLine(mousePositionDown, e.GetPosition(CanvasTest));
            }
            if (ListaDoMaloania.SelectedItem == "Path")
            {
                PaintPath(mousePositionDown, e.GetPosition(CanvasTest));
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDown = true;
            mousePositionDown = e.GetPosition(CanvasTest);

            if (ListaDoMaloania.SelectedItem == "Polygon" || ListaDoMaloania.SelectedItem == "Polyline")
            {
                listOfPoints.Add(mousePositionDown);
            }
        }

        private void CanvasTest_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ListaDoMaloania.SelectedItem == "Polygon")
            {
                if (listOfPoints.Count >= 3)
                {
                    PaintPolygon(listOfPoints);
                    listOfPoints.Clear();
                }
            }
            else if (ListaDoMaloania.SelectedItem == "Polyline")
            {
                if (listOfPoints.Count >= 2) 
                {
                    PaintPolyline(listOfPoints);
                    listOfPoints.Clear();
                }
            }
        }

        //przyklad uruchomienia kolejnego okna
        private void button_Click(object sender, RoutedEventArgs e)
        {
            AnotherWindow someWindow = new AnotherWindow();
            someWindow.Show();
        }

    }
}