﻿// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using Fmines.Geometry;

namespace Fmines.Ui
{
    public static class OptionSelector
    {
        public static Option ReadOptions()
        {
            var alternatives = new Dictionary<char, Option>
            {
                ['1'] = new Option(new Size(9, 9), minesCount: 10, description: "Beginner"),
                ['2'] = new Option(new Size(16, 16), minesCount: 40, description: "Intermediate"),
                ['3'] = new Option(new Size(30, 10), minesCount: 99, description: "Advanced")
            };


            return PickOne(alternatives);
        }

        private static Option PickOne(IDictionary<char, Option> options)
        {
            Print(options);
            var key = Console.ReadKey().KeyChar;
            Option result;
            var isOk = options.TryGetValue(key, out result);
            return isOk
                ? result
                : PickOne(options);
        }

        private static void Print(IDictionary<char, Option> options)
        {
            Console.Clear();
            Console.WriteLine("Difficulty:");
            foreach (var option in options)
            {
                Console.WriteLine($"{option.Key}:{option.Value}");
            }
            Console.Write("Please enter a number:");
        }
    }
}