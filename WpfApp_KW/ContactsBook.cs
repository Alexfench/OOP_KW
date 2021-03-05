using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace WpfApp_KW
{
    class ContactsBook
    {
        private List<Contact> contacts = new List<Contact>();
        private BinaryFormatter formatter = new BinaryFormatter();
        public string path = Path.Combine(Environment.CurrentDirectory, "Kontakts");
        public Contact this[int i] { get { return contacts[i]; } }
        public int KontaktsCount() => contacts.Count();
        public void AddContact(string name) => this.contacts.Add(new Contact(name));
       
        public void RemoveContact(int index) { contacts.RemoveAt(index); }
        public void Save()
        {
            using (FileStream fs = new FileStream(path + $"/Kontakt.kon", FileMode.OpenOrCreate))
                formatter.Serialize(fs, contacts);
        }
        public void Load()
        {
            string[] fileEntries = Directory.GetFiles(path, "*.kon");
            foreach (string item in fileEntries)
            {
                using (FileStream fs = new FileStream(item, FileMode.OpenOrCreate))
                {
                    List<Contact> kon = (List<Contact>)formatter.Deserialize(fs);
                    contacts.AddRange(kon);
                }
            }
        }
        public void Clear() => contacts.Clear();
    }
    [Serializable]
    public class Contact
    {
        public string name { get; private set; }
        public string bday { get; private set; }
        public string bdayVcard { get; private set; }
        public List<string> adresses = new List<string>() { 
            "Subscriber box",
            "extended address",
            "House & street",
            "Locality",
            "Region",
            "Postcode",
            "Country" };
        public List<Numbers> numbers = new List<Numbers>();
        public List<Mails> mails = new List<Mails>();
        public Contact(string name) { this.name = name; }
        public void AddNumber(string type, string number) { numbers.Add(new Numbers(type, number)); }
        public void AddMail(string mail) { mails.Add(new Mails(mail)); }
        public void AddBday(string bday) => this.bday = bday;
        public void AddBdayVcard(string bdayVcard) => this.bdayVcard = bdayVcard;
        public void RemoveNumber(int index) => numbers.RemoveAt(index);
        public void RemoveMail(int index) => mails.RemoveAt(index);
        public void ChangeName(string name) => this.name = name;
        public int NumbersCount() => numbers.Count();
        public int MailsCount() => mails.Count();
        public override string ToString() => $"Имя: {name}, " +
            $"Номер: {numbers[0].number}, " +
            $"E-mail: {mails[0].mail}, " +
            $"Адрес: {adresses[1]}, " +
            $"Дата рождения: {bday}";
        public string returnNumbersForVcard()
        {
            string number = "";
            foreach (Numbers a in numbers)
                number += $"TEL;TYPE={a.type}:{a.number}\n";
            return number;
        }
        public string returnMailsForVcard()
        {
            string mail = "";
            foreach (Mails a in mails)
                mail += $"EMAIL;TYPE=INTERNET:{a.mail}\n";
            return mail;
        }
    }
    [Serializable]
    public class Numbers
    {
        public string number { get; private set; }
        public string type { get; private set; }
        private string CheckNumber(string num)
        {
            string numm = num;
            if (Regex.IsMatch(num, "^[+][0-9]{11}$") || Regex.IsMatch(num, "^[0-9]{11}$"))
                return numm;
            else
                return "INVALID NUMBER";
        }
        private string CheckType(string typ)
        {
            switch (typ)
            {
                case "mobile":
                    return "MOBILE";
                case "home":
                    return "HOME";
                case "work":
                    return "WORK";
                default:
                    return "VOICE";
            }
        }
        public void changeNumber(string number)
        {
            this.number = CheckNumber(number);
        }
        public void changeType(string type)
        {
            this.type = CheckType(type);
        }
        public Numbers(string type, string number)
        {
            this.number = CheckNumber(number);
            this.type = CheckType(type);
        }
    }
    [Serializable]
    public class Mails
    {
        public string mail { get; private set; }
        private string CheckMail(string num)
        {
            string numm = num;
            if (Regex.IsMatch(num, "^[a-zA-Z0-9]+[@][a-zA-Z0-9]+[.]([a-zA-Z]){3}$") || Regex.IsMatch(num, "^[a-zA-Z0-9]+[@][a-zA-Z0-9]+[.]([a-zA-Z]){2}$"))
                return numm;
            else
                return "INVALID E-MAIL";
        }
        public void changeMail(string mail)
        {
            this.mail = CheckMail(mail);
        }

        public Mails(string mail)
        {
            this.mail = CheckMail(mail);
        }
    }
}


