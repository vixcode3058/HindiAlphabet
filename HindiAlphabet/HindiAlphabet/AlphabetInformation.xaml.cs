using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HindiAlphabet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AlphabetInformation : ContentPage
	{
        Letter selectedLetter;
		public AlphabetInformation ( Letter letter)
		{
			InitializeComponent ();
            BindingContext = letter;
            this.selectedLetter = letter;
            ToggelBtn_learn.IsToggled =Convert.ToBoolean( selectedLetter.learned);

            AddGestureProperty();
 }

        private void AddGestureProperty()
        {  
            L_aplhName.GestureRecognizers.Add(AddProperty(L_aplhName.Text));
            //L_hindiWord.GestureRecognizer.Add(AddGestureProperty(L_hindiWord.text));
        }

        private IGestureRecognizer AddProperty(string text)
        {
            var tapGesture = new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    var locales = await TextToSpeech.GetLocalesAsync();

                    var locale = (from s in locales where s.Language.Contains("hi") select s).FirstOrDefault();

                    var settings = new SpeechOptions()
                    {
                        Volume = (float).75,
                        Pitch = (float)1.0,
                        Locale = locale
                    };

                    await TextToSpeech.SpeakAsync(text, settings);
                })
            };

            return tapGesture;
        }



        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            var updateValue= App._letters.FirstOrDefault(c => c.name == selectedLetter.name);
            updateValue.learned =Convert.ToString( e.Value);

            Storage.SerializeAndWriteList(App._letters, App._fileName);


          
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var text = (sender as Label).Text;

            var locales = await TextToSpeech.GetLocalesAsync();

            var locale = (from s in locales where s.Language.Contains("hi") select s).FirstOrDefault();

            var settings = new SpeechOptions()
            {
                Volume = (float).75,
                Pitch = (float)1.0,
                Locale = locale
            };

            await TextToSpeech.SpeakAsync(text, settings);
        }
    }
}