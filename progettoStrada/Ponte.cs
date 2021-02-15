using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace progettoStrada
{
    public class Ponte
    {
        private const int PESO_MASSIMO_SOSTENIBILE = 10;//10 per far andare massimo 10 macchina alla volta (se faccio la parte extra i camion li farò pesare 10)
        private List<Macchina> _macchineSulPonte;

        public Ponte()
        {
            List<Macchina> appoggio = new List<Macchina>();
            MacchineSulPonte = appoggio;
        }

        public List<Macchina> MacchineSulPonte
        {
            get => default;
            set
            {
                _macchineSulPonte = value;
            }
        }

        public void NuovaMacchinaIniziaAttraversataDelPonte(Macchina macchina)
        {
            MacchineSulPonte.Add(macchina);
        }

        public void MacchinaArrivaAllAltraEstremitaDelPonte(Macchina macchina)
        {
            MacchineSulPonte.Remove(macchina);
        }
    }
}