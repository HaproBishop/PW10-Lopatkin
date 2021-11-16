using System;
using System.Collections.Generic;
using System.Collections;
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
/// <summary>
/// Практическая работ №10. Лопаткин Сергей ИСП-31.
/// Задание 8. Составьте программу вычисления в массиве суммы всех чисел кратных 3
/// </summary>
namespace PW10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        List<int> listfornumbers = new List<int>();
        List<int> listformultiplenumbers = new List<int>();        
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutProgram_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Практическая работ №10. Лопаткин Сергей ИСП-31." +
                "Задание 8. Составьте программу вычисления в массиве суммы всех чисел кратных 3", 
                "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Support_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Программа включает в себя следующие особенности:\n1) " +
                "После ввода делителя сразу будут отображены кратные числа(Если они есть среди исходных)\n2) " +
                "Для удаления исходного числа(ел) необходимо его(их) выбрать во вкладке \"Исходные числа\" и нажать \"Удалить\"\n3) " +
                "Максимально можно ввести шестизначное число\n4) " +
                "Для быстрого закрытия программы можно воспользоваться нажатием клавиши Esc\n5) " +
                "Нельзя ввести то же число, которое находится в списке", 
                "Справка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddNumber_Click(object sender, RoutedEventArgs e)
        {
            if (Number.Text != "")
            {
                if (listfornumbers.Contains(Convert.ToInt32(Number.Text)) != true)
                {
                    listfornumbers.Add(Convert.ToInt32(Number.Text));                    
                    VisualNumbers.Items.Add(listfornumbers[VisualNumbers.Items.Count]);
                    MultipleNumbers.ItemsSource = null;
                }
                else MessageBox.Show("Данное число уже имеется в списке, введите другое!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Для начала введите число в поле!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0; //Проверяем наличие 
        }

        private void EnterDivisor_Click(object sender, RoutedEventArgs e)
        {
            if (Divisor.Text != "")
            {
                int divisor = Convert.ToInt32(Divisor.Text);
                foreach (int value in listfornumbers)
                {
                    if (value % divisor == 0) listformultiplenumbers.Add(value);//Нахождение кратных чисел
                }
                MultipleNumbers.ItemsSource = listformultiplenumbers;
                if (listformultiplenumbers.Count == 0) MessageBox.Show($"К сожалению, нет кратных чисел для {divisor}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("Введите в поле число для \"Ввода делителя\"", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DeleteNumbers_Click(object sender, RoutedEventArgs e)
        {
            if (VisualNumbers.SelectedItem != null)
            {
                ArrayList list = new ArrayList(VisualNumbers.SelectedItems);
                foreach (int value in list)
                {
                    listfornumbers.Remove(value);
                    VisualNumbers.Items.Remove(value);
                }
                listformultiplenumbers.Clear();//Очистка итога(List) из-за изменения исходных чисел
                MultipleNumbers.ItemsSource = null;//Очистка listbox'a из-за изменения
            }
            else MessageBox.Show("Для начала выберите числа!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Divisor_TextChanged(object sender, TextChangedEventArgs e)//Очистка результата при изменении значения делителя
        {
            listformultiplenumbers.Clear();
            MultipleNumbers.ItemsSource = null;
        }

        private void Number_GotFocus(object sender, RoutedEventArgs e) //Событие получения фокуса для смены кнопки для enter по дефолту
        {
            if (e.Source == Number)
            {
                AddNumber.IsDefault = true;                
                EnterDivisor.IsDefault = false;
                DeleteNumbers.IsDefault = false;
            }
            else if (e.Source == Divisor)
            {
                AddNumber.IsDefault = false;
                EnterDivisor.IsDefault = true;
                DeleteNumbers.IsDefault = false;
            }
            else if (e.Source == VisualNumbers)
            {
                AddNumber.IsDefault = false;
                EnterDivisor.IsDefault = false;
                DeleteNumbers.IsDefault = true;
            }
        }
    }
}
