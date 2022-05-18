﻿using System.Windows;
using System.Windows.Controls;

namespace WPFDungeon
{
    internal class MoveCheck
    {
        public Rect C1 { get; }
        public Rect C2 { get; }
        public MoveCheck(char faceing,IBody body)
        {
            //distance between the hitbox and the checker
            double dis = 2;

            //checker width
            double w = 2;

            //checker height
            double h = 2;

            if (faceing == 'T')
            {
                C1 = new Rect(Canvas.GetLeft(body.Mesh), Canvas.GetTop(body.Mesh) - h - dis,w,h);

                C2 = new Rect(Canvas.GetLeft(body.Mesh) + body.Mesh.Width - w, Canvas.GetTop(body.Mesh) - dis - h, w, h);
            }
            else if (faceing == 'B')
            {
                C1 = new Rect(Canvas.GetLeft(body.Mesh), Canvas.GetTop(body.Mesh) + body.Mesh.Height + dis, w, h);

                C2 = new Rect(Canvas.GetLeft(body.Mesh) + body.Mesh.Width - w, Canvas.GetTop(body.Mesh) + body.Mesh.Height + dis, w, h);
            }
            else if (faceing == 'L')
            {
                C1 = new Rect(Canvas.GetLeft(body.Mesh) - dis - w, Canvas.GetTop(body.Mesh),w,h);

                C2 = new Rect(Canvas.GetLeft(body.Mesh) - dis - w, Canvas.GetTop(body.Mesh) + body.Mesh.Height - h, w, h);
            }
            else if (faceing == 'R')
            {
                C1 = new Rect(Canvas.GetLeft(body.Mesh) + body.Mesh.Width + dis, Canvas.GetTop(body.Mesh), w, h);

                C2 = new Rect(Canvas.GetLeft(body.Mesh) + body.Mesh.Width + dis, Canvas.GetTop(body.Mesh) + body.Mesh.Height - h, w, h);
            }
        }
        public bool Check(Rect hitbox)
        {
            return C1.IntersectsWith(hitbox) && C2.IntersectsWith(hitbox);
        }
        
    }
}
