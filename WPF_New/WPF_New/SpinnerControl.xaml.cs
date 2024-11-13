using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WPF_New
{
    public partial class SpinnerControl : UserControl
    {
        public SpinnerControl()
        {
            InitializeComponent();
            Loaded += OnLoaded; 
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CreateCircles();
            StartAnimation();
        }

        // Свойства для настройки Spinner
        public Color CircleColor
        {
            get => (Color)GetValue(CircleColorProperty);
            set => SetValue(CircleColorProperty, value);
        }

        public static readonly DependencyProperty CircleColorProperty =
            DependencyProperty.Register("CircleColor", typeof(Color), typeof(SpinnerControl), new PropertyMetadata(Colors.Blue));

        public int CircleCount
        {
            get => (int)GetValue(CircleCountProperty);
            set => SetValue(CircleCountProperty, value);
        }

        public static readonly DependencyProperty CircleCountProperty =
            DependencyProperty.Register("CircleCount", typeof(int), typeof(SpinnerControl), new PropertyMetadata(8));

        public double CircleSize
        {
            get => (double)GetValue(CircleSizeProperty);
            set => SetValue(CircleSizeProperty, value);
        }

        public static readonly DependencyProperty CircleSizeProperty =
            DependencyProperty.Register("CircleSize", typeof(double), typeof(SpinnerControl), new PropertyMetadata(10.0));

        public bool IsClockwise
        {
            get => (bool)GetValue(IsClockwiseProperty);
            set => SetValue(IsClockwiseProperty, value);
        }

        public static readonly DependencyProperty IsClockwiseProperty =
            DependencyProperty.Register("IsClockwise", typeof(bool), typeof(SpinnerControl), new PropertyMetadata(true));

        public double RotationSpeed
        {
            get => (double)GetValue(RotationSpeedProperty);
            set => SetValue(RotationSpeedProperty, value);
        }

        public static readonly DependencyProperty RotationSpeedProperty =
            DependencyProperty.Register("RotationSpeed", typeof(double), typeof(SpinnerControl), new PropertyMetadata(2.0));

        private void CreateCircles()
        {
            SpinnerCanvas.Children.Clear();
            double radius = Math.Min(SpinnerCanvas.Width, SpinnerCanvas.Height) / 2 - CircleSize;

            for (int i = 0; i < CircleCount; i++)
            {
                double angle = 360.0 / CircleCount * i;
                double angleRadians = angle * Math.PI / 180;
                double x = SpinnerCanvas.Width / 2 + radius * Math.Cos(angleRadians) - CircleSize / 2;
                double y = SpinnerCanvas.Height / 2 + radius * Math.Sin(angleRadians) - CircleSize / 2;

                Ellipse circle = new Ellipse
                {
                    Width = CircleSize,
                    Height = CircleSize,
                    Fill = new SolidColorBrush(CircleColor)
                };

                Canvas.SetLeft(circle, x);
                Canvas.SetTop(circle, y);
                SpinnerCanvas.Children.Add(circle);
            }
        }

        private void StartAnimation()
        {
            // Устанавливаем анимацию вращения для Canvas
            double angleFrom = 0;
            double angleTo = IsClockwise ? 360 : -360;

            RotateTransform rotateTransform = new RotateTransform();
            SpinnerCanvas.RenderTransform = rotateTransform;
            SpinnerCanvas.RenderTransformOrigin = new Point(0.5, 0.5);

            DoubleAnimation rotationAnimation = new DoubleAnimation
            {
                From = angleFrom,
                To = angleTo,
                Duration = TimeSpan.FromSeconds(RotationSpeed),
                RepeatBehavior = RepeatBehavior.Forever
            };

            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotationAnimation);
        }
    }
}
