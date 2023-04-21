using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class BeverageDAO : IBeverageDAO
    {
        private readonly string connectionString;
        public BeverageDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public List<Item> GetItems()
        {
            List<Item> returnItems = new List<Item>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * from beverage ;", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Item item = new Item();
                        item = GetBeverageFromReader(reader);
                        returnItems.Add(item);
                    }
                    return returnItems;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Item ToggleAvailability(Item item)
        {
            int changeTo = item.Availability ? 0 : 1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE beverage SET availability = @new " +
                        "WHERE beverage.beverageId = @id ;", conn);
                    cmd.Parameters.AddWithValue("@new", changeTo);
                    cmd.Parameters.AddWithValue("@id", item.ItemId);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cmd.CommandText = "SELECT * from beverage where name = @name ;";
                        cmd.Parameters.AddWithValue("@name", item.ItemName);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            return GetBeverageFromReader(reader);
                        }
                        else return null;
                    }
                    else return null;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Item GetBeverageFromReader(SqlDataReader reader)
        {
            Item i = new Item();
            i.ItemName = Convert.ToString(reader["name"]);
            i.Price = Convert.ToDecimal(reader["beveragePrice"]);
            i.Availability = Convert.ToInt16(reader["availability"]) == 1;
            i.ItemId = Convert.ToInt32(reader["beverageId"]);

            return i;
        }
    }
}
