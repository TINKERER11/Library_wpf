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
using System.IO;
using System.Windows.Markup;
namespace My_first_design
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string file = "data.txt";
            try
            {
                using (StreamWriter w = File.AppendText(file))
                {
                    if (search.Text.Length > 0 && search1.Text.Length > 0 && search2.Text.Length > 0 && search3.Text.Length > 0)
                        w.WriteLine($"{search.Text}&{search1.Text}&{search2.Text}&{search3.Text}");
                    else
                        MessageBox.Show("Строка(и) пустые", "Ошибка");
                }

            }
            catch
            {
                MessageBox.Show("Ошибка записи в файл", "Ошибка");
            }
            this.Close();
        }
    }
}
