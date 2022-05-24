using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace WPFDungeon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Page gamePage;
        public MainWindow()
        {
            InitializeComponent();

            gamePage = new GamePage();
            frame.Content = gamePage;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                gamePage.KeyDown();
            }
            else if (e.Key == Key.S)
            {
                mDown = true;
            }
            else if (e.Key == Key.A)
            {
                mLeft = true;
            }
            else if (e.Key == Key.D)
            {
                mRight = true;
            }

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
