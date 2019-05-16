using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace FridgeAPI
{
    public class Fridge : IFridge
    {
        private string[] products = { "Milk", "Eggs", "Tomatoes" };
        private int[] deltas = { -1, -2, -3, 5 };
        private Random r;

        public IEnumerable<KeyValuePair<string, int>> GetActivityLog(Guid fridgeId)
        {
            r = new Random(fridgeId.GetHashCode());
            
            Dictionary<string, int> current = new Dictionary<string, int>{
                { "Milk", 0 },
                { "Eggs", 0 },
                { "Tomatoes", 0 }
            };

            for(int i = 0; i < 15; i++)
            {
                string item = products[r.Next(products.Length)];

                int[] goodDeltas = deltas.Where(d => current[item] + d >= 0).ToArray();

                int delta = goodDeltas[r.Next(goodDeltas.Length)];

                current[item] += delta;

                yield return new KeyValuePair<string, int>(item, delta);
            }
        }
    }
}
