using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace crud_treinamento
{
    public class DALLIMPEZA : IDALProdutos
    {
        //ConexaoPGBD conexaobd = new ConexaoPGBD();
        IConexaoBD connectBD;
        

        public DALLIMPEZA(IConexaoBD connectBD) {
            this.connectBD = connectBD;
        }


        public void GetTodosRegistros(List<Produto> produtoList)
        {
            try
            {
                connectBD.Open();

                string cmdSeleciona = "Select P.id, P.nome, P.preco, P.marca, P.tipo, L.cheiro from produtoslimpeza L " +
                    "INNER JOIN produtos P ON L.idproduto_fk = P.id order by P.id";

                DbDataReader reader = connectBD.getAll(cmdSeleciona, produtoList);

                if (reader.HasRows)
                    while (reader.Read())
                        produtoList.Add(new ProdutoLimpeza()
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Preco = reader.GetDouble(2),
                            Marca = reader.GetString(3),
                            Tipo = reader.GetString(4),
                            Cheiro = reader.GetString(5),
                        });
            }
            catch (Exception ex)
            {
                if(ex.Message == "Falha no Open")                
                    throw new Exception("Falha ao tentar abrir conexao");
                else
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

            string cmdSeleciona = $"Select P.id, P.nome, P.preco, P.marca, P.tipo, L.cheiro from produtoslimpeza L " +
                $"INNER JOIN produtos P ON L.idproduto_fk = P.id where P.id = {id}";

            DbDataReader reader = connectBD.getById(cmdSeleciona);

            if (reader.HasRows && reader.Read())
                return new ProdutoLimpeza()
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Preco = reader.GetDouble(2),
                    Marca = reader.GetString(3),
                    Tipo = reader.GetString(4),
                    Cheiro = reader.GetString(5),
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

                string cheiro;
                produto.TryGetValue("cheiro", out cheiro);

                double novoPreco = Convert.ToDouble(preco, System.Globalization.CultureInfo.InvariantCulture);


                connectBD.Open();

                string cmdInserir = String.Format("Insert into produtos(nome, preco, marca, tipo)" +
                    $" values('{nome}', '{novoPreco}', '{marca}', '{tipo}') RETURNING id");


                int prodID = connectBD.insereProduto(cmdInserir);

                cmdInserir = String.Format("" +
                    $"insert into produtoslimpeza(cheiro, idproduto_fk) values ('{cheiro}', {prodID})");

                connectBD.insereProdutoEspecifico(cmdInserir);


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

            string cheiro;
            produto.TryGetValue("cheiro", out cheiro);

            int novoId = Convert.ToInt32(id, System.Globalization.CultureInfo.InvariantCulture);
            double novoPreco = Convert.ToDouble(preco, System.Globalization.CultureInfo.InvariantCulture);

            connectBD.Open();

             string cmdAtualiza = String.Format($"Update produtos Set nome = '{nome}', preco = {novoPreco}, marca = '{marca}' Where id = {novoId}");

            connectBD.update(cmdAtualiza);

            cmdAtualiza = String.Format($"UPDATE produtoslimpeza SET cheiro = '{cheiro}' WHERE idproduto_fk = {novoId}");


            connectBD.update(cmdAtualiza);
           
                
            connectBD.Close();
           
        }


        public void DeletarRegistro(int id)
        {
                connectBD.Open();

                    string cmdDeletarProdutoLimpeza = String.Format(
                        $"Delete from produtoslimpeza Where idproduto_fk = '{id}' RETURNING id;" +
                        $" DELETE FROM produtos WHERE id = {id}");

            connectBD.delete(cmdDeletarProdutoLimpeza);

            connectBD.Close();
            
        }


    }
}
