using System;
using System.Collections.Generic;


namespace Resonate_Test.Level_200
{

    class CodeToData
    {
        public CodeToData(Model model)
        {
            Result = model;

            Initialize();
        }



        public Model Result { get; }



        private Queue<bool> Bites(string code)
        {
            var _bites = new Queue<bool>();

            foreach (char c in code)
            {
                var binStr = Convert.ToString(c - 48, 2).PadLeft(6, '0');

                for (int i = 0; i < binStr.Length; i++)
                    _bites.Enqueue(binStr[i] == '1' ? true : false);
            }

            return _bites;
        }


        private Queue<int> Numbers(Queue<bool> bites)
        {
            var numbers = new Queue<int>();
            var seq1 = 4;
            var seq2 = 5; 

            BitesToInt32(seq1);

            var position = numbers.Dequeue();

            for (int i = 0; i < position - seq1; i++)
                bites.Dequeue();

            BitesToInt32(seq2);
            BitesToInt32(seq2);
            BitesToInt32(numbers.Dequeue());
            BitesToInt32(numbers.Dequeue());
            BitesToInt32(bites.Count);

            void BitesToInt32(int size)
            {
                var length = bites.Count;
                var temp = "";
                while (bites.Count > length - size)
                    temp += bites.Dequeue() == true ? '1' : '0';

                numbers.Enqueue(Convert.ToInt32(temp, 2));
            }

            return numbers;
        }


        private void InitializeModel(Queue<int> numbers)
        {
            var store = numbers.Dequeue();

            var yearMonth = numbers.Dequeue();
            var year = (yearMonth / 12) + Result.MinYear;
            var month = yearMonth % 12;
            if (month == 0)
            {
                year--;
                month = 12;
            }

            var customerDay = numbers.Dequeue();
            var customer = Math.Truncate((double)customerDay / 100);
            var day = customerDay - customer * 100;

            var date = new DateTime(year, month, (int)day);

            Result.Store = store;
            Result.Date = date;
            Result.Customer = (int)customer;

        }


        private void Initialize()
        {
            var bites = Bites(Result.Code);

            var numbers = Numbers(bites);

            InitializeModel(numbers);
        }


    }
}
