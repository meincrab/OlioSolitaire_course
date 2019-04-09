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

namespace OlioSolitaire3000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Switcher.pageSwitcher = this;
            // on window init load mainmenu view
            Switcher.Switch(new mainmenu());
        }
        // used to load other views
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }
        // when loading views, with this apparently one can pass parameters to other views
        public void Navigate(UserControl nextPage, object state)
        {
            //load page
            this.Content = nextPage;
            // use ISwitchable interface
            ISwitchable s = nextPage as ISwitchable;
            // check if s is not null
            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }
    }
    public interface ISwitchable
    {
        void UtilizeState(object state);
    }
    //view swithcer class
    public static class Switcher
    {
        public static MainWindow pageSwitcher;

        public static void Switch(UserControl newPage)
        {
            pageSwitcher.Navigate(newPage);
        }

        public static void Switch(UserControl newPage, object state)
        {
            pageSwitcher.Navigate(newPage, state);
        }


    }
}
