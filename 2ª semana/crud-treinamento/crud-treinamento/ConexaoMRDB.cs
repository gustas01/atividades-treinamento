using MySqlConnector;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace crud_treinamento
{
    public class ConexaoMRDB : IConexaoBD
    {
        private string serverName = "localhost";
        private string port = "3306";
        private string userName = "root";
        private string password = "mariadb";
        private string databaseName = "ProdutosMercado";
        private MySqlConnection sqlConnection = null;
        private string connString = null;

        


        public ConexaoMRDB()
        {
            connString = String.Format($"Server={serverName}; Port={port};User Id={userName}; Password={password};Database={databaseName}");


            sqlConnection = new MySqlConnection(connString);
        }

        ~ConexaoMRDB()
        {
            sqlConnection.Dispose();
        }

        public bool Open()
        {
            try
            {
                // abre a conexão com o PgSQL
                sqlConnection.Open();
                return true;

            }
            catch
            {
                throw new Exception("Falha no Open");
            }
        }

        public void Close()
        {
            try
            {
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        

        public int insereProduto(string query)
        {
            try
            {

                using (MySqlCommand pgsqlcommand = new MySqlCommand(query, sqlConnection))
                {
                    return (int)pgsqlcommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void insereProdutoEspecifico(string query)
        {
            using (MySqlCommand pgsqlcommandProdutoLaticinio = new MySqlCommand(
                           query, sqlConnection))
            {
                pgsqlcommandProdutoLaticinio.ExecuteNonQuery();
            }
        }

        public DbDataReader getAll(string query, List<Produto> produtoList)
        {
            try
            {
                using (MySqlCommand Adpt = new MySqlCommand(query, sqlConnection))
                {
                    MySqlDataReader reader = Adpt.ExecuteReader();

                    return reader;

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Falha ao ler tabela(s)");
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void update(string query)
        {
            try
            {
                using (MySqlCommand pgsqlcommand = new MySqlCommand(query, sqlConnection))
                {
                    pgsqlcommand.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Falha ao atualizar registro");
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void delete(string query)
        {
            try
            {
                using (MySqlCommand pgsqlcommand = new MySqlCommand(query, sqlConnection))
                {
                    pgsqlcommand.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Falha ao atualizar registro");
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DbDataReader getById(string query)
        {
            try
            {
                using (MySqlCommand Adpt = new MySqlCommand(query, sqlConnection))
                {
                    MySqlDataReader reader = Adpt.ExecuteReader();

                    return reader;

                }

            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}

