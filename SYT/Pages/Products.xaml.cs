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
using SYT.Entities;

namespace SYT.Pages
{
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        SYTEntities db = new SYTEntities();
        public Products()
        {
            InitializeComponent();
            ProductsList.ItemsSource = db.Products.OrderBy(p => p.Barcode).ToList();

            using (var db = new SYTEntities())
            {
                // Получаем общее количество товаров
                int totalQuantity = db.Products.Sum(p => p.Quantity).GetValueOrDefault(); // Используем метод GetValueOrDefault() для получения значения типа int

                // Отображаем общее количество товаров в TextBlock
                TextBlock_Tovary.Text = $"{totalQuantity} штук";

                // Получаем общую сумму товаров
                decimal? totalSum = db.Products.Sum(p => p.Price * p.Quantity); // Обратите внимание на тип decimal?

                // Если totalSum равно null, устанавливаем значение по умолчанию равным 0
                decimal totalSumValue = totalSum ?? 0;

                // Отображаем общую сумму товаров в TextBlock
                TextBlock_Summa.Text = $"{totalSumValue} ₽";
            }
        }

        private void BtnPostavki_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Postavki());
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Window window = Window.GetWindow(this);
            window.Close();
        }
    }
}
