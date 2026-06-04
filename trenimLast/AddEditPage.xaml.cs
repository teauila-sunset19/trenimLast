using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

namespace trenimLast
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Porducts _currentProduct = new Porducts();
        private bool _isNew = false;

        public AddEditPage(Porducts currentProduct)
        {
            InitializeComponent();
            if (currentProduct == null)
            {
                _isNew = true;
                _currentProduct = new Porducts();
                ArticluTextBox.IsReadOnly = false;
            }
            if (currentProduct != null)
            {
                _isNew = false;
                _currentProduct = currentProduct;
                ArticluTextBox.IsReadOnly = true;
            }
            DataContext = _currentProduct;
            catCombo.SelectedIndex = 0;
            if (_currentProduct.category == 1)
                catCombo.SelectedIndex = 1;
            if (_currentProduct.category == 2)
                catCombo.SelectedIndex = 2;
            if (_currentProduct.category == 3)
                catCombo.SelectedIndex = 3;
            if (_currentProduct.category == 4)
                catCombo.SelectedIndex = 4;
            
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(_currentProduct.artikul))
                errors.AppendLine("Артикуль не может быть пустым");
            if (string.IsNullOrEmpty(_currentProduct.nameProducts))
                errors.AppendLine("Напишите название товара");

            if (string.IsNullOrEmpty(_currentProduct.capacity))
                errors.AppendLine("Напишите единицу измерения");
            if (_currentProduct.cost == null || _currentProduct.cost <=0)
                errors.AppendLine("Напишите кореткную цену");
            if (string.IsNullOrEmpty(_currentProduct.postav))
                errors.AppendLine("Напишите поставщика");

            if (string.IsNullOrEmpty(_currentProduct.proisvod))
                errors.AppendLine("Напишите производителя");
            if (_currentProduct.descount < 0)
                errors.AppendLine("Напишите коректную скидку");
            if (_currentProduct.countInStorage < 0)
                errors.AppendLine("Количество на складе не может быть отрицательным");
            if (string.IsNullOrEmpty(_currentProduct.opisav))
                errors.AppendLine("Напишите описание");
            if (catCombo.SelectedIndex == 0)
                errors.AppendLine("Выберите категорию");
            if (catCombo.SelectedIndex == 1)
                _currentProduct.category = 1;
            if (catCombo.SelectedIndex == 2)
                _currentProduct.category = 2;
            if (catCombo.SelectedIndex == 3)
                _currentProduct.category = 3;
            if (catCombo.SelectedIndex == 4)
                _currentProduct.category = 4;
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var context = trenimLastEntities.GetContext();
                var currentProduct = context.Porducts.Where(p => p.artikul == _currentProduct.artikul).ToList();
                if (_isNew)
                {
                    if (currentProduct.Count != 0)
                    {
                        MessageBox.Show("Такой артикуль уже используется", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    context.Porducts.Add(_currentProduct);
                }
                context.SaveChanges();
                MessageBox.Show("Информация сохранена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void editPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
            }
        }
    }
}
