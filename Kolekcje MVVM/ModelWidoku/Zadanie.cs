using Kolekcje_MVVM.Model;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Kolekcje_MVVM.ModelWidoku
{
    public class Zadanie : ObservedObject
    {
        private Model.Zadanie model;

        #region Własności
        public string Opis
        {
            get { return model.Opis; }
        }
        public DateTime DataUtworzenia
        {
            get { return model.DataUtworzenia; }
        }
        public DateTime PlanowanyTerminRealizacji
        {
            get { return model.PlanowanyTerminRealizacji; }
        }

        public Model.PriorytetZadania Priorytet
        {
            get { return model.Priorytet; }
        }
        public bool CzyZrealizowane
        {
            get { return model.CzyZrealizowane; }
        }
        public bool CzyZadaniePozostajeNiezrealizowanePoTerminie
        {
            get { return !model.CzyZrealizowane && (DateTime.Now > PlanowanyTerminRealizacji); }
        }
        #endregion

        public Zadanie(Model.Zadanie model)
        {
            this.model = model;
        }

        public Zadanie(string opis, DateTime dataUtworzenia, DateTime planowanyTerminRealizacji,
            Model.PriorytetZadania priorytet, bool czyZrealizowane)
        {
            model = new Model.Zadanie(opis, dataUtworzenia, planowanyTerminRealizacji, priorytet, czyZrealizowane);
        }
        public Model.Zadanie GetModel()
        {
            return model;
        }
        #region Polecenia
        private ICommand oznaczJakoZrealizowane;
        public ICommand OznaczJakoZrealizowane
        {
            get
            {
                if (oznaczJakoZrealizowane == null) oznaczJakoZrealizowane = new RelayCommand(
                    (object o)=>
                    {
                        model.CzyZrealizowane = true;
                        OnPropertyChanged(nameof(CzyZrealizowane), nameof(CzyZadaniePozostajeNiezrealizowanePoTerminie));
                    },
                    (object o)=>
                    {
                        return !model.CzyZrealizowane;
                    }
                    );
                return oznaczJakoZrealizowane;
            }
        }
        private ICommand oznaczJakoNiezrealizowane;
        public ICommand OznaczJakoNiezrealizowane
        {
            get
            {
                if (oznaczJakoNiezrealizowane == null) oznaczJakoNiezrealizowane = new RelayCommand(
                    (object o) =>
                    {
                        model.CzyZrealizowane = false;
                        OnPropertyChanged(nameof(CzyZrealizowane), nameof(CzyZadaniePozostajeNiezrealizowanePoTerminie));
                    },
                    (object o) =>
                    {
                        return model.CzyZrealizowane;
                    }
                    );
                return oznaczJakoNiezrealizowane;
            }
        }
        #endregion
        public override string ToString()
        {
            return model.ToString();
        }
    }
}
