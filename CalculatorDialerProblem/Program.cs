using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace CalculatorDialerProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            //var watch = new System.Diagnostics.Stopwatch();
            TimeSpan startTime;
            TimeSpan duration;

            classExample obj = new classExample();
            obj.display();
            obj.Check();

            //watch.Start();
            startTime = Process.GetCurrentProcess().Threads[0].UserProcessorTime;
            obj.Calculate();
            //watch.Stop();
            duration =Process.GetCurrentProcess().Threads[0].UserProcessorTime.Subtract(startTime);

            Console.WriteLine("The end");
            Console.WriteLine("Elapsed Time(seconds): " + duration.TotalSeconds);
            Console.ReadKey();

        }
    }

    class classExample
    {
        int N, M, O;
        int result, temp, maxDigits;
        bool found = false;
        List<int> resultList = new List<int>();
        List<int> digitList = new List<int>();
        List<char> operatorList = new List<char>();
        List<string> strMinTouch = new List<string>();
        string[] strDigits = { "0", "2", "3", "4", "7", "8" }; //Console.ReadLine().Split(",");
        string[] strOps = { "+", "*", "-", "/" }; //Console.ReadLine().Split(",");
        char[] arrOpr;
        int[] arrDigit;
        string[] arrComputn;
        int MinTouch=-1;
        bool possible = false;

        public classExample()
        {
            string[] numbers = { "6", "4", "5", "18" };//Console.ReadLine().Split(",");
            N = Convert.ToInt32(numbers[0]);
            M = Convert.ToInt32(numbers[1]);
            O = Convert.ToInt32(numbers[2]);
            result = Convert.ToInt32(numbers[3]);
            temp = result;
            maxDigits = O - 2; // = and atleast one operator
            MinTouch = O - 1;

            File.Delete("log.txt");

            while (temp > 10)
            {
                resultList.Add(temp % 10);
                temp = temp / 10;

            }
            resultList.Add(temp);

            Console.WriteLine("Result list: ");
            foreach (var item in resultList)
            {
                Console.WriteLine(item);
            }
            foreach (var item in strDigits)
            {
                digitList.Add(Convert.ToInt32(item));
            }
            Console.WriteLine("Digits list:");
            foreach (var item in digitList)
            {
                Console.WriteLine(item);
            }
            foreach (var item in strOps)
            {
                operatorList.Add(Convert.ToChar(item));
            }
            Console.WriteLine("Ops list:");
            foreach (var item in operatorList)
            {
                Console.WriteLine(item);
            }

            arrOpr = operatorList.ToArray();
            arrDigit = digitList.ToArray();
            arrComputn = new string[O - 1];
        }

        public void display()
        {
            Console.WriteLine("Entered:");
            Console.WriteLine(N);
            Console.WriteLine(M);
            Console.WriteLine(O);
            Console.WriteLine(result);
        }

        public void Check()
        {
            Console.WriteLine("Checking if result can be formed with available digits...");
            foreach (var res in resultList)
            {
                found = false;
                foreach (var dig in digitList)
                {
                    if (res == dig)
                        found = true;
                }
            }

            if (found)
                Console.WriteLine("Found all digits!");
            else
                Console.WriteLine("Not found, continue calculation..");

            Console.ReadKey();

        }

        public void Calculate()
        {
            //int j = 0;
            for (int i = 0; i < arrDigit.Length; i++)
            {
                arrComputn = new string[MinTouch];
                arrComputn[0] = Convert.ToString(arrDigit[i]);
                NextNumber(1);
               // if (NextNumber(1))
                  //  i = 0;
            }

            if (!possible)
                Console.WriteLine("Not Possible!");
            else
                Console.WriteLine("Got it, MinTouch : " + MinTouch);
            foreach (var item in strMinTouch)
            {
                Console.WriteLine(item);
            }
        }

        void NextOperator(int i)
        {
            for (int op = 0; op < arrOpr.Length; op++)
            {
                arrComputn[i] = arrOpr[op].ToString();
                if ((i + 1) != (MinTouch))
                {
                    NextNumber(i + 1);
                   // return (NextNumber(i + 1));
                }
            }

           // return false;
        }

        void NextNumber(int i)
        {
            if (int.TryParse(arrComputn[i - 1], out int num1))
            {
                NextOperator(i);
               // return (NextOperator(i));
            }
            for (int n = 0; n < arrDigit.Length; n++) //Adding random comments
            {
                arrComputn[i] = arrDigit[n].ToString();
                Compute(i);
               // if (Compute(i))
                 //   return true;

                if ((i + 1) != (MinTouch))
                {
                    NextNumber(i + 1);
                   // return(NextNumber(i + 1));
                }
            }

          //  return false;
        }
        void Compute(int i)
        {
            string str = " ";
            for (int index = 0; index <= i; index++)
                str += arrComputn[index];

            DataTable dt = new DataTable();
            double answer = Convert.ToDouble(dt.Compute(str, ""));
            Console.WriteLine(str + " : " + answer);
            Log(str + " : " + answer);
            if (Convert.ToDouble(result).Equals(answer))
            {
                possible = true;
                strMinTouch.Add(str + " : " + answer + " Touch: " + str.Trim().Length);
                Console.WriteLine("Got It!"); //Log("Got It!");
                if (str.Trim().Length < MinTouch)
                {
                    MinTouch = str.Trim().Length;
                    Log("MinTouch :" + MinTouch);
                    // return true; //Found a new MinTouch
                }
            }

            //return false;
        }

        void Log(string logMsg)
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                w.WriteLine("{0}", logMsg);
            }
        }

    }
}
