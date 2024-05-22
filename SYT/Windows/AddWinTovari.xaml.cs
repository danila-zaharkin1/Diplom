using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AddWinTovari.xaml
    /// </summary>
    public partial class AddWinTovari : Window
    {
        SYTEntities db = new SYTEntities();

        // Обработчик события кнопки сохранения в окне AddWinTovari
        private ObservableCollection<Products> _productsCollection;
        private AddWin _parentWindow;

        public AddWinTovari(AddWin parentWindow, ObservableCollection<Products> productsCollection)
        {
            InitializeComponent();
            _productsCollection = productsCollection;
            _parentWindow = parentWindow;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из текстовых полей
            string barcode = TextBoxCode.Text;
            string productName = TextBoxName.Text;
            decimal price;
            int quantity;

            // Валидация данных
            if (string.IsNullOrEmpty(barcode) || string.IsNullOrEmpty(productName) ||
                !decimal.TryParse(TextBoxPrice.Text, out price) || !int.TryParse(TextBoxQuantity.Text, out quantity))
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.");
                return;
            }

            // Проверка на уникальность barcode
            using (var db = new SYTEntities())
            {
                bool barcodeExists = db.Products.Any(p => p.Barcode == barcode);
                if (barcodeExists)
                {
                    MessageBox.Show("Товар с таким штрих-кодом уже существует.");
                    return;
                }

                // Создаем новый объект Product с уникальным идентификатором
                var productToAdd = new Products()
                {
                    Product_ID = Guid.NewGuid(), // Генерация нового GUID
                    Barcode = barcode,
                    Product_Name = productName,
                    Price = price,
                    Quantity = quantity
                };

                // Добавляем новый продукт в базу данных
                db.Products.Add(productToAdd);
                db.SaveChanges();

                // Добавляем новый продукт в коллекцию
                _productsCollection.Add(productToAdd);
            }

            MessageBox.Show("Товар успешно добавлен.");

            // Очищаем текстовые поля для ввода следующего товара
            TextBoxCode.Clear();
            TextBoxName.Clear();
            TextBoxPrice.Clear();
            TextBoxQuantity.Clear();

            // Обновляем сумму в TextBoxSum
            _parentWindow.UpdateTotalSum();
        }



        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
