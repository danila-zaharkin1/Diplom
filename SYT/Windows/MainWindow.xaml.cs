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
using SYT.Class;
using SYT.Entities;

namespace SYT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SYTEntities db = new SYTEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBoxEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEmail.Text != "Введите номер...") return;
            TextBoxEmail.Text = "";
            TextBoxEmail.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void TextBoxEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxEmail.Text != "") return;
            TextBoxEmail.Text = "Введите номер...";
            TextBoxEmail.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void TextBoxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxPassword.Text != "Введите пароль...") return;
            TextBoxPassword.Text = "";
            TextBoxPassword.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void TextBoxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBoxPassword.Text != "") return;
            TextBoxPassword.Text = "Введите пароль...";
            TextBoxPassword.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void Button_Enter_Click(object sender, RoutedEventArgs e)
        {
            var employee = db.Employees.FirstOrDefault(s => s.Logn == TextBoxEmail.Text && s.Pass == TextBoxPassword.Text);
            if (employee != null)
            {
                new Window1().Show();
                Close();
                IdEmploeeMemory.ID = employee.ID;
            }
            else
            {
                MessageBox.Show("Введите данные");
            }
        }
    }
}
