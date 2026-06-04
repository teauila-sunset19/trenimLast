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

namespace trenimLast
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage(int users)
        {
            InitializeComponent();
            FIOPanel.Visibility = Visibility.Hidden;
            searchPanel.Visibility = Visibility.Hidden;
            FilterPanel.Visibility = Visibility.Hidden;
            addBtn.Visibility = Visibility.Hidden;
            if (users != 0) 
            {
                var currentUsers = trenimLastEntities.GetContext().Users.FirstOrDefault(u => u.id == users);
                FIOBloxk.Text = currentUsers.lastName + " " + currentUsers.firstName + " " + currentUsers.otchest;
                RolyBlock.Text = currentUsers.correctRoly;
                VisibleUsers.currentUsers = currentUsers.correctRoly;
                FIOPanel.Visibility = Visibility.Visible;
                if (currentUsers.correctRoly == "Администратор" || currentUsers.correctRoly == "Менеджер")
                {
                    searchPanel.Visibility = Visibility.Visible;
                    FilterPanel.Visibility = Visibility.Visible;
                }
                if (currentUsers.correctRoly == "Администратор") addBtn.Visibility = Visibility.Visible;
            }
            else
            {
                RolyBlock.Text = "Гость";
                VisibleUsers.currentUsers = "Гость";
            }
            var currentProducts = trenimLastEntities.GetContext().Porducts.ToList();
            productsListview.ItemsSource = currentProducts;
            sortCombo.SelectedIndex = 0;
            filterCombo.SelectedIndex = 0;
            Update();
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void filterCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void sortCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
        public void Update() 
        {
            var currentProducts = trenimLastEntities.GetContext().Porducts.ToList();
            if (sortCombo.SelectedIndex == 0) { }
            if (filterCombo.SelectedIndex == 1) 
            {
                currentProducts = currentProducts.Where(p => p.postav == "Vinylon").ToList();
            }
            if (filterCombo.SelectedIndex == 2)
            {
                currentProducts = currentProducts.Where(p => p.postav == "Knauf").ToList();
            }

            if (filterCombo.SelectedIndex == 3)
            {
                currentProducts = currentProducts.Where(p => p.postav == "Playbig").ToList();
            }
            if (filterCombo.SelectedIndex == 4)
            {
                currentProducts = currentProducts.Where(p => p.postav == "CHILITOY").ToList();
            }
            if (sortCombo.SelectedIndex == 0) { }
            if (sortCombo.SelectedIndex == 1)
            {
                currentProducts = currentProducts.OrderBy(p=>p.cost).ToList();
            }
            if (sortCombo.SelectedIndex == 2)
            {
                currentProducts = currentProducts.OrderByDescending(p => p.cost).ToList();
            }
            if (sortCombo.SelectedIndex == 3)
            {
                currentProducts = currentProducts.OrderBy(p => p.countInStorage).ToList();
            }
            if (sortCombo.SelectedIndex == 4)
            {
                currentProducts = currentProducts.OrderByDescending(p => p.countInStorage).ToList();
            }
            currentProducts = currentProducts.Where(p => p.nameProducts.ToLower().Contains(searchBox.Text.ToLower()) ||
            p.postav.ToLower().Contains(searchBox.Text.ToLower()) ||
            p.proisvod.ToLower().Contains(searchBox.Text.ToLower())||
            p.opisav.ToLower().Contains(searchBox.Text.ToLower()) ||
            p.correctCat.ToLower().Contains(searchBox.Text.ToLower())).ToList();
            productsListview.ItemsSource = currentProducts.ToList();
            productsListview.ItemsSource = currentProducts;
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Porducts));
            Update();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentProduct = (sender as Button).DataContext as Porducts;
            var currentArticlu = trenimLastEntities.GetContext().OrdersProducts.Where(o => o.artucul == currentProduct.artikul).ToList();
            if (currentArticlu.Count != 0)
            {
                MessageBox.Show("Этот товар нельзя удалить. Он заказен покупателем!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (MessageBox.Show("Вы точно хотите удалить товар?","Внимание",MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        trenimLastEntities.GetContext().Porducts.Remove(currentProduct);
                        trenimLastEntities.GetContext().SaveChanges();
                        productsListview.ItemsSource = trenimLastEntities.GetContext().Porducts.ToList();
                        Update();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
            Update();
        }
    }
}
