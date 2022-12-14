using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer
{
    public class ContactRepository : IContactRepository
    {
        private IDbConnection db;

        public ContactRepository(string connectionString)
        {
            this.db = new SqlConnection(connectionString);
        }

        public Contact Add(Contact contact)
        {
            var sql =
                "INSERT INTO Contacts " +
                "(FirstName, LastName, Email, Company, Title) " +
                "VALUES(@FirstName, @LastName, @Email, @Company, @Title);" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = this.db.Query<int>(sql, contact).Single();
            contact.Id = id;
            return contact;
        }

        public Contact Find(int id)
        {
            return this.db.Query<Contact>("SELECT * FROM Contacts WHERE Id = @Id", new { id }).SingleOrDefault();
        }

        public List<Contact> GetAll()
        {
            return this.db.Query<Contact>("SELECT " +
                                            "FirstName AS FName," +
                                            "LastName," +
                                            "Email," +
                                            "Company," +
                                            "Title " +
                                            "FROM Contacts").ToList();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public Contact Update(Contact contact)
        {
            throw new System.NotImplementedException();
        }
    }
}
