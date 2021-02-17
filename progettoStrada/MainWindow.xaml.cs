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
        int arrivoAlSemaforoPerDestra = 800;
        int finePontePerStradaSinistra = 700;
        int finePontePerStradaDestra = 500;
        int altezzaInMainWindowDelPonte = 200;
        int altezzaPuntoDiArrivo = 250;
        int puntoDiArrivoPerStradaDestra = 123;
        int puntoDiArrivoPerStradaSinistra = 1000;
        int altezzaMacchinaADestra;
        int distanzaDaDestraMacchinaADestra;
        int altezzaMacchinaASinistra;
        int distanzaDaDestraMacchinaSinistra;
        
        ///

        int distanzaDaDestraPerSpawnDelleMacchineInStradaDestra = 1130;
        int distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra = -60;
        int altezzaPerSpawnMacchine = 174;

        public MainWindow()
        {
            InitializeComponent();
            x = new object();
            stradaDestra = new Strada("destra");
            stradaSinistra = new Strada("sinistra");
            ponte = new Ponte();
            r = new Random();
            
            Thread t2 = new Thread(new ThreadStart(SpawnInizialeDiMacchine));
            //t2.Start();
           
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
            int numeroMacchineDaSpawnareInStradaDestra = 2;

            if (numeroMacchineDaSpawnareInStradaDestra == 1)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Destra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                }));
                Macchina nuova = new Macchina(stradaDestra);
                stradaDestra.MacchineInStrada.Add(nuova);

                //metodo per far arrivare al semaforo
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_0);
                IniziaMovimentoMacchinaDestra(Immagine_Destra_0);
               
            }
            else if (numeroMacchineDaSpawnareInStradaDestra == 2)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Destra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Destra_1.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                }));

                //metto lo stesso oggetto macchina due volte nella lista dell'oggetto strada perchè non mi interressa che oggetto c'è nella lista, ma mi interessa che questo oggetto ci sia per poi toglierolo nel metodo IniziaMovimentoMacchinaDestra()
                Macchina nuova = new Macchina(stradaDestra);
                stradaDestra.MacchineInStrada.Add(nuova);
                stradaDestra.MacchineInStrada.Add(nuova);

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    altezzaMacchinaADestra = (int)Immagine_Destra_1.Margin.Top;
                    distanzaDaDestraMacchinaADestra = (int)Immagine_Destra_1.Margin.Left;
                }));
                //metodo per far arrivare al semaforo
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_0);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_1);
                IniziaMovimentoMacchinaDestra(Immagine_Destra_0);
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Destra_0.Margin = new Thickness(1100, 1000, 0, 0);
                }));
                arrivoAlSemaforoPerDestra -= 60;
                //questa parte non fa rifare lo spawn alla seconda macchina
                int i=distanzaDaDestraPerSpawnDelleMacchineInStradaDestra;
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    distanzaDaDestraPerSpawnDelleMacchineInStradaDestra = (int)Immagine_Destra_1.Margin.Top;
                }));
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_1);
                

                IniziaMovimentoMacchinaDestra(Immagine_Destra_1);
                distanzaDaDestraPerSpawnDelleMacchineInStradaDestra = i;


            }
            else if (numeroMacchineDaSpawnareInStradaDestra == 3)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Destra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Destra_1.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Destra_2.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                }));

                //metto lo stesso oggetto macchina due volte nella lista dell'oggetto strada perchè non mi interressa che oggetto c'è nella lista, ma mi interessa che questo oggetto ci sia per poi toglierolo nel metodo IniziaMovimentoMacchinaDestra()
                Macchina nuova = new Macchina(stradaDestra);
                stradaDestra.MacchineInStrada.Add(nuova);
                stradaDestra.MacchineInStrada.Add(nuova);
                stradaDestra.MacchineInStrada.Add(nuova);

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    altezzaMacchinaADestra = (int)Immagine_Destra_1.Margin.Top;
                    distanzaDaDestraMacchinaADestra = arrivoAlSemaforoPerDestra;
                }));

                //metodo per far arrivare al semaforo
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_0);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_1);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_2);
                IniziaMovimentoMacchinaDestra(Immagine_Destra_0);
                arrivoAlSemaforoPerDestra -= 60;
                
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_1);
                IniziaMovimentoMacchinaDestra(Immagine_Destra_1);
                arrivoAlSemaforoPerDestra -= 60;
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_2);
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    altezzaMacchinaADestra = (int)Immagine_Destra_2.Margin.Top;
                    distanzaDaDestraMacchinaADestra = arrivoAlSemaforoPerDestra;
                }));
                IniziaMovimentoMacchinaDestra(Immagine_Destra_2);
                
            }
            else if (numeroMacchineDaSpawnareInStradaDestra == 4)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Destra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Destra_1.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Destra_2.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Destra_3.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                }));
                //metodo per far arrivare al semaforo
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_0);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_1);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_2);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_3);
            }
            else if (numeroMacchineDaSpawnareInStradaDestra == 5)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Destra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Destra_1.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Destra_2.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Destra_3.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Destra_4.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                }));
                //metodo per far arrivare al semaforo
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_0);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_1);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_2);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_3);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("D", Immagine_Destra_4);
            }

            int numeroMacchineDaSpawnareInStradaSinistra = r.Next(0, 6);

            if (numeroMacchineDaSpawnareInStradaSinistra == 1)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Sinistra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                }));

                //metodo per far arrivare al semaforo
                MovimentoPerArrivareASemaforo("S", Immagine_Sinistra_0);
            }
            else if (numeroMacchineDaSpawnareInStradaSinistra == 2)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Sinistra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Sinistra_1.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                }));
                //metodo per far arrivare al semaforo
                MovimentoPerArrivareASemaforo("S", Immagine_Sinistra_0);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("S", Immagine_Sinistra_1);
            }
            else if (numeroMacchineDaSpawnareInStradaSinistra == 3)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Sinistra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Sinistra_1.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Sinistra_2.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                }));
                //metodo per far arrivare al semaforo
                MovimentoPerArrivareASemaforo("S", Immagine_Sinistra_0);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("S", Immagine_Sinistra_1);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("S", Immagine_Sinistra_2);
            }
            else if (numeroMacchineDaSpawnareInStradaSinistra == 4)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Sinistra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Sinistra_1.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Sinistra_2.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Sinistra_3.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                }));
                //metodo per far arrivare al semaforo
                MovimentoPerArrivareASemaforo("S", Immagine_Sinistra_0);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("S", Immagine_Sinistra_1);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("S", Immagine_Sinistra_2);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("S", Immagine_Sinistra_3);
            }
            else if (numeroMacchineDaSpawnareInStradaSinistra == 5)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Sinistra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Sinistra_1.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Sinistra_2.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Sinistra_3.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Sinistra_4.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                }));
                //metodo per far arrivare al semaforo
                MovimentoPerArrivareASemaforo("sinistra", Immagine_Sinistra_0);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("sinistra", Immagine_Sinistra_1);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("sinistra", Immagine_Sinistra_2);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("sinistra", Immagine_Sinistra_3);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
                MovimentoPerArrivareASemaforo("sinistra", Immagine_Sinistra_4);
            }
        }

        public void MovimentoPerArrivareASemaforo(string lato, System.Windows.Controls.Image immagineDaMuovere)
        {
            bool arrivata = false;
            while (!arrivata)
            {
                if (lato == "D")
                {

                    distanzaDaDestraMacchinaADestra = distanzaDaDestraPerSpawnDelleMacchineInStradaDestra;
                    while (distanzaDaDestraMacchinaADestra >= arrivoAlSemaforoPerDestra + 30)
                    {
                        distanzaDaDestraMacchinaADestra -= r.Next(25, 31);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaPerSpawnMacchine, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(400));
                    }
                    distanzaDaDestraMacchinaADestra = arrivoAlSemaforoPerDestra;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaPerSpawnMacchine, 0, 0);
                    }));
                    Thread.Sleep(TimeSpan.FromMilliseconds(400));

                    
                    arrivoAlSemaforoPerDestra += 60;
                    arrivata = true;
                }
                else
                {
                    distanzaDaDestraMacchinaSinistra = distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra;
                    while (distanzaDaDestraMacchinaSinistra <= arrivoAlSemaforoPerSinistra - 30)
                    {
                        distanzaDaDestraMacchinaSinistra += r.Next(25, 31);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaPerSpawnMacchine, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(400));
                    }
                    distanzaDaDestraMacchinaSinistra = arrivoAlSemaforoPerSinistra;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaPerSpawnMacchine, 0, 0);
                    }));
                    Thread.Sleep(TimeSpan.FromMilliseconds(400));
                   
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
                            altezzaMacchinaADestra += r.Next(10,13);
                            distanzaDaDestraMacchinaADestra -= r.Next(10, 16);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                            }));

                        }
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));
                        altezzaMacchinaADestra = altezzaInMainWindowDelPonte;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                        }));

                        //faccio il ponte
                        while (distanzaDaDestraMacchinaADestra >= finePontePerStradaDestra + 30)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                            distanzaDaDestraMacchinaADestra -= r.Next(20, 31);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                            }));

                        }
                        distanzaDaDestraMacchinaADestra = finePontePerStradaDestra;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));



                        //esco dal ponte
                        while (altezzaMacchinaADestra <= altezzaPuntoDiArrivo - 15)
                        {
                            altezzaMacchinaADestra += r.Next(5, 7);
                            distanzaDaDestraMacchinaADestra -= r.Next(20, 26);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                            }));
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                        }
                    }
                    altezzaMacchinaADestra = altezzaPuntoDiArrivo;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                    }));
                    Thread.Sleep(TimeSpan.FromMilliseconds(500));
                    macchinaStaFerma = false;

                    stradaDestra.MacchineInStrada.RemoveAt(0);


                    //finisco la tratta
                    while (distanzaDaDestraMacchinaADestra >= puntoDiArrivoPerStradaDestra + 30)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));
                        distanzaDaDestraMacchinaADestra -= r.Next(20, 31);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                        }));

                    }
                    distanzaDaDestraMacchinaADestra = puntoDiArrivoPerStradaDestra;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                    }));



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
                        while (altezzaMacchinaASinistra <= altezzaInMainWindowDelPonte - 15)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                            altezzaMacchinaASinistra += r.Next(10, 16);
                            distanzaDaDestraMacchinaSinistra += r.Next(10, 16);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                            }));

                        }
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));
                        altezzaMacchinaASinistra = altezzaInMainWindowDelPonte;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                        }));

                        //faccio il ponte
                        while (distanzaDaDestraMacchinaSinistra <= finePontePerStradaSinistra - 30)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                            distanzaDaDestraMacchinaSinistra += r.Next(20, 31);
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
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));




                        //esco dal ponte
                        while (altezzaMacchinaASinistra <= altezzaPuntoDiArrivo - 7)
                        {
                            altezzaMacchinaASinistra += r.Next(5, 7);
                            distanzaDaDestraMacchinaSinistra += r.Next(20, 26);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                            }));
                            Thread.Sleep(TimeSpan.FromMilliseconds(500));
                        }
                    }
                    altezzaMacchinaASinistra = altezzaPuntoDiArrivo;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                    }));

                    Thread.Sleep(TimeSpan.FromMilliseconds(500));
                    macchinaStaFerma = false;
                    stradaSinistra.MacchineInStrada.RemoveAt(0);

                    //finisco la tratta
                    while (distanzaDaDestraMacchinaSinistra >= puntoDiArrivoPerStradaSinistra + 30)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(500));
                        distanzaDaDestraMacchinaSinistra += r.Next(20, 31);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaMacchinaASinistra, 0, 0);
                        }));

                    }
                    distanzaDaDestraMacchinaSinistra = puntoDiArrivoPerStradaSinistra;
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

