using System;
using System.Text;

namespace TravellingSalesmanProblem
{
    internal class Program
    {
        static void Main()
        {

            Random random = new Random();

            int[,] prices = new int[100, 100];
            
            for (int i = 0; i < prices.GetLength(0); i++) // filling the 2-dimensional array of prices with random prices
            { 
                for (int j = 0; j < prices.GetLength(1); j++)
                {
                    prices[i, j] = random.Next(5, 51);
                }
            }

            int[] route = new int[100]; 

            for (int i = 0; i < route.GetLength(0); i++) // filling route array with numbers 0-99
            {
                route[i] = i;
            }

            string routeString = String.Join(">", route);
            Console.WriteLine($"First route: ");
            Console.WriteLine(routeString);

            int cost;

            int CalculateCost(int[] array) // calculates cost for the entire route (price from x>y is represented by the index in the prices array)
            {
                int c = 0;
                for (int i = 1; i < route.Length; i++)
                {
                    c += prices[array[i - 1], array[i]];
                }
                return c;
            }

            cost = CalculateCost(route);
            Console.WriteLine("First cost: {0}", cost);

            int[] newRoute = new int[100];
            for (int i = 0; i < route.GetLength(0); i++)
            {
                newRoute[i] = i;
            }

            void Mutation()
            {

                int firstRandomIndex = random.Next(route.Length);
                int secondRandomIndex = random.Next(route.Length);  

                while (firstRandomIndex == secondRandomIndex) // checking if the random indexes are the same - if so, must randomize again
                {
                    secondRandomIndex = random.Next(route.Length);
                }

                (newRoute[firstRandomIndex], newRoute[secondRandomIndex]) = (newRoute[secondRandomIndex], newRoute[firstRandomIndex]); // swapping two elements

                cost = CalculateCost(route); // checking the cost of the old solution
                int newCost = CalculateCost(newRoute); // checking the cost of the new solution

                if (cost > newCost) // if the new solution is cheaper, route is swapped with the new route
                {
                    newRoute.CopyTo(route, 0); 
                }


            }

            int m = 1000000; // number of mutations

            for (int i = 0; i < m; i++)
            {
                Mutation();
            }


            Console.WriteLine();
            Console.WriteLine("Solution after {0} mutations: ", m);
            routeString = String.Join(">", route);
            Console.WriteLine(routeString);
            Console.WriteLine("Cost: {0}", cost);

        }
    }
}