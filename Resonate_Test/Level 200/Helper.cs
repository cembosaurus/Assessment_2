using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resonate_Test.Level_200
{

    public static class Helper
    {
        public static readonly string Store = "store";
        public static readonly string Date = "date";
        public static readonly string Customer = "customer";
        public static readonly string Months = "months";
        public static readonly string CustomerDay = "customer_day";



        public static int BinaryLength(this int value)
        {
            int length = 1, root = 1;

            int Calculate()
            {
                root *= 2;
                length++;

                if (!(root * 2 > value))
                    Calculate();

                return length;
            }

            return value <= 1 ? 1 : Calculate();
        }


        public static int BinaryLengthByBytes(this int value)
        {
            int length = 1, root = 1, mod = 0;

            int Calculate()
            {
                root *= 2;
                length++;
                mod = root % length;
                var exp = mod == 0 || mod == 8 || mod == 32;

                if (!(root * 2 > value && exp))
                    Calculate();

                return length;
            }

            return value <= 15 ? length + 3 : Calculate();
        }


        public static bool IsBetween(this int value, int a, int b)
        {
            if (a > b)
            {
                a = a + b;
                b = a - b;
                a = a - b;
            }

            return value >= a && value <= b ? true : false;
        }
    }
}
