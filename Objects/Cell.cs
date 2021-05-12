using System;
using System.Collections.Generic;
using System.Text;
using JeuDlaVie.Enum;
using System.Windows.Shapes;
using System.Windows.Media;


namespace JeuDlaVie.Objects
{
    class Cell
    {
        public Enums.State State { get; set; }


        public Cell(Enums.State s)
        {
            this.State = s;
        }
    }
}
