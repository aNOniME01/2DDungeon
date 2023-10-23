using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFDungeon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainMenu menu;
        private RegisterPage registerPage;
        private ScoreboardPage scoreboardPage;
        private Thread connectionThread;
        public MainWindow()
        {
            InitializeComponent();

            menu = new MainMenu();
            registerPage = new RegisterPage();
            scoreboardPage = new ScoreboardPage();

            frame.Content = menu;


            login.Visibility = Visibility.Hidden;
            register.Visibility = Visibility.Hidden;

            usernameHolder.Visibility = Visibility.Visible;
            userText.Text = "Connecting";
            userText.Foreground = Brushes.Orange;

            scoreButton.Visibility = Visibility.Hidden;            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connectionThread = new Thread(CheckConnection);
            connectionThread.Start();
        }

        private void CheckConnection()
        {
            try
            {
                if (!SQLOperations.Connect())
                {
                    Dispatcher.Invoke(() =>
                    {
                        userText.Text = "offline";
                        userText.Foreground = Brushes.Red;
                    });
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        usernameHolder.Visibility = Visibility.Hidden;

                        login.Visibility = Visibility.Visible;
                        register.Visibility = Visibility.Visible;

                        scoreButton.Visibility = Visibility.Visible;
                    });
                }
            }
            catch { }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQLOperations.Disconnect();
            menu.Closeing();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = registerPage;
            publish.Visibility = Visibility.Visible;
            publish.Content = "LogIn";

            backToMenu.Visibility = Visibility.Visible;

            scoreButton.Visibility = Visibility.Hidden;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = registerPage;
            publish.Visibility = Visibility.Visible;
            publish.Content = "Register";

            backToMenu.Visibility = Visibility.Visible;

            scoreButton.Visibility = Visibility.Hidden;
        }

        private void Scoreboard_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = scoreboardPage;

            backToMenu.Visibility = Visibility.Visible;

            scoreButton.Visibility = Visibility.Hidden;
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            backToMenu.Visibility = Visibility.Hidden;

            frame.Content = menu;
            publish.Visibility= Visibility.Hidden;

            registerPage.UsernameBox.Text = "";
            registerPage.PasswordBox.Password = "";

            scoreButton.Visibility = Visibility.Visible;
        }

        private void Publish_Click(object sender, RoutedEventArgs e)
        {
            if (publish.Content == "Register")
            {
                int[] errors = RegistAndLogIn.Register(registerPage.UsernameBox.Text, registerPage.PasswordBox.Password);
                if (errors[0] == 1)
                {
                    registerPage.UsernameError.Text = $"The username \"{registerPage.UsernameBox.Text}\" is already used.";
                }
                else if (errors[0] == 2)
                {
                    registerPage.UsernameError.Text = $"The username has to be at more than 4 and less than 20 characters";
                }
                else if (errors[0] == 3)
                {
                    registerPage.UsernameError.Text = $"The username can\'t contain special characters like \'space\',\',\",~,/...";
                }

                if (errors[1] == 1)
                {
                    registerPage.PasswordError.Text = $"The password has to be at more than 8 and less than 16 characters";
                }
                else if (errors[1] == 2)
                {
                    registerPage.PasswordError.Text = $"The password can\'t contain special characters like \'space\',\',\",~,/...";
                }
                if (errors[0] == 0 && errors[1] == 0)
                {
                    SQLOperations.CreatePlayer(registerPage.UsernameBox.Text, registerPage.PasswordBox.Password);
                    frame.Content = menu;

                    publish.Visibility = Visibility.Hidden;
                    publish.Content = "";

                    register.Visibility = Visibility.Hidden;
                    login.Visibility = Visibility.Hidden;

                    backToMenu.Visibility = Visibility.Hidden;

                    userText.Text = $"@{registerPage.UsernameBox.Text}";
                    usernameHolder.Visibility = Visibility.Visible;

                    registerPage.UsernameBox.Text = "";
                    registerPage.PasswordBox.Password = "";

                    scoreButton.Visibility = Visibility.Visible;
                }

            }

            else if (publish.Content == "LogIn") 
            {
                int[] errors = RegistAndLogIn.LoggingIn(registerPage.UsernameBox.Text, registerPage.PasswordBox.Password);
                if (errors[0] == 1 || errors[1] == 1)
                {
                    registerPage.UsernameError.Text = $"Your username or password dosn\'t match";
                    registerPage.PasswordError.Text = $"Your username or password dosn\'t match";
                }

                if (errors[0] == 0 && errors[1] == 0)
                {
                    frame.Content = menu;

                    publish.Visibility = Visibility.Hidden;
                    publish.Content = "";

                    register.Visibility = Visibility.Hidden;
                    login.Visibility = Visibility.Hidden;

                    backToMenu.Visibility = Visibility.Hidden;

                    userText.Text = $"@{registerPage.UsernameBox.Text}";
                    usernameHolder.Visibility = Visibility.Visible;

                    registerPage.UsernameBox.Text = "";
                    registerPage.PasswordBox.Password = "";

                    scoreButton.Visibility = Visibility.Visible;
                }
            }
        }

    }
}
