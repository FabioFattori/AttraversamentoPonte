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
        int finePontePerStradaSinistra = 700;
        int finePontePerStradaDestra = 487;
        int altezzaInMainWindowDelPonte = 200;
        int altezzaPuntoDiArrivo = 250;
        int puntoDiArrivoPerStradaDestra = 123;
        int puntoDiArrivoPerStradaSinistra = 1000;
        int altezzaMacchinaADestra;
        int distanzaDaDestraMacchinaADestra;
        int altezzaMacchinaASinistra;
        int distanzaDaDestraMacchinaSinistra;

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
                    altezzaMacchinaADestra = (int)Immagine_Nuova_Destra.Margin.Top;
                    distanzaDaDestraMacchinaADestra = (int)Immagine_Nuova_Destra.Margin.Left;

                }));
                IniziaMovimentoMacchinaDestra();

            }
            else
            {
                Macchina nuova = new Macchina(stradaSinistra);
                stradaSinistra.MacchineInStrada.Add(nuova);
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Nuova_Sinistra.Margin = new Thickness(388, 168, 0, 0);
                }));

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    altezzaMacchinaASinistra = (int)Immagine_Nuova_Sinistra.Margin.Top;
                    distanzaDaDestraMacchinaSinistra = (int)Immagine_Nuova_Sinistra.Margin.Left;

                }));
                IniziaMovimentoMacchinaSinistra();
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
        public void IniziaMovimentoMacchinaDestra()
        {
            bool macchinaStaFerma = true;
            while (macchinaStaFerma)
            {
                if (ControlloSeSulPonteSonoPresentiMacchineProvenientiDaDestra() == true && stradaSinistra.MacchineInStrada.Count == 0)
                {

                    lock (x)
                    {
                        //ponte.NuovaMacchinaIniziaAttraversataDelPonte(altezzaMacchinaADestra);
                        //entro nel ponte
                        while (altezzaMacchinaADestra <= altezzaInMainWindowDelPonte - 15)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                            altezzaMacchinaADestra += r.Next(10, 16);
                            distanzaDaDestraMacchinaADestra -= r.Next(10, 16);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                            }));

                        }
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));
                        altezzaMacchinaADestra = altezzaInMainWindowDelPonte;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                        }));

                        //faccio il ponte
                        while (distanzaDaDestraMacchinaADestra >= finePontePerStradaDestra + 30)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                            distanzaDaDestraMacchinaADestra -= r.Next(20, 31);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                            }));

                        }
                        distanzaDaDestraMacchinaADestra = finePontePerStradaDestra;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));




                        //esco dal ponte
                        while (altezzaMacchinaADestra <= altezzaPuntoDiArrivo - 15)
                        {
                            altezzaMacchinaADestra += r.Next(5, 7);
                            distanzaDaDestraMacchinaADestra -= r.Next(20, 26);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                            }));
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                        }
                        altezzaMacchinaADestra = altezzaPuntoDiArrivo;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));

                        stradaDestra.MacchineInStrada.RemoveAt(0);

                        //finisco la tratta
                        while (distanzaDaDestraMacchinaADestra >= puntoDiArrivoPerStradaDestra + 30)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                            distanzaDaDestraMacchinaADestra -= r.Next(20, 31);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                            }));

                        }
                        distanzaDaDestraMacchinaADestra = puntoDiArrivoPerStradaDestra;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Immagine_Nuova_Destra.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                        }));
                        macchinaStaFerma = false;


                    }
                }
            }
        }

        public void IniziaMovimentoMacchinaSinistra()
        {
            bool macchinaStaFerma = true;
            while (macchinaStaFerma)
            {
                if (ControlloSeSulPonteSonoPresentiMacchineProvenientiDaSinistra() == true && stradaDestra.MacchineInStrada.Count == 0)
                {

                    lock (x)
                    {
                        //ponte.NuovaMacchinaIniziaAttraversataDelPonte(altezzaMacchinaADestra);
                        //entro nel ponte
                        while (altezzaMacchinaASinistra <= altezzaInMainWindowDelPonte - 15)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                            altezzaMacchinaASinistra += r.Next(10, 16);
                            distanzaDaDestraMacchinaSinistra += r.Next(10, 16);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                Immagine_Nuova_Sinistra.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                            }));

                        }
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));
                        altezzaMacchinaASinistra = altezzaInMainWindowDelPonte;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Immagine_Nuova_Sinistra.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                        }));

                        //faccio il ponte
                        while (distanzaDaDestraMacchinaSinistra <= finePontePerStradaSinistra + 30)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                            distanzaDaDestraMacchinaSinistra += r.Next(20, 31);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                Immagine_Nuova_Sinistra.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                            }));

                        }
                        distanzaDaDestraMacchinaSinistra = finePontePerStradaSinistra;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Immagine_Nuova_Sinistra.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));




                        //esco dal ponte
                        while (altezzaMacchinaASinistra <= altezzaPuntoDiArrivo - 15)
                        {
                            altezzaMacchinaASinistra += r.Next(5, 7);
                            distanzaDaDestraMacchinaSinistra += r.Next(20, 26);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                Immagine_Nuova_Sinistra.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                            }));
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                        }
                        altezzaMacchinaASinistra = altezzaPuntoDiArrivo;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Immagine_Nuova_Sinistra.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));

                        stradaSinistra.MacchineInStrada.RemoveAt(0);

                        //finisco la tratta
                        while (distanzaDaDestraMacchinaSinistra >= puntoDiArrivoPerStradaSinistra + 30)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                            distanzaDaDestraMacchinaSinistra += r.Next(20, 31);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                Immagine_Nuova_Sinistra.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                            }));

                        }
                        distanzaDaDestraMacchinaSinistra = puntoDiArrivoPerStradaSinistra;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Immagine_Nuova_Sinistra.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                        }));
                        macchinaStaFerma = false;


                    }
                }
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

        public bool ControlloSeSulPonteSonoPresentiMacchineProvenientiDaSinistra()
        {
            bool sonoPresenti = true;
            try
            {
                foreach (Macchina m in ponte.MacchineSulPonte)
                {
                    if (m.StradaDiPartenza.LatoDelPonteDoveSiTrovaLaStrada != "sinistra")
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

