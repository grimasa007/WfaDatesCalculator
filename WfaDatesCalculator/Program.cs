using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WfaDatesCalculator
{

    public class Wfa
    {
        public List<Test> ISTests { get; set; }
        public List<Test> OSTests { get; set; }
    }

    public class Test
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //делаем два поля с datepicker для ввода
            var startDate = new DateTime(2010, 1, 1);
            var endDate = new DateTime(2011, 1, 1);

            //еще два поля для ввод Number of tests: OS Percent: 
            Console.WriteLine("Input number of tests: ");
            var numberOfTests = double.Parse(Console.ReadLine());
            Console.WriteLine("Input % for OS: ");
            var osPercent = double.Parse(Console.ReadLine());

            var totalTimeSpan = (endDate - startDate).TotalDays;
            var osIsLengthDays = (100d * totalTimeSpan) / (100 - osPercent + osPercent * numberOfTests);

            var isLengthDays = Math.Round(osIsLengthDays * (100 - osPercent) / 100);
            var osLengthDays = Math.Round(osIsLengthDays *  osPercent/ 100);
            osIsLengthDays = Math.Round(osIsLengthDays);

            //выово строка 1
            Console.WriteLine("OS/IS: " + osIsLengthDays + " IS: "+ isLengthDays + " OS: " + osLengthDays);

            var wfa  = new Wfa()
            {
                ISTests = new List<Test>(),
                OSTests = new List<Test>()
            };

            for (int i = 0; i < numberOfTests; i++)
            {

                if (i == 0)
                {
                    var isTest = new Test()
                    {
                        StartDate = startDate,
                        EndDate = startDate.AddDays(isLengthDays)
                    };

                    wfa.ISTests.Add(isTest);

                    var osTest = new Test()
                    {
                        StartDate = wfa.ISTests[i].EndDate,
                        EndDate = wfa.ISTests[i].EndDate.AddDays(osLengthDays)
                    };

                    wfa.OSTests.Add(osTest);
                }

                if (i > 0)
                {
                    var isTest = new Test()
                    {
                        StartDate = wfa.ISTests[i - 1].StartDate.AddDays(osLengthDays),
                        EndDate = wfa.ISTests[i - 1].StartDate.AddDays(osLengthDays).AddDays(isLengthDays)
                    };

                    wfa.ISTests.Add(isTest);

                    var osTest = new Test()
                    {
                        StartDate = wfa.ISTests[i].EndDate,
                        EndDate = wfa.ISTests[i].EndDate.AddDays(osLengthDays)
                    };

                    wfa.OSTests.Add(osTest);
                }

            }

            //вывод после расчетов
            for (int i = 0; i < numberOfTests; i++)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Test: " + i + Environment.NewLine +
                                                    " IS Start: " + wfa.ISTests[i].StartDate.Date.ToShortDateString() + Environment.NewLine +
                                                    " IS End: " + wfa.ISTests[i].EndDate.Date.ToShortDateString() + Environment.NewLine+
                                                    " OS Start: " + wfa.OSTests[i].StartDate.Date.ToShortDateString() + Environment.NewLine+
                                                    " OS End: " + wfa.OSTests[i].EndDate.Date.ToShortDateString() + Environment.NewLine);
            }

            Console.ReadKey();



        }
    }
}
