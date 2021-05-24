using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HindiAlphabet
{
	public partial class MainPage : ContentPage
	{

        public MainPage()
		{
            InitializeComponent();
            //LoadData();
            GetData();
            CreateGrid();

        }

        private async void GetData()
        {

            if (File.Exists(Storage.GetLocalPath(App._fileName)))
            {
                App._letters = await Storage.ReadList(App._fileName);

            }
            else
            {
                List<Letter> data = await Storage.ReadStorageFile(App._fileName);
                 Storage.SerializeAndWriteList<List<Letter>>(data, App._fileName);

                App._letters = await Storage.ReadList(App._fileName);

            }


            //if (App._letters == null)
            //{
            //   
            //}


        }


        private void CreateGrid()
        {
            for (int row = 0; row < 15; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    int index = (row*4)+column;

                    if (index< App._letters.Count)
                    {
                        Label L_letterName = new Label();
                        L_letterName.FontFamily = "{StaticResource HindiFont}";
                        L_letterName.FontSize = 30;
                        L_letterName.FontAttributes = FontAttributes.Bold;
                        L_letterName.VerticalTextAlignment = TextAlignment.Center;
                        L_letterName.HorizontalTextAlignment = TextAlignment.Center;

                        Label L_letterTransliteration = new Label();
                        L_letterTransliteration.FontSize = 20;
                        L_letterTransliteration.FontFamily = "{StaticResource HindiFont}";

                        L_letterTransliteration.VerticalTextAlignment = TextAlignment.Center;
                        L_letterTransliteration.HorizontalTextAlignment = TextAlignment.Center;
                        StackLayout SL_letterLayout = new StackLayout
                        {
                            HeightRequest = 80,
                            BackgroundColor = Color.LightBlue,
                            BindingContext = App._letters.ElementAt(index),
                            GestureRecognizers = {
                                                new TapGestureRecognizer {
                                                    Command = new Command( async  () =>
                                                    { await  Navigation.PushAsync(new AlphabetInformation(App._letters[index])); })}
                                                }
                        };
                        L_letterName.SetBinding(Label.TextProperty, "name");
                        L_letterTransliteration.SetBinding(Label.TextProperty, "transliteration");
                        SL_letterLayout.Children.Add(L_letterName);
                        SL_letterLayout.Children.Add(L_letterTransliteration);
                        SL_letterLayout.SetValue(Grid.RowProperty, row);
                        SL_letterLayout.SetValue(Grid.ColumnProperty, column);
                        Grd_letters.Children.Add(SL_letterLayout);

                    }

                }
            }

        }



        private void Quiz_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Quiz());
        }

        private void Words_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Words());
        }

        private void Histrory_Clicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new History());
        }

        private  void BTn_forignWords_Clicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new ForeignWords());
        }

        private async void About_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new About());
        }
    }
}
