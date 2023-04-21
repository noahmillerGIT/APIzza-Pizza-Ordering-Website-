using Capstone.DTO;
using Capstone.Models;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public class CreateItemSqlDao : ICreateItems
    {
        private readonly string connectionString;

        public CreateItemSqlDao(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        public BeverageItem AddBeverageItem(BeverageItem beverageItem, int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create Beverages List
                    string sql = "INSERT INTO CreateBeverage (beveragePrice, beverageName, isAvailable, employeeId, imageUrl, description) " +
                        "VALUES(@beveragePrice, @beverageName, @isAvailable, @employeeId, @imageUrl, @description); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    SqlCommand Cmd = new SqlCommand();
                    Cmd.CommandText = sql;
                    Cmd.Connection = connection;

                    Cmd.Parameters.AddWithValue("@beverageName", beverageItem.ItemName);
                    Cmd.Parameters.AddWithValue("@beveragePrice", beverageItem.Price);
                    Cmd.Parameters.AddWithValue("@isAvailable", beverageItem.Available);
                    Cmd.Parameters.AddWithValue("@employeeId", userId);
                    Cmd.Parameters.AddWithValue("@imageUrl", beverageItem.ImageUrl);
                    Cmd.Parameters.AddWithValue("@description", beverageItem.Description);
                    int beverageId = (int)Cmd.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Did not enter the data correctly. {e.Message}");
            }
            return beverageItem;
        }

        public SideItem AddSideItem(SideItem sideItem, int userId)  
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create Side List
                    string sql = "INSERT INTO CreateSides (sideName, sidePrice, isAvailable, employeeId, imageUrl, description) " +
                        "VALUES(@sideName, @sidePrice, @isAvailable, @employeeId, @imageUrl, @description); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    SqlCommand Cmd = new SqlCommand();
                    Cmd.CommandText = sql;
                    Cmd.Connection = connection;

                    Cmd.Parameters.AddWithValue("@sideName", sideItem.ItemName);
                    Cmd.Parameters.AddWithValue("@sidePrice", sideItem.Price);
                    Cmd.Parameters.AddWithValue("@isAvailable", sideItem.Available);
                    Cmd.Parameters.AddWithValue("@employeeId", userId);
                    Cmd.Parameters.AddWithValue("@imageUrl", sideItem.ImageUrl);
                    Cmd.Parameters.AddWithValue("@description", sideItem.Description);
                    int sideId = (int)Cmd.ExecuteScalar(); 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Did not enter the data correctly. {e.Message}");
            }
            return sideItem;
        }
        public List<BeverageItem> GetBeverageItems()
        {
            List<BeverageItem> result = new List<BeverageItem>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    string sql = "SELECT beverageName, beveragePrice, imageUrl, description " +
                        "FROM CreateBeverage;";
                    cmd.CommandText = sql;
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        BeverageItem item = new BeverageItem();
                         item = CreateBeveratesFromReader(reader);
                        result.Add(item);
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to view the pendingOrders {e.Message}");
            }
            return result;
        }

        public List<SideItem> GetSideItems() 
        {
            List<SideItem> result = new List<SideItem>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    string sql = "SELECT sideName, sidePrice, imageUrl, description " +
                        "FROM CreateSides;";
                    cmd.CommandText = sql;
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SideItem item = new SideItem();
                        item = CreateSideFromReader(reader);
                        result.Add(item);
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to view the pendingOrders {e.Message}");
            }
            return result;
        }
        private BeverageItem CreateBeveratesFromReader(SqlDataReader reader)
        {
            BeverageItem items = new BeverageItem();
            items.ItemName = Convert.ToString(reader["beverageName"]);
            items.Description = Convert.ToString(reader["description"]);
            items.ImageUrl = Convert.ToString(reader["imageUrl"]);
            items.Price = Convert.ToDecimal(reader["beveragePrice"]);

            return items;
        }

        private SideItem CreateSideFromReader(SqlDataReader reader) 
        {
            SideItem items = new SideItem();
            items.ItemName = Convert.ToString(reader["sideName"]);
            items.Description = Convert.ToString(reader["description"]);
            items.ImageUrl = Convert.ToString(reader["imageUrl"]);
            items.Price = Convert.ToDecimal(reader["sidePrice"]);

            return items;
        }
    }
}
