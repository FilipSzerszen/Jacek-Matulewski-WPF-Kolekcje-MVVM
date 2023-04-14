using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolekcje_MVVM.Model
{
    public enum PriorytetZadania : byte { MniejWażne, Ważne, Krytyczne }
    public class Zadanie
    {
        public string Opis { get; private set; }
        public DateTime DataUtworzenia { get; private set; }
        public DateTime PlanowanyTerminRealizacji { get; private set; }
        public PriorytetZadania Priorytet { get; private set; }
        public bool CzyZrealizowane { get; set; }

        public Zadanie(string opis, DateTime dataUtworzenia, DateTime planowanyTerminRealizacji, 
            PriorytetZadania priorytet, bool czyZrealizowane = false)
        {
            this.Opis = opis;
            this.DataUtworzenia = dataUtworzenia;
            this.PlanowanyTerminRealizacji = planowanyTerminRealizacji;
            this.Priorytet = priorytet;
            this.CzyZrealizowane = czyZrealizowane;
        }

        public override string ToString()
        {
            return Opis + ", priorytet: " + Priorytet.ToString() + ", data utworzenia: " + DataUtworzenia.ToString() +
                ", planowany termin realizacji: " + PlanowanyTerminRealizacji.ToString() + ", zrealizowane: " +
                (CzyZrealizowane ? "tak" : "nie");
        }
    }
}
