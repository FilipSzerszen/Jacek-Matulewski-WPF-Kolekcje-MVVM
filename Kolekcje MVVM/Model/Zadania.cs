using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolekcje_MVVM.Model
{
    public class Zadania : IEnumerable<Zadanie>
    {
        private List<Zadanie> listaZadań = new List<Zadanie>();

        //CRUD
        public void DodajZadanie(Zadanie zadanie)
        {
            listaZadań.Add(zadanie);
        }

        public bool UsuńZadanie(Zadanie zadanie)
        {
            return listaZadań.Remove(zadanie);
        }

        public int LiczbaZadań
        {
            get
            {
                return listaZadań.Count;
            }
        }

        public Zadanie this[int indeks]
        {
            get
            {
                return listaZadań[indeks];
            }
        }

        public IEnumerator<Zadanie> GetEnumerator()
        {
            return listaZadań.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator(); 
        }
    }
}
