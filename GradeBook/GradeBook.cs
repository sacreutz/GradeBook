using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public class GradeBook
    {

        public GradeBook(string name = "There is no name")
        {
            _name = name;
            grades = new List<float>();
        }

        public void AddGrade(float grade)
        {
            if (grade >= 0 && grade <= 100)
            {
                grades.Add(grade);
            }
        }

        public GradeStatistics ComputeStatistics()
        {
           GradeStatistics stats = new GradeStatistics();
         
           float sum = 0f;

            foreach (float grade in grades)
            {
                stats.HighestGrade = Math.Max(grade, stats.HighestGrade);
                stats.LowestGrade = Math.Min(grade, stats.LowestGrade); 
                sum += grade; 
            }

            stats.AverageGrade = sum / grades.Count; 

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
                if(!String.IsNullOrEmpty(value))
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

        private List<float> grades;
    }
}
