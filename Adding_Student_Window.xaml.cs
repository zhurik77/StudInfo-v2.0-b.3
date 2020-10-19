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
    /// Логика взаимодействия для Adding_Student_Window.xaml
    /// </summary>
    public partial class Adding_Student_Window : Window
    {
        string Name, Group, Date;
        public Adding_Student_Window()
        {
            InitializeComponent();
        }

        private void Cancel_Adding(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Отменить создание студента?", "StudInfo v2.0 b.1", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();

            }
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

        private void Save_Student_Click(object sender, RoutedEventArgs e)
        {
            Name = FIO_Student.Text.ToString();
            Group = Group_Student.Text.ToString();
            DateTime? DT_Date = Date_Student.SelectedDate;
            Date = DT_Date.Value.ToShortDateString();
            Add_S();
            MessageBox.Show("Обновите окно для отображения изменений", "", MessageBoxButton.OK);
            this.Close();
        }

    }
}
