using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HindiAlphabet
{
    public class GetExamplePart : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<HelpToGetLetter> helpToGetLetters = new List<HelpToGetLetter>();
            if (value != null)
            {
                var words = value.ToString().Split(';');
                foreach (var item in words)
                {
                    var seprateWords = item.ToString().Split(':');

                    for (int i = 0; i < seprateWords.Length/3; i++)
                    
                    {
                        helpToGetLetters.Add(new HelpToGetLetter { hindiLetter = seprateWords[0+ i*3], englishMeaning = seprateWords[1 + i * 3], pronc = seprateWords[2 + i * 3] });

                    }
                }

                return helpToGetLetters;

            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
