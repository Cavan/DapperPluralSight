using DataLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;

namespace Runner
{
    class Program
    {
        private static IConfigurationRoot config;
        static void Main(string[] args)
        {
            Initializer();
            //Get_all_should_return_6_results();
            var id = Insert_should_assign_identity_to_new_entity();
            Find_should_retrieve_existing_entity(id);
        }

        static void Find_should_retrieve_existing_entity(int id)
        {
            // Arrange
            IContactRepository repository = CreateRepository();

            // Action
            var contact = repository.Find(id);

            // Assert
            Console.WriteLine("*** Get Contact ***");
            contact.Output();
            Debug.Assert(contact.FirstName == "Mr.");
            Debug.Assert(contact.LastName == "X");
        }

        static void Get_all_should_return_6_results()
        {
            // Arrange
            var repository = CreateRepository();

            // Action
            var contacts = repository.GetAll();

            // Assert
            Console.WriteLine($"Count: {contacts.Count}");
            Debug.Assert(contacts.Count == 6);
            contacts.Output();
        }

        static int Insert_should_assign_identity_to_new_entity()
        {
            // Arrange
            IContactRepository repository = CreateRepository();
            var contact = new Contact
            {
                FirstName = "Mr.",
                LastName = "X",
                Email = "huh@gamil.com",
                Company = "Busted Widgets",
                Title = "Big Cheese"
            };
            // Action
            repository.Add(contact);
            // Assert
            Debug.Assert(contact.Id != 0);
            Console.WriteLine("*** Contact Inserted ***");
            Console.WriteLine($"New ID: {contact.Id}");
            return contact.Id;
        }

        private static void Initializer()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            config = builder.Build();
        }

        private static IContactRepository CreateRepository()
        {
            return new ContactRepository(config.GetConnectionString("DefaultConnection"));
        }
    }
}
