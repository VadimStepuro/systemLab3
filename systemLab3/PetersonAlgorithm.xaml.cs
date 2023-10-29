using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для PetersonAlgorithm.xaml
    /// </summary>
    public partial class PetersonAlgorithm : Window
    {
        public PetersonAlgorithm()
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
        private bool[] interested = new bool[2];

        public char Ch { get { return ch; } set { ch = value; } }

        private int Thread1Work()
        {
            return ch;
        }
        private char Thread2Work()
        {
            return (char)(ch + 2);
        }


        private void enter_region(int process)
        { 
            int other = 1 - process;
            interested[process] = true;
            turn = process;
            while (turn == process && interested[other] == true) ;
            
        }

        private void exit_region(int process)
        {
            interested[process] = false;
        }

        private void Thread1Alg()
        {
            while (isWorking)
            {
                Thread.Sleep(1000);
                string str = "";
                Action action = () => { result.Text += str; };
                enter_region(0);
                str = "\nThread 1 entered critical region";
                Dispatcher.Invoke(action);
                str = '\n' + Thread1Work().ToString();
                Dispatcher.Invoke(action);
                
                exit_region(0);
                str = "\nThread 1 exited critical region";
                Dispatcher.Invoke(action);


            }
        }

        private void Thread2Alg()
        {
            while (isWorking)
            {
                Thread.Sleep(1000);
                string str = "";
                Action action = () => { result.Text += str; };
                enter_region(1);
                str = "\nThread 2 entered critical region";
                Dispatcher.Invoke(action);
                str = '\n' + Thread2Work().ToString();
                Dispatcher.Invoke(action);
                exit_region(1);
                str = "\nThread 2 exited critical region";
                Dispatcher.Invoke(action);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(charBox.Text != null && charBox.Text.Length > 0 )
                ch = charBox.Text[0];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isWorking = true;
            thread1.Start();
            thread2.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            isWorking= false;
            this.Close();
        }
    }
}
