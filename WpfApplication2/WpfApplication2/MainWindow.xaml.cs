using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using WpfApplication2.Models;
using WpfApplication2.ViewModels;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    /// 
    public partial class MainWindow : Window
    {
        int type = 0;
        public MainWindow()
        {
            InitializeComponent();
            //Настройваме функцията MainWindow да се извиква при зареждане на програмата за да създадем нужните обекти и да ги заредим
            Loaded += MainWindow_Loaded;
        }
 
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //извикаме моделите
            VetContext vc = new VetContext();
            //Създаваме два прегледа 
            Exam exam = new Exam();
            exam.ExamDate = new DateTime(2022, 04, 03);
            Exam examtwo = new Exam();
            examtwo.ExamDate = new DateTime(2023, 04, 03);
            //Създаваме куче
            Dog newDog = new Dog();
            newDog.Name = "Рекс";
            newDog.Sex = "М";
            newDog.Breed = "Джар Ръсел Териер";
            newDog.Birth = new DateTime(2020, 01, 03);
            newDog.Weight = 6;
            newDog.Exams = new List<Exam>();
            newDog.Exams.Add(exam);
            newDog.Exams.Add(examtwo);
            //Създаваме котка 
            Cat newCat = new Cat();
            newCat.Name = "Котар";
            newCat.Sex = "Ж";
            newCat.Breed = "Египетска Котка";
            newCat.Birth = new DateTime(2021, 07, 04);
            newCat.Weight = 3;
            //Добавяме промените в бд
            vc.Exams.Add(exam);
            vc.Dogs.Add(newDog);
            vc.Cats.Add(newCat);
            //Записваме промените в бд
            vc.SaveChanges();

            //Използваме ViewModel за да изкараме данните за кучето на View-то
            MainWIndowVM mw = new MainWIndowVM(newDog);

            //Променяме данни на View-то
            var dog = mw.ViewAnimal;
            Name.Text = dog.Name;
            Sex.Text = dog.Sex;
            Weight.Text = dog.Weight.ToString();
            Breed.Text = dog.Breed;
            Birth.Text = dog.Birth.ToString().Substring(0, dog.Birth.ToString().Length - 9).Replace('/','.');
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            VetContext vc = new VetContext();
            //взимаме последното куче и котка
            var dog = vc.Dogs.First();
            var cat = vc.Cats.First();
            //правим проверка и сменяме данните в зависимост от животното което се показва
            if (type == 0)
            {
                type = 1;
                AnimalType.Content = "Досие на Котка:";
                Name.Text = cat.Name;
                Sex.Text = cat.Sex; 
                Weight.Text = cat.Weight.ToString();
                Breed.Text = cat.Breed;
                Birth.Text = cat.Birth.ToString().Substring(0, cat.Birth.ToString().Length - 9).Replace('/', '.');
            }
            else
            {
                type = 0;
                AnimalType.Content = "Досие на Куче:";
                Name.Text = dog.Name;
                Sex.Text = dog.Sex;
                Weight.Text = dog.Weight.ToString();
                Breed.Text = dog.Breed;
                Birth.Text = dog.Birth.ToString().Substring(0, dog.Birth.ToString().Length - 9).Replace('/', '.');
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //Функция за взимане на последен преглед
        {
           
            VetContext vc = new VetContext();
            Dog dog = vc.Dogs.First();
            MainWIndowVM mw = new MainWIndowVM(dog);
            Exam lastExam = mw.ReturnLastExam(dog); // Викаме последния преглед според подаденото куче
            Result.Content = "" + lastExam.ExamDate.ToString().Substring(0, lastExam.ExamDate.ToString().Length - 9).Replace('/', '.'); 
        }
    }
}
