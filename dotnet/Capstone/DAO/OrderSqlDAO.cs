using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Capstone.DAO
{
    public class OrderSqlDAO : IOrderDAO
    {
        private readonly string connectionString;

        public OrderSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public string GetEmail(int orderId)
        {
            string email = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT email from OrderPizza where orderId = @orderId;", conn);
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        email = reader.GetString(0);
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }

            return email;
        }

        public string GetOrderStatus(int orderId)
        {
            string orderStatus = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT orderStatus from OrderPizza where orderId = @orderId;", conn);
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        orderStatus = reader.GetString(0);
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }

            return orderStatus;
        }

        public bool UpdateOrderStatus(int orderId, string updatedStatus, int userId)
        {
            try
            {
                int rowsAffected;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE OrderPizza SET orderStatus = @updatedStatus WHERE orderId = @orderId;");
                    cmd.Parameters.AddWithValue("@updatedStatus", updatedStatus);
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.Parameters.AddWithValue("@employeeId", userId);
                    cmd.Connection = conn;
                    rowsAffected = cmd.ExecuteNonQuery();

                }
                return (rowsAffected == 1);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<Order> GetAllOrders()
        {
            List<Order> list = new List<Order>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM orderPizza ;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Order addMe = new Order();
                        addMe = BuildOrderFromReader(reader);
                        list.Add(addMe);
                    }
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Order GetToppings()
        {
            Order order = new Order();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    string sql = "SELECT STRING_AGG(t.toppingName, ',') AS toppings " +
                        "FROM OrderPizza op " +
                        "JOIN Pizza p ON p.orderId = op.orderId " +
                        "JOIN Crust c ON c.crustId = p.crustId " +
                        "JOIN Sauce s ON s.sauceId = p.sauceId " +
                        "JOIN Size size ON size.sizeId = p.sizeId " +
                        "JOIN PizzaTopping pt ON pt.pizzaId = p.pizzaId " +
                        "JOIN Topping t ON t.toppingId = pt.toppingId " +
                        "WHERE op.orderId = @orderId";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@orderId", order.OrderId);
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string firstName = reader.GetString(0);
                    }
                }
            }
            catch (Exception e) { Console.WriteLine($"An error occurred while retrieving toppings: {e.Message}"); }
            return order;
        }
        public CustomOrder BuildOrderFromReader(SqlDataReader reader)
        {
            CustomOrder gotten = new CustomOrder();
            if (reader.Read())
            {
                gotten.OrderId = Convert.ToInt32(reader["orderId"]);
                gotten.OrderCost = Convert.ToDecimal(reader["orderCost"]);
                gotten.Email = Convert.ToString(reader["email"]);
                gotten.PhoneNumber = Convert.ToString(reader["phoneNumber"]);
                gotten.OrderStatus = Convert.ToString(reader["orderStatus"]);
                gotten.OrderTime = Convert.ToDateTime(reader["orderDate"]);
                gotten.OrderType = Convert.ToString(reader["orderType"]);
                gotten.EmployeeId = Convert.ToInt32(reader["employeeId"]);
            }
            return gotten;
        }
    }
}
