using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HindiAlphabet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Quiz : ContentPage
	{
        List<Letter> letters;
        int random = 0;
        int num = 0;
        ObservableCollection<Result> results;
        Result result ;
        int[] indexForFalseOptions = new int[3];
        Random randomObject = new Random();
        List<int> number = new List<int>();
        List<Letter> learnedLetters = new List<Letter>();


        public Quiz ()
		{
			InitializeComponent ();
		}
        
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            results = new ObservableCollection<Result>();
            letters = await Storage.ReadList(App._fileName);

            checkLearnedLetters();
          //  LV_options.ItemsSource = q;
        }
        private async void checkLearnedLetters()
        {

            for (int i=0; i< letters.Count; i++)
            {
                if (letters[i].learned == "True")
                {
                    learnedLetters.Add(letters[i]);
                }
            }

            if (learnedLetters.Count>=4)
            {
                GenrateQuestions();
            }
            else
            {
                await DisplayAlert("Information", "Please first learn minimum 5 Letters", "ok");
               // await Navigation.PopAsync();
            }
        }

        private void GenrateQuestions()
        {


            result = new Result();
            num = randomObject.Next(learnedLetters.Count);
            Question question = new Question { question = "What is the Transliteration of \n " , questionObject= learnedLetters[num].name };
            result.letterName = learnedLetters[num].name;

            CheckRandomNumbers();

            question.answers.Add(new Answer { answer = learnedLetters[num].transliteration, status = true });
            question.answers.Add(new Answer { answer = letters[number.ElementAt(0)].transliteration, status = false });
            question.answers.Add(new Answer { answer = letters[number.ElementAt(1)].transliteration, status = false });
            question.answers.Add(new Answer { answer = letters[number.ElementAt(2)].transliteration, status = false });

            random = randomObject.Next(3);
            var help = question.answers[0];
            question.answers[0] = question.answers[random];

            question.answers[random] = help;


            SP_quiz.BindingContext = question;
        }

        private void CheckRandomNumbers()
        {
            while (number.Count < 3)
            {
                int i = randomObject.Next(letters.Count);
                if (!number.Contains(i))
                {
                    number.Add(i);
                }
            }
        }

        private async void LV_options_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var ans = (sender as ListView).SelectedItem as Answer;
            if (ans.status)
            {
                await DisplayAlert("Right Answer", "You are doing great work.", "ok");
                int i = result.correctAnswer;
                result.correctAnswer = i+1;
                result.lastTimeAnswer = "Correct";
                await App.Database.SaveItemAsync(result);
                GenrateQuestions();
            }
            else
            {
                await DisplayAlert("Wrong answer", " Right Answer is : " + learnedLetters[num].transliteration, "ok");
                int i = result.wrongAnswer;
                result.wrongAnswer = i+1;
                result.lastTimeAnswer = "Wrong";

                await App.Database.SaveItemAsync(result);

                GenrateQuestions();
            }

        }

        private async void Result_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResultPage());
        }
    }
}