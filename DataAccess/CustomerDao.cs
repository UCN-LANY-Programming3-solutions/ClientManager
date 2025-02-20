﻿using Model;
using System.Data.SqlClient;

namespace DataAccess
{
    public class CustomerDao
    {

        SqlConnectionStringBuilder _connectionStringBuilder;

        public CustomerDao()
        {
            _connectionStringBuilder = new();
            _connectionStringBuilder.DataSource = "localhost\\sqlexpress";
            _connectionStringBuilder.InitialCatalog = "ClientManager";
            _connectionStringBuilder.Encrypt = false;
            _connectionStringBuilder.IntegratedSecurity = true;
        }
        public IEnumerable<Customer> GetAll()
        {
            // TODO: Implement call to database for returning all rows from the Customers table

            // 1. Create and open a connection to the database
            SqlConnection conn = new(_connectionStringBuilder.ConnectionString);
            conn.Open();

            // 2. Create a sql command that will be executed on the database
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Customers";

            // 3. Create a datareader object to retrieve data
            SqlDataReader reader = cmd.ExecuteReader();

            // 4. Pass the data from the reader into a collection of Customer objects
            List<Customer> list = new List<Customer>();

            while (reader.Read())
            {
                Customer customer = new()
                {
                    Id = reader.GetInt32(0),
                    Firstname = reader.GetString(1),
                    Lastname = reader.GetString(2),
                    Address = reader.GetString(3),
                    Zip = reader.GetString(4),
                    City = reader.GetString(5),
                    Phone = reader.GetString(6),
                    Email = reader.GetString(7)
                };

                list.Add(customer);
            }

            // 5. Clean up
            conn.Close();

            return list;
        }

        public Customer? GetById(int id)
        {
            // TODO: Implement call to database for returning a specific row from the Customers table with the given id
            SqlConnection conn = new(_connectionStringBuilder.ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Customers WHERE Id = @id";
            cmd.Parameters.AddWithValue("id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Customer customer = new()
                {
                    Id = reader.GetInt32(0),
                    Firstname = reader.GetString(1),
                    Lastname = reader.GetString(2),
                    Address = reader.GetString(3),
                    Zip = reader.GetString(4),
                    City = reader.GetString(5),
                    Phone = reader.GetString(6),
                    Email = reader.GetString(7)
                };
                return customer;
            }
            return null;
        }


        public bool Insert(Customer entity)
        {
            // TODO: Implement call to database that inserts the entity into the customers table
            SqlConnection conn = new(_connectionStringBuilder.ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Customers VALUES (@firstname, @lastname, @address, @zip, @city, @phone, @email)";
            cmd.Parameters.AddWithValue("firstname", entity.Firstname);
            cmd.Parameters.AddWithValue("lastname", entity.Lastname);
            cmd.Parameters.AddWithValue("address", entity.Address);
            cmd.Parameters.AddWithValue("zip", entity.Zip);
            cmd.Parameters.AddWithValue("city", entity.City);
            cmd.Parameters.AddWithValue("phone", entity.Phone);
            cmd.Parameters.AddWithValue("email", entity.Email);
            
            int rowsAffedted = cmd.ExecuteNonQuery();

            return rowsAffedted == 1;
        }

        public bool Update(Customer entity)
        {
            // TODO: Implement call to database that updates the entity in the customers table
            SqlConnection conn = new(_connectionStringBuilder.ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Customers SET " +
                "Firstname = @firstname, " +
                "Lastname = @lastname, " +
                "Address = @address, " +
                "Zip = @zip, " +
                "City = @city, " +
                "Phone = @phone, " +
                "Email = @email " +
                "WHERE Id = @id ";
            cmd.Parameters.AddWithValue("firstname", entity.Firstname);
            cmd.Parameters.AddWithValue("lastname", entity.Lastname);
            cmd.Parameters.AddWithValue("address", entity.Address);
            cmd.Parameters.AddWithValue("zip", entity.Zip);
            cmd.Parameters.AddWithValue("city", entity.City);
            cmd.Parameters.AddWithValue("phone", entity.Phone);
            cmd.Parameters.AddWithValue("email", entity.Email);
            cmd.Parameters.AddWithValue("id", entity.Id);
            int rowsAffedted = cmd.ExecuteNonQuery();

            return rowsAffedted == 1;
        }

        public bool Delete(Customer entity)
        {
            // TODO: Implement call to database that deletes the entity from the customers table
            SqlConnection conn = new(_connectionStringBuilder.ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Customers WHERE Id = @id";
            cmd.Parameters.AddWithValue("id", entity.Id);

            int rowsAffedted = cmd.ExecuteNonQuery();

            return rowsAffedted == 1;
        }
    }
}