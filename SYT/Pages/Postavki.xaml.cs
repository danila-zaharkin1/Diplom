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
            var selectedItem = PostavkiList.SelectedItem as Supplies;
            if (selectedItem != null)
            {
                Supplies itemToDelete = db.Supplies.FirstOrDefault(t => t.Supply_Number == selectedItem.Supply_Number);
                if (itemToDelete != null)
                {
                    db.Supplies.Remove(itemToDelete);
                    db.SaveChanges();
                    MessageBox.Show("Товар успешно удален");
                }
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
    }
}
