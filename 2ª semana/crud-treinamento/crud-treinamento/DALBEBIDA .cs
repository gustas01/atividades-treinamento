using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class DALBEBIDA : IDALProdutos
    {
        IConexaoBD connectBD;

        public DALBEBIDA(IConexaoBD connectBD)
        {
            this.connectBD = connectBD;
        }


        public void GetTodosRegistros(List<Produto> produtoList)
        {
            try
            {

                connectBD.Open();

                string cmdSeleciona = "Select P.id, P.nome, P.preco, P.marca, P.tipo, B.alcoolica from produtosbebida B " +
                    "INNER JOIN produtos P ON B.idproduto_fk = P.id order by P.id";

            DbDataReader reader = connectBD.getAll(cmdSeleciona, produtoList);

            if (reader.HasRows)
                while (reader.Read())
                    produtoList.Add(new ProdutoBebida()
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Preco = reader.GetDouble(2),
                        Marca = reader.GetString(3),
                        Tipo = reader.GetString(4),
                        Alcoolica = reader.GetBoolean(5),
                    });

            }catch (Exception ex)
            {
                throw ex;
            }
            finally { 
                connectBD.Close();
            }

        }

        public Produto GetRegistroPorId(int id)
        {
            ProdutoBebida produtoBebida = new ProdutoBebida();
           
                connectBD.Open();
                string cmdSeleciona = $"Select P.id, P.nome, P.preco, P.marca, P.tipo, B.alcoolica from produtosbebida B " +
                    $"INNER JOIN produtos P ON B.idproduto_fk = P.id where idproduto_fk={id}";

            DbDataReader reader = connectBD.getById(cmdSeleciona);

            if (reader.HasRows && reader.Read())
                return new ProdutoBebida()
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Preco = reader.GetDouble(2),
                    Marca = reader.GetString(3),
                    Tipo = reader.GetString(4),
                    Alcoolica = reader.GetBoolean(5),
                };

            connectBD.Close();
            return null;
        }
            


        public int InserirRegistro(Dictionary<string, string> produto)
        {
            try
            {

                string nome;
                produto.TryGetValue("nome", out nome);

                string preco;
                produto.TryGetValue("preco", out preco);

                string marca;
                produto.TryGetValue("marca", out marca);

                string tipo;
                produto.TryGetValue("tipo", out tipo);

                string alcoolica;
                produto.TryGetValue("alcoolica", out alcoolica);

                double novoPreco = Convert.ToDouble(preco, System.Globalization.CultureInfo.InvariantCulture);
                bool novoAlcoolica = Convert.ToBoolean(alcoolica, System.Globalization.CultureInfo.InvariantCulture);

                connectBD.Open();

                string cmdInserir = String.Format("Insert into produtos(nome, preco, marca, tipo)" +
                    $" values('{nome}', '{novoPreco}', '{marca}', '{tipo}') RETURNING id");


                int prodID = connectBD.insereProduto(cmdInserir);

                string cmdInserirProdutoBebida = String.Format("" +
                            $"insert into produtosbebida(alcoolica, idproduto_fk) values ({novoAlcoolica}, {prodID})");

                connectBD.insereProdutoEspecifico(cmdInserirProdutoBebida);




                return prodID;
            }catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connectBD.Close();
            }
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

            string alcoolica;
            produto.TryGetValue("alcoolica", out alcoolica);

            int novoId = Convert.ToInt32(id, System.Globalization.CultureInfo.InvariantCulture);
            double novoPreco = Convert.ToDouble(preco, System.Globalization.CultureInfo.InvariantCulture);
            bool novoAlcoolica = Convert.ToBoolean(alcoolica, System.Globalization.CultureInfo.InvariantCulture);

            connectBD.Open();

            string cmdAtualiza = String.Format($"Update produtos Set nome = '{nome}', preco = {novoPreco}, marca = '{marca}' Where id = {novoId}");

            connectBD.update(cmdAtualiza);

            cmdAtualiza = String.Format($"UPDATE produtosbebida SET alcoolica = {novoAlcoolica} WHERE idproduto_fk = {novoId}");


            connectBD.update(cmdAtualiza);

              connectBD.Close();
            
        }


        public void DeletarRegistro(int id)
        {
            
                connectBD.Open();

                    string cmdDeletarProdutoBebida = String.Format(
                        $"Delete from produtosbebida Where idproduto_fk = '{id}' RETURNING id;" +
                        $" DELETE FROM produtos WHERE id = {id}");

            connectBD.delete(cmdDeletarProdutoBebida);





            connectBD.Close();
            
        }


    }
}
