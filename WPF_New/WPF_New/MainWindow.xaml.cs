using System.Windows;

namespace WPF_New
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

        }

        private void OpenDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = true; // Открыть диалог
        }

        private void DialogHostControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }


}