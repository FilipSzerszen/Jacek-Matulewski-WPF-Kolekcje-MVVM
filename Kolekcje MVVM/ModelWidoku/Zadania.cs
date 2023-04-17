using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kolekcje_MVVM.ModelWidoku
{
    using static Model.PlikXML;
    public class Zadania
    {
        private Model.Zadania model;

        public ObservableCollection<Zadanie> ListaZadań { get; } = new ObservableCollection<Zadanie>();

        private void kopiujZadaniaZModelu()
        {
            ListaZadań.CollectionChanged -= synchronizacjaModelu;
            ListaZadań.Clear();
            foreach (Model.Zadanie zadanie in model)
            {
                ListaZadań.Add(new Zadanie(zadanie));
            }
            ListaZadań.CollectionChanged += synchronizacjaModelu;
        }

        private string ścieżkaPliku = "zadania.xml";

        public Zadania()
        {
            if (System.IO.File.Exists(ścieżkaPliku)) model = Czytaj(ścieżkaPliku);
            else model = new Model.Zadania();

            //test
            //model.DodajZadanie(new Model.Zadanie("pierwsze", DateTime.Now, DateTime.Now.AddDays(3), Model.PriorytetZadania.Krytyczne));
            //model.DodajZadanie(new Model.Zadanie("drugie", DateTime.Now, DateTime.Now.AddDays(-2), Model.PriorytetZadania.MniejWażne));
            //model.DodajZadanie(new Model.Zadanie("trzecie", DateTime.Now, DateTime.Now.AddDays(5), Model.PriorytetZadania.Ważne));

            kopiujZadaniaZModelu();
        }

        private void synchronizacjaModelu(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Zadanie noweZadanie = (Zadanie)e.NewItems[0];
                    if (noweZadanie != null) model.DodajZadanie(noweZadanie.GetModel());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Zadanie usuwaneZadanie = (Zadanie)e.OldItems[0];
                    if (usuwaneZadanie != null) model.UsuńZadanie(usuwaneZadanie.GetModel());
                    break;
            }
        }

        public ICommand Zapisz
        {
            get
            {
                return new RelayCommand(argument => { Model.PlikXML.Zapisz(model , ścieżkaPliku); });
            }
        }

        private ICommand usuńZadanie;
        public ICommand UsuńZadanie
        {
            get
            {
                if (usuńZadanie == null) usuńZadanie = new RelayCommand(
                       o =>
                       {
                           int indeksZadanie = (int)o;
                           Zadanie zadanie = ListaZadań[indeksZadanie];
                           ListaZadań.Remove(zadanie);
                       },
                        o =>
                       {
                           if (o == null) return false;
                           int indeksZadanie = (int)o;
                           return indeksZadanie >= 0;
                       });
                return usuńZadanie;
            }
        }

        private ICommand dodajZadanie;
        public ICommand DodajZadanie
        {
            get
            {
                if (dodajZadanie == null) dodajZadanie = new RelayCommand(
                        o =>
                       {
                           Zadanie zadanie =o as Zadanie;
                           if (zadanie != null) ListaZadań.Add(zadanie);
                       },
                        o =>
                       {
                           return o as Zadanie !=null;
                       });
                return dodajZadanie;
            }
        }
    }
}
