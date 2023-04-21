using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class PizzaSqlDAO
    {
        private readonly string connectionString;

        public PizzaSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        //TODO
        //create a Pizza object from the database to add to an Order object
        public Pizza buildPizza(SqlDataReader reader)
        {
            Pizza pizza = new Pizza();

         //   pizza.Price = Convert.ToDecimal(reader["price"]);

            return pizza;
        }
        public List<Pizza> GetPizzasByOrderId(int id)
        {
            List<Pizza> returnMe = new List<Pizza>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * from pizza WHERE pizzaId = @id ;", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        
                    }
                }
                return returnMe;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        //public string GetPizzaDescFromReader(SqlDataReader reader)
        //{
        //    string sauce = Enum.GetName(typeof(PizzaSauces), Convert.ToInt32(reader["sauceId"]));
        //    string crust = Enum.GetName(typeof(PizzaCrusts), Convert.ToInt32(reader["crustId"]));
        //    string size = Enum.GetName(typeof(PizzaSize), Convert.ToInt32(reader["sizeId"]));
        //    string quantity = " x" + Convert.ToString(reader["pizzaQuantity"]);


        //    //return $"{}"
        //}
        public string GetToppingsStringFromReader(SqlDataReader reader)
        {
            string desc = "";
            while (reader.Read()){
                desc += Enum.GetName(typeof(PizzaTopping), Convert.ToInt32(reader["toppingId"])) + " ";
            }
            return desc;
        }
    }
}
