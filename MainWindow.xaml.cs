using System;
using System.Windows;
using System.Windows.Controls;

namespace DatabasePerp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static DatabaseOperator dbo = new();

        public MainWindow()
        {
            InitializeComponent();
            UsersData.ItemsSource = dbo.Select();
        }

        private void Operate(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name[4].ToString();
            if (btnName == "0")
            {
                if (!string.IsNullOrEmpty(login.Text) && !string.IsNullOrEmpty(passowrd.Password) && !string.IsNullOrEmpty(birthday.Text))
                    SetButtons(true);

            }
            else
            {
                if(btnName == "1")
                {
                    SetButtons(false);
                    DateTime? birthdate = birthday.SelectedDate;
                    dbo.Insert(login.Text, passowrd.Password, birthdate.Value);
                }
                else if (btnName == "2")
                {
                    SetButtons(false);
                    UsersData.ItemsSource = dbo.Select(login.Text);
                }
                else if (btnName == "3")
                {
                    SetButtons(false); DateTime? birthdate = birthday.SelectedDate;
                    dbo.Update(login.Text, passowrd.Password, birthdate.Value);
                }
                else if (btnName == "4")
                {
                    SetButtons(false);
                    dbo.Delete(login.Text);
                }
                else if (btnName == "5")
                {
                    SetButtons(false);
                    UsersData.ItemsSource = dbo.Select();
                }
                else
                {
                    Console.WriteLine("Error - buton name is unset or unknown");
                }
            }

        }

        private void SetButtons(Boolean set)
        {
            if (set)
            {
                btn_0.IsEnabled = false;
                btn_1.IsEnabled = true;
                btn_2.IsEnabled = true;
                btn_3.IsEnabled = true;
                btn_4.IsEnabled = true;
            }
            else
            {
                btn_0.IsEnabled = true;
                btn_1.IsEnabled = false;
                btn_2.IsEnabled = false;
                btn_3.IsEnabled = false;
                btn_4.IsEnabled = false;
            }

        }
    }
}
