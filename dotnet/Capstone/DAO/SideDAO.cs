using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class SideDAO : ISideDAO
    {
        private readonly string connectionString;
        public SideDAO(string dbConnectionString)
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

                    SqlCommand cmd = new SqlCommand("SELECT * from sides ;", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Item item = new Item();
                        item = GetItemFromReader(reader);
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

                    SqlCommand cmd = new SqlCommand("UPDATE sides SET availability = @new " +
                        "WHERE sides.sideId = @id ;", conn);
                    cmd.Parameters.AddWithValue("@new", changeTo);
                    cmd.Parameters.AddWithValue("@id", item.ItemId);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cmd.CommandText = "SELECT * from sides where name = @name ;";
                        cmd.Parameters.AddWithValue("@name", item.ItemName);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            return GetItemFromReader(reader);
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

        public Item GetItemFromReader(SqlDataReader reader)
        {
            Item i = new Item();
            i.ItemName = Convert.ToString(reader["name"]);
            i.Price = Convert.ToDecimal(reader["sidePrice"]);
            i.Availability = Convert.ToInt16(reader["availability"]) == 1;
            i.ItemId = Convert.ToInt32(reader["sideId"]);

            return i;
        }
    }
}

