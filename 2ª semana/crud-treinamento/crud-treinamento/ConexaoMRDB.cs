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
    internal class ConexaoMRDB : IConexaoBD
    {
        public string serverName = "localhost";
        public string port = "3306";
        public string userName = "root";
        public string password = "mariadb";
        public string databaseName = "ProdutosMercado";
        public MySqlConnection sqlConnection = null;
        public string connString = null;

        


        public ConexaoMRDB()
        {
            connString = String.Format($"Server={serverName}; Port={port};User Id={userName}; Password={password};Database={databaseName}");
        }

        public void Open()
        {
            try
            {
                sqlConnection = new MySqlConnection(connString);

                // abre a conexão com o PgSQL e define a instrução SQL
                sqlConnection.Open();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Falha ao se conectar ao banco");
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Close()
        {
            sqlConnection.Dispose();
            sqlConnection.Close();
        }

        public DbConnection retornaConexao()
        {
            return sqlConnection;
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
            catch (MySqlException ex)
            {
                MessageBox.Show("Falha ao inserir produto");
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

