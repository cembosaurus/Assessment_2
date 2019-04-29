using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resonate_Test.Level_200
{
    class Service
    {

        private Model _model;
        private bool _codeState;
        private bool _dataState;


        public Service()
        {
            
        }

        public Service(Model model)
        {
            _model = model;

            Initialize();
        }




        public bool DataState => _dataState;

        public bool CodeState => _codeState;


        public Model Result
        {
            get
            {
                if (_model != null)
                {
                    if (_dataState)
                        return new DataToCode(_model).Result;
                    else if (_codeState)
                        return new CodeToData(_model).Result;

                    return _model;
                }

                return new Model();
            }
        }



        public Model GetCode(Model model)
        {
                return _dataState ? new DataToCode(model).Result : model;
        }


        public Model GetData(Model model)
        {
            return _codeState ? new CodeToData(model).Result : model;
        }



        private void Initialize()
        {
            _codeState = _model.CodeIsValid;
            _dataState = _model.DataIsValid;
        }

    }
}
