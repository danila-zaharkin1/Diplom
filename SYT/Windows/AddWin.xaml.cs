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
using SYT.Entities;

namespace SYT.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddWin.xaml
    /// </summary>
    public partial class AddWin : Window
    {
        SYTEntities db = new SYTEntities();
        private Supplies newSupplies;
        public AddWin()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string number = TextBoxNumber.Text;
            DateTime date = DateTime.Now; // Получаем текущую дату
            string supplier = TextBoxSupplier.Text;
            decimal sum;

            // Пробуем преобразовать строку в число
            if (!int.TryParse(TextBoxNumber.Text, out int parsedNumber))
            {
                MessageBox.Show("Номер поставки должен быть целым числом.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TextBoxSupplier.Text))
            {
                MessageBox.Show("Введите поставщика.");
                return;
            }

            if (!decimal.TryParse(TextBoxSum.Text, out sum))
            {
                MessageBox.Show("Неверный формат суммы.");
                return;
            }

            // Проверяем, не существует ли уже такого номера поставки
            if (db.Supplies.Any(s => s.Number == parsedNumber))
            {
                MessageBox.Show("Номера поставок не должны совпадать");
                return;
            }

            // Создаем новый объект Supplies
            Supplies newSupplies = new Supplies()
            {
                Supply_Number = Guid.NewGuid(),
                Number = parsedNumber, // Используем введенный номер поставки
                Supply_Date = date, // Используем текущую дату
                Supplier = supplier,
                Amount = sum
            };

            // Добавляем новый объект в базу данных и сохраняем изменения
            db.Supplies.Add(newSupplies);
            db.SaveChanges();
            MessageBox.Show("Поставка упешно внесена в базу");

            // Закрываем окно
            Close();
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
