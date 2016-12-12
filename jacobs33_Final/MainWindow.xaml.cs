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
using jacobs33_Final.Models;
using jacobs33_Final.DAL;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace jacobs33_Final
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DataServiceXML dsXML;
        public static BudgetSheet activeSheet;
        public static bool eventFlag = false;
        public MainWindow()
        {
            InitializeComponent();

            //initialize
            dsXML = new DataServiceXML();
            activeSheet = new BudgetSheet();

            //path info
            string basedir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            dgBudgetSheets.ItemsSource = new DirectoryInfo(basedir + @"\Data\").GetFiles();

    }

        #region events
        private void dgBudgetSheets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            string filename = dgBudgetSheets.SelectedItem.ToString();

            activeSheet = dsXML.LoadBudgetSheetFromFile(filename);

            dgLineItems.ItemsSource = activeSheet.BudgetItems;
        }
        private void dgLineItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LineItem item = (LineItem)dgLineItems.SelectedItem;

            eventFlag = true;

            try
            {
                tbAmount.Text = item.ItemAmount.ToString();
                tbDescription.Text = item.Description;
                tbName.Text = item.Name;
                cbLineItemType.Text = item.ItemType.ToString();

            } catch (NullReferenceException ex)
            {
                tbAmount.Text = "";
                tbDescription.Text = "";
                tbName.Text = "";
                cbTypeSelector.Text = "";
            }

            eventFlag = false;
        }
        private void btnNewItem_Click(object sender, RoutedEventArgs e)
        {
            activeSheet.AddNewLineItem();
            RefreshAndSave();
        }
        private void btnRemoveLineItem_Click(object sender, RoutedEventArgs e)
        {
            LineItem item = (LineItem)dgLineItems.SelectedItem;

            activeSheet.BudgetItems.Remove(item);

            cbLineItemType.Text = "";

            dgLineItems.Items.Refresh();
        }
        private void btnSaveSheet_Click(object sender, RoutedEventArgs e)
        {
            RefreshAndSave();
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!eventFlag)
            {
                LineItem item = (LineItem)dgLineItems.SelectedItem;

                activeSheet.BudgetItems.First(x => x.ID == item.ID).Name = tbName.Text;

                dgLineItems.Items.Refresh();
            }
        }
        private void tbAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!eventFlag)
            {
                LineItem item = (LineItem)dgLineItems.SelectedItem;

                try
                {
                    double value = double.Parse(tbAmount.Text);
                    activeSheet.BudgetItems.First(x => x.ID == item.ID).ItemAmount = value;
                }
                catch (Exception ex)
                {
                    if (tbAmount.Text != "")
                        MessageBox.Show("Please enter a valid dollar amount.");
                }
                dgLineItems.Items.Refresh();
            }
        }
        private void tbDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!eventFlag)
            {
                LineItem item = (LineItem)dgLineItems.SelectedItem;

                activeSheet.BudgetItems.First(x => x.ID == item.ID).Description = tbDescription.Text;

                dgLineItems.Items.Refresh();
            }
        }
        private void cbLineItemType_DropDownClosed(object sender, EventArgs e)
        {
            LineItem item = (LineItem)dgLineItems.SelectedItem;

            activeSheet.BudgetItems.First(x => x.ID == item.ID).ItemType = (LineItem.LineItemType)Enum.Parse(typeof(LineItem.LineItemType), cbLineItemType.Text, true);

            dgLineItems.Items.Refresh();
        }
        private void cbTypeSelector_SelectionChanged(object sender, EventArgs e)
        {
            //filter results

            if (cbTypeSelector.Text == "All")
                dgLineItems.ItemsSource = activeSheet.BudgetItems;
            else
                dgLineItems.ItemsSource = activeSheet.BudgetItems.Where(x => x.ItemType.ToString() == cbTypeSelector.Text).ToList();

            //refresh ItemsSource
            dgLineItems.Items.Refresh();
        }

        #endregion
        /// <summary>
        /// Refresh lists and save data
        /// </summary>
        private void RefreshAndSave()
        {
            dgLineItems.Items.Refresh();

            bool result = dsXML.SaveToFile(activeSheet);
            
            dgBudgetSheets.Items.Refresh();

            if (result)
                MessageBox.Show("Data saved!");
        }

        /// <summary>
        /// Make sure only numbers and decimals can be input
        /// </summary>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        
    }
}
