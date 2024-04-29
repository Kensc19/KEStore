using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO.Compression;
using MySqlConnector;
using TodoApi.Models;

namespace TodoApi.Database
{
    public sealed class SaleDB
    {
        public void Save(Sale sale)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3407;Database=mysql;Uid=root;Pwd=123456;"))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string insertQuery = @"
                            use store;
                            INSERT INTO sales (purchase_date, total, payment_method, purchase_number)
                            VALUES (@purchase_date, @total, @payment_method, @purchase_number);";

                        using (var insertCommand = new MySqlCommand(insertQuery, connection, transaction))
                        {
                            insertCommand.Parameters.AddWithValue("@purchase_date", DateTime.Now);
                            insertCommand.Parameters.AddWithValue("@total", sale.Amount);
                            insertCommand.Parameters.AddWithValue("@payment_method", sale.PaymentMethod);
                            insertCommand.Parameters.AddWithValue("@purchase_number", sale.PurchaseNumber);
                            insertCommand.ExecuteNonQuery();
                        }

                        string insertQueryLines = @"
                            use store;
                            INSERT INTO saleLines (productId, purchaseNumber, price)
                            VALUES (@product_Id, @purchase_Number, @product_Price);";

                        foreach (var product in sale.Products)
                        {
                            using (var insertCommandLines = new MySqlCommand(insertQueryLines, connection, transaction))
                            {
                                insertCommandLines.Parameters.AddWithValue("@product_Id", product.Id);
                                insertCommandLines.Parameters.AddWithValue("@purchase_Number", sale.PurchaseNumber);
                                insertCommandLines.Parameters.AddWithValue("@product_Price", product.Price);
                                insertCommandLines.ExecuteNonQuery();
                            }
                        }

                        // Commit the transaction if all inserts are successful
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Rollback the transaction if an error occurs
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<SaleReports> GetWeeklySales(DateTime date)
        {
            List<SaleReports> weeklySales = new List<SaleReports>();

            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3407;Database=mysql;Uid=root;Pwd=123456;"))
            {
                connection.Open();

                string selectQuery = @"
                    use store;
                    SELECT DAYNAME(sale.purchase_date) AS day, SUM(sale.total) AS total
                    FROM sales sale 
                    WHERE YEARWEEK(sale.purchase_date) = YEARWEEK(@date)
                    GROUP BY DAYNAME(sale.purchase_date);";

                using (var command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@date", date);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string day = reader.GetString("day");
                            decimal total = reader.GetDecimal("total");
                            SaleReports saleReports = new SaleReports(day, total);
                            weeklySales.Add(saleReports);
                        }
                    }
                }
            }

            return weeklySales;
        }
    }
}