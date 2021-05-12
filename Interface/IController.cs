using System;
using System.Collections.Generic;
using System.Text;
using JeuDlaVie.Objects;

namespace JeuDlaVie
{
    interface IController
    {
        public List<Cell> getGrid();

        public void calculNextGen();

        public void resetGrid();

        public int[] getTaille();

        public void changeFile(string path);
    }
}
