using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class ConexaoPGBD : IConexaoBD
    {
        public string serverName = "localhost";
        public string port = "5432";        
        public string userName = "postgres";
        public string password = "postgres";
        public string databaseName = "ProdutosMercado";
        public NpgsqlConnection pgsqlConnection = null;
        public string connString = null;


        public ConexaoPGBD()
        {
            connString = String.Format($"Server={serverName};Port={port};User Id={userName};Password={password};Database={databaseName};");
        }

        public void conectar()
        {
           
        }

        public void desconectar()
        {
           
        }
    }
}
