using Capstone.Models;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Collections.Generic;
using Capstone.DTO;
using System.Linq;
using System.Data;

namespace Capstone.DAO
{
    public class SpecialtySqlDAO : ISpecialtyDAO
    {
        private readonly string connectionString;
        public SpecialtySqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        //Get the list of all specialty pizzas
        public IList<SpecialtyPizza> GetAllAvailableSpecialtyPizza()
        {
            IList<SpecialtyPizza> result = new List<SpecialtyPizza>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT c.createSpecialtyPizzaId, c.name, description, isAvailable, size, sauces, crusts, c.price, imageUrl, STRING_AGG(t.name, ',') AS toppings " +
                        "FROM CreateSpecialtyPizzas c " +
                        "JOIN CreatePizzaToppings ct ON ct.createPizzaId = c.createSpecialtyPizzaId " +
                        "JOIN CreateToppings t ON t.toppingId = ct.toppingId WHERE isAvailable = 1 " +
                        "GROUP BY c.createSpecialtyPizzaId, c.name, description, isAvailable, size, sauces, crusts, c.price, imageUrl " +
                        "ORDER BY c.createSpecialtyPizzaId;";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SpecialtyPizza specialtyPizza = new SpecialtyPizza();
                        specialtyPizza = CreateSpecialtyPizzaFromReader(reader);
                        result.Add(specialtyPizza);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to retrieve to data {e.Message}");
            }
            return result;
        }

        public SpecialtyOrderPizzaDto GetSpecialtyOrderPickUp(SpecialtyOrderPizzaDto specialtyOrder, decimal orderCost)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Insert Order
                    string orderSql = "INSERT INTO [OrderPizza] (orderCost, email, phoneNumber, orderStatus, orderDate, employeeId, addressId, ordertype) " +
                        "VALUES(@orderCost, @email, @phoneNumber, @orderStatus, @orderDate, @employeeId, @addressId, @ordertype); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    SqlCommand orderCmd = new SqlCommand();
                    orderCmd.CommandText = orderSql;
                    orderCmd.Connection = connection;

                    if (specialtyOrder == null || specialtyOrder == null)
                    {
                        Console.WriteLine("Invalid order.");
                    }

