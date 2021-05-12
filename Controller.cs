using System;
using System.Collections.Generic;
using System.Text;
using JeuDlaVie.Objects;

namespace JeuDlaVie
{
    class Controller : IController
    {
        private static Controller _instance;
        
        public Model model { get; set; }

        public static Controller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Controller();
                }
                return _instance;
            }
        }

        private Controller()
        {
            this.model = new Model();
        }

        
        public List<Cell> getGrid()
        {
            return this.model.getGrid();
        }

        public void calculNextGen()
        {
            this.model.calculNextGen();
        }

        public void resetGrid()
        {
            this.model.resetGrid();
        }

        public int[] getTaille()
        {
            int[] values = { this.model.height, this.model.width };

            return values;
        }

        public void changeFile(string path)
        {
            this.model.changeFile(path);
        }
    }
}
