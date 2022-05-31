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

namespace WPFDungeon
{
    /// <summary>
    /// Interaction logic for ScoreboardPage.xaml
    /// </summary>
    public partial class ScoreboardPage : Page
    {
        private bool firstLoaded;
        public ScoreboardPage()
        {
            InitializeComponent();
            firstLoaded = true;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (firstLoaded)
            {
                List<int[]> scores = SQLOperations.ReadInScores();

                foreach (int[] score in scores)
                {
                    CreateScoreTab(score[0], score[1]);
                }
                firstLoaded = false;
            }
        }
        private void CreateScoreTab(int userId, int score)
        {
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            stack.Margin = Stack.Margin;

            Border userNameBorder = new Border();
            userNameBorder.Style = UserBorder.Style;
            userNameBorder.Width = UserBorder.Width;

            TextBlock userNameText = new TextBlock();
            userNameText.Text = SQLOperations.GetUserById(userId);
            userNameText.Style = UserText.Style;

            userNameBorder.Child = userNameText;
            stack.Children.Add(userNameBorder);


            Border scoreBorder = new Border();
            scoreBorder.Style = ScoreBorder.Style;
            scoreBorder.Width = ScoreBorder.Width;
            scoreBorder.Margin = ScoreBorder.Margin;

            TextBlock scoreText = new TextBlock();
            scoreText.Text = Convert.ToString(score);
            scoreText.Style = ScoreText.Style;

            scoreBorder.Child = scoreText;
            stack.Children.Add(scoreBorder);

            scoreboard.Children.Add(stack);

        }
    }
}