                    orderCmd.Parameters.AddWithValue("@orderCost", orderCost);
                    orderCmd.Parameters.AddWithValue("@email", specialtyOrder.CustomerOrder.Email);
                    orderCmd.Parameters.AddWithValue("@phoneNumber", specialtyOrder.CustomerOrder.PhoneNumber);
                    orderCmd.Parameters.AddWithValue("@orderStatus", "Pending");
                    orderCmd.Parameters.AddWithValue("@orderDate", DateTime.Now);
                    orderCmd.Parameters.AddWithValue("@employeeId", 1);
                    orderCmd.Parameters.AddWithValue("@addressId", 1);
                    orderCmd.Parameters.AddWithValue("@ordertype", specialtyOrder.CustomerOrder.OrderType);
                    int orderId = (int)orderCmd.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Unable insert to data {e.Message}");
            }
            return specialtyOrder;
        }

        public SpecialtyOrder GetSpecialtyOrderDeliver(SpecialtyOrderPizzaDto specialtyOrder, decimal orderCost)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Insert Address
                    string deliberySql = "INSERT INTO [Address] (homeType, streetAddress, state, zipCode, apartmentNumber, instructions, city) " +
                        "VALUES(@homeType, @streetAddress, @state, @zipCode, @apartmentNumber, @instructions, @city); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    SqlCommand deliberyCmd = new SqlCommand();
                    deliberyCmd.CommandText = deliberySql;
                    deliberyCmd.Connection = connection;

                    if (specialtyOrder == null || specialtyOrder.Address == null)
                    {
                        Console.WriteLine("Invalid order.");
                    }
                    if (specialtyOrder.Address == null)
                    {
                        Console.WriteLine("Invalid Address.");
                    }

                    deliberyCmd.Parameters.AddWithValue("@homeType", specialtyOrder.Address.HomeType);
                    deliberyCmd.Parameters.AddWithValue("@streetAddress", specialtyOrder.Address.StreetAddress);
                    deliberyCmd.Parameters.AddWithValue("@state", specialtyOrder.Address.StateAbbreviation);
                    deliberyCmd.Parameters.AddWithValue("@zipCode", specialtyOrder.Address.Zip);
                    deliberyCmd.Parameters.AddWithValue("@apartmentNumber", specialtyOrder.Address.Apartment);
                    deliberyCmd.Parameters.AddWithValue("@instructions", specialtyOrder.Address.Instructions);
                    deliberyCmd.Parameters.AddWithValue("@city", specialtyOrder.Address.City);
                    int addressId = (int)deliberyCmd.ExecuteScalar();

                    // Insert Order
                    string orderSql = "INSERT INTO [OrderPizza] (orderCost, email, phoneNumber, orderStatus, orderDate, employeeId, addressId, ordertype) " +
                        "VALUES(@orderCost, @email, @phoneNumber, @orderStatus, @orderDate, @employeeId, @addressId, @ordertype); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    SqlCommand orderCmd = new SqlCommand();
                    orderCmd.CommandText = orderSql;
                    orderCmd.Connection = connection;

                    orderCmd.Parameters.AddWithValue("@orderCost", orderCost);
                    orderCmd.Parameters.AddWithValue("@email", specialtyOrder.CustomerOrder.Email);
                    orderCmd.Parameters.AddWithValue("@phoneNumber", specialtyOrder.CustomerOrder.PhoneNumber);
                    orderCmd.Parameters.AddWithValue("@orderStatus", "Pending");
                    orderCmd.Parameters.AddWithValue("@orderDate", DateTime.Now);
                    orderCmd.Parameters.AddWithValue("@employeeId", 1);
                    orderCmd.Parameters.AddWithValue("@addressId", 1);
                    orderCmd.Parameters.AddWithValue("@ordertype", specialtyOrder.CustomerOrder.OrderType);
                    int orderId = (int)orderCmd.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Unable insert to data {e.Message}");
            }
            return null;
        }

        public List<SpecialtyPizza> GetSpecialtyPizzasByIds(List<int> ids)
        {
            List<SpecialtyPizza> specialtyPizzas = new List<SpecialtyPizza>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("", conn); // Create an empty SqlCommand object

                    // Build the SQL query with the IN clause and dynamically add the parameter values
                    string query = $"SELECT c.createSpecialtyPizzaId, c.name, description, isAvailable, size, sauces, crusts, c.price, imageUrl, STRING_AGG(t.name, ',') AS toppings " +
                                   $"FROM CreateSpecialtyPizzas c " +
                                   $"JOIN CreatePizzaToppings ct ON ct.createPizzaId = c.createSpecialtyPizzaId " +
                                   $"JOIN CreateToppings t ON t.toppingId = ct.toppingId " +
                                   $"WHERE c.createSpecialtyPizzaId IN ({string.Join(",", ids)}) " +
                                   $"GROUP BY c.createSpecialtyPizzaId, c.name, description, isAvailable, size, sauces, crusts, c.price, imageUrl " +
                                   $"ORDER BY c.name ";
                    cmd.CommandText = query; // Set the SQL query to the SqlCommand object

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        SpecialtyPizza newSpecialtyPizza = CreateSpecialtyPizzaFromReader(reader);
                        specialtyPizzas.Add(newSpecialtyPizza);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return specialtyPizzas;
        }

        private SpecialtyPizza CreateSpecialtyPizzaFromReader(SqlDataReader reader)
        {
            SpecialtyPizza specialtyPizza = new SpecialtyPizza();
            specialtyPizza.CreatePizzaId = Convert.ToInt32(reader["createSpecialtyPizzaId"]);
            specialtyPizza.Name = Convert.ToString(reader["name"]);
            specialtyPizza.Description = Convert.ToString(reader["description"]);
            specialtyPizza.IsAvailable = Convert.ToBoolean(reader["isAvailable"]);
            specialtyPizza.PizzaSize = Convert.ToString(reader["size"]);
            specialtyPizza.PizzaCrust = Convert.ToString(reader["crusts"]);
            specialtyPizza.PizzaSauce = Convert.ToString(reader["sauces"]);
            specialtyPizza.PizzaToppings = Convert.ToString(reader["toppings"]);
            specialtyPizza._Price = Convert.ToDecimal(reader["price"]);
            specialtyPizza.ImageUrl = Convert.ToString(reader["imageUrl"]);
            return specialtyPizza;
        }
    }
}
