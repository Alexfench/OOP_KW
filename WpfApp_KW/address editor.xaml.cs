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

namespace WpfApp_KW
{
    /// <summary>
    /// Логика взаимодействия для address_editor.xaml
    /// </summary>
    public partial class address_editor : Window
    {
        public address_editor()
        {
            InitializeComponent();
        }
        ContactsBook book = new ContactsBook();
        int index;
        public void edit(int index)
        {
            this.index = index;
            book.Load();
            if (book[index].adresses.Count > 0)
            {
                adr1.Text = book[index].adresses[0];
                adr2.Text = book[index].adresses[1];
                adr3.Text = book[index].adresses[2];
                adr4.Text = book[index].adresses[3];
                adr5.Text = book[index].adresses[4];
                adr6.Text = book[index].adresses[5];
                adr7.Text = book[index].adresses[6];
            }
        }
        private void adr1_TextChanged(object sender, TextChangedEventArgs e)
        {
            book[index].adresses[0] = adr1.Text;
            book.Save();
        }

        private void adr2_TextChanged(object sender, TextChangedEventArgs e)
        {
            book[index].adresses[1] = adr2.Text;
            book.Save();
        }

        private void adr3_TextChanged(object sender, TextChangedEventArgs e)
        {
            book[index].adresses[2] = adr3.Text;
            book.Save();
        }

        private void adr4_TextChanged(object sender, TextChangedEventArgs e)
        {
            book[index].adresses[3] = adr4.Text;
            book.Save();
        }

        private void adr5_TextChanged(object sender, TextChangedEventArgs e)
        {
            book[index].adresses[4] = adr5.Text;
            book.Save();
        }

        private void adr6_TextChanged(object sender, TextChangedEventArgs e)
        {
            book[index].adresses[5] = adr6.Text;
            book.Save();
        }

        private void adr7_TextChanged(object sender, TextChangedEventArgs e)
        {
            book[index].adresses[6] = adr7.Text;
            book.Save();
        }
    }
}
