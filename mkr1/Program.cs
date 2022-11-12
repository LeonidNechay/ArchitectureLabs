using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace mkr1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User("Ivan", 1102452930, 5000);
            CheckTotalSum checkTotalSum = new CheckTotalSum();
            Console.WriteLine(user);
            user.AddTotalSum(1000);
            user.Accept(checkTotalSum);
            user.AddTotalSum(7382);
            user.Accept(checkTotalSum);
        }
    }
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }

    public class User : IVisitable
    {
        public string name;
        public int individualNumber;
        public float totalSum;

        public User(string name, int individualNumber, float totalSum)
        {
            this.name = name;
            this.individualNumber = individualNumber;
            this.totalSum = totalSum;
        }
        public void AddTotalSum(float money)
        {
            totalSum += money;
        }
        public void Accept(IVisitor visitor)
        {
            visitor.VisitUser(this);
        }
        public override string ToString()
        {
            return $"Ім'я користувача - {name}; ІПН - {individualNumber}; Сума поповнень - {totalSum} грн.";
        }
    }


    public interface IVisitor
    {
        void VisitUser(User user);
    }

    public class CheckTotalSum : IVisitor
    {
        public void VisitUser(User user)
        {
            if(user.totalSum >= 10000)
            {
                Console.WriteLine($"{user.name}({user.individualNumber}), з сумою поповнень {user.totalSum} грн., перевищив поріг в 10000 грн. на {user.totalSum - 10000} грн., {DateTime.Now}");
            }
            else
            {
                Console.WriteLine($"У {user.name} залишилось {10000-user.totalSum} грн. до перевищення погору");
            }
        }
    }
}
