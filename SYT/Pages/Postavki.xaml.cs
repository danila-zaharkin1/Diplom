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
using SYT.Windows;

namespace SYT.Pages
{
    /// <summary>
    /// Логика взаимодействия для Postavki.xaml
    /// </summary>
    public partial class Postavki : Page
    {
        SYTEntities db = new SYTEntities();

        public Postavki()
        {
            InitializeComponent();
            PostavkiList.ItemsSource = null;
            PostavkiList.Items.Clear(); 
            PostavkiList.ItemsSource = db.Supplies.OrderBy(s => s.Number).ToList();             
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Products());
        }

        private void BtnPostavki_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Window window = Window.GetWindow(this);
            window.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            new AddWin().ShowDialog();
            PostavkiList.ItemsSource = new SYTEntities().Supplies.ToList();
        }

        private void DtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранную поставку
            var selectedSupply = (Supplies)PostavkiList.SelectedItem;
            if (selectedSupply != null)
            {
                // Находим поставку для удаления по Supply_Number
                var supplyToDelete = db.Supplies.Include("Products")
                                               .FirstOrDefault(s => s.Supply_Number == selectedSupply.Supply_Number);
                if (supplyToDelete != null)
                {
                    // Удаляем все товары, связанные с этой поставкой
                    db.Products.RemoveRange(supplyToDelete.Products);

                    // Удаляем саму поставку
                    db.Supplies.Remove(supplyToDelete);

                    // Сохраняем изменения
                    db.SaveChanges();

                    MessageBox.Show("Поставка успешно удалена.");
                }
            }
            else
            {
                MessageBox.Show("Выберите поставку для удаления.");
            }
            PostavkiList.ItemsSource = new SYTEntities().Supplies.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = PostavkiList.SelectedItem as Supplies;
            if (selectedItem != null)
            {
                Supplies itemToEdit = db.Supplies.Find(selectedItem.Supply_Number);
                if (itemToEdit != null)
                {
                    Window editWin = new EditWin(itemToEdit);
                    editWin.ShowDialog();
                }
            }
            PostavkiList.ItemsSource = new SYTEntities().Supplies.ToList();
        }

        private void BtnDeteils_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранную поставку
            Supplies selectedSupplyItem = PostavkiList.SelectedItem as Supplies;

            if (selectedSupplyItem != null)
            {
                using (var db = new SYTEntities())
                {
                    // Получаем товары в выбранной поставке
                    var productsInSupply = db.Products
                                              .Where(p => p.Supply_Number == selectedSupplyItem.Supply_Number)
                                              .OrderBy(p => p.Barcode)
                                              .ToList();

                    // Очищаем список товаров
                    TovariList.Items.Clear();

                    // Добавляем товары в список товаров
                    foreach (var product in productsInSupply)
                    {
                        TovariList.Items.Add(product);
                    }
                }
            }
        }
    }
}
