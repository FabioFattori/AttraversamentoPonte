using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace progettoStrada
{
    public class Macchina
    {
        private Strada _stradaDiPartenza;
        private int _sensoDiMarcia;// vale 0 per il senso di marcia che va da sinistra e va verso destra.  Vale 1 per il senso di marcia che va da destra verso sinistra
        private const int PESO = 1;//valore sempre uguale a 1

        public Macchina(Strada stradaDiPartenza)//se si vuole creare una macchina
        {
            StradaDiPartenza = stradaDiPartenza;
        }

        public Macchina()//macchina generata dal bottone
        {
            Random r = new Random();
            if (r.Next(0, 2) == 0)
            
                StradaDiPartenza = new Strada("sinistro");
            else
                StradaDiPartenza = new Strada("destro");
        }

        public int SensoDiMarcia
        {
            get => default;
            private set
            {
                if (value == 1 || value == 0)
                    _sensoDiMarcia = value;
                else
                    throw new Exception("senso di marcia non valido");
            }
        }

        public Strada StradaDiPartenza
        {
            get => default;
            private set
            {
                _stradaDiPartenza = value;
            }
        }
    }
}