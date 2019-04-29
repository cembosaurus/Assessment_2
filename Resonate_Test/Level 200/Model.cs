using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Resonate_Test.Level_200
{
    class Model
    {
        private const int _minYear = 1990;
        private const int _maxYear = 2090;
        private const int _maxCustomersADay = 10000;

        private int _store;
        private DateTime _date;
        private int _customer;
        private string _code;
        private bool b1 = false, b2 = false, b3 = false, b4 = false;


        public Model()
        {
            Initialize();
        }



        public int Store
        {
            get { return _store; }
            set {
                if (value >= 1 && value <= 200)
                    b1 = true;
                else Initialize();

                _store = value;
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set {
                if (value.Year >= _minYear && value.Year <= _maxYear)
                    b2 = true;
                else Initialize();

                _date = value;
            }
        }

        public int Customer
        {
            get { return _customer; }
            set {
                if (value >= 1 && value <= _maxCustomersADay)
                    b3 = true;
                else Initialize();

                _customer = value;
            }
        }

        public string Code
        {
            get { return _code; }
            set {
                if(value == null) Initialize();
                if (new Regex(@"\s+").Matches(value).Count == 0 && (value.Length > 5 && value.Length < 10))
                    b4 = true;

                _code = value;
            }
        }
        
        public bool DataIsValid
        {
            get { return (b1 && b2 && b3) ? true : false; }
        }

        public bool CodeIsValid
        {
            get { return b4 ? true : false; }
        }

        public int MaxYear { get { return _maxYear; } }

        public int MinYear { get { return _minYear; } }

        public int MaxCustomersADay { get { return _maxCustomersADay; } }



        private void Initialize()
        {
            _store = _customer = 0;
            _date = new DateTime();
            _code = string.Empty;
            b1 = b2 = b3 = b4 = false;
        }


    }
}
