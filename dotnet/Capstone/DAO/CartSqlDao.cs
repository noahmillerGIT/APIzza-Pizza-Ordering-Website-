using Capstone.DTO;
using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Linq;

namespace Capstone.DAO
{
    public class CartSqlDao : ICartDao
    {
        private readonly string connectionString;

        public CartSqlDao(string dbconnectionString) 
        {
            this.connectionString = dbconnectionString;
        }
        public CartDto GetAddress(CartDto cartDto)
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

                    deliberyCmd.Parameters.AddWithValue("@homeType", cartDto.Address.HomeType);
                    deliberyCmd.Parameters.AddWithValue("@streetAddress", cartDto.Address.StreetAddress);
                    deliberyCmd.Parameters.AddWithValue("@state", cartDto.Address.StateAbbreviation);
                    deliberyCmd.Parameters.AddWithValue("@zipCode", cartDto.Address.Zip);
                    deliberyCmd.Parameters.AddWithValue("@apartmentNumber", cartDto.Address.Apartment);
                    deliberyCmd.Parameters.AddWithValue("@instructions", cartDto.Address.Instructions);
                    deliberyCmd.Parameters.AddWithValue("@city", cartDto.Address.City);
                    int addressId = (int)deliberyCmd.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Did not enter the data correctly. {e.Message}");
            }
            return cartDto;
        }
        public CartDto GetOrders(CartDto cartDto, decimal orderCost)
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

                    orderCmd.Parameters.AddWithValue("@orderCost", orderCost);
                    orderCmd.Parameters.AddWithValue("@email", cartDto.CustomerOrder.Email);
                    orderCmd.Parameters.AddWithValue("@phoneNumber", cartDto.CustomerOrder.PhoneNumber);
                    orderCmd.Parameters.AddWithValue("@orderStatus", "Pending");
                    orderCmd.Parameters.AddWithValue("@orderDate", DateTime.Now);
                    orderCmd.Parameters.AddWithValue("@employeeId", 1);
                    orderCmd.Parameters.AddWithValue("@addressId", 1);
                    orderCmd.Parameters.AddWithValue("@ordertype", cartDto.CustomerOrder.OrderType);
                    int orderId = (int)orderCmd.ExecuteScalar();
                    cartDto.CustomerOrder.OrderId = orderId;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Did not enter the data correctly. {e.Message}");
            }
            return cartDto;
        }
        public CartDto GetCustomPizza(CartDto cartDto, int orderId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (Pizza items in cartDto.Pizzas)
                    {
                        // Insert size
                        string sizeSql = "INSERT INTO Size (size, sizePrice) VALUES (@size, @sizePrice); " +
                                         "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                        SqlCommand sizeCmd = new SqlCommand(sizeSql, connection);
                        sizeCmd.Parameters.AddWithValue("@size", items.Size.Size.ToString());
                        sizeCmd.Parameters.AddWithValue("@sizePrice", items.Size.Price);
                        int sizeId = (int)sizeCmd.ExecuteScalar();

                        // Insert Crusts
                        string crustsSql = "INSERT INTO Crust (crustName) VALUES (@crustName); " +
                                           "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                        SqlCommand crustsCmd = new SqlCommand(crustsSql, connection);
                        crustsCmd.Parameters.AddWithValue("@crustName", items.Crusts.Crusts.ToString());
                        int crustsId = (int)crustsCmd.ExecuteScalar();

                        //Insert Sauces
                        string saucesSql = "INSERT INTO Sauce (sauceName) VALUES (@sauceName); " +
                            "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                        SqlCommand saucesCmd = new SqlCommand(saucesSql, connection);
                        saucesCmd.Parameters.AddWithValue("@sauceName", items.Sauces.Sauces.ToString());
                        int saucesId = (int)saucesCmd.ExecuteScalar();


                        // Insert pizza
                        string pizzaSql = "INSERT INTO Pizza (sizeId, orderId, price, sauceId, crustId, pizzaQuantity) " +
                            "VALUES (@sizeId, @orderId, @price, @sauceId, @crustId, @pizzaQuantity);" +
                                           "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                        SqlCommand pizzaCmd = new SqlCommand(pizzaSql, connection);
                        pizzaCmd.Parameters.AddWithValue("@sizeId", sizeId);
                        pizzaCmd.Parameters.AddWithValue("@orderId", orderId);
                        pizzaCmd.Parameters.AddWithValue("@price", items.Price);
                        pizzaCmd.Parameters.AddWithValue("@sauceId", saucesId);
                        pizzaCmd.Parameters.AddWithValue("@crustId", crustsId);
                        pizzaCmd.Parameters.AddWithValue("@pizzaQuantity", items.PizzaQuantity);
                        int pizzaId = (int)pizzaCmd.ExecuteScalar();

                        items.PizzaId = pizzaId;

                        // Insert toppings
                        foreach (PizzaTopping topping in items.Toppings)
                        {
                            string toppingsSql = "INSERT INTO Topping (toppingName, toppingPrice) VALUES (@toppingName, @toppingPrice); " +
                                              "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                            SqlCommand toppingsCmd = new SqlCommand(toppingsSql, connection);
                            toppingsCmd.Parameters.AddWithValue("@toppingName", topping.Topping.ToString());
                            toppingsCmd.Parameters.AddWithValue("@toppingPrice", topping.Price);
                            int toppingId = (int)toppingsCmd.ExecuteScalar();

                            // Insert into PizzaToppings table
                            string pizzaToppingsSql = "INSERT INTO PizzaTopping (pizzaId, toppingId) VALUES (@pizzaId, @toppingId);";
                            SqlCommand pizzaToppingsCmd = new SqlCommand(pizzaToppingsSql, connection);
                            pizzaToppingsCmd.Parameters.AddWithValue("@pizzaId", pizzaId);
                            pizzaToppingsCmd.Parameters.AddWithValue("@toppingId", toppingId);
                            pizzaToppingsCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Did not enter the data correctly. {e.Message}");
            }
            return cartDto;
        }
        
        public List<SpecialtyPizza> GetSpecialtyPizzasByNames(List<string> names)
        {
            List<SpecialtyPizza> specialtyPizzas = new List<SpecialtyPizza>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("", conn); // Create an empty SqlCommand object

                    // Build the SQL query with the IN clause and dynamically add the parameter values
                    string query = $"SELECT c.createSpecialtyPizzaId, c.name, description, isAvailable, size, sauces, crusts, c.price, STRING_AGG(t.name, ',') AS toppings, imageUrl " +
                        $"FROM CreateSpecialtyPizzas c " +
                        $"JOIN CreatePizzaToppings ct ON ct.createPizzaId = c.createSpecialtyPizzaId " +
                        $"JOIN CreateToppings t ON t.toppingId = ct.toppingId " +
                        $"WHERE c.name IN ({string.Join(",", names.Select(name => "'" + name + "'"))}) " +
                        $"GROUP BY c.createSpecialtyPizzaId, c.name, description, isAvailable, size, sauces, crusts, c.price, imageUrl " +
                        $"ORDER BY c.name;";

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

        public List<ItemNames> GetSidesByNames(List<string> names) 
        {
            List<ItemNames> sides = new List<ItemNames>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("", conn); 

                    string query = $"SELECT sideName, Price " +
                                   $"FROM CreateSides " +
                                   $"WHERE sideName IN ({string.Join(",", names.Select(name => "'" + name + "'"))});";

                    cmd.CommandText = query;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ItemNames newSides = CreateNameFromReader(reader); 
                        sides.Add(newSides);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return sides;
        }

        public List<ItemNames> GetBeveragesByNames(List<string> names) 
        {
            List<ItemNames> beverages = new List<ItemNames>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("", conn);

                    string query = $"SELECT beverageName, Price " +
                                   $"FROM CreateBeverage " +
                                   $"WHERE beverageName IN ({string.Join(",", names.Select(name => "'" + name + "'"))});";

                    cmd.CommandText = query;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ItemNames newBeverages = CreateNameFromReader(reader);
                        beverages.Add(newBeverages);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return beverages;
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

        private ItemNames CreateNameFromReader(SqlDataReader reader) 
        {
            ItemNames items = new ItemNames();
            items.Name = Convert.ToString(reader["Name"]);
            items.Price = Convert.ToInt32(reader["Price"]);

            return items;
        }
    }
}
