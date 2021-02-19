﻿using System;
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
        object ControlloPonte;
        //valori usati per il movimento:
        int arrivoAlSemaforoPerSinistra = 445;
        int arrivoAlSemaforoPerSinistraPerSecondaMacchina = 375;
        int arrivoAlSemaforoPerDestra = 880;
        int arrivoAlSemaforoPerDestraPerSecondaMacchina = 950;
        int finePontePerStradaSinistra = 755;
        int finePontePerStradaDestra = 550;
        int altezzaInMainWindowDelPonte = 200;
        int altezzaPuntoDiArrivo = 235;
        int altezzaMacchinaADestra;
        int altezzaSecondaMacchina;
        int distanzaDaDestraMacchinaADestra;
        int distanzaDaDestraSecondaMacchinaADestra;
        int distanzaDaDestraSecondaMacchinaASinistra;
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
            ControlloPonte = new object();
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
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            int numeroMacchineDaSpawnareInStradaDestra = r.Next(0,3);

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
            else if (numeroMacchineDaSpawnareInStradaDestra == 2)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Destra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Destra_1.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaDestra, altezzaPerSpawnMacchine, 0, 0);
                }));

                //metto lo stesso oggetto macchina due volte nella lista dell'oggetto strada perchè non mi interressa che oggetto c'è nella lista, ma mi interessa che questo oggetto ci sia per poi toglierolo nel metodo IniziaMovimentoMacchinaDestra()
                /*
                Macchina nuova = new Macchina(stradaDestra);
                stradaDestra.MacchineInStrada.Add(nuova);
                stradaDestra.MacchineInStrada.Add(nuova);
                */
               
                Thread.Sleep(TimeSpan.FromMilliseconds(400));
                immagineInMovimento = Immagine_Destra_0;


                Thread Macchina1 = new Thread(new ThreadStart(MacchinaNuovaSpawnataARandomDestra));
                Macchina1.Name = "MacchinaDestra1";
                Macchina1.Start();
               
                int i = arrivoAlSemaforoPerDestra;
                

                
                Thread.Sleep(TimeSpan.FromSeconds(1));
                immagineInMovimento = Immagine_Destra_1;
                Thread macchina2 = new Thread(new ThreadStart(MacchinaNuovaSpawnataARandomDestra));

                macchina2.Name = "MacchinaDestra2";
                macchina2.Start();
                arrivoAlSemaforoPerDestra = arrivoAlSemaforoPerDestraPerSecondaMacchina;

                
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                arrivoAlSemaforoPerDestra = i;
            }
            if(numeroMacchineDaSpawnareInStradaDestra>=2)
                Thread.Sleep(TimeSpan.FromMilliseconds(250));

            //spawn macchine a sinistra
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
            else if (numeroMacchineDaSpawnareInStradaSinistra == 2)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Immagine_Sinistra_0.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                    Immagine_Sinistra_1.Margin = new Thickness(distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra, altezzaPerSpawnMacchine, 0, 0);
                }));
                //metodo per far arrivare al semaforo
                Thread.Sleep(TimeSpan.FromMilliseconds(400));
                immagineInMovimento = Immagine_Sinistra_0;


                Thread Macchina1 = new Thread(new ThreadStart(MacchinaNuovaSpawnataARandomDestra));
                Macchina1.Name = "MacchinaSinistra1";
                Macchina1.Start();

                int i = arrivoAlSemaforoPerSinistra;



                Thread.Sleep(TimeSpan.FromSeconds(1));
                immagineInMovimento = Immagine_Sinistra_1;
                Thread macchina2 = new Thread(new ThreadStart(MacchinaNuovaSpawnataARandomSinistra));

                macchina2.Name = "MacchinaSinistra2";
                macchina2.Start();
                arrivoAlSemaforoPerSinistra = arrivoAlSemaforoPerSinistraPerSecondaMacchina;


                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                arrivoAlSemaforoPerSinistra = i;
            }

        }


        public void MacchinaNuovaSpawnataARandomDestra()
        {
            //arrivoAlSemaforoPerDestra += 60;

            MovimentoPerArrivareASemaforo("D", immagineInMovimento,arrivoAlSemaforoPerDestra);
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

            MovimentoPerArrivareASemaforo("S", immagineInMovimento1,arrivoAlSemaforoPerSinistra);
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                altezzaMacchinaASinistra = (int)immagineInMovimento1.Margin.Top;
            }));


            Macchina nuova = new Macchina(stradaSinistra);
            stradaSinistra.MacchineInStrada.Add(nuova);
            IniziaMovimentoMacchinaSinistra(immagineInMovimento1);

        }


        public void MovimentoPerArrivareASemaforo(string lato, System.Windows.Controls.Image immagine,int arrivo)
        {
            bool arrivata = false;
            while (!arrivata)
            {
                if (lato == "D")
                {

                    if (Thread.CurrentThread.Name == "MacchinaDestra2")
                    {
                        distanzaDaDestraSecondaMacchinaADestra = distanzaDaDestraPerSpawnDelleMacchineInStradaDestra;


                        while (distanzaDaDestraSecondaMacchinaADestra >= arrivo-4)
                        {

                            distanzaDaDestraSecondaMacchinaADestra -= r.Next(2, 5);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagine.Margin = new Thickness(distanzaDaDestraSecondaMacchinaADestra, altezzaPerSpawnMacchine, 0, 0);
                            }));
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                        }

                        distanzaDaDestraSecondaMacchinaADestra = arrivo;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagine.Margin = new Thickness(distanzaDaDestraSecondaMacchinaADestra, altezzaPerSpawnMacchine, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));



                        arrivata = true;
                    }
                    else
                    {
                        distanzaDaDestraMacchinaADestra = distanzaDaDestraPerSpawnDelleMacchineInStradaDestra;


                        while (distanzaDaDestraMacchinaADestra >= arrivo)
                        {

                            distanzaDaDestraMacchinaADestra -= r.Next(2, 5);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagine.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaPerSpawnMacchine, 0, 0);
                            }));
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                        }

                        distanzaDaDestraMacchinaADestra = arrivo;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagine.Margin = new Thickness(distanzaDaDestraMacchinaADestra, altezzaPerSpawnMacchine, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));



                        arrivata = true;
                    }
                }
                else
                {
                    if (Thread.CurrentThread.Name == "MacchinaSinitra2")
                    {
                        distanzaDaDestraMacchinaSinistra = distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra;
                        while (distanzaDaDestraMacchinaSinistra <= arrivo - 4)
                        {
                            distanzaDaDestraMacchinaSinistra += r.Next(2, 5);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagine.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaPuntoDiArrivo, 0, 0);
                            }));
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                        }
                        distanzaDaDestraMacchinaSinistra = arrivo;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagine.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaPuntoDiArrivo, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));


                        arrivata = true;
                    }
                    else
                    {
                        distanzaDaDestraMacchinaSinistra = distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra;
                        while (distanzaDaDestraMacchinaSinistra <= arrivo - 4)
                        {
                            distanzaDaDestraMacchinaSinistra += r.Next(2, 5);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagine.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaPuntoDiArrivo, 0, 0);
                            }));
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                        }
                        distanzaDaDestraMacchinaSinistra = arrivo;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagine.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaPuntoDiArrivo, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));


                        arrivata = true;
                    }
                }
            }
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
                MovimentoPerArrivareASemaforo("D", Immagine_Nuova_Destra,arrivoAlSemaforoPerDestra);
                Macchina nuova = new Macchina(stradaDestra);
                stradaDestra.MacchineInStrada.Add(nuova);
                
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

                MovimentoPerArrivareASemaforo("S", Immagine_Nuova_Sinistra,arrivoAlSemaforoPerSinistra);
                Macchina nuova = new Macchina(stradaSinistra);
                stradaSinistra.MacchineInStrada.Add(nuova);
                

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
                if (Thread.CurrentThread.Name == "MacchinaDestra1")
                {
                    if (ControlloSeSulPonteSonoPresentiMacchineProvenientiDaDestra() == true && stradaSinistra.MacchineInStrada.Count == 0)
                    {
                       

                        lock (ControlloPonte)
                        {
                            //ponte.NuovaMacchinaIniziaAttraversataDelPonte(altezzaMacchinaADestra);
                            //entro nel ponte
                            distanzaDaDestraMacchinaADestra = arrivoAlSemaforoPerDestra;
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

                            stradaDestra.MacchineInStrada.RemoveAt(0);
                        }
                       
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
                    
                }
                else
                {
                   
                    if (ControlloSeSulPonteSonoPresentiMacchineProvenientiDaDestra() == true && stradaSinistra.MacchineInStrada.Count == 0)
                    {
                        altezzaSecondaMacchina = altezzaPerSpawnMacchine;
                        //arrivo al semaforo
                        while (distanzaDaDestraSecondaMacchinaADestra >= arrivoAlSemaforoPerDestra + 4)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                            distanzaDaDestraSecondaMacchinaADestra -= r.Next(2, 5);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraSecondaMacchinaADestra, altezzaSecondaMacchina, 0, 0);
                            }));

                        }
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraSecondaMacchinaADestra, altezzaSecondaMacchina, 0, 0);
                        }));

                        distanzaDaDestraSecondaMacchinaADestra = arrivoAlSemaforoPerDestra;
                        lock (ControlloPonte)
                        {
                            //ponte.NuovaMacchinaIniziaAttraversataDelPonte(altezzaMacchinaADestra);
                            //entro nel ponte
                            while (altezzaSecondaMacchina <= altezzaInMainWindowDelPonte - 2)
                            {
                                Thread.Sleep(TimeSpan.FromMilliseconds(10));
                                altezzaSecondaMacchina += r.Next(1, 3);
                                distanzaDaDestraSecondaMacchinaADestra -= r.Next(2, 5);
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    immagineDaMuovere.Margin = new Thickness(distanzaDaDestraSecondaMacchinaADestra, altezzaSecondaMacchina, 0, 0);
                                }));

                            }
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                            altezzaSecondaMacchina = altezzaInMainWindowDelPonte;
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraSecondaMacchinaADestra, altezzaSecondaMacchina, 0, 0);
                            }));

                            //faccio il ponte
                            while (distanzaDaDestraSecondaMacchinaADestra >= finePontePerStradaDestra + 4)
                            {
                                Thread.Sleep(TimeSpan.FromMilliseconds(10));
                                distanzaDaDestraSecondaMacchinaADestra -= r.Next(2, 5);
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    immagineDaMuovere.Margin = new Thickness(distanzaDaDestraSecondaMacchinaADestra, 200, 0, 0);
                                }));

                            }
                            distanzaDaDestraSecondaMacchinaADestra = finePontePerStradaDestra;
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraSecondaMacchinaADestra, 200, 0, 0);
                            }));
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                            stradaDestra.MacchineInStrada.RemoveAt(0);

                        }
                        
                        altezzaSecondaMacchina = 200;
                        //esco dal ponte
                        while (altezzaSecondaMacchina >= altezzaPerSpawnMacchine - 2)
                        {
                            altezzaSecondaMacchina -= r.Next(1, 3);
                            distanzaDaDestraSecondaMacchinaADestra -= r.Next(2, 5);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraSecondaMacchinaADestra, altezzaSecondaMacchina, 0, 0);
                            }));
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                        }

                        altezzaSecondaMacchina = altezzaPerSpawnMacchine;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraSecondaMacchinaADestra, altezzaSecondaMacchina, 0, 0);
                        }));
                        Thread.Sleep(TimeSpan.FromMilliseconds(10));
                        macchinaStaFerma = false;




                        //finisco la tratta
                        while (distanzaDaDestraSecondaMacchinaADestra >= distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra + 4)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                            distanzaDaDestraSecondaMacchinaADestra -= r.Next(2, 5);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraSecondaMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                            }));

                        }
                        distanzaDaDestraSecondaMacchinaADestra = distanzaDaDestraPerSpawnDelleMacchineInStradaSinistra + 1;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraSecondaMacchinaADestra, altezzaMacchinaADestra, 0, 0);
                        }));


                        
                    }
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
                if (Thread.CurrentThread.Name == "MacchinaSinistra1")
                {
                    if (ControlloSeSulPonteSonoPresentiMacchineProvenientiDaSinistra() == true && stradaDestra.MacchineInStrada.Count == 0)
                    {

                        lock (ControlloPonte)
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
                else
                {
                    if (ControlloSeSulPonteSonoPresentiMacchineProvenientiDaSinistra() == true && stradaDestra.MacchineInStrada.Count == 0)
                    {
                        altezzaSecondaMacchina = altezzaPerSpawnMacchine;
                        //arrivo al semaforo
                        while (distanzaDaDestraMacchinaSinistra >= arrivoAlSemaforoPerSinistra + 4)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(10));
                            distanzaDaDestraMacchinaSinistra -= r.Next(2, 5);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaSecondaMacchina, 0, 0);
                            }));

                        }
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            immagineDaMuovere.Margin = new Thickness(distanzaDaDestraMacchinaSinistra, altezzaSecondaMacchina, 0, 0);
                        }));

                        distanzaDaDestraSecondaMacchinaASinistra = arrivoAlSemaforoPerSinistra;
                        lock (ControlloPonte)
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

