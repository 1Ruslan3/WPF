using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_New
{
    public partial class DialogHost : UserControl
    {
        public DialogHost()
        {
            InitializeComponent();
        }

        // Свойство для управления содержимым диалога
        public object DialogContent
        {
            get { return GetValue(DialogContentProperty); }
            set { SetValue(DialogContentProperty, value); }
        }
        public static readonly DependencyProperty DialogContentProperty =
            DependencyProperty.Register(nameof(DialogContent), typeof(object), typeof(DialogHost), new PropertyMetadata(null));

        // Свойство для управления видимостью диалога
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(DialogHost), new PropertyMetadata(false));

        // Свойство для настройки закругления углов
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(DialogHost), new PropertyMetadata(new CornerRadius(0)));

        // Свойство для управления прозрачностью фона
        public double BackgroundOpacity
        {
            get { return (double)GetValue(BackgroundOpacityProperty); }
            set { SetValue(BackgroundOpacityProperty, value); }
        }
        public static readonly DependencyProperty BackgroundOpacityProperty =
            DependencyProperty.Register(nameof(BackgroundOpacity), typeof(double), typeof(DialogHost), new PropertyMetadata(0.4));

        // Обработчик кликов по фону
        private void BackgroundLayer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Можно закрыть диалог, если нужно
            IsOpen = false;
        }
    }
}
