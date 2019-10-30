using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resonate_Test.Level_100;
using Resonate_Test.Level_200;

namespace Resonate_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nAlgorithm demonstration by Milos Bencek.");

            int sw = 0;

            do
            {
                sw = Menu();

                switch (sw)
                {
                    case 1:
                        {
                            RemoveDuplicates();
                            break;
                        }
                    case 2:
                        {
                            Permutation();
                            break;
                        }
                    case 3:
                        {
                            CodeGenerator();
                            break;
                        }
                    default:
                        {

                            break;
                        }

                }
                
            }
            while (sw.IsBetween(1, 3));

            Console.WriteLine("\n\nThank you.");
        }



        private static int Menu()
        {
            Console.WriteLine("\n\n--------------------------------------------------\n" +
                "Please selet:\n\n" +
                "Remove duplicates from string: 1\n" +
                "Permutation test: 2\n" +
                "Code generator: 3\n" +
                "Exit: Any\n" +
                "--------------------------------------------------\n");
            int i;
            return Int32.TryParse(
                Console.ReadKey(true).KeyChar.ToString(), out i) 
                ? i 
                : 0;
        }


        private static void RemoveDuplicates()
        {
            Console.WriteLine("Unsorted list: 12,11,12,21,41,43,21");

            var _unsortedList = new Stack<int>( new int[] {
                12, 11, 12, 21, 41, 43, 21
            });

            var service100 = new Level_100.Service();
            var arr = service100.RemoveDuplicate(_unsortedList);
            Console.WriteLine("Result:        {0}", string.Join(",", arr));
        }


        private static void Permutation()
        {
            Console.WriteLine("Insert first string:");
            var str1 = Console.ReadLine();
            Console.WriteLine("Insert second string:");
            var str2 = Console.ReadLine();

            var service100 = new Level_100.Service();
            Console.WriteLine("\nStrings are{0} permutations of eachother.", service100.Permutation(str1, str2) ? "":" NOT");
        }


        private static void CodeGenerator()
        {
            Console.WriteLine("Insert Store number (1 - 200):");
            int store = int.TryParse(Console.ReadLine(), out store) && store.IsBetween(1, 200) ? store : 0;
            Console.WriteLine("Store number is {0}", store != 0 ? store.ToString() : "out of range.");


            Console.WriteLine("Insert Year (1990 - 2090):");
            int year = int.TryParse(Console.ReadLine(), out year) && year.IsBetween(1990, 2090) ? year : 0;
            Console.WriteLine("Year is {0}", year != 0 ? year.ToString() : "out of range.");


            Console.WriteLine("Insert Month:");
            int month = int.TryParse(Console.ReadLine(), out month) && month.IsBetween(1, 12) ? month : 0;
            Console.WriteLine("Month is {0}", month != 0 ? month.ToString() : "out of range.");

            Console.WriteLine("Insert Day:");
            int day = int.TryParse(Console.ReadLine(), out day) && day.IsBetween(1, 31) ? day : 0;
            Console.WriteLine("Day is {0}", day != 0 ? day.ToString() : "out of range.");

            Console.WriteLine("Insert Customer number (1 - 10000):");
            int customer = int.TryParse(Console.ReadLine(), out customer) && customer.IsBetween(1, 10000) ? customer : 0;
            Console.WriteLine("Customer's number is {0}", customer != 0 ? customer.ToString() : "out of range.");


            if (VerifyData(store, year, month, customer))
            {

                var date = new DateTime(year, month, day);

                var model = new Model()
                {
                    Store = store,
                    Date = date,
                    Customer = customer
                };

                var service = new Level_200.Service(model);

                Console.WriteLine("\n{0}\n"
                    , !service.DataState
                    ? "Entered data was invalid !"
                    : ("Generated code is:   " + service.Result.Code));

                // Code verification:

                var userInput = service.Result.Code;
                model = new Model();
                model.Code = userInput;
                service = new Level_200.Service(model);

                if (service.CodeState)
                {
                    Console.WriteLine("Press any key for code verification of code: {0}", model.Code);
                    Console.ReadLine();

                    var data = service.GetData(model);

                    Console.WriteLine(
                        "Entered store number was: {0}\n" +
                        "Date of issue was:        {1}\n" +
                        "Customer number was:      {2}\n"
                        , data.Store, data.Date.ToShortDateString(), data.Customer
                        );
                }

            }
            else Console.WriteLine("\n\nYou have entered incorect data ! Please try again.\n\n");

            Console.ReadLine();


            bool VerifyData(params int[] data)
            {
                foreach (int i in data)
                    if (i == 0)
                        return false;
                return true;
            }
        }

    }
}
