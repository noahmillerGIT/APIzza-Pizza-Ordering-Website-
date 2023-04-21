using Capstone.Models;
using System.Data.SqlClient;
using System;
using System.Linq;

namespace Capstone.DAO
{
    public class CreateSpecialtyPizzaSqlDao : ICreateSpecialtyPizaa
    {
        private readonly string connectionString;

        public CreateSpecialtyPizzaSqlDao(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }
        public CreateSpecialtyPizza CreateSpecialtyPizza(CreateSpecialtyPizza createSpecialty, int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    // Insert pizza
                    string createSpecialtyPizzaSql = "INSERT INTO CreateSpecialtyPizzas (name, description, isAvailable, size, sauces, crusts, price, employeeId, imageUrl) " +
                        "VALUES (@name, @description, @isAvailable, @size, @sauces, @crusts, @price, @employeeId, @imageUrl);" +
                                       "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    SqlCommand createSpecialtyPizza = new SqlCommand(createSpecialtyPizzaSql, connection);
                    createSpecialtyPizza.Parameters.AddWithValue("@name", createSpecialty.Name);
                    createSpecialtyPizza.Parameters.AddWithValue("@description", createSpecialty.Description);
                    createSpecialtyPizza.Parameters.AddWithValue("@isAvailable", createSpecialty.IsAvailable);
                    createSpecialtyPizza.Parameters.AddWithValue("@size", createSpecialty.Size.Size.ToString());
                    createSpecialtyPizza.Parameters.AddWithValue("@sauces", createSpecialty.Sauces.Sauces.ToString());
                    createSpecialtyPizza.Parameters.AddWithValue("@crusts", createSpecialty.Crusts.Crusts.ToString());
                    createSpecialtyPizza.Parameters.AddWithValue("@price", createSpecialty.Price);
                    createSpecialtyPizza.Parameters.AddWithValue("@imageUrl", createSpecialty.ImageUrl);
                    createSpecialtyPizza.Parameters.AddWithValue("@employeeId", userId);
                    int createSpecialtyPizzaId = (int)createSpecialtyPizza.ExecuteScalar();

                    createSpecialty.PizzaId = createSpecialtyPizzaId;

                    // Insert toppings
                    foreach (PizzaTopping topping in createSpecialty.Toppings)
                    {
                        string toppingSql = "INSERT INTO CreateToppings (name, price) VALUES (@name, @price); " +
                                            "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                        SqlCommand toppingCmd = new SqlCommand(toppingSql, connection);
                        toppingCmd.Parameters.AddWithValue("@name", topping.Topping.ToString());
                        toppingCmd.Parameters.AddWithValue("@price", topping.Price);
                        int toppingId = (int)toppingCmd.ExecuteScalar();

                        // Insert into PizzaToppings table
                        string pizzaToppingsSql = "INSERT INTO CreatePizzaToppings (createPizzaId, toppingId) VALUES (@createPizzaId, @toppingId);";
                        SqlCommand pizzaToppingsCmd = new SqlCommand(pizzaToppingsSql, connection);
                        pizzaToppingsCmd.Parameters.AddWithValue("@createPizzaId", createSpecialtyPizzaId);
                        pizzaToppingsCmd.Parameters.AddWithValue("@toppingId", toppingId);
                        pizzaToppingsCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Did not enter the data correctly. {e.Message}");
            }
            return createSpecialty;
        }

        //Employee can able to show the Specialty Pizza to customers as per the availability
        public bool ChangeAvailability(int createId, bool isAvailable)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE CreateSpecialtyPizzas SET isAvailable = @isAvailable WHERE createSpecialtyPizzaId = @createSpecialtyPizzaId;";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("@isAvailable", isAvailable);
                    cmd.Parameters.AddWithValue("@createSpecialtyPizzaId", createId);

                    cmd.Connection = connection;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EditSpecialtyPizza(int createId, CreateSpecialtyPizza specialtyPizza)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE CreateSpecialtyPizzas SET name = @name, description = @description, size = @size, sauces = @sauces, " +
                        "crusts = @crusts, price = @price, imageUrl = @imageUrl WHERE createSpecialtyPizzaId = @createSpecialtyPizzaId;";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("@name", specialtyPizza.Name);
                    cmd.Parameters.AddWithValue("@description", specialtyPizza.Description);
                    cmd.Parameters.AddWithValue("@size", specialtyPizza.Size.Size);
                    cmd.Parameters.AddWithValue("@sauces", specialtyPizza.Sauces.Sauces);
                    cmd.Parameters.AddWithValue("@crusts", specialtyPizza.Crusts.Crusts);
                    cmd.Parameters.AddWithValue("@price", specialtyPizza.Price);
                    cmd.Parameters.AddWithValue("@imageUrl", specialtyPizza.ImageUrl);
                    cmd.Parameters.AddWithValue("@createSpecialtyPizzaId", createId);

                    cmd.Connection = connection;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
