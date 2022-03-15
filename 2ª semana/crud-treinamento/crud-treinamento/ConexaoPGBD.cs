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
        public string serverName = "localhost";
        public string port = "5432";        
        public string userName = "postgres";
        public string password = "postgres";
        public string databaseName = "ProdutosMercado";
        public NpgsqlConnection sqlConnection = null;
        public string connString = null;

        //IDbConnection dbConnection;


        public ConexaoPGBD()//receber string no parâmetro do construtor
        {
            connString = String.Format($"Server={serverName};Port={port};User Id={userName};Password={password};Database={databaseName};");

            //dbConnection.ConnectionString = connString;
        }

        public void Open()
        {
            try
            {
                sqlConnection = new NpgsqlConnection(connString);

                // abre a conexão com o PgSQL e define a instrução SQL
                sqlConnection.Open();
                
            }
            catch (NpgsqlException ex)
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

        public DbConnection retornaConexao () {
                
            return sqlConnection;
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
            catch (NpgsqlException ex) {
                MessageBox.Show("Falha ao inserir produto");
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
