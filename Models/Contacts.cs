namespace ksiazka.Models
{
    using System.Collections.Generic;

    public class ContactsBook
    {
        public static List<ContactsBook> contacts = new List<ContactsBook>();

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

        // Metoda do dodania kontaktu
        public static void AddContact(ContactsBook contact)
        {
            contacts.Add(contact);
        }

        // Metoda do usunięcia kontaktu
        public static void RemoveContact(ContactsBook contact)
        {
            contacts.Remove(contact);
        }

        // Metoda do edytowania kontaktu
        public static void EditContact(ContactsBook oldContact, ContactsBook newContact)
        {
            var index = contacts.IndexOf(oldContact);
            if (index != -1)
            {
                contacts[index] = newContact;
            }
        }


        public static List<ContactsBook> SearchContacts(string searchTerm)
        {
            var result = contacts
                .Where(c => $"{c.Name.ToLower()} {c.LastName.ToLower()} {c.PhoneNumber.ToLower()}".Contains(searchTerm.ToLower()))
                .ToList();
            return result;
        }
        public static void InitializeTestContacts()
        {
            contacts.Add(new ContactsBook { Id = 1, Name = "Kamil", LastName = "Jankowski", PhoneNumber = "321-654-987" });
            contacts.Add(new ContactsBook { Id = 2, Name = "Monika", LastName = "Szymańska", PhoneNumber = "654-987-321" });
            contacts.Add(new ContactsBook { Id = 3, Name = "Łukasz", LastName = "Woźniak", PhoneNumber = "777-888-999" });
            contacts.Add(new ContactsBook { Id = 4, Name = "Katarzyna", LastName = "Wiśniewska", PhoneNumber = "111-222-333" });
            contacts.Add(new ContactsBook { Id = 5, Name = "Michał", LastName = "Wójcik", PhoneNumber = "444-555-666" });
            contacts.Add(new ContactsBook { Id = 6, Name = "Zofia", LastName = "Kaczmarek", PhoneNumber = "777-888-999" });
            contacts.Add(new ContactsBook { Id = 7, Name = "Tomasz", LastName = "Mazur", PhoneNumber = "222-333-444" });
            contacts.Add(new ContactsBook { Id = 8, Name = "Alicja", LastName = "Dąbrowska", PhoneNumber = "333-444-555" });
            contacts.Add(new ContactsBook { Id = 9, Name = "Jakub", LastName = "Kubiak", PhoneNumber = "666-777-888" });
            contacts.Add(new ContactsBook { Id = 10, Name = "Magdalena", LastName = "Pawlak", PhoneNumber = "999-000-111" });

        }
    }
}
