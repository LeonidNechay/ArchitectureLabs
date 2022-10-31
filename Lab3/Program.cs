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

            var number1 = factory.GetFlyweight("abcd");
            number1.Show();
            var number2 = factory.GetFlyweight("196");
            number2.Show();
            var number3 = factory.GetFlyweight("3682094");
            number3.Show();
        }


        public class Flyweight
        {
            private string key;
            private Dictionary<char, List<string>> numbers = new Dictionary<char, List<string>> {
                ['0']= new List<string>{"###", "# #", "# #", "# #", "###" } ,
                ['1']= new List<string>{"## ", " # ", " # ", " # ", "###" } ,
                ['2']= new List<string>{"###", "  #", "###", "#  ", "###" } ,
                ['3']= new List<string>{"###", "  #", "###", "  #", "###" } ,
                ['4']= new List<string>{"# #", "# #", "###", "  #", "  #" } ,
                ['5']= new List<string>{"###", "#  ", "###", "  #", "###" } ,
                ['6']= new List<string>{"###", "#  ", "###", "# #", "###" } ,
                ['7']= new List<string>{"###", "  #", "  #", "  #", "  #" } ,
                ['8']= new List<string>{"###", "# #", "###", "# #", "###" } ,
                ['9']= new List<string>{"###", "# #", "###", "  #", "###" } ,
                ['a']= new List<string>{" # ", "# #", "###", "# #", "# #" } ,
                ['b']= new List<string>{"## ", "# #", "## ", "# #", "## " } ,
                ['c']= new List<string>{" ##", "#  ", "#  ", "#  ", " ##" } ,
                ['d']= new List<string>{"## ", "# #", "# #", "# #", "## " } ,
            };

            public Flyweight(string key)
            {
                this.key = key;
            }
            public void Show()
            {
                List<char> array = new List<char> { };
                for (int i = 0; i < key.Length; i++)
                {
                    array.Add(key[i]);
                }

                for(int j = 0; j < 5; j++)
                {
                    for (int i = 0; i < array.Count(); i++)
                    {
                        foreach (var item in numbers)
                        {
                            if (item.Key == array[i])
                            {
                                Console.Write($"{item.Value[j]}");
                                Console.Write(" ");
                            }
                        }
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
