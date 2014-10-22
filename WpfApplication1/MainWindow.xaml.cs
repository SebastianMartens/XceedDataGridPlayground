using System.Windows;
using System.Windows.Input;

namespace WpfApplication1
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

        void myGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Example of how to change behaviour "onEnter" to move to the next row:
            if (e.Key == Key.Return)
            {
                MyGrid.Items.MoveCurrentToNext();
                MyGrid.BringItemIntoView(MyGrid.CurrentItem);                
                e.Handled = true;
            }                        
        }
 
    }
}
