using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public interface IConexaoBD
    {
        
        int insereProduto(string query);
        void insereProdutoEspecifico(string query);
        DbDataReader getAll(string query, List<Produto> produtoList);
        void update(string query);
        void delete(string query);
        DbDataReader getById(string query);
        bool Open();
        void Close();
    }
}
