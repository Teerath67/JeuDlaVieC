using System;
using System.Collections.Generic;
using System.Text;

namespace JeuDlaVie.Objects
{
    class DataGrid
    {
        public int Rows { get; set; }

        public int Columns { get; set; }

        public DataGrid(int width, int height)
        {
            this.Rows = height;

            this.Columns = width;
        }
    }
}
