using System.IO;
using System.Text.Json;

namespace ConsoleContactBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Book contactBook = new Book();
          
            string executionPath = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo projectDir = Directory.GetParent(executionPath).Parent.Parent.Parent;
            string projectRootPath = projectDir.FullName;
            string filePath = Path.Combine(projectRootPath, "contacts.json");

            contactBook.LoadFromJSON(filePath);

            bool run = true;
            while (run)
            {
                Console.WriteLine("\n1. Add contact\n2. List contacts\n3. Update contact\n4. Delete contact\n5. Help\nx. Exit");
                Console.Write("\nEnter: ");
                string input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "x":
                        run = false;
                        break;

                    case "1":
                        contactBook.LoadFromJSON(filePath); 
                        Console.Write("Enter contact name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter phone number: ");
                        string phone = Console.ReadLine();

                        int newId = contactBook.Contacts.Count > 0 ? contactBook.Contacts.Max(c => c.Id) + 1 : 1;
                        Contact newContact = new Contact { Id = newId, Name = name, PhoneNumber = phone };
                        contactBook.AddContact(newContact);

                        contactBook.SaveToJSON(filePath);
                        Console.WriteLine("Contact added successfully.");
                        break;

                    case "2":
                        contactBook.LoadFromJSON(filePath);
                        contactBook.ListContacts();
                        break;

                    case "3":
                        contactBook.LoadFromJSON(filePath);
                        Console.Write("Enter ID of contact to update: ");
                        if (int.TryParse(Console.ReadLine(), out int idToChange))
                        {
                            Console.Write("Enter new name: ");
                            string updatedName = Console.ReadLine();
                            Console.Write("Enter new phone number: ");
                            string updatedPhone = Console.ReadLine();

                            contactBook.UpdateContact(filePath, idToChange, updatedName, updatedPhone);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID input.");
                        }
                        break;

                    case "4":
                        contactBook.LoadFromJSON(filePath);
                        Console.Write("Enter ID of contact to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int idToDelete))
                        {
                            bool success = contactBook.DeleteContact(idToDelete);
                            if (success)
                            {
                                contactBook.SaveToJSON(filePath);
                                Console.WriteLine("Contact deleted.");
                            }
                            else
                            {
                                Console.WriteLine("Contact not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID input.");
                        }
                        break;

                    case "5":
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine($"{input} is not a valid option.");
                        break;
                }
            }
        }
    }

    class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
    }

    class Book
    {
        public List<Contact> Contacts { get; set; }

        public Book()
        {
            Contacts = new List<Contact>();
        }

        public void AddContact(Contact contact) => Contacts.Add(contact);

        public bool DeleteContact(int id)
        {
            var contactToRemove = Contacts.FirstOrDefault(c => c.Id == id);
            if (contactToRemove != null)
            {
                Contacts.Remove(contactToRemove);
                return true;
            }
            return false;
        }

        public void UpdateContact(string filePath, int id, string name, string phoneNumber)
        {
            var contactToUpdate = Contacts.FirstOrDefault(c => c.Id == id);
            if (contactToUpdate != null)
            {
                contactToUpdate.Name = name;
                contactToUpdate.PhoneNumber = phoneNumber;
                SaveToJSON(filePath);
                Console.WriteLine($"Contact with id: {id} updated.");
            }
            else
            {
                Console.WriteLine($"Contact with id: {id} not found.");
            }
        }

        public void SaveToJSON(string filePath)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(Contacts, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonString);
                Console.WriteLine($"Contacts saved to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void LoadFromJSON(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                var loadedContacts = JsonSerializer.Deserialize<List<Contact>>(jsonString);
                Contacts = loadedContacts ?? new List<Contact>();
            }
            else
            {
                Contacts = new List<Contact>();
            }
        }

        public void ListContacts()
        {
            if (Contacts.Count == 0)
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            foreach (Contact contact in Contacts)
            {
                Console.WriteLine($"ID: {contact.Id}, Name: {contact.Name}, Phone: {contact.PhoneNumber}");
            }
        }
    }
}
