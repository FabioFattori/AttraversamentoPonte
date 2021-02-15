using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace progettoStrada
{
    public class Ponte
    {
        private int pesoSostenibilePonte = 10;//10 per far andare massimo 10 macchina alla volta (se faccio la parte extra i camion li farò pesare 10)
        private List<Macchina> _macchineSulPonte;

        public Ponte()
        {
            List<Macchina> appoggio = new List<Macchina>();
            MacchineSulPonte = appoggio;
        }

        public List<Macchina> MacchineSulPonte
        {
            get
            {
                return _macchineSulPonte;
            }
            set
            {
                _macchineSulPonte = value;
            }
        }

        public void NuovaMacchinaIniziaAttraversataDelPonte(Macchina macchina)
        {
            if (pesoSostenibilePonte - 1 >= 0)
            {
                MacchineSulPonte.Add(macchina);
                pesoSostenibilePonte = pesoSostenibilePonte - 1;
            }
            else
            {
                throw new Exception("non è possibile far passare la macchina ora");
            }
        }

        public void MacchinaArrivaAllAltraEstremitaDelPonte(Macchina macchina)
        {
            MacchineSulPonte.Remove(macchina);
            pesoSostenibilePonte++;
        }
    }
}