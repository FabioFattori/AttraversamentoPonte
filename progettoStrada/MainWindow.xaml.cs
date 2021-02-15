using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Drawing;





namespace progettoStrada
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Strada stradaDestra;
        Strada stradaSinistra;
        public MainWindow()
        {
            InitializeComponent();
            stradaDestra = new Strada("destra");
            stradaSinistra = new Strada("sinistra");
        }

        

        private void SpawnMacchinaInStradaDestra_Click(object sender, RoutedEventArgs e)
        {
            Thread nuovaMacchina = new Thread(new ThreadStart(SpawnMacchinaDestra));
            nuovaMacchina.Start();
        }

        public void SpawnMacchinaDestra()
        {
            CreaNuovaMacchina("destra");
        }

        public void CreaNuovaMacchina(string lato)
        {
            
            if (lato == "destra")
            {
                Macchina nuova = new Macchina(stradaDestra);
                stradaDestra.MacchineInStrada.Add(nuova);
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Nuova_Destra.Margin = new Thickness(625, 170, 0, 0);
                }));
                
            }
            else
            {
                Macchina nuova = new Macchina(stradaSinistra);
                stradaSinistra.MacchineInStrada.Add(nuova);
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                   Immagine_Nuova_Sinistra.Margin = new Thickness(10, 239, 0, 0);
                }));
            }
        }

        private void SpawnMacchinaInStradaSinistra_Click(object sender, RoutedEventArgs e)
        {
            Thread nuovaMacchina = new Thread(new ThreadStart(SpawnMacchinaSinistra));
            nuovaMacchina.Start();
        }

        public void SpawnMacchinaSinistra()
        {
            CreaNuovaMacchina("sinistra");
        }

        /*metodo per far muovere le immagini
         * this.Dispatcher.BeginInvoke(new Action(() =>//scriviamo cosi perchè il wpf è gestito da thread quindi vanno in conflitto:action è un delegato che risolve il conflitto
                {
            


        }));
        */
    }
}
