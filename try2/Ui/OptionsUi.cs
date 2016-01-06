using System;
using System.Collections.Generic;

namespace try2
{
    internal class OptionsUi
    {
        public static Options ReadOptions()
        {
            var alternatives = new Dictionary<char, Options>
            {
                ['1'] = new Options(new Size(9, 9), minesCount: 10, description: "Beginner"),
                ['2'] = new Options(new Size(16, 16), minesCount: 40, description: "Intermediate"),
                ['3'] = new Options(new Size(30, 10), minesCount: 99, description: "Advanced")
            };


            return PickOne(alternatives);
        }

        private static Options PickOne(IDictionary<char, Options> options)
        {
            Print(options);
            var key = Console.ReadKey().KeyChar;
            Options result;
            var isOk = options.TryGetValue(key, out result);
            return isOk
                ? result
                : PickOne(options);
        }

        private static void Print(IDictionary<char, Options> options)
        {
            Console.Clear();
            Console.WriteLine("Difficulcy:");
            foreach (var option in options)
            {
                Console.WriteLine($"{option.Key}:{option.Value}");
            }
            Console.Write("Please enter a number:");
        }
    }
}