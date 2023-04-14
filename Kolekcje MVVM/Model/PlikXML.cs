using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Kolekcje_MVVM.Model
{
    public static class PlikXML
    {
        private static readonly IFormatProvider formatProvider = CultureInfo.InvariantCulture;
        public static void Zapisz(this Zadania zadania, string ścieżkaPliku)
        {
            try
            {
                XDocument xml = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("Data zapisania: " + DateTime.Now.ToString(formatProvider)),
                    new XElement("Zadania",
                        from Zadanie zadanie in zadania
                        select new XElement("Zadanie",
                            new XElement("Opis", zadanie.Opis),
                            new XElement("DataUtworzenia", zadanie.DataUtworzenia.ToString(formatProvider)),
                            new XElement("PlanowanyTerminRealizacji", zadanie.PlanowanyTerminRealizacji.ToString(formatProvider)),
                            new XElement("Priorytet", ((byte)(zadanie.Priorytet)).ToString()),
                            new XElement("CzyZrealizowane", zadanie.CzyZrealizowane.ToString(formatProvider)))));
                xml.Save(ścieżkaPliku);
            }
            catch(Exception ex)
            {
                throw new Exception("Błąd przy zapisie do pliku XML", ex);
            }
        }
        public static Zadania Czytaj(string ścieżkaPliku)
        {
            try
            {
                XDocument xml = XDocument.Load(ścieżkaPliku);
                IEnumerable<Zadanie> dane=
                    from zadanie in xml.Root.Descendants("Zadanie")
                    select new Zadanie(
                        zadanie.Element("Opis").Value,
                        DateTime.Parse(zadanie.Element("DataUtworzenia").Value, formatProvider),
                        DateTime.Parse(zadanie.Element("PlanowanyTerminRealizacji").Value, formatProvider),
                        (PriorytetZadania)byte.Parse(zadanie.Element("Priorytet").Value, formatProvider),
                        bool.Parse(zadanie.Element("CzyZrealizowane").Value));
                Zadania zadania = new Zadania();
                foreach (Zadanie zadanie in dane) zadania.DodajZadanie(zadanie);
                return zadania;
            }
            catch(Exception ex)
            {
                throw new Exception("Błąd przy odczycie danych z pliku XML", ex);
            }
        }
    }
}
