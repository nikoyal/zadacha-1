using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WpfApplication2.Models
{
    class VetContext : DbContext
    {
        public VetContext() : base() { }
        public DbSet<Dog> Dogs { get; set; } 
        public DbSet<Cat> Cats { get; set; } 
        public DbSet<Exam> Exams { get; set; } 

    }

    class Dog
    {
        [Key] // Атрибут за ключово поле
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; } 
        public double Weight { get; set; }
        public DateTime Birth { get; set; }
        public string Breed { get; set; }
        public List<Exam> Exams { get; set; }
    }

    // Mодела за котка
    class Cat
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public double Weight { get; set; }
        public DateTime Birth { get; set; }
        public string Breed { get; set; }
        public List<Exam> Exams { get; set; }
    }

    
    class Exam
    {
        [Key] 
        public int Id { get; set; } 
        public DateTime ExamDate { get; set; }
    }
}