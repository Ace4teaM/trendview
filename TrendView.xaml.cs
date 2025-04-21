using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace trendview_wpf
{
    public partial class TrendGraph : FrameworkElement
    {
        public Point[] points;

        protected StringBuilder geometry;
        protected Path path;

        public TrendGraph(Point[] points)
        {
            this.points = points;

            geometry = new StringBuilder();
            geometry.Append(String.Format("M {0},{1} L", points[0].X, points[0].Y));
            foreach (var point in points.Skip(1))
            {
                geometry.Append(String.Format(" {0},{1}", point.X, point.Y));
            }

            path = new Path();
            path.Data = PathGeometry.Parse(geometry.ToString());
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawGeometry(null, new Pen(Brushes.Red,2.0),path.Data);

            foreach (var point in points)
                drawingContext.DrawEllipse(Brushes.DarkRed, null, point, 3,3);

        }
    }

    /// <summary>
    /// Logique d'interaction pour TrendView.xaml
    /// </summary>
    public partial class TrendView : UserControl
    {
        public TrendGraph trend1 = new TrendGraph(new Point[] { new Point(0, 0), new Point(100, 10), new Point(200, 50), new Point(300, 30), new Point(400, 10) });
        public TrendGraph trend2 = new TrendGraph(new Point[] { new Point(0, 200), new Point(100, 210), new Point(200, 250), new Point(300, 230), new Point(400, 210) });
        public TrendView()
        {
            InitializeComponent();
            grid.Children.Add(trend1);
            grid.Children.Add(trend2);
        }

        private void grid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var element = Mouse.DirectlyOver;

            if (element is TrendGraph)
            {
                var trend = element as TrendGraph;
            }
        }
    }
}
