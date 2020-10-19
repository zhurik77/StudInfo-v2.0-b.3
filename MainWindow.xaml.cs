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

namespace StudInfo_v2._0_b._3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StudentDBEntities db = new StudentDBEntities();
            //this.gridStudents.ItemsSource = db.Students.ToList();
            var docs = from d in db.Students
                       select new
                       {
                           ID = d.Id,
                           Студент = d.S_Name,
                           Группа = d.S_Group,
                           Дата = d.S_Date
                       };
            this.gridStudents.ItemsSource =docs.ToList(); // Вывод базовой структуры БД из .mdf файла с использованием EntityFramework, но с измененияем названия столбцов в DataGrid
        }
        private void Load_StudentDB()
        {
            StudentDBEntities db = new StudentDBEntities();
            var docs = from d in db.Students
                       select new
                       {
                           ID = d.Id,
                           Студент = d.S_Name,
                           Группа = d.S_Group,
                           Дата = d.S_Date
                       };
            this.gridStudents.ItemsSource = docs.ToList();

        }
        private void Add_Student(object sender, RoutedEventArgs e)
        {
            Adding_Student_Window adding_Student_Window = new Adding_Student_Window();
            adding_Student_Window.Show();
        }
        private void Update_StudentDB_Click(object sender, RoutedEventArgs e)
        {
            Load_StudentDB();
        }

        private void Open_Diary(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Student(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Student(object sender, RoutedEventArgs e)
        {
            Edit_Student_Window edit_Student_Window = new Edit_Student_Window();
            edit_Student_Window.Show();
        }

        private void gridStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(this.gridStudents.SelectedItem);
        }
    }
}
