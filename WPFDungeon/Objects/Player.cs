﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDungeon
{
    internal class Player
    {
        public double[] Location { get; private set; }
        public double Height { get; private set; }
        public double Width { get; private set; }
        public char Faceing { get; private set; }
        public PlayerLooks PlayerLooks { get; private set; }
        public List<Bullet> Bullets { get; private set; }
        public Player(double height, double width)
        {
            Location = new double[2];
            Location[0] = width / 2;
            Location[1] = height / 2;

            Width = 10;
            Height = 10;

            Faceing = 'T';//T,B,L,F

            Bullets = new List<Bullet>();

            PlayerLooks = new PlayerLooks(Height, Width);
        }
        public void AddToLocation(double x, double y)
        {
            Location[0] += x;
            Location[1] += y;
        }
        public void FaceTo(char direction)
        {
            Faceing = direction;
            PlayerLooks.FaceTo(direction);
        }
        public void Shoot()
        {
            Bullets.Add(new Bullet("pB", Location[0] + (Height / 2), Location[1] + (Width / 2) - 1, Faceing));
        }
    }
}