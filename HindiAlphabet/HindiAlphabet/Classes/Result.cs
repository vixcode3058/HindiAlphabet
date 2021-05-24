using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HindiAlphabet
{
   public class Result
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string letterName { get; set; }
        public int correctAnswer { get; set; }
        public int wrongAnswer { get; set; }
        public string lastTimeAnswer { get; set; }
    }
}
