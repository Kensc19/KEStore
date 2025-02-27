using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Core;
using KEStoreApi.Models;
using MySqlConnector;

namespace KEStoreApi.Data
{
    public sealed class DatabaseStore
    {
        public DatabaseStore()
        {
        }

        public static void VerifyAndCreateDatabase(string connectionString)
        {
            var connectionStringWithoutDb = connectionString.Replace("Database=store;", string.Empty);

            try
            {
                using (var connection = new MySqlConnection(connectionStringWithoutDb))
                {
                    connection.Open();

                    using (var command = new MySqlCommand("CREATE DATABASE IF NOT EXISTS store;", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar o crear la base de datos.", ex);
            }
        }

public static void Store_MySql()
{
    var products = new List<Product>
    {
        new Product
        {
            Id = 1,
            Name = "Xbox",
            Price = 349,
            ImageUrl = "https://xxboxnews.blob.core.windows.net/prod/sites/2/05-23.jpg",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Gaming").Id,
            Description = "Consola Xbox con 1TB de almacenamiento, incluye mando inalámbrico."
        },
        new Product
        {
            Id = 2,
            Name = "PlayStation 4",
            Price = 212,
            ImageUrl = "https://media.wired.com/photos/62eabb0e58719fe5c578ec7c/master/pass/What-To-Do-With-Old-PS4-Gear.jpg",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Gaming").Id,
            Description = "Consola PlayStation 4 de 500GB, perfecta para juegos y entretenimiento."
        },
        new Product
        {
            Id = 3,
            Name = "Hogwarts Legacy",
            Price = 47,
            ImageUrl = "https://assets.nintendo.com/image/upload/c_fill,w_1200/q_auto:best/f_auto/dpr_2.0/ncom/software/switch/70070000019147/8d6950111fb9a0ece31708dcd6ac893f93c012bc585a6a09dfd986d56ab483d1",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Gaming").Id,
            Description = "Videojuego Hogwarts Legacy para Nintendo Switch, sumérgete en el mundo mágico de Harry Potter."
        },
        new Product
        {
            Id = 4,
            Name = "Keyboard Corsair",
            Price = 79,
            ImageUrl = "https://i.ytimg.com/vi/KhVg_7WqCaA/maxresdefault.jpg",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Accesorios de Tecnología").Id,
            Description = "Teclado mecánico Corsair, ideal para gaming y productividad."
        },
        new Product
        {
            Id = 5,
            Name = "Xbox",
            Price = 349,
            ImageUrl = "https://xxboxnews.blob.core.windows.net/prod/sites/2/05-23.jpg",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Gaming").Id,
            Description = "Consola Xbox con 1TB de almacenamiento, incluye mando inalámbrico."
        },
        new Product
        {
            Id = 7,
            Name = "Xbox",
            Price = 349,
            ImageUrl = "https://xxboxnews.blob.core.windows.net/prod/sites/2/05-23.jpg",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Gaming").Id,
            Description = "Consola Xbox con 1TB de almacenamiento, incluye mando inalámbrico."
        },
        new Product
        {
            Id = 8,
            Name = "Xbox",
            Price = 349,
            ImageUrl = "https://xxboxnews.blob.core.windows.net/prod/sites/2/05-23.jpg",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Gaming").Id,
            Description = "Consola Xbox con 1TB de almacenamiento, incluye mando inalámbrico."
        },
        new Product
        {
            Id = 9,
            Name = "Celular",
            Price = 100,
            ImageUrl = "https://d.newsweek.com/en/full/1995235/galaxy-s22-series-phones.jpg",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Smartphones").Id,
            Description = "Smartphone de alta gama con pantalla AMOLED y cámara de 108MP."
        },
        new Product
        {
            Id = 10,
            Name = "Celular",
            Price = 100,
            ImageUrl = "https://d.newsweek.com/en/full/1995235/galaxy-s22-series-phones.jpg",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Smartphones").Id,
            Description = "Smartphone de alta gama con pantalla AMOLED y cámara de 108MP."
        },
        new Product
        {
            Id = 11,
            Name = "Celular",
            Price = 100,
            ImageUrl = "https://d.newsweek.com/en/full/1995235/galaxy-s22-series-phones.jpg",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Smartphones").Id,
            Description = "Smartphone de alta gama con pantalla AMOLED y cámara de 108MP."
        },
        new Product
        {
            Id = 12,
            Name = "Celular",
            Price = 100,
            ImageUrl = "https://d.newsweek.com/en/full/1995235/galaxy-s22-series-phones.jpg",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Smartphones").Id,
            Description = "Smartphone de alta gama con pantalla AMOLED y cámara de 108MP."
        },
        new Product
        {
            Id = 13,
            Name = "Logitech Superlight",
            Price = 126,
            ImageUrl = "https://cdn.mos.cms.futurecdn.net/uFsmnUYaPrVA48v9ubwhCV.jpg",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Accesorios de Tecnología").Id,
            Description = "Ratón Logitech Superlight, ultra ligero y preciso para gaming."
        },
        new Product
        {
            Id = 14,
            Name = "AMD RYZEN 9 7950X3D",
            Price = 626,
            ImageUrl = "https://www.techspot.com/articles-info/2636/images/2023-02-27-image-14.jpg",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Hardware").Id,
            Description = "Procesador AMD RYZEN 9 7950X3D, rendimiento extremo para juegos y creación de contenido."
        },
        new Product
        {
            Id = 15,
            Name = "GIGABYTE GeForce RTX 4060 AERO OC 8G Graphics Card",
            Price = 319,
            ImageUrl = "https://static.gigabyte.com/StaticFile/Image/Global/dc8aeef6fa8e07dd2202a33fb0c59266/Product/35407/Png",
            Quantity = 1,
            CategoriaId = Categorias.Instance.GetCategorias().ToList().First(c => c.Nombre == "Hardware").Id,
            Description = "Tarjeta gráfica GIGABYTE GeForce RTX 4060, ideal para gaming en alta resolución."
        }
    };

    string connectionString = DatabaseConfiguration.Instance.ConnectionString;

    using (var connection = new MySqlConnection(connectionString))
    {
        connection.Open();

        using (var transaction = connection.BeginTransaction())
        {
            try
            {
                string createTableQuery = @"
                    DROP DATABASE IF EXISTS store;
                    CREATE DATABASE store;
                    USE store;
                    CREATE TABLE IF NOT EXISTS products (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        name VARCHAR(100) NOT NULL,
                        price DECIMAL(10, 2) NOT NULL,
                        ImageUrl VARCHAR(500) NOT NULL,
                        idCategoria INT NOT NULL,
                        description VARCHAR(500) NOT NULL,
                        isDeleted BOOLEAN DEFAULT FALSE
                    );
                    CREATE TABLE IF NOT EXISTS paymentMethod (
                        id INT PRIMARY KEY,
                        description VARCHAR(50) NOT NULL
                    );
                    INSERT INTO paymentMethod (id, description) VALUES (0, 'Cash');
                    INSERT INTO paymentMethod (id, description) VALUES (1, 'Sinpe');

                    CREATE TABLE IF NOT EXISTS addresses (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        street VARCHAR(255) NOT NULL,
                        city VARCHAR(100) NOT NULL,
                        state VARCHAR(100) NOT NULL,
                        zipCode VARCHAR(20) NOT NULL,
                        country VARCHAR(100) NOT NULL
                    );
                    CREATE TABLE IF NOT EXISTS Sales (
                        purchaseNumber VARCHAR(50) NOT NULL PRIMARY KEY,
                        total DECIMAL(10, 2) NOT NULL,
                        purchase_date DATETIME NOT NULL,
                        payment_method INT NOT NULL,
                        address_id INT NOT NULL,
                        FOREIGN KEY (payment_method) REFERENCES paymentMethod(id),
                        FOREIGN KEY (address_id) REFERENCES addresses(id)
                    );

                    CREATE TABLE IF NOT EXISTS Lines_Sales (
                        id_line INT AUTO_INCREMENT PRIMARY KEY,
                        id_Sale VARCHAR(50) NOT NULL,
                        id_Product INT NOT NULL,
                        quantity INT NOT NULL,
                        price DECIMAL(10, 2) NOT NULL,
                        FOREIGN KEY (id_Sale) REFERENCES Sales(purchaseNumber),
                        FOREIGN KEY (id_Product) REFERENCES products(id)
                    );

                    CREATE TABLE IF NOT EXISTS Campaign (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        Title VARCHAR(255) NOT NULL,
                        ContentCam TEXT NOT NULL,
                        DateCam DATETIME NOT NULL,
                        IsDeleted BOOLEAN DEFAULT FALSE
                    );

                    INSERT INTO addresses (street, city, state, zipCode, country) VALUES 
                        ('123 Main St', 'San Jose', 'SJ', '12345', 'Costa Rica'),
                        ('456 Market St', 'Alajuela', 'AL', '67890', 'Costa Rica'),
                        ('789 Elm St', 'Heredia', 'HE', '54321', 'Costa Rica'),
                        ('101 Oak St', 'Cartago', 'CA', '98765', 'Costa Rica'),
                        ('202 Pine St', 'Limon', 'LI', '11223', 'Costa Rica'),
                        ('303 Cedar St', 'Puntarenas', 'PU', '44556', 'Costa Rica'),
                        ('404 Birch St', 'Guanacaste', 'GU', '77889', 'Costa Rica');

                    -- Semana del 16 de junio al 20 de junio
                    INSERT INTO Sales (purchase_date, total, payment_method, address_id, purchaseNumber)
                    VALUES 
                        ('2024-06-16 10:00:00', 280.00, 0, 1, 'PUR001'),
                        ('2024-06-17 10:00:00', 480.00, 1, 2, 'PUR002'),
                        ('2024-06-18 12:00:00', 910.00, 1, 3, 'PUR003'),
                        ('2024-06-19 14:00:00', 450.00, 0, 4, 'PUR004'),
                        ('2024-06-20 16:00:00', 340.00, 1, 5, 'PUR005');

                    -- Semana del 25 de junio al 29 de junio
                    INSERT INTO Sales (purchase_date, total, payment_method, address_id, purchaseNumber)
                    VALUES 
                        ('2024-06-25 10:00:00', 390.00, 0, 1, 'PUR006'),
                        ('2024-06-26 12:00:00', 520.00, 1, 2, 'PUR007'),
                        ('2024-06-27 14:00:00', 930.00, 0, 3, 'PUR008'),
                        ('2024-06-28 16:00:00', 950.00, 1, 4, 'PUR009'),
                        ('2024-06-29 18:00:00', 550.00, 0, 5, 'PUR010');
                ";

                using (var command = new MySqlCommand(createTableQuery, connection, transaction))
                {
                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                        throw new Exception("Error creating database tables");
                }

                foreach (Product product in products)
                {
                    string insertProductQuery = @"
                        INSERT INTO products(name, price, ImageUrl, idCategoria, description)
                        VALUES(@name, @price, @imageUrl, @categoryId, @description);";

                    using (var insertCommand = new MySqlCommand(insertProductQuery, connection, transaction))
                    {
                        insertCommand.Parameters.AddWithValue("@name", product.Name);
                        insertCommand.Parameters.AddWithValue("@price", product.Price);
                        insertCommand.Parameters.AddWithValue("@imageUrl", product.ImageUrl);
                        insertCommand.Parameters.AddWithValue("@categoryId", product.CategoriaId);
                        insertCommand.Parameters.AddWithValue("@description", product.Description);
                        insertCommand.ExecuteNonQuery();
                    }
                }

                string insertLinesSalesQuery = @"
                    INSERT INTO Lines_Sales (id_Sale, id_Product, quantity, price)
                    VALUES 
                        ('PUR001', 1, 2, 100.00),
                        ('PUR001', 3, 3, 20.00),
                        ('PUR001', 4, 1, 80.00),
                        ('PUR002', 1, 1, 100.00),
                        ('PUR002', 2, 2, 60.00),
                        ('PUR002', 4, 1, 80.00),
                        ('PUR003', 2, 1, 60.00),
                        ('PUR003', 3, 3, 60.00),
                        ('PUR003', 4, 2, 80.00),
                        ('PUR004', 1, 1, 100.00),
                        ('PUR004', 3, 2, 40.00),
                        ('PUR004', 4, 1, 80.00),
                        ('PUR005', 2, 1, 60.00),
                        ('PUR005', 4, 2, 80.00),
                        ('PUR006', 1, 2, 100.00),
                        ('PUR006', 2, 1, 60.00),
                        ('PUR007', 3, 1, 20.00),
                        ('PUR007', 4, 2, 80.00),
                        ('PUR008', 1, 1, 100.00),
                        ('PUR008', 3, 3, 60.00),
                        ('PUR008', 4, 1, 80.00),
                        ('PUR009', 2, 1, 60.00),
                        ('PUR009', 4, 2, 80.00),
                        ('PUR010', 1, 2, 100.00),
                        ('PUR010', 2, 1, 60.00),
                        ('PUR010', 3, 2, 40.00);
                ";

                using (var command = new MySqlCommand(insertLinesSalesQuery, connection, transaction))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected <= 0)
                        throw new Exception("Error inserting sales data");
                }

                transaction.Commit();
            }
            catch (MySqlException)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}

        public static async Task<int> SaveAddressAsync(Address address)
        {
            string connectionString = DatabaseConfiguration.Instance.ConnectionString;
            int addressId;

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                    INSERT INTO addresses (street, city, state, zipCode, country)
                    VALUES (@street, @city, @state, @zipCode, @country);
                    SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@street", address.Street);
                    command.Parameters.AddWithValue("@city", address.City);
                    command.Parameters.AddWithValue("@state", address.State);
                    command.Parameters.AddWithValue("@zipCode", address.ZipCode);
                    command.Parameters.AddWithValue("@country", address.Country);

                    addressId = Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }

            return addressId;
        }

        public static async Task<IEnumerable<Product>> GetProductsFromDBaAsync()
        {
            List<Product> products = new List<Product>();
            string connectionString = DatabaseConfiguration.Instance.ConnectionString;

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT id, name, price, ImageUrl, idCategoria, description FROM products WHERE isDeleted = 0";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int idCategoria = reader.GetInt32("idCategoria");
                        products.Add(new Product
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Price = reader.GetDecimal("price"),
                            ImageUrl = reader.GetString("ImageUrl"),
                            CategoriaId = idCategoria,
                            Description = reader.GetString("description"),
                            IsDeleted = false,
                        });
                    }
                }
            }

            return products;
        }

        public static async Task AddProductAsync(Product product, Store.ProductActionDelegate actions)
        {
            string connectionString = DatabaseConfiguration.Instance.ConnectionString;

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        string insertProductQuery = @"
                            INSERT INTO products(name, price, ImageUrl, idCategoria, description)
                            VALUES(@name, @price, @imageUrl, @categoryId, @description);";

                        using (var insertCommand = new MySqlCommand(insertProductQuery, connection, (MySqlTransaction)transaction))
                        {
                            insertCommand.Parameters.AddWithValue("@name", product.Name);
                            insertCommand.Parameters.AddWithValue("@price", product.Price);
                            insertCommand.Parameters.AddWithValue("@imageUrl", product.ImageUrl);
                            insertCommand.Parameters.AddWithValue("@categoryId", product.CategoriaId);
                            insertCommand.Parameters.AddWithValue("@description", product.Description);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                        await transaction.CommitAsync();

                        if (actions != null)
                        {
                            await actions(product);
                        }
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public static async Task DeleteProductAsync(int productId)
        {
            string connectionString = DatabaseConfiguration.Instance.ConnectionString;

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE products SET isDeleted = 1 WHERE id = @productId";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productId", productId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }

    internal class Categories
    {
        public static Categories Instance { get; } = new Categories();
    }
}
