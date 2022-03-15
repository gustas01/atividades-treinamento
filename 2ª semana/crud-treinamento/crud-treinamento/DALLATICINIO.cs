using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class DALLATICINIO : IDALProdutos
    {
        IConexaoBD connectBD;

        public DALLATICINIO(IConexaoBD connectBD) {
            this.connectBD = connectBD;
        }


        public void GetTodosRegistros(List<Produto> produtoList)
        {       
            connectBD.Open();

            string cmdSeleciona = "Select P.id, P.nome, P.preco, P.marca, P.tipo, L.desnatado from produtoslaticinio L " +
                "INNER JOIN produtos P ON L.idproduto_fk = P.id order by P.id";

            try
            {

            DbDataReader reader = connectBD.getAll(cmdSeleciona, produtoList);

            if (reader.HasRows)
                while (reader.Read())
                    produtoList.Add(new ProdutoLaticinio()
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Preco = reader.GetDouble(2),
                        Marca = reader.GetString(3),
                        Tipo = reader.GetString(4),
                        Desnatado = reader.GetBoolean(5),
                    });
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                connectBD.Close();
            }

        }

        public Produto GetRegistroPorId(int id)
        {
            connectBD.Open();

            string cmdSeleciona = $"Select P.id, P.nome, P.preco, P.marca, P.tipo, L.desnatado from produtoslaticinio L " +
                $"INNER JOIN produtos P ON L.idproduto_fk = P.id P.id where idproduto_fk={id}";


            DbDataReader reader = connectBD.getById(cmdSeleciona);

            if (reader.HasRows && reader.Read())
                return new ProdutoLaticinio()
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Preco = reader.GetDouble(2),
                    Marca = reader.GetString(3),
                    Tipo = reader.GetString(4),
                    Desnatado = reader.GetBoolean(5),
                };

            connectBD.Close();
            return null;
        }


        public int InserirRegistro(Dictionary<string, string> produto)
        {
            string nome;
            produto.TryGetValue("nome", out nome);

            string preco;
            produto.TryGetValue("preco", out preco);

            string marca;
            produto.TryGetValue("marca", out marca);

            string tipo;
            produto.TryGetValue("tipo", out tipo);

            string desnatado;
            produto.TryGetValue("desnatado", out desnatado);

            double novoPreco = Convert.ToDouble(preco, System.Globalization.CultureInfo.InvariantCulture);
            bool novoDesnatado = Convert.ToBoolean(desnatado, System.Globalization.CultureInfo.InvariantCulture);

            connectBD.Open();

            string cmdInserir = String.Format("Insert into produtos(nome, preco, marca, tipo)" +
                $" values('{nome}', '{novoPreco}', '{marca}', '{tipo}') RETURNING id");
                    
            int prodID = connectBD.insereProduto(cmdInserir);

            cmdInserir = String.Format("" +
                $"insert into produtoslaticinio(desnatado, idproduto_fk) values ({novoDesnatado}, {prodID})");

            connectBD.insereProdutoEspecifico(cmdInserir);

            connectBD.Close();
                
            return prodID;
            
        }


        public void AtualizarRegistro(Dictionary<string, string> produto)
            {
            string id;
            produto.TryGetValue("id", out id);
            
            string nome;
            produto.TryGetValue("nome", out nome);

            string preco;
            produto.TryGetValue("preco", out preco);

            string marca;
            produto.TryGetValue("marca", out marca);

            string desnatado;
            produto.TryGetValue("desnatado", out desnatado);

            int novoId = Convert.ToInt32(id, System.Globalization.CultureInfo.InvariantCulture);
            double novoPreco = Convert.ToDouble(preco, System.Globalization.CultureInfo.InvariantCulture);
            bool novoDesnatado = Convert.ToBoolean(desnatado, System.Globalization.CultureInfo.InvariantCulture);


            connectBD.Open();

             string cmdAtualiza = String.Format($"Update produtos Set nome = '{nome}', preco = {novoPreco}, marca = '{marca}' Where id = {novoId}");

            connectBD.update(cmdAtualiza);

            string cmdAtualizaProdutoLaticinio = String.Format($"UPDATE produtoslaticinio SET desnatado = {novoDesnatado} WHERE idproduto_fk = {novoId}");

            connectBD.update(cmdAtualizaProdutoLaticinio);
           
            connectBD.Close();
        }


        public void DeletarRegistro(int id)
        {
           
            connectBD.Open();

            string cmdDeletarProdutoLaticinio = String.Format(
                $"Delete from produtosLaticinio Where idproduto_fk = '{id}' RETURNING id;" +
                $" DELETE FROM produtos WHERE id = {id}");

            connectBD.delete(cmdDeletarProdutoLaticinio);
                
           
            connectBD.Close();
           
        }


    }
}
