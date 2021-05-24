using System;
using System.Collections.Generic;
using System.Text;

namespace HindiAlphabet
{
    public class Question
    {
        public string question { get; set; }
        public string questionObject { get; set; }

        public List<Answer> answers { get; set; }

        public Question()
        {
            answers = new List<Answer>();
        }
    }

    public class Answer
    {
        public string answer { get; set; }
        public bool status { get; set; }

    }
}
