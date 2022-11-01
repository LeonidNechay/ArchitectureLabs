using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Teacher teacher1 = new Teacher("Maria", 5000, 3);
            Teacher teacher2 = new Teacher("Paul", 7000, 5);
            Teacher teacher3 = new Teacher("John", 8000, 8);
            Teacher teacher4 = new Teacher("Ana", 9000, 10);
            Chair sa_chair = new Chair("SA");
            Chair bio_chair = new Chair("Bio");
            Faculty fmdt_faculty = new Faculty("FMDT");
            Faculty bio_faculty = new Faculty("Bio");
            University uzhnu_university = new University("UzhNU");
            sa_chair.Add(teacher1);
            sa_chair.Add(teacher2);
            bio_chair.Add(teacher3);
            bio_chair.Add(teacher4);
            fmdt_faculty.Add(sa_chair);
            bio_faculty.Add(bio_chair);
            uzhnu_university.Add(fmdt_faculty);
            uzhnu_university.Add(bio_faculty);
            Console.WriteLine(uzhnu_university);
            var visitor2 = new RaiseSalary(6, 50);
            uzhnu_university.Accept(visitor2);
            Console.WriteLine(uzhnu_university);
            var visitor1 = new CountSalary();
            sa_chair.Accept(visitor1);
            bio_faculty.Accept(visitor1);
            uzhnu_university.Accept(visitor1);
        }
    }

    public interface IVisitable
    {
        void Accept(ICompositeVisitor visitor);
    }

    public interface ICompositeVisitor
    {
        void VisitTeacher(Teacher teacher);
        void VisitChair(Chair chair);
        void VisitFaculty(Faculty faculty);
        void VisitUniversity(University university);
    }
    public interface ICompositeComponent
    {
        ICompositeComponent Add(ICompositeComponent Component);

        ICompositeComponent Remove(ICompositeComponent Component);

        bool IsComposite { get; }
    }
    public class CompositeComponent : ICompositeComponent, IVisitable
    {
        public string Name;

        public CompositeComponent(string Name)
        {
            this.Name = Name;
        }
        public virtual ICompositeComponent Add(ICompositeComponent Component)
        {
            throw new Exception($"Can't add {Component} to {this}");
        }

        public virtual ICompositeComponent Remove(ICompositeComponent Component)
        {
            throw new Exception($"Can't remove {Component} to {this}");
        }

        public virtual bool IsComposite { get { return false; } }

        public virtual string ToString(int Level)
        {
            return $"{new String('.', 3 * Level)} {this}\n";
        }
        public virtual void Accept(ICompositeVisitor visitor)
        {
            throw new Exception("Not implemented");
        }
    }
    public class Teacher : CompositeComponent
    {
        public float salary;
        public float experience;
        
        public Teacher(string Name, float salary, float experience) : base(Name)
        {
            this.salary = salary;
            this.experience = experience;
        }
        public override string ToString()
        {
            return $"{Name} has {salary} of salary and {experience} years of experience";
        }
        public override void Accept(ICompositeVisitor visitor)
        {
            visitor.VisitTeacher(this);
        }
    }
    public class Chair : CompositeComponent
    {
        public List<Teacher> teachers = new List<Teacher>();

        public Chair(string Name) : base(Name) { }
        public ICompositeComponent Add(Teacher teacher)
        {
            teachers.Add(teacher as Teacher);
            return this;
        }
        public ICompositeComponent Remove(Teacher teacher)
        {
            teachers.ForEach(teachers => this.Remove(teacher));
            teachers.Remove(teacher as Teacher);
            return this;
        }
        public override bool IsComposite { get { return true; } }
        public override string ToString()
        {
            string text = "";
            text += $"Chair: {Name}\n";
            foreach (Teacher teacher in teachers)
            {
                text += "\n" + teacher.ToString();
            }
            return text;
        }
        public override void Accept(ICompositeVisitor visitor)
        {
            visitor.VisitChair(this);
        }
    }
    public class Faculty : CompositeComponent
    {
        public List<Chair> chairs = new List<Chair>();

        public Faculty(string name) : base(name) { }
        public ICompositeComponent Add(Chair chair)
        {
            chairs.Add(chair as Chair);
            return this;
        }
        public ICompositeComponent Remove(Chair chair)
        {
            chairs.ForEach(teachers => this.Remove(chair));
            chairs.Remove(chair as Chair);
            return this;
        }
        public override bool IsComposite { get { return true; } }
        public override string ToString()
        {
            string str = "";
            str += $"Faculty: {Name}\n";
            foreach (Chair chair in chairs)
            {
                str += "\n" + chair.ToString() + "\n";
            }
            return str;
        }
        public override void Accept(ICompositeVisitor visitor)
        {
            visitor.VisitFaculty(this);
        }
    }

    public class University : CompositeComponent
    {
        public List<Faculty> faculties = new List<Faculty>();

        public University(string name) : base(name) { }
        public ICompositeComponent Add(Faculty faculty)
        {
            faculties.Add(faculty as Faculty);
            return this;
        }
        public ICompositeComponent Remove(Faculty faculty)
        {
            faculties.ForEach(teachers => this.Remove(faculty));
            faculties.Remove(faculty as Faculty);
            return this;
        }
        public override bool IsComposite { get { return true; } }
        public override string ToString()
        {
            string str = string.Empty;
            str += $"University: {Name}\n";
            foreach (Faculty faculty in faculties)
            {
                str += "\n" + faculty.ToString();
            }
            return str;
        }

        public override void Accept(ICompositeVisitor visitor)
        {
            visitor.VisitUniversity(this);
        }
    }
    public class CountSalary : ICompositeVisitor
    {
        public void VisitTeacher(Teacher teacher)
        {

        }
        public void VisitChair(Chair chair)
        {
            float sum = chair.teachers.Select(teacher => teacher.salary).Sum();
            Console.WriteLine($"Total sum at {chair.Name}: {sum} \n");
        }
        public void VisitFaculty(Faculty faculty)
        {
            float sum = faculty.chairs.Select(chair => chair.teachers.Select(teacher => teacher.salary).Sum()).Sum();
            Console.WriteLine($"Total sum at {faculty.Name}: {sum}\n");
        }
        public void VisitUniversity(University university)
        {
            float sum = university.faculties.Select(faculty => faculty.chairs.Select(chair => chair.teachers.Select(teacher => teacher.salary).Sum()).Sum()).Sum();
            Console.WriteLine($"Total sum at {university.Name}: {sum}\n");
        }
    }
    public class RaiseSalary : ICompositeVisitor
    {
        public int experiance;
        public float persantages;
        public RaiseSalary(int experiance, float persantages)
        {
            this.experiance = experiance;
            this.persantages = persantages;
        }
        public void VisitTeacher(Teacher teacher)
        {
            if(this.experiance <= teacher.experience)
            {
                teacher.salary *= persantages / 100 + 1;
            }
        }
        public void VisitChair(Chair chair)
        {
            chair.teachers.ForEach(teacher => teacher.Accept(this));
        }
        public void VisitFaculty(Faculty faculty)
        {
            faculty.chairs.ForEach(chair => chair.Accept(this));
        }
        public void VisitUniversity(University university)
        {
            university.faculties.ForEach(faculty => faculty.Accept(this));
        }
    }
}
