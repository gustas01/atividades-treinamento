using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class ConexaoBD
    {
        public string serverName = "localhost";                                          //localhost
        public string port = "5432";                                                            //porta default
        public string userName = "postgres";                                               //nome do administrador
        public string password = "postgres";                                             //senha do administrador
        public string databaseName = "ProdutosMercado";                                       //nome do banco de dados
        public NpgsqlConnection pgsqlConnection = null;
        public string connString = null;


        public ConexaoBD()
        {
            connString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};"
                , serverName, port, userName, password, databaseName);
        }

    }
}
