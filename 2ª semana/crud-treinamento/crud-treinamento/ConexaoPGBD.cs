using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace crud_treinamento
{
    public class ConexaoPGBD : IConexaoBD
    {
        private string serverName = "localhost";
        private string port = "5432";
        private string userName = "postgres";
        private string password = "postgres";
        private string databaseName = "ProdutosMercado";
        private NpgsqlConnection sqlConnection = null;
        private string connString = null;


        public ConexaoPGBD()
        {
            connString = String.Format($"Server={serverName};Port={port};User Id={userName};Password={password};Database={databaseName};");

            sqlConnection = new NpgsqlConnection(connString);
        }

        ~ConexaoPGBD()
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

                using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(query, sqlConnection))
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
            using (NpgsqlCommand pgsqlcommandProdutoLaticinio = new NpgsqlCommand(
                            query, sqlConnection))
            {
                pgsqlcommandProdutoLaticinio.ExecuteNonQuery();
            }
        }

        public DbDataReader getAll(string query, List<Produto> produtoList)
        {
            try
            {

                using (NpgsqlCommand Adpt = new NpgsqlCommand(query, sqlConnection))
                {
                    NpgsqlDataReader reader = Adpt.ExecuteReader();

                    return reader;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Falha ao ler tabela(s)");
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
                using (NpgsqlCommand Adpt = new NpgsqlCommand(query, sqlConnection))
                {
                    NpgsqlDataReader reader = Adpt.ExecuteReader();

                    return reader;

                }

            }
            catch (NpgsqlException ex)
            {
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
                using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(query, sqlConnection))
                {
                    pgsqlcommand.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException ex)
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
                using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(query, sqlConnection))
                {
                    pgsqlcommand.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Falha ao atualizar registro");
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
