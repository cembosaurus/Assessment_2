using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Resonate_Test.Level_200
{
    class DataToCode
    {
        private const int bitBase = 6;
        private Model _model;
        private IDictionary<string, int> _data;
        private StringBuilder _result;


        public DataToCode(Model model)
        {
            _model = model;

            Initialize();
        }




        public Model Result
        {
            get
            {
                _model.Code = _result.ToString();
                return _model;
            }
        }




        private Stack<bool> GenerateBites()
        {
            var result = new Stack<bool>();
            BitArray ba;
            int number, length, binLength = 0;
            var start = "";

            for (int x = _data.Count - 1; x >= 0; x--)
            {
                number = _data.ElementAt(x).Value;
                length = number.BinaryLength();

                ba = new BitArray(new int[] { number });

                for (int i = 0; i < length; i++)
                    result.Push(ba[i]);

                if (x < 2)
                {
                    binLength += length * (x == 0 ? 32 : 1);   
                }
            }          

            ba = new BitArray(new int[] { binLength });
            for (int i = 0; i < 10; i++)
                result.Push(ba[i]);

            var rest = result.Count % bitBase;
            var need = bitBase - rest;
            var pad = ((need == 5) || (need == 4)) ? need : need + bitBase;
            var value = ((need == 5) || (need == 4)) ? pad : pad; 

            start = Convert.ToString(value, 2).PadLeft(4, '0');
            start  += (need != 4) ? Convert.ToString(0, 2).PadRight(pad - 4, '0') : "";

            for(int i = start.Length - 1; i >= 0; i--)
                result.Push(start[i] == '1' ? true : false);


            return result; 
        }


        private void GenerateCode(Stack<bool> s)
        {
            var temp = new StringBuilder();

            while (s.Count > 0)
            {
                temp.Append(s.Pop() == true ? '1' : '0');

                if ((temp.Length == bitBase) || s.Count == 0)
                {
                    _result.Append((char)(Convert.ToInt32(temp.ToString(), 2) + 48));
                    temp.Clear();
                }

            }
            
        }



        private void Initialize()
        {
            _data = new Dictionary<string, int>()
            {
                { Helper.Store, _model.Store},
                { Helper.Months, ((_model.Date.Year - _model.MinYear) * 12) + _model.Date.Month},
                { Helper.CustomerDay, (_model.Customer * 100) + _model.Date.Day}
            };

            _result = new StringBuilder();

            var bites = GenerateBites();

            GenerateCode(bites);

        }

    }
}
