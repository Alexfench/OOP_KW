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
            book.DirCheck();
            reload();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            book.AddContact("New contact");
            book[book.ContactsCount() - 1].AddNumber("home", "00000000000");
            book[book.ContactsCount() - 1].AddMail("null@null.net");
            book[book.ContactsCount() - 1].AddBday("1970.12.31");

            book.Save();
            list.Items.Add(book[book.ContactsCount() - 1].ToString());
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
                for (int i = 0; i < book.ContactsCount(); i++)
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
            if (check.IsChecked == false) 
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
                    Console.WriteLine(VCARD);
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, $"Contacts/{book[list.SelectedIndex].name}.vcf"), VCARD);
                }
            }
            else
            {
                string VCARD = "";
                for (int i=0; i<book.ContactsCount(); i++)
                {
                    VCARD +=
                    $"BEGIN:VCARD\n" +
                    $"VERSION:3.0\n" +
                    $"N:{book[i].name};{book[i].name}\n" +
                    $"FN:{book[i].name}\n" +
                    $"{book[i].returnNumbersForVcard()}" +
                    $"{book[i].returnMailsForVcard()}" +
                    $"BDAY:{book[i].bdayVcard}\n" +
                    $"ADR:{book[i].adresses[0]};{book[i].adresses[1]};{book[i].adresses[2]};{book[i].adresses[3]};{book[i].adresses[4]};{book[i].adresses[5]};{book[i].adresses[6]};\n" +
                    $"END:VCARD\n";
                }
                Console.WriteLine(VCARD);
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, $"Contacts/allContacts.vcf"), VCARD);
            }
        }
    }
}

