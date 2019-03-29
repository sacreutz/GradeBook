using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public class GradeBook
    {

        public GradeBook(string name = "There is no name")
        {
            Console.WriteLine("gradebook ctor");
            _name = name;
            _grades = new List<float>();
        }

        public void AddGrade(float grade)
        {
            if (grade >= 0 && grade <= 100)
            {
                _grades.Add(grade);
            }
        }

        public void WriteGrades(TextWriter textWriter)
        {
            textWriter.WriteLine("Grades:");
            foreach (float grade in _grades)
            {
                textWriter.WriteLine(grade);
            }
            textWriter.WriteLine("********");
        }

        public virtual GradeStatistics ComputeStatistics()
        {
           GradeStatistics stats = new GradeStatistics();
         
           float sum = 0f;

            foreach (float grade in _grades)
            {
                stats.HighestGrade = Math.Max(grade, stats.HighestGrade);
                stats.LowestGrade = Math.Min(grade, stats.LowestGrade); 
                sum += grade; 
            }

            stats.AverageGrade = sum / _grades.Count; 

           return stats;
        }

        private string _name;

        public string Name
        {
            get
            {
                return _name; 
            }
            set
            {
                if(String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be null or empty");
                }
                {
                    var oldValue = _name;
                    _name = value;
                    if (NameChanged != null)
                    {
                        NameChangedEventArgs args = new NameChangedEventArgs();
                        args.OldValue = oldValue;
                        args.NewValue = value;
                        NameChanged(this, args);
                    }
                }
            }
        }

        public event NameChangedDelegate NameChanged; 

        protected List<float> _grades;

     
    }
}
