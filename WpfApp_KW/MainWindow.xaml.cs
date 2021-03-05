using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp_KW
{
    public partial class MainWindow : Window
    {
        ContactsBook book = new ContactsBook();
        public MainWindow()
        {
            InitializeComponent();
            reload();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            book.AddContact("New contact");
            book[book.KontaktsCount() - 1].AddNumber("home", "00000000000");
            book[book.KontaktsCount() - 1].AddMail("null@null.net");
            book[book.KontaktsCount() - 1].AddBday("1970.12.31");

            book.Save();
            list.Items.Add(book[book.KontaktsCount() - 1].ToString());
            numberEdit edit = new numberEdit();
            edit.edit(list.Items.Count - 1);
            if (edit.ShowDialog() == false)
            {
                reload();
            }
        }
        public void reload()
        {
            list.Items.Clear();
            book.Clear();
            try
            {
                book.Load();
                for (int i = 0; i < book.KontaktsCount(); i++)
                    list.Items.Add(book[i].ToString());
            }
            catch { };
        }
        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            if (list.SelectedIndex >= 0)
            {
                book.RemoveContact(list.SelectedIndex);
                book.Save();
                reload();
            }
        }

        private void EditThisCont(object sender, MouseButtonEventArgs e)
        {
            if (list.SelectedIndex >= 0)
            {
                numberEdit edit = new numberEdit();
                edit.edit(list.SelectedIndex);
                if (edit.ShowDialog() == false)
                {
                    reload();
                }
            }
        }

        private void vcardex_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedIndex >= 0)
            {
                string VCARD =
                $"BEGIN:VCARD\n" +
                $"VERSION:3.0\n" +
                $"N:{book[list.SelectedIndex].name};{book[list.SelectedIndex].name}\n" +
                $"FN:{book[list.SelectedIndex].name}\n" +
                $"{book[list.SelectedIndex].returnNumbersForVcard()}" +
                $"{book[list.SelectedIndex].returnMailsForVcard()}" +
                $"BDAY:{book[list.SelectedIndex].bdayVcard}\n" +
                $"ADR:{book[list.SelectedIndex].adresses[0]};{book[list.SelectedIndex].adresses[1]};{book[list.SelectedIndex].adresses[2]};{book[list.SelectedIndex].adresses[3]};{book[list.SelectedIndex].adresses[4]};{book[list.SelectedIndex].adresses[5]};{book[list.SelectedIndex].adresses[6]};\n" +
                $"END:VCARD";
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, $"Kontakts/{book[list.SelectedIndex].name}.vcf"), VCARD);
            }
        }
    }
}

