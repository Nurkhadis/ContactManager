using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactLib;

namespace Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ContactManager contactManager = new ContactManager();

            contactManager.LoadContactsFromFile("contacts.txt");

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить контакт");
                Console.WriteLine("2. Показать контакты");
                Console.WriteLine("3. Найти контакт");
                Console.WriteLine("4. Изменить контакт");
                Console.WriteLine("5. Удалить контакт");
                Console.WriteLine("6. Выйти");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите имя: ");
                        string name = Console.ReadLine();
                        Console.Write("Введите номер телефона: ");
                        string phoneNumber = Console.ReadLine();
                        contactManager.AddContact(name, phoneNumber);
                        break;
                    case "2":
                        contactManager.DisplayContacts();
                        break;
                    case "3":
                        Console.Write("Введите имя для поиска: ");
                        string searchName = Console.ReadLine();
                        Contact foundContact = contactManager.FindContact(searchName);
                        if (foundContact != null)
                        {
                            Console.WriteLine($"Найден контакт: Имя: {foundContact.Name}, Телефон: {foundContact.PhoneNumber}");
                        }
                        else
                        {
                            Console.WriteLine("Контакт не найден.");
                        }
                        break;
                    case "4":
                        Console.Write("Введите имя для обновления: ");
                        string updateName = Console.ReadLine();
                        contactManager.UpdateContact(updateName);
                        break;
                    case "5":
                        Console.Write("Введите имя для удаления: ");
                        string deleteName = Console.ReadLine();
                        contactManager.RemoveContact(deleteName);
                        break;
                    case "6":
                        // Сохранение контактов в файл перед выходом
                        contactManager.SaveContactsToFile("contacts.txt");
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
