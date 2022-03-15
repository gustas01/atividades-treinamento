using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace crud_treinamento
{
    public class DAL
    {
        IConexaoBD connectBD;

        public DAL(IConexaoBD connectBD) {
            this.connectBD = connectBD;            
        }


        public List<Produto> GetTodosRegistrosProduto(List<IDALProdutos> listaProdutos)
        {
            
            List<Produto> produtoList = new List<Produto>();

            foreach (IDALProdutos produto in listaProdutos)
                produto.GetTodosRegistros(produtoList);

            return produtoList;
        }

    }
}
