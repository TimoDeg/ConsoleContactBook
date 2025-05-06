# 📒 ConsoleContactBook

A simple C# console application to manage your contacts. Add, view, update, and delete contacts easily via a clean command-line interface. All contacts are saved in a `contacts.json` file to ensure your data persists across sessions.

## 🚀 Features

* Add new contacts with name and phone number.
* List all contacts with their ID, name, and phone number.
* Update a contact’s name and phone number by ID.
* Delete a contact by ID.
* Console menu interface for easy navigation.
* JSON file (`contacts.json`) automatically loaded and saved.
* Smart path handling to ensure the JSON file is always placed at the project root.

## 🔧 How to Run

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/ConsoleContactBook.git
   cd ConsoleContactBook
   ```

2. Open the project in Visual Studio or build and run it from the command line:

   ```bash
   dotnet build
   dotnet run
   ```

## 💡 Usage

When you run the program, you'll see:

```
1. Add contact
2. List contacts
3. Update contact
4. Delete contact
5. Help
x. Exit
```

### Commands

* Enter `1` to add a contact (you’ll be prompted for name and phone number).
* Enter `2` to list all contacts.
* Enter `3` to update a contact’s details (you’ll enter the ID, new name, and phone number).
* Enter `4` to delete a contact by ID.
* Enter `5` to clear the console (Help/refresh).
* Enter `x` to exit.

## 📂 How File Paths Work

The program **automatically determines the correct file path** for `contacts.json` at runtime. It rewires the file path to ensure it finds or creates the JSON file in the **project root directory**, even if the app runs from the `bin` folder.
Example (under the hood):

```csharp
AppDomain.CurrentDomain.BaseDirectory
```

is used to navigate to the project’s root.

## 🗄 Example JSON

Your `contacts.json` file looks like this:

```json
[
  {
    "Id": 1,
    "Name": "Alice Johnson",
    "PhoneNumber": "555-1234"
  },
  {
    "Id": 2,
    "Name": "Bob Smith",
    "PhoneNumber": "555-5678"
  }
]
```

---
