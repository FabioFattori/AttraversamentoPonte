using System;
using System.Threading;
using System.Windows;





namespace progettoStrada
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Strada stradaDestra;
        Strada stradaSinistra;
        Ponte ponte;
        Random r;
        object x;
        int finePonte = 487;
        int altezzaInMainWindowDelPonte = 200;
        int altezzaPuntoDiArrivo = 250;
        int puntoDiArrivoPerStradaDestra = 123;
        public MainWindow()
        {
            InitializeComponent();
            x = new object();
            stradaDestra = new Strada("destra");
            stradaSinistra = new Strada("sinistra");
            ponte = new Ponte();
            r = new Random();
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
                    Immagine_Nuova_Destra.Margin = new Thickness(800, 168, 0, 0);
                }));
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    IniziaMovimentoMacchinaDestra((int)Immagine_Nuova_Destra.Margin.Top, (int)Immagine_Nuova_Destra.Margin.Left, nuova);
                }));


            }
            else
            {
                Macchina nuova = new Macchina(stradaSinistra);
                stradaSinistra.MacchineInStrada.Add(nuova);
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Nuova_Sinistra.Margin = new Thickness(388, 168, 0, 0);
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

        /* public void IniziaMovimento()
         {
             IniziaMovimentoMacchinaDestra((int)Immagine_Nuova_Destra.Margin.Top,(int) Immagine_Nuova_Destra.Margin.Left);
         }
        */
        public void IniziaMovimentoMacchinaDestra(int altezzaMacchina, int distanzaDaLatoDestro, Macchina macchina)
        {
            if (ControlloSeSulPonteSonoPresentiMacchineProvenientiDaDestra()==true && stradaSinistra.MacchineInStrada.Count == 0)
            {

                lock (x)
                {
                    ponte.NuovaMacchinaIniziaAttraversataDelPonte(macchina);
                    //entro nel ponte
                    while (altezzaMacchina <= altezzaInMainWindowDelPonte - 15)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(200));
                        altezzaMacchina = r.Next(10, 16);
                        distanzaDaLatoDestro -= r.Next(10, 16);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaLatoDestro, altezzaMacchina, 0, 0);
                        }));

                    }
                    Thread.Sleep(TimeSpan.FromMilliseconds(200));
                    altezzaMacchina = altezzaInMainWindowDelPonte;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaLatoDestro, altezzaMacchina, 0, 0);
                    }));
                }

                lock (x)
                {
                    //faccio il ponte
                    while (distanzaDaLatoDestro <= finePonte + 30)
                    {
                        distanzaDaLatoDestro += r.Next(20, 31);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaLatoDestro, altezzaMacchina, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(200));
                    }
                    distanzaDaLatoDestro = finePonte;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaLatoDestro, altezzaMacchina, 0, 0);
                    }));
                    Thread.Sleep(TimeSpan.FromMilliseconds(200));

                }
                /*
                
                //esco dal ponte
                while (altezzaMacchina <= altezzaPuntoDiArrivo - 15)
                {
                    altezzaMacchina = altezzaMacchina + r.Next(5, 7);
                    distanzaDaLatoDestro = distanzaDaLatoDestro - r.Next(20, 26);
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaLatoDestro, altezzaMacchina, 0, 0);
                    }));
                    Thread.Sleep(TimeSpan.FromMilliseconds(200));
                }
                altezzaMacchina = altezzaInMainWindowDelPonte;
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaLatoDestro, altezzaMacchina, 0, 0);
                }));
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                */

            }
        }

        public bool ControlloSeSulPonteSonoPresentiMacchineProvenientiDaDestra()
        {
            bool sonoPresenti = true;
            try
            {
                foreach (Macchina m in ponte.MacchineSulPonte)
                {
                    if (m.StradaDiPartenza.LatoDelPonteDoveSiTrovaLaStrada != "destra")
                    {
                        sonoPresenti = false;
                    }
                }
            }
            catch (Exception)
            {
                return sonoPresenti;
            }

            return sonoPresenti;
        }
        /*metodo per far muovere le immagini
         * this.Dispatcher.BeginInvoke(new Action(() =>
         {
            


        }));
        */
    }
}

