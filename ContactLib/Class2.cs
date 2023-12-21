using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactLib
{
    public class ContactManager
    {
        private List<Contact> contacts = new List<Contact>();

        public void AddContact(string name, string phoneNumber)
        {
            if (!IsValidPhoneNumber(phoneNumber))
            {
                Console.WriteLine("Ошибка: Введен некорректный номер телефона. Введите только цифры.");
                return; // Прерываем выполнение метода при ошибке
            }

            Contact newContact = new Contact
            {
                Name = name,
                PhoneNumber = phoneNumber
            };
            contacts.Add(newContact);
            Console.WriteLine("Контакт добавлен.");
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.All(char.IsDigit);
        }
        public void DisplayContacts()
        {
            Console.WriteLine("Список контактов:");
            foreach (var contact in contacts)
            {
                Console.WriteLine($"Имя: {contact.Name}, Телефон: {contact.PhoneNumber}");
            }
        }

        public Contact FindContact(string name)
        {
            return contacts.Find(contact => contact.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void RemoveContact(string name)
        {
            Contact contactToRemove = FindContact(name);
            if (contactToRemove != null)
            {
                contacts.Remove(contactToRemove);
                Console.WriteLine("Контакт удален.");
            }
            else
            {
                Console.WriteLine("Контакт не найден.");
            }
        }

        public void UpdateContact(string name)
        {

            // Находим контакт по имени
            Contact contactToUpdate = FindContact(name);

            if (contactToUpdate != null)
            {
                Console.WriteLine($"Текущие данные контакта: Имя: {contactToUpdate.Name}, Телефон: {contactToUpdate.PhoneNumber}");

                // Запрашиваем новые данные для обновления
                Console.Write("Введите новое имя (оставьте пустым для сохранения текущего): ");
                string newName = Console.ReadLine();

                Console.Write("Введите новый номер телефона (оставьте пустым для сохранения текущего): ");
                string newPhoneNumber = Console.ReadLine();

                // Обновляем данные контакта, если пользователь ввел новые значения
                if (!string.IsNullOrEmpty(newName))
                {
                    contactToUpdate.Name = newName;
                }

                if (!string.IsNullOrEmpty(newPhoneNumber))
                {
                    contactToUpdate.PhoneNumber = newPhoneNumber;
                }

                Console.WriteLine("Контакт успешно обновлен.");
            }
            else
            {
                Console.WriteLine("Контакт не найден.");
            }
        }

        public void SaveContactsToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var contact in contacts)
                    {
                        writer.WriteLine($"{contact.Name} {contact.PhoneNumber}");
                    }
                    Console.WriteLine("Контакты сохранены в файл.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении контактов в файл: {ex.Message}");
            }
        }

        public void LoadContactsFromFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(' ');
                            if (parts.Length == 2)
                            {
                                Contact contact = new Contact
                                {
                                    Name = parts[0],
                                    PhoneNumber = parts[1]
                                };
                                contacts.Add(contact);
                            }
                        }
                        Console.WriteLine("Контакты загружены из файла.");
                    }
                }
                else
                {
                    Console.WriteLine("Файл с контактами не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке контактов из файла: {ex.Message}");
            }
        }
    }
}
