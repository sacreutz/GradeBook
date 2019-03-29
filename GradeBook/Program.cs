using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.IO;

namespace Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Speak("What\'s up? I am building this because I love to code");

            GradeBook book = CreateGradeBook();

            try
            {
                string[] lines = File.ReadAllLines("grades.txt");
                foreach (string line in lines)
                {
                    float grade = float.Parse(line);
                    book.AddGrade(grade);
                }

            } 

            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not locate the file");
                return;
            }

            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("No access");
                return;
            }

            book.WriteGrades(Console.Out); 

            book.NameChanged += OnNameChanged;
            book.NameChanged += OnNameChanged2;

            try
            {
              //  Console.WriteLine("Please enter a name for the book");
              //  book.Name = Console.ReadLine();
              

            }

            catch (ArgumentException)
            {
                Console.WriteLine("Invalid Name");
            }
            Console.WriteLine(book.Name);

            GradeStatistics stats = book.ComputeStatistics();
            Console.WriteLine(stats.AverageGrade);

            WriteBytes(stats.AverageGrade); 
            Console.WriteLine("{0}, {1}", stats.LetterGrade, stats.Description);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static GradeBook CreateGradeBook()
        {
            GradeBook book = new ThrowAwayGradeBook("Sophie's Book");
            return book;
        }

        private static void OnNameChanged2(object sender, NameChangedEventArgs args)
        {
            Console.WriteLine("****");
        }

        private static void OnNameChanged(object sender, NameChangedEventArgs args)
        {
            Console.WriteLine("Name changed from {0} to {1}", args.OldValue, args.NewValue);
        }

    

        private static void WriteByteArray(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            WriteByteArray(bytes);
        }


        private static void WriteBytes(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            WriteByteArray(bytes); 
        }


        private static void WriteByteArray(byte[] bytes)
        {
            foreach (byte b in bytes)
            {
                Console.Write("0x{0:X} ", b);
            }
            Console.WriteLine();
        }
    }
}
 