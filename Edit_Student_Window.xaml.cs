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
using System.Windows.Shapes;

namespace StudInfo_v2._0_b._3
{
    /// <summary>
    /// Логика взаимодействия для Edit_Student_Window.xaml
    /// </summary>
    public partial class Edit_Student_Window : Window
    {
        string Name, Group, Date;
        public Edit_Student_Window()
        {
            InitializeComponent();
            StudentDBEntities db = new StudentDBEntities();
            this.gridStudents.ItemsSource = db.Students.ToList();
            /* var docs = from d in db.Students
                       select new
                       {
                           ID = d.Id,
                           Студент = d.S_Name,
                           Группа = d.S_Group,
                           Дата = d.S_Date
                       };
            this.gridStudents.ItemsSource = docs.ToList(); // Вывод базовой структуры БД из .mdf файла с использованием EntityFramework, но с измененияем названия столбцов в DataGrid*/
        }
        private int updatingStudentID = 0;
        private void gridStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridStudents.SelectedIndex >= 0)
            {
                if (this.gridStudents.SelectedItems.Count >= 0)
                {
                    if (this.gridStudents.SelectedItems[0].GetType() == typeof(Student))
                    {
                        Student Edit_S = (Student)this.gridStudents.SelectedItems[0];
                        FIO_Student.Text = Edit_S.S_Name;
                        Date_Student.Text = Edit_S.S_Date;
                        Group_Student.Text = Edit_S.S_Group;
                        updatingStudentID = Edit_S.Id;
                    }
                }
            }
        }
        private void Load_StudentDB()
        {
            StudentDBEntities db = new StudentDBEntities();
            this.gridStudents.ItemsSource = db.Students.ToList();

            /*var docs = from d in db.Students
                       select new
                       {
                           ID = d.Id,
                           Студент = d.S_Name,
                           Группа = d.S_Group,
                           Дата = d.S_Date
                       };
            this.gridStudents.ItemsSource = docs.ToList();*/

        }
        private void Add_S()
        {

            StudentDBEntities db = new StudentDBEntities();
            Student student = new Student()
            {
                S_Name = Name,
                S_Group = Group,
                S_Date = Date
            };
            db.Students.Add(student);
            db.SaveChanges();
        }

        private void Add_Student_Click(object sender, RoutedEventArgs e)
        {
            Name = FIO_Student.Text.ToString();
            Group = Group_Student.Text.ToString();
            DateTime? DT_Date = Date_Student.SelectedDate;
            Date = DT_Date.Value.ToShortDateString();
            Add_S();
            MessageBox.Show("Обновите окно для отображения изменений", "", MessageBoxButton.OK, MessageBoxImage.Warning) ;
            
        }
        

        private void Delete_Student_Click(object sender, RoutedEventArgs e)
        {
            StudentDBEntities db = new StudentDBEntities();
            var r = from d in db.Students
                    where d.Id == updatingStudentID
                    select d;
            Student obj = r.SingleOrDefault();
            if (obj != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить студента?", "StudInfo v2.0 b.3", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        db.Students.Remove(obj);
                        db.SaveChanges();
                        MessageBox.Show("Студент успешно удален", "StudInfo v2.0 b.3");
                        break;
                }
                
            }
            db.SaveChanges();
            MessageBox.Show("Обновите окно для отображения изменений", "", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void Save_Student_Click(object sender, RoutedEventArgs e)
        {
            StudentDBEntities db = new StudentDBEntities();
            var r = from d in db.Students
                    where d.Id == updatingStudentID
                    select d;
            Student obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.S_Name = FIO_Student.Text.ToString();
                DateTime? DT_Date = Date_Student.SelectedDate;
                Date = DT_Date.Value.ToShortDateString();
                obj.S_Date = Date;
                obj.S_Group = Group_Student.Text.ToString();
            }
            db.SaveChanges();
            MessageBox.Show("Обновите окно для отображения изменений", "", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Cancel_Adding(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Load_StudentDB();
        }
    }
}
