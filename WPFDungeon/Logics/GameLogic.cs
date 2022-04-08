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
        public static void GameLoop(Game game,bool mUp,bool mDown,bool mLeft,bool mRight,double wWidth,double wHeight)
        {
            Rectangle body = game.Player.PlayerLooks.Body;

            if (mUp && game.Player.Location[1] > 0)
            {
                game.Player.FaceTo('T');
                game.Player.AddToLocation(0, -2);
                Canvas.SetTop(body, game.Player.Location[1]);
            }
            if (mDown && game.Player.Location[1] <  wHeight)
            {
                game.Player.FaceTo('B');
                game.Player.AddToLocation(0, 2);
                Canvas.SetTop(body, game.Player.Location[1]);
            }
            if (mLeft && game.Player.Location[0] > 0)
            {
                game.Player.FaceTo('L');
                game.Player.AddToLocation(-2, 0);
                Canvas.SetLeft(body, game.Player.Location[0]);
            }
            if (mRight && game.Player.Location[0] < wWidth)
            {
                game.Player.FaceTo('R');
                game.Player.AddToLocation(2, 0);
                Canvas.SetLeft(body, game.Player.Location[0]);
            }

            if (game.Player.Bullets.Count > 0)
            {
                foreach (Bullet bullet in game.Player.Bullets)
                {
                    bullet.Navigate();
                    Canvas.SetTop(bullet.Hitbox, bullet.Location[0]);
                    Canvas.SetLeft(bullet.Hitbox,bullet.Location[1]);
                }
            }
        }

    }
}
