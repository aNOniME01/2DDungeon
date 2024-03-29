﻿using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDungeon
{
    internal class Door
    {
        public double X { get; set; }
        public double[] Location { get; private set; }
        public Direction Facing { get; private set; }
        public int RoomId { get; private set; }
        public int Id { get; private set; }

        /// <summary>
        /// Door constructor
        /// </summary>
        /// <param name="x">Distance from the starting point (in one axies)</param>
        /// <param name="faceing">The direction witch the door is faceing</param>
        /// <param name="rHeight">Room height</param>
        /// <param name="rWidth">Room width</param>
        public Door(double x, Direction faceing, double rHeight, double rWidth,int roomId,int id)
        {
            RoomId = roomId;
            Id = id;

            X = x;

            Location = new double[2];
            
            SetLocRot(x, faceing, rHeight, rWidth);
        }

        /// <summary>
        /// Duplicates door without a reference
        /// </summary>
        /// <param name="door">The door thats going to be duplicated</param>
        public Door(Door door)
        {
            RoomId = door.RoomId;
            Id = door.Id;

            X = door.X;
            Location = new double[2];

            Location[0] = door.Location[0];
            Location[1] = door.Location[1];

            Facing = door.Facing;
        }

        public void SetLocRot(double x, Direction faceing, double rHeight, double rWidth)
        {
            Facing = faceing;
            if (faceing == Direction.Top)
            {
                Location[0] = 0;
                Location[1] = x;
            }
            else if (faceing == Direction.Bottom)
            {
                Location[0] = rHeight;
                Location[1] = x;
            }
            else if (faceing == Direction.Left)
            {
                Location[0] = x;
                Location[1] = 0;
            }
            else
            {
                Location[0] = x;
                Location[1] = rWidth;
            }
        }

        /// <summary>
        /// Modifies the hallway location
        /// </summary>
        /// <param name="length">Hallway length</param>
        public void ModifyLocation(double length)
        {
            if (this.Facing == Direction.Top) this.Location[0] -= length;
            else if (this.Facing == Direction.Bottom) this.Location[0] += length;
            else if (this.Facing == Direction.Left) this.Location[1] -= length;
            else this.Location[1] += length;
        }

        public void ToRoomLocation(double[] roomLoc)
        {
            Location[0] += roomLoc[0];
            Location[1] += roomLoc[1];
        }
    }
}
