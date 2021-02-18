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
        //valori usati per il movimento:
        int arrivoAlSemaforoPerSinistra = 388;
        int arrivoAlSemaforoPerDestra = 740;
        int finePontePerStradaSinistra = 700;
        int finePontePerStradaDestra = 500;
        int altezzaInMainWindowDelPonte = 200;
        int altezzaPuntoDiArrivo = 250;
        int altezzaMacchinaADestra;
        int distanzaDaDestraMacchinaADestra;
        int altezzaMacchinaASinistra;
        int distanzaDaDestraMacchinaSinistra;
        int controlloSeSiInloopa;
        System.Windows.Controls.Image immagineInMovimento;
        System.Windows.Controls.Image immagineInMovimento1;


        ///

        int distanzaDaDestraPerSpawnDelleMacchineInStradaDestra = 1130;
        int distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra = -100;
        int altezzaPerSpawnMacchine = 160;

        public MainWindow()
        {
            InitializeComponent();
            x = new object();
            stradaDestra = new Strada("destra");
            stradaSinistra = new Strada("sinistra");
            ponte = new Ponte();
            r = new Random();

            Thread t2 = new Thread(new ThreadStart(SpawnInizialeDiMacchine));
            t2.Start();

        }
        /*
        public void SpawnCasualeDiMacchine()
        {
            SpawnInizialeDiMacchine();
            while (true)
            {
                //int attesa=r.Next()
            }
        }
        */
        public void SpawnInizialeDiMacchine()
        {
            int numeroMacchineDaSpawnareInStradaDestra = 1;

            if (numeroMacchineDaSpawnareInStradaDestra == 1)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Destra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                }));


                //metodo per far arrivare al semaforo

                Thread t3 = new Thread(new ThreadStart(MacchinaNuovaSpawnataARandomDestra));
                t3.Start();
                immagineInMovimento = Immagine_Destra_0;
                Thread.Sleep(TimeSpan.FromMilliseconds(450));

            }


            int numeroMacchineDaSpawnareInStradaSinistra = 1;

            if (numeroMacchineDaSpawnareInStradaSinistra == 1)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Sinistra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                }));

                //metodo per far arrivare al semaforo
                Thread.Sleep(TimeSpan.FromMilliseconds(400));
                immagineInMovimento1 = Immagine_Sinistra_0;
                Thread t4 = new Thread(new ThreadStart(MacchinaNuovaSpawnataARandomSinistra));
                t4.Start();
            }

        }


        public void MacchinaNuovaSpawnataARandomDestra()
        {
            //arrivoAlSemaforoPerDestra += 60;

            MovimentoPerArrivareASemaforo("D", immagineInMovimento);
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                altezzaMacchinaADestra = (int)immagineInMovimento.Margin.Top;
            }));

            Macchina nuova = new Macchina(stradaDestra);
            stradaDestra.MacchineInStrada.Add(nuova);

            IniziaMovimentoMacchinaDestra(immagineInMovimento);

        }
        public void MacchinaNuovaSpawnataARandomSinistra()
        {
            //arrivoAlSemaforoPerDestra += 60;

            MovimentoPerArrivareASemaforo("S", immagineInMovimento1);
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                altezzaMacchinaADestra = (int)immagineInMovimento1.Margin.Top;
            }));


            Macchina nuova = new Macchina(stradaSinistra);
            stradaSinistra.MacchineInStrada.Add(nuova);
            IniziaMovimentoMacchinaSinistra(immagineInMovimento1);

        }


        public void MovimentoPerArrivareASemaforo(string lato, System.Windows.Controls.Image immagine)
        {
            bool arrivata = false;
            while (!arrivata)
            {
                if (lato == "D")
                {

                    distanzaDaDestraMacchinaADestra = distanzaDaDestraPerSpawnDelleMacchineInStradaDestra;
                    int i = arrivoAlSemaforoPerDestra + 60;

                    while (distanzaDaDestraMacchinaADestra >= i)
                    {

                        distanzaDaDestraMacchinaADestra -= r.Next(2, 5);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagine.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaPerSpawnMacchine, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));
                    }

                    distanzaDaDestraMacchinaADestra = i;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        immagine.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaPerSpawnMacchine, 0, 0);
                    }));
                    Thread.Sleep(TimeSpan.FromMilliseconds(10));


                    //arrivoAlSemaforoPerDestra += 60;
                    arrivata = true;
                }
                else
                {
                    distanzaDaDestraMacchinaSinistra = distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra;
                    while (distanzaDaDestraMacchinaSinistra <= arrivoAlSemaforoPerSinistra - 4)
                    {
                        distanzaDaDestraMacchinaSinistra += r.Next(2, 5);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagine.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaPuntoDiArrivo, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));
                    }
                    distanzaDaDestraMacchinaSinistra = arrivoAlSemaforoPerSinistra;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        immagine.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaPuntoDiArrivo, 0, 0);
                    }));
                    Thread.Sleep(TimeSpan.FromMilliseconds(10));

                    arrivoAlSemaforoPerSinistra -= 60;
                    arrivata = true;
                }
            }
        }

        private void SpawnMacchinaInStradaDestra_Click(object sender, RoutedEventArgs e)
        {

            arrivoAlSemaforoPerDestra = 800;
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
                MovimentoPerArrivareASemaforo("D", Immagine_Nuova_Destra);
                Macchina nuova = new Macchina(stradaDestra);
                stradaDestra.MacchineInStrada.Add(nuova);
                arrivoAlSemaforoPerDestra += 60;
                Thread.Sleep(TimeSpan.FromMilliseconds(200));

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    altezzaMacchinaADestra = (int)Immagine_Nuova_Destra.Margin.Top;
                    distanzaDaDestraMacchinaADestra = (int)Immagine_Nuova_Destra.Margin.Left;

                }));
                IniziaMovimentoMacchinaDestra(Immagine_Nuova_Destra);


            }
            else
            {

                MovimentoPerArrivareASemaforo("S", Immagine_Nuova_Sinistra);
                Macchina nuova = new Macchina(stradaSinistra);
                stradaSinistra.MacchineInStrada.Add(nuova);
                arrivoAlSemaforoPerSinistra += 60;

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    altezzaMacchinaASinistra = (int)Immagine_Nuova_Sinistra.Margin.Top;
                    distanzaDaDestraMacchinaSinistra = (int)Immagine_Nuova_Sinistra.Margin.Left;

                }));
                IniziaMovimentoMacchinaSinistra(Immagine_Nuova_Sinistra);

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


        public void IniziaMovimentoMacchinaDestra(System.Windows.Controls.Image immagineDaMuovere)
        {
            controlloSeSiInloopa = 0;
            bool macchinaStaFerma = true;
            while (macchinaStaFerma)
            {
                controlloSeSiInloopa++;
                if (ControlloSeSulPonteSonoPresentiMacchineProvenientiDaDestra() == true && stradaSinistra.MacchineInStrada.Count == 0)
                {

                    lock (x)
                    {
                        //ponte.NuovaMacchinaIniziaAttraversataDelPonte(altezzaMacchinaADestra);
                        //entro nel ponte
                        while (altezzaMacchinaADestra <= altezzaInMainWindowDelPonte - 2)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                            altezzaMacchinaADestra += r.Next(1, 3);
                            distanzaDaDestraMacchinaADestra -= r.Next(2, 5);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                            }));

                        }
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));
                        altezzaMacchinaADestra = altezzaInMainWindowDelPonte;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                        }));

                        //faccio il ponte
                        while (distanzaDaDestraMacchinaADestra >= finePontePerStradaDestra + 4)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                            distanzaDaDestraMacchinaADestra -= r.Next(2, 5);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, 200, 0, 0);
                            }));

                        }
                        distanzaDaDestraMacchinaADestra = finePontePerStradaDestra;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, 200, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));


                    }
                    stradaDestra.MacchineInStrada.RemoveAt(0);
                    altezzaMacchinaADestra = 200;
                    //esco dal ponte
                    while (altezzaMacchinaADestra >= altezzaPerSpawnMacchine - 2)
                    {
                        altezzaMacchinaADestra -= r.Next(1, 3);
                        distanzaDaDestraMacchinaADestra -= r.Next(2, 5);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));
                    }

                    altezzaMacchinaADestra = altezzaPerSpawnMacchine;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                    }));
                    Thread.Sleep(TimeSpan.FromMilliseconds(10));
                    macchinaStaFerma = false;




                    //finisco la tratta
                    while (distanzaDaDestraMacchinaADestra >= distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra + 4)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));
                        distanzaDaDestraMacchinaADestra -= r.Next(2, 5);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                        }));

                    }
                    distanzaDaDestraMacchinaADestra = distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra + 1;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                    }));



                }
                if (controlloSeSiInloopa == 4)
                {
                    stradaSinistra.MacchineInStrada.RemoveAt(0);
                }
            }
        }

        public void IniziaMovimentoMacchinaSinistra(System.Windows.Controls.Image immagineDaMuovere)
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
                        altezzaMacchinaASinistra = altezzaPuntoDiArrivo;

                        while (altezzaMacchinaASinistra >= altezzaInMainWindowDelPonte + 2)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                            altezzaMacchinaASinistra -= r.Next(1, 3);
                            distanzaDaDestraMacchinaSinistra += r.Next(2, 5);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                            }));

                        }
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));
                        altezzaMacchinaASinistra = altezzaInMainWindowDelPonte;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                        }));

                        //faccio il ponte
                        while (distanzaDaDestraMacchinaSinistra <= finePontePerStradaSinistra - 4)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                            distanzaDaDestraMacchinaSinistra += r.Next(2, 5);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                            }));

                        }
                        distanzaDaDestraMacchinaSinistra = finePontePerStradaSinistra;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));



                        try
                        {
                            if (controlloSeSiInloopa != 4)
                                stradaSinistra.MacchineInStrada.RemoveAt(0);
                            //esco dal ponte
                            while (altezzaMacchinaASinistra <= altezzaPuntoDiArrivo - 2)
                            {
                                altezzaMacchinaASinistra += r.Next(1, 3);
                                distanzaDaDestraMacchinaSinistra += r.Next(2, 5);
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                                }));
                                Thread.Sleep(TimeSpan.FromMilliseconds(10));
                            }
                        }
                        catch (Exception)
                        {
                            //esco dal ponte
                            while (altezzaMacchinaASinistra <= altezzaPuntoDiArrivo - 2)
                            {
                                altezzaMacchinaASinistra += r.Next(1, 3);
                                distanzaDaDestraMacchinaSinistra += r.Next(2, 5);
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                                }));
                                Thread.Sleep(TimeSpan.FromMilliseconds(10));
                            }
                        }
                        //esco dal ponte
                        while (altezzaMacchinaASinistra <= altezzaPuntoDiArrivo - 2)
                        {
                            altezzaMacchinaASinistra += r.Next(1, 3);
                            distanzaDaDestraMacchinaSinistra += r.Next(2, 5);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                            }));
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                        }
                    }

                    altezzaMacchinaASinistra = altezzaPuntoDiArrivo;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                    }));

                    Thread.Sleep(TimeSpan.FromMilliseconds(10));
                    macchinaStaFerma = false;


                    //finisco la tratta
                    while (distanzaDaDestraMacchinaSinistra <= distanzaDaDestraPerSpawnDelleMacchineInStradaDestra + 4)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));
                        distanzaDaDestraMacchinaSinistra += r.Next(2, 5);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                        }));

                    }
                    distanzaDaDestraMacchinaSinistra = distanzaDaDestraPerSpawnDelleMacchineInStradaDestra + 1;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                    }));




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

