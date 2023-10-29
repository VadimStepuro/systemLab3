using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace systemLab3
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class StrictAlternation : Window
    {
        public StrictAlternation()
        {
            InitializeComponent();
            thread1 = new Thread(() => { Thread1Alg(); });
            thread2 = new Thread(() => { Thread2Alg(); });
            Ch = 'e';
        }

        private int turn = 0;
        private Thread thread1;
        private Thread thread2;
        private char ch;
        private bool isWorking;

        public char Ch { get { return ch; } set { ch = value; } }

        private int Thread1Work()
        {
            return ch;
        }
        private char Thread2Work()
        {
            return (char)(ch + 2);
        }

        private void Thread1Alg()
        {
            while (isWorking)
            {
                while (turn != 0) ;
                Thread.Sleep(300);

                string alg = "";
                alg = "\nThread 1 entered critical region";
                Action action = () => result.Text += alg;
                this.Dispatcher.Invoke(action);
                alg = '\n' + Thread1Work().ToString();
                this.Dispatcher.Invoke(action);
                alg = "\nThread 1 exited critical region";
                this.Dispatcher.Invoke(action);

                turn = 1;

            }
        }

        private void Thread2Alg()
        {
            while (isWorking)
            {
                while (turn != 1) ;
                Thread.Sleep(300);

                string alg = "";
                alg = "\nThread 2 entered critical region";
                Action action = () => result.Text += alg;
                this.Dispatcher.Invoke(action);
                alg = '\n' + Thread2Work().ToString();
                this.Dispatcher.Invoke(action);
                alg = "\nThread 2 exited critical region";
                this.Dispatcher.Invoke(action);
                turn = 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isWorking = true;
            thread1.Start();
            thread2.Start();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (charBox.Text != null && charBox.Text.Length >= 1)
            {
                Ch = charBox.Text[0];
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            isWorking = false;
            this.Close();
        }
    }
}
