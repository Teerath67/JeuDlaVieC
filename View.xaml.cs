using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using JeuDlaVie.Objects;
using JeuDlaVie.Enum;
using System.Windows.Threading;
using Microsoft.Win32;

namespace JeuDlaVie.View
{
    /// <summary>
    /// Logique d'interaction pour View.xaml
    /// </summary>
    public partial class View : Window
    {
        private DispatcherTimer gameThread = new DispatcherTimer();

        public View()
        {
            InitializeComponent();

            DataContext = this;
            
            this.gameThread.Tick += new EventHandler(refresh);
            this.gameThread.Interval = TimeSpan.FromSeconds(0.5);

            int[] taille = Controller.Instance.getTaille();

            Objects.DataGrid dg = new Objects.DataGrid(taille[1], taille[0]);

            ic.DataContext = dg;

            afficher();
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            this.gameThread.Start();
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            this.gameThread.Stop();
        }
        private void Button_Reset(object sender, RoutedEventArgs e)
        {
            Controller.Instance.resetGrid();

            afficher();
        }
        private void Button_Next(object sender, RoutedEventArgs e)
        {
            Controller.Instance.calculNextGen();

            afficher();
        }


        private void Button_Import(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var filePath = string.Empty;

            openFileDialog.ShowDialog();

           
            if (openFileDialog.FileName != null)
            {
                filePath = openFileDialog.FileName;

                Controller.Instance.changeFile(filePath);
            }

            int[] taille = Controller.Instance.getTaille();

            Objects.DataGrid dg = new Objects.DataGrid(taille[1], taille[0]);

            ic.DataContext = dg;

            afficher();
        }
        

    private void afficher()
        {
            List<Cell> cellGrid = Controller.Instance.getGrid();

            List<Rectangle> recGrid = new List<Rectangle>();

            foreach (Cell cell in cellGrid)
            {
                Rectangle rec = new Rectangle();
                rec.Width = 2000;
                rec.Height = 2000;

                if (cell.State == Enums.State.Alive)
                {
                    rec.Fill = Brushes.Black;
                }
                else
                {
                    rec.Fill = Brushes.White;
                }

                recGrid.Add(rec);
            }

            ic.ItemsSource = recGrid;
        }

        private void refresh(object sender, EventArgs e)
        {
            afficher();

            Controller.Instance.calculNextGen();
        }
    }
}