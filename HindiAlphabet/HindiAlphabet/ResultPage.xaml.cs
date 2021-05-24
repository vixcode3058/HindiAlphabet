using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HindiAlphabet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultPage : ContentPage
	{
		public ResultPage ()
		{
			InitializeComponent ();
		}

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            LV_results.ItemsSource = null;
            try
            {
                LV_results.ItemsSource = await App.Database.getIetmsAsync();

            }
            catch (Exception)
            {

                await this.DisplayAlert("Alert!", "There is no result to show.", "Ok");
                await Navigation.PopAsync();

            }
         }
        
        private async void deleteResult_Clicked(object sender, EventArgs e)
        {
            var data = await App.Database.getIetmsAsync();

            if (data.Count>0)
            {
                var result = await this.DisplayAlert("Alert!", "Do you really want to Delete All Results ?", "Yes", "No");

                if (result)
                {
                    await App.Database.DeleteItemAsync();
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await DisplayAlert("Information", "There is no record to delete", "Ok");
            }

           
        }
    }
}