using System;
using System.Windows;


namespace WpfApp_KW
{
    public partial class numberEdit : Window
    {
        ContactsBook book = new ContactsBook();

        int index;
        public numberEdit()
        {
            InitializeComponent();
        }
        public void edit(int index)
        {
            select.Content = "Now selected: null";
            book.Load();
            this.index = index;
            Title = book[index].name;
            nam.Text = Title;
            datePicker.SelectedDate = Convert.ToDateTime(book[index].bday);

            for (int i = 0; i < book[index].NumbersCount(); i++)
            {
                numbtypes.Items.Add(book[index].numbers[i].number + " " + book[index].numbers[i].type);
            }
            for (int i = 0; i < book[index].MailsCount(); i++)
            {
                mails.Items.Add(book[index].mails[i].mail);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // select.Content = "Now selected: null";
            try
            {
                if (numbed.IsChecked == true)
                {
                    book[index].numbers[numbtypes.SelectedIndex].changeNumber(edittextbox.Text);
                    book.Save();
                }
                if (typed.IsChecked == true)
                {
                    book[index].numbers[numbtypes.SelectedIndex].changeType(edittextbox.Text);
                    book.Save();
                }
                if (EmailEd.IsChecked == true)
                {
                    book[index].mails[mails.SelectedIndex].changeMail(edittextbox.Text);
                    book.Save();
                }
                book.Clear();
                numbtypes.Items.Clear();
                mails.Items.Clear();
                edit(index);
            }
            catch { }
        }
        private void Check(object sender, RoutedEventArgs e)
        {
            try
            {
                if (numbed.IsChecked == true)
                {
                    edittextbox.Text = book[index].numbers[numbtypes.SelectedIndex].number;
                }
                if (typed.IsChecked == true)
                {
                    edittextbox.Text = book[index].numbers[numbtypes.SelectedIndex].type;
                }
                if (EmailEd.IsChecked == true)
                {
                    edittextbox.Text = book[index].mails[mails.SelectedIndex].mail;
                }
            }
            catch { }
        }
        private void nam_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            this.Title = nam.Text;
            book[index].ChangeName(nam.Text);
            book.Save();
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if ((numbtypes.SelectedIndex >= 0) && (numbtypes.Items.Count > 1))
            {
                book[index].RemoveNumber(numbtypes.SelectedIndex);
                numbtypes.Items.Remove(numbtypes.SelectedItem);
                book.Save();
                book.Clear();
                numbtypes.Items.Clear();
                mails.Items.Clear();
                edit(index);
            }
            if ((mails.SelectedIndex >= 0) && (mails.Items.Count > 1)) 
            {
                book[index].RemoveMail(mails.SelectedIndex);
                mails.Items.Remove(mails.SelectedItem);
                book.Save();
                book.Clear();
                numbtypes.Items.Clear();
                mails.Items.Clear();
                edit(index);
            }
        }
        private void addMailButton_Click(object sender, RoutedEventArgs e)
        {
            book[index].AddMail(mailText.Text);
            mails.Items.Add(mailText.Text);
            book.Save();
            book.Clear();
            mails.Items.Clear();
            numbtypes.Items.Clear();
            edit(index);
        }
        private void addNumber_Click(object sender, RoutedEventArgs e)
        {
            book[index].AddNumber(typeTxt.Text, nuberTxt.Text);
            mails.Items.Add(typeTxt.Text + " " + nuberTxt.Text);
            book.Save();
            book.Clear();
            mails.Items.Clear();
            numbtypes.Items.Clear();
            edit(index);
        }
        private void numbtypes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            select.Content = $"Now selected: number {numbtypes.SelectedIndex}";
        }
        private void mails_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            select.Content = $"Now selected: mail {mails.SelectedIndex}";
        }
        private void editadress_Click(object sender, RoutedEventArgs e)
        {
            address_editor ad = new address_editor();
            ad.edit(index);
            ad.ShowDialog();
        }
        private void DataChanger(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string dateForVcard = "";
            book[index].AddBday(datePicker.SelectedDate.Value.ToShortDateString());
            book.Save();
            dateForVcard += datePicker.SelectedDate.Value.Year.ToString();
            dateForVcard += "-";
            dateForVcard += datePicker.SelectedDate.Value.Month.ToString();
            dateForVcard += "-";
            dateForVcard += datePicker.SelectedDate.Value.Day.ToString();
            book[index].AddBdayVcard(dateForVcard);
        }
    }
}
