using System;
using System.Collections.Generic;
using System.Text;
using JeuDlaVie.Objects;

namespace JeuDlaVie
{
    interface IModel
    {
        public void loadGrid();

        public void changeFile(string path);

        public List<Cell> getGrid();

        public void calculNextGen();

        public void resetGrid();

        public void threadingCalcul(int i);
    }
}
