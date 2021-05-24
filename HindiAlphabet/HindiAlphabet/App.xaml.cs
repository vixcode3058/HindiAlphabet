using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace HindiAlphabet
{
	public partial class App : Application
	{
        public static string _fileName = "Letters.xml";
        public static string _resultFileName = "Result.db3";
        public static List<Result> _results = new List<Result>();
        public static List<Letter> _letters;
        public static bool learingStatus = false;

        static MyDatabase database;

        public static MyDatabase Database
        {
            get
            {

                if (database == null)
                {
                    database = new MyDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("Result.db3"));
                }
                return database;
            }
        }

        public App ()
		{
			InitializeComponent();

			MainPage = new MasterDetailPage1();
		}

		protected async override void OnStart ()
		{
            // Handle when your app starts

            ////var a = Storage.ReadStorageFile(App._fileName);
            ////Storage.SerializeAndWriteList<List<Letter>>(a, "Letter.txt");

            //  var status= await Storage.WriteStorageFile<List<Letter>>(a, App._fileName);
            //   var data = await Storage.ReadLocalStorageFile("Letter.txt");
          


        }

        protected override void OnSleep ()
		{
            Storage.SerializeAndWriteList(_letters, _fileName);

            // Handle when your app sleeps
            //Storage.ListToStream<List<Letter>>(_letters);
            //Storage.WriteStorageFile<List<Letter>>(_letters, _fileName);
            //Storage.SerializeAndWriteList<List<Letter>>(_letters, _fileName);
		}
		protected async override void OnResume ()
		{
          //  var data = Storage.DeserializeAndReadList(_fileName);
            App._letters =  await Storage.ReadList(_fileName);
			// Handle when your app resumes
		}

        
	}
}
