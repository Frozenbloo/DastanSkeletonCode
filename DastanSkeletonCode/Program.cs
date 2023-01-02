using System;
using System.Collections.Generic;

namespace DastanSkeletonCode
{
        class Program
        {
            static void Main(string[] args)
            {
                Dastan ThisGame = new Dastan(6, 6, 4);
                ThisGame.PlayGame();
                Console.WriteLine("Goodbye!");
                Console.ReadLine();
            }
        }
    }

