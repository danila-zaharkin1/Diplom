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
    /// Логика взаимодействия для EditWin.xaml
    /// </summary>
    public partial class EditWin : Window
    {
        private SYTEntities db = new SYTEntities();
        private Supplies suppliesForUpdate;

        public EditWin(Supplies suppliesForEdit)
        {
            InitializeComponent();
            suppliesForUpdate = suppliesForEdit;
            TextBoxNumber.Text = suppliesForUpdate.Number.ToString();
            TextBoxDate.Text = suppliesForUpdate.Supply_Date.ToString();
            TextBoxSupplier.Text = suppliesForUpdate.Supplier.ToString();
            TextBoxSum.Text = suppliesForUpdate.Amount.ToString();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            if(TextBoxNumber.Text != "" && TextBoxDate.Text != "" && TextBoxSupplier.Text != null && TextBoxSum.Text != "")
            {
                var postavka = db.Supplies.Find(suppliesForUpdate.Supply_Number);
                if (postavka != null)
                {
                    postavka.Number = int.Parse(TextBoxNumber.Text);
                    postavka.Supply_Date = DateTime.Parse(TextBoxDate.Text);
                    postavka.Supplier = TextBoxSupplier.Text;
                    postavka.Amount = decimal.Parse(TextBoxSum.Text);
                }

                db.SaveChanges();
                Close();
                MessageBox.Show("Данные успешно сохранены");
            }
            else
            {
                MessageBox.Show("Заполните все данные");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
