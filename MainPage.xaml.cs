using ksiazka.Models;
using System.Linq;

namespace ksiazka
{
    public partial class MainPage : ContentPage
    {
        public List<ContactsBook> Contacts { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ContactsBook.InitializeTestContacts();
            Contacts = ContactsBook.contacts; 
            Contacts = Contacts.OrderBy(c => c.LastName).ThenBy(c => c.Name).ToList();
            contactsListView.ItemsSource = Contacts;
        }

        // przycisk do dodawania kontaktu
        private void OnAddContactClicked(object sender, EventArgs e)
        {
            AddContact();
        }

        // przycisk do usuwania kontaktu
        private void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button; // przy bledzie w rzutowaniu wyrzuci null zamiast wyjatku

            // zwraca przycisk ktory jest przpisany do kontaktu
            var contact = button?.BindingContext as ContactsBook;

            if (contact != null)
            {
                RemoveContact(contact);
            }
        }


        private void RemoveContact(ContactsBook contact)
        {
            ContactsBook.RemoveContact(contact); 
            Contacts = ContactsBook.contacts.OrderBy(c => c.LastName).ThenBy(c => c.Name).ToList();
            contactsListView.ItemsSource = null;
            contactsListView.ItemsSource = Contacts;
        }



        private void OnEditClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var contact = button?.BindingContext as ContactsBook;

            if (contact != null)
            {
                //znajduje label konkretnego a nie wszystkie
                var nameLabel = button.Parent.FindByName<Label>("nameLabel");
                var nameEdit = button.Parent.FindByName<Entry>("nameEdit");

                var lastNameLabel = button.Parent.FindByName<Label>("lastNameLabel");
                var lastNameEdit = button.Parent.FindByName<Entry>("lastNameEdit");

                var phoneNumLabel = button.Parent.FindByName<Label>("phoneNumLabel");
                var phoneNumEdit = button.Parent.FindByName<Entry>("phoneNumEdit");

                var editButton = button.Parent.FindByName<Button>("editButton");

                bool isEditing = nameEdit.IsVisible;

                if (isEditing)
                {
                    contact.Name = nameEdit.Text;
                    contact.LastName = lastNameEdit.Text;
                    contact.PhoneNumber = phoneNumEdit.Text;

                    ContactsBook.EditContact(contact, new ContactsBook
                    {
                        Id = contact.Id,
                        Name = contact.Name,
                        LastName = contact.LastName,
                        PhoneNumber = contact.PhoneNumber,
                        Age = contact.Age
                    });  

                    Contacts = ContactsBook.contacts.OrderBy(c => c.LastName).ThenBy(c => c.Name).ToList();
                    contactsListView.ItemsSource = null;
                    contactsListView.ItemsSource = Contacts;

                    nameLabel.IsVisible = true;
                    nameEdit.IsVisible = false;

                    lastNameLabel.IsVisible = true;
                    lastNameEdit.IsVisible = false;

                    phoneNumLabel.IsVisible = true;
                    phoneNumEdit.IsVisible = false;

                    editButton.Text = "Edytuj";
                }
                else
                {
                    nameLabel.IsVisible = false;
                    nameEdit.IsVisible = true;

                    lastNameLabel.IsVisible = false;
                    lastNameEdit.IsVisible = true;

                    phoneNumLabel.IsVisible = false;
                    phoneNumEdit.IsVisible = true;

                    editButton.Text = "Zapisz";
                    editButton.BackgroundColor = Color.FromArgb("22ff22");
                }
            }
        }

        private async void AddContact()
        {
            string name = await DisplayPromptAsync("Dodaj Kontakt", "Wpisz imię:", maxLength: 30);
            string lastName = await DisplayPromptAsync("Dodaj Kontakt", "Wpisz nazwisko:", maxLength: 30);
            string phoneNumber = await DisplayPromptAsync("Dodaj Kontakt", "Wpisz numer telefonu:", maxLength: 15);

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(lastName) && !string.IsNullOrWhiteSpace(phoneNumber))
            {
                var newContact = new ContactsBook
                {
                    Id = Contacts.Count + 1,
                    Name = name,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    Age = 45
                };

                ContactsBook.AddContact(newContact);  
                Contacts = ContactsBook.contacts.OrderBy(c => c.LastName).ThenBy(c => c.Name).ToList();
                contactsListView.ItemsSource = null;
                contactsListView.ItemsSource = Contacts;
            }
            else
            {
                await DisplayAlert("Błąd", "Proszę wpisać wszystkie wymagane dane.", "OK");
            }
        }


        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = e.NewTextValue;


            List<ContactsBook> results = ContactsBook.SearchContacts(searchTerm);
            results = results.OrderBy(c => c.LastName).ThenBy(c => c.Name).ToList();

            string message = results.Count > 0
                ? string.Join("\n", results.Select(c => $"{c.Name} {c.LastName} - {c.PhoneNumber}"))
                : "Brak wyników.";

            contactsListView.ItemsSource = results;

        }
    }
}