using System;
using System.Collections.Generic;
using System.Text;

namespace FinderApp.Class
{
    class User
    {
        private ConsoleColor _color;
        private double latitude;
        private double longitude;
        private int _id;

        public User(ConsoleColor color, double latitude, double longitude, int id)
        {
            this._color = color;
            this.latitude = latitude;
            this.longitude = longitude;
            this._id = id;
        }

        public int GetId()
        {
            return _id;
        }

        public void UpdatePosition(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public double[] GetPosition()
        {
            return new[] {this.latitude, this.longitude };
        }

        public ConsoleColor GetColor()
        {
            return this._color;
        }
    }
}
