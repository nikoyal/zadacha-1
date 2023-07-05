    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication2.Models;
using System.Diagnostics;

namespace WpfApplication2.ViewModels
{
    class MainWIndowVM
    { 
        private Dog animal;
        public Dog ViewAnimal
        {
            get { return animal; }
            set { animal = value; }
        }
        public List<Exam> Exams { get; set; }
        public MainWIndowVM(Dog a)
        {
            if (a == null)
            {
                animal = new Dog { };
            }
            else
            {
                animal = a;
            }
            Exams = a.Exams;
        }

 
        public Exam ReturnLastExam(Dog a) // Функция за извличане на последен изпит 
        {
            using (var context = new VetContext())
            {
                var dogWithExams = context.Dogs
                    .Include("Exams") // Включва навигационното свойство Exams, използвайки името на свойството като низ
                    .FirstOrDefault(d => d.Id == a.Id);

                if (dogWithExams != null && dogWithExams.Exams.Count > 0)
                {
                    dogWithExams.Exams.Sort((x, y) => x.ExamDate.CompareTo(y.ExamDate)); // Сортира списъка с изпити във възходящ ред според датата на изпит
                    Exam exam = dogWithExams.Exams.Last(); // Взема последния изпит от списъка
                    return exam;
                }

                return null; // Ако няма изпити, връща null
            }
        }
    }
}
