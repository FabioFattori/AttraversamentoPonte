using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace progettoStrada
{
    public class Strada
    {
        private string _latoDelPonte;
        private List<Macchina> _macchineInStrada;

        public Strada(string latoDelPonte)
        {
            LatoDelPonteDoveSiTrovaLaStrada = latoDelPonte;
            List<Macchina> appoggio = new List<Macchina>();
            MacchineInStrada = appoggio;
        }

        public string LatoDelPonteDoveSiTrovaLaStrada
        {
            get => default;
            set
            {
                if (value.ToLower() == "sinistro" || value.ToLower() == "destro")
                    _latoDelPonte = value.ToLower();
                else
                    throw new Exception("lato dele ponte nella strada non valido");
            }
        }

        public List<Macchina> MacchineInStrada
        {
            get => default;
            set
            {
                _macchineInStrada = value;
            }
        }
    }
}