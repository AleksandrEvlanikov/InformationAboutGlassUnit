using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationAboutGlassUnit
{
    internal class BDGlassPacketSQL
    {
        private readonly string _connectionString;

        public BDGlassPacketSQL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateGlassPacketTable()
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS InformationGlassPacket (" +
                                      "Id INT AUTO_INCREMENT PRIMARY KEY, " +
                                      "АртикулСП VARCHAR(255) NOT NULL, " +
                                      "Камерность INT NOT NULL, " +
                                      "ТолщинаСП DECIMAL(5,2) NOT NULL, " +
                                      "ТолщинаСтекла DECIMAL(5,2) NOT NULL)";
                command.ExecuteNonQuery();

            }
        }

        public void CreateGlassPacket(GlassPacket glassPacket) 
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO InformationGlassPacket (АртикулСП,Камерность,ТолщинаСП,ТолщинаСтекла) " +
                                      "VALUES (@АртикулСП,@Камерность,@ТолщинаСП,@ТолщинаСтекла)";

                command.Parameters.AddWithValue("@АртикулСП", glassPacket.ArticleGP);
                command.Parameters.AddWithValue("@Камерность", glassPacket.Chambering);
                command.Parameters.AddWithValue("@ТолщинаСП", glassPacket.ThicknessGP);
                command.Parameters.AddWithValue("@ТолщинаСтекла", glassPacket.ThicknessGlass);
                command.ExecuteNonQuery();

            }
        }

        public void GetGlassPacketByArticle(string inputArticle)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM InformationGlassPacket " +
                                      "WHERE АртикулСП = @АртикулСП";

                command.Parameters.AddWithValue("@АртикулСП", inputArticle);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"Артикул {reader["АртикулСП"]}");
                        Console.WriteLine($"Камерность => {reader["Камерность"]}");
                        Console.WriteLine($"Толщина СП => {reader["ТолщинаСП"]}");
                        Console.WriteLine($"Толщина стекла => {reader["ТолщинаСтекла"]}");
                    }
                    else Console.WriteLine($"Артикул {inputArticle} не найден.");
                }
            }
        }
        public void DropTableInformationGlassPacket()
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using (MySqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "DROP TABLE IF EXISTS informationglasspacket";
                command.ExecuteNonQuery();
            }
        }
    }
}
