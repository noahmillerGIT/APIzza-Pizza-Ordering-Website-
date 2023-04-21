using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class PendingOrdersSqlDao : IPendingOrders
    {
        private readonly string connectionString;

        public PendingOrdersSqlDao(string dbconnectionString) 
        {
            this.connectionString = dbconnectionString;
        }

        public IList<PendingOrders> ViewPendingOrders()
        {
            IList<PendingOrders> result = new List<PendingOrders>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    string sql = "SELECT op.orderId, ordercost, orderStatus, ordertype, orderDate, c.crustName, s.sauceName, size.size, STRING_AGG(t.toppingName, ', ') AS toppings " +
                        "FROM OrderPizza op JOIN Pizza p ON p.orderId = op.orderId " +
                        "JOIN Crust c ON c.crustId = p.crustId " +
                        "JOIN Sauce s ON s.sauceId = p.sauceId " +
                        "JOIN Size size ON size.sizeId = p.sizeId " +
                        "JOIN PizzaTopping pt ON pt.pizzaId = p.pizzaId " +
                        "JOIN Topping t ON t.toppingId = pt.toppingId " +
                        "WHERE  op.orderStatus = 'Pending' " +
                        "GROUP BY op.orderId, ordercost, orderStatus, ordertype, orderDate, c.crustName, s.sauceName, size.size ORDER BY op.orderId;";
                    cmd.CommandText = sql;
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PendingOrders pendingOrders = new PendingOrders();
                        pendingOrders = CreatePendingOrdersFromReader(reader);
                        result.Add(pendingOrders);
                    }

                }
                
            }catch(Exception e)
            {
                Console.WriteLine($"Unable to view the pendingOrders {e.Message}");
            }
            return result;
        }

        public IList<PendingOrders> ViewHistoricalReport() 
        {
            IList<PendingOrders> result = new List<PendingOrders>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    string sql = "SELECT e.username, op.orderDate, op.orderStatus, op.orderStatus, op.ordertype " +
                        "FROM OrderPizza op JOIN Employee e ON e.user_id = op.employeeId " +
                        "WHERE op.orderStatus = 'Completed';";
                    cmd.CommandText = sql;
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PendingOrders pendingOrders = new PendingOrders();
                        pendingOrders = CreatePendingOrdersFromReader(reader);
                        result.Add(pendingOrders);
                    }

                }

            }
            catch (Exception e) 
            {
                Console.WriteLine($"Unable to view the pendingOrders {e.Message}");
            }
            return result;
        }
        public IList<PendingOrders> GetPizzaById(int id)
        {
            IList<PendingOrders> result = new List<PendingOrders>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    string sql = "SELECT op.orderId, ordercost, orderStatus, ordertype, orderDate, c.crustName, " +
                        "s.sauceName, size.size, STRING_AGG(t.toppingName, ', ') AS toppings FROM OrderPizza op " +
                        "JOIN Pizza p ON p.orderId = op.orderId " +
                        "JOIN Crust c ON c.crustId = p.crustId " +
                        "JOIN Sauce s ON s.sauceId = p.sauceId " +
                        "JOIN Size size ON size.sizeId = p.sizeId " +
                        "JOIN PizzaTopping pt ON pt.pizzaId = p.pizzaId " +
                        "JOIN Topping t ON t.toppingId = pt.toppingId " +
                        "where op.orderId = @param " +
                        "GROUP BY op.orderId, ordercost, orderStatus, ordertype, orderDate, c.crustName, s.sauceName, size.size ORDER BY op.orderId";

                    cmd.CommandText = sql;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@param", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PendingOrders pendingOrders = new PendingOrders();
                        pendingOrders = CreatePendingOrdersFromReader(reader);
                        result.Add(pendingOrders);
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to view the pendingOrders {e.Message}");
            }
            return result;
        }

        private PendingOrders CreatePendingOrdersFromReader(SqlDataReader reader)
        {
            PendingOrders pendingOrders = new PendingOrders();
            pendingOrders.OrderId = Convert.ToInt32(reader["orderId"]);
            pendingOrders.OrderStatus = Convert.ToString(reader["orderStatus"]);
            pendingOrders.OrderType = Convert.ToString(reader["orderType"]);
            pendingOrders.OrderTime = Convert.ToDateTime(reader["orderDate"]);
            pendingOrders.OrderCost = Convert.ToDecimal(reader["ordercost"]);
            pendingOrders.Toppings = Convert.ToString(reader["toppings"]);
            pendingOrders.Size = Convert.ToString(reader["size"]);
            pendingOrders.CrustName = Convert.ToString(reader["crustName"]);
            pendingOrders.SauceName = Convert.ToString(reader["sauceName"]);
          //  pendingOrders.Username = Convert.ToString(reader["username"]);

            return pendingOrders;
        }
    }
}
