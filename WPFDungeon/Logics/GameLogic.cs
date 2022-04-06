using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class GameLogic
    {
        public static void GameLoop(Player player,bool mUp,bool mDown,bool mLeft,bool mRight,double wWidth,double wHeight)
        {
            if (mUp && player.Location[1] > 0)
            {
                player.FaceTo('T');
                player.AddToLocation(0, -5);
                Canvas.SetTop(player.playerLooks.Body, player.Location[1]);
            }
            if (mDown && player.Location[1] <  wHeight)
            {
                player.FaceTo('B');
                player.AddToLocation(0, 5);
                Canvas.SetTop(player.playerLooks.Body, player.Location[1]);
            }
            if (mLeft && player.Location[0] > 0)
            {
                player.FaceTo('L');
                player.AddToLocation(-5, 0);
                Canvas.SetLeft(player.playerLooks.Body, player.Location[0]);
            }
            if (mRight && player.Location[0] < wWidth)
            {
                player.FaceTo('R');
                player.AddToLocation(5, 0);
                Canvas.SetLeft(player.playerLooks.Body, player.Location[0]);
            }
        }

    }
}
