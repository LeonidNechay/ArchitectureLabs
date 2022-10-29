using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new FlyweightFactory();

            var number1 = factory.GetFlyweight("228");
            number1.Show();
            var number2 = factory.GetFlyweight("196");
            number2.Show();
            var number3 = factory.GetFlyweight("3682094");
            number3.Show();
        }
        public abstract class AbstractFlyweight
        {
            public abstract void Show();
        }

        public class Flyweight : AbstractFlyweight
        {
            private string key;
            private List<List<string>> numbers = new List<List<string>> {
                new List<string>{"###", "# #", "# #", "# #", "###" },
                new List<string>{"## ", " # ", " # ", " # ", "###" },
                new List<string>{"###", "  #", "###", "#  ", "###" },
                new List<string>{"###", "  #", "###", "  #", "###" },
                new List<string>{"# #", "# #", "###", "  #", "  #" },
                new List<string>{"###", "#  ", "###", "  #", "###" },
                new List<string>{"###", "#  ", "###", "# #", "###" },
                new List<string>{"###", "  #", "  #", "  #", "  #" },
                new List<string>{"###", "# #", "###", "# #", "###" },
                new List<string>{"###", "# #", "###", "  #", "###" }
            };

            public Flyweight(string key)
            {
                this.key = key;
            }
            public override void Show()
            {
                List<int> array = new List<int> { };
                for (int i = 0; i < key.Length; i++)
                {
                    array.Add(Convert.ToInt32(Char.GetNumericValue(key[i])));
                }


                for (int j = 0; j < numbers[0].Count; j++)
                {
                    for (int i = 0; i < array.Count(); i++)
                    {
                        Console.Write($"{numbers[array[i]][j]}");
                        Console.Write(" ");
                    }
                    Console.WriteLine("");

                }
                Console.WriteLine("");
            }
        }

        public class FlyweightFactory
        {
            private Dictionary<string, Flyweight> flyweights = new Dictionary<string, Flyweight>();
            public Flyweight GetFlyweight(string key)
            {
                Flyweight flyweight = null;
                if (flyweights.ContainsKey(key))
                {
                    flyweight = flyweights[key];
                }
                else
                {
                    flyweight = new Flyweight(key);
                    flyweights.Add(key, flyweight);
                }
                return flyweight;
            }
        }
    }
}
