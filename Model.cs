using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using JeuDlaVie.Objects;
using JeuDlaVie.Enum;
using System.Threading;
using System.Threading.Tasks;

namespace JeuDlaVie
{
    class Model : IModel
    {
        private List<Cell> grid = new List<Cell>();
        private List<Cell> oldGrid = new List<Cell>();
        private char empty = '0';
        private char alive = '1';

        private string path = @"E:\Cours\rattrapage C#\JeuDlaVie\grid.txt";


        public int height { get; set; }
        public int width { get; set; }

        public Model()
        {
            loadGrid();
        }


        public void loadGrid()
        {
            if(File.Exists(path))
            {
                string[] readText = File.ReadAllLines(path);

                string[] taille = readText[0].Split(',');

                this.height = Int32.Parse(taille[0]);
                this.width = Int32.Parse(taille[1]);

                this.grid = new List<Cell>();

                for(int i = 1; i <= this.height; i++)
                {
                    for(int o = 0; o < this.width; o++) 
                    {
                        if (readText[i][o] == this.alive)
                        {
                            this.grid.Add(new Cell(Enums.State.Alive));
                        }
                        else if (readText[i][o] == this.empty)
                        {
                            this.grid.Add(new Cell(Enums.State.Dead));
                        }
                    }
                }
            }
        }

        public void changeFile(string path)
        {
            this.path = path;

            loadGrid();
        }

        public List<Cell> getGrid()
        {
            return this.grid;
        }

        public void calculNextGen()
        {
            this.oldGrid = new List<Cell>();

            foreach (Cell cell in this.grid)
            {
                this.oldGrid.Add(new Cell(cell.State));
            }

            List<Task> tasks = new List<Task>();

            for (int i = 0; i < (this.width * this.height); i++)
            {
                int a = i;
                Task calcul = Task.Run(() => threadingCalcul(a));
                tasks.Add(calcul);
            }

            Task.WaitAll(tasks.ToArray());
        }

        public void threadingCalcul(int i)
        {
            
            //check de chaque cellule pour voir son état
            int count = 0;

            int[] voisinPos = { -(this.width + 1), -this.width, -(this.width - 1), -1, 1, (this.width - 1), this.width, (this.width + 1) };

            foreach (int pos in voisinPos)
            {
                int cellInPos = i + pos;
                if (cellInPos >= 0 && cellInPos < (this.width * this.height))
                {
                    if ((pos == -1 || pos == (this.width - 1) || pos == -(this.width + 1)) && (i + 1) % this.width != 1)
                    { 
                        if (this.oldGrid[cellInPos].State == Enums.State.Alive)
                        {
                            count++;
                        }
                    }
                    else if ((pos == 1 || pos == -(this.width - 1) || pos == (this.width + 1)) && (i + 1) % this.width != 0)
                    {
                        if (this.oldGrid[cellInPos].State == Enums.State.Alive)
                        {
                            count++;
                        }
                    }
                    else if(pos == this.width || pos == -this.width)
                    {
                        if (this.oldGrid[cellInPos].State == Enums.State.Alive)
                        {
                            count++;
                        }
                    }
                }
            }

            if (count == 3)
            {
                this.grid[i].State = Enums.State.Alive;
            }
            else if(count != 2)
            {
                this.grid[i].State = Enums.State.Dead;
            }
        }

        public void resetGrid()
        {
            this.grid = new List<Cell>();

            this.loadGrid();
        }
    }
}
