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
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows.Markup;
namespace My_first_design
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Book> books;
        public MainWindow()
        {
            InitializeComponent();
            books = init_data();
            table.ItemsSource = books;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private bool IsMax = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMax)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;
                    IsMax = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMax = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (check_poisk.SelectedItem != null)
            {
                switch (check_poisk.Text)
                {
                    case "Названию":
                        var resultsProp1 = books.Where(s => s.Name.Contains(search.Text)).ToList();
                        table.ItemsSource = resultsProp1;
                        break;
                    case "Автору":
                        var resultsProp2 = books.Where(s => s.Author.Contains(search.Text)).ToList();
                        table.ItemsSource = resultsProp2;
                        break;
                    case "Жанру":
                        var resultsProp3 = books.Where(s => s.Genre.Contains(search.Text)).ToList();
                        table.ItemsSource = resultsProp3;
                        break;
                }
            }
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            Add add = new Add();
            add.ShowDialog();
            books = init_data();
            table.ItemsSource = books;
        }
        private List<Book> init_data()
        {
            books = new List<Book>();
            string file = "data.txt";
            try
            {
                using (StreamReader read = new StreamReader(file))
                {
                    string line;
                    while ((line = read.ReadLine()) != null)
                    {
                        string[] rows = line.Split('&').Select(x => x.Trim()).ToArray();
                        if (rows.Length == 4)
                        {
                            int id = int.Parse(rows[0]);
                            string name = rows[1];
                            string author = rows[2];
                            string genre = rows[3];
                            books.Add(new Book { Author = author, Name = name, Genre = genre, ID = id });
                        }
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Ошибка чтения файла", "Ошибка");
            }
            return books;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (table.SelectedItem != null)
            {
                books.Remove((Book)table.SelectedItem);
                table.Items.Refresh();
                string file = "data.txt";
                using (StreamWriter w = new StreamWriter(file))
                {
                    foreach (var  row in books)
                    {
                        w.WriteLine($"{row.ID}&{row.Name}&{row.Author}&{row.Genre}");
                    }
                }
            }
            else
                MessageBox.Show("Выберите нужную строку для удаления", "Ошибка");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (check_filter.SelectedItem != null)
            {
                switch(check_filter.Text)
                {
                    case "Названию":
                        books = books.OrderBy(x => x.Name).ToList();  
                        break;
                    case "Автору":
                        books = books.OrderBy(x => x.Author).ToList();
                        break;
                    case "Жанру":
                        books = books.OrderBy(x => x.Genre).ToList();
                        break;
                }
                table.ItemsSource = books;
            }
        }
    }
}
