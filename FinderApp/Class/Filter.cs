using System;
using System.Collections.Generic;
using System.Text;

namespace FinderApp.Class
{
    class Filter
    {
        private string _name;

        public Filter(string name)
        {
            this._name = name;
        }

        public string GetName()
        {
            return _name;
        }
    }
}
