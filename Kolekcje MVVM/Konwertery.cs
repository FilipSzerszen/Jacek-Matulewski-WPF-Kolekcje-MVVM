using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Kolekcje_MVVM
{
    public class BoolToBrushKonwerter : IValueConverter
    {
        public Brush KolorDlaFałszu { get; set; } = Brushes.Black;
        public Brush KolorDlaPrawdy { get; set; } = Brushes.Gray;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = (bool)value;
            return b ? KolorDlaPrawdy : KolorDlaFałszu;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ZadanieConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string opis = (string)values[0];
            DateTime dataUtworzenia = DateTime.Now;
            DateTime? planowanyTerminRealizacji = (DateTime?)values[1];
            Model.PriorytetZadania priorytet = (Model.PriorytetZadania)(int)values[2];

            if (!string.IsNullOrWhiteSpace(opis) && planowanyTerminRealizacji.HasValue)
            {
                return new ModelWidoku.Zadanie(opis, dataUtworzenia, planowanyTerminRealizacji.Value, priorytet, false);
            }
            else return null;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
