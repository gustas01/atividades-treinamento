using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class DALLATICINIO
    {
        ConexaoBD conexaobd = new ConexaoBD();

        public DALLATICINIO() {
        }


        public void GetTodosRegistrosProdutoLaticinio(List<Produto> produtoList)
        {
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {
                    // abre a conexão com o PgSQL e define a instrução SQL
                    conexaobd.pgsqlConnection.Open();

                    string cmdSeleciona = "Select P.id, P.nome, P.preco, P.marca, P.tipo, L.desnatado from produtoslaticinio L " +
                        "INNER JOIN produtos P ON L.idproduto_fk = P.id order by P.id";

                    using (NpgsqlCommand Adpt = new NpgsqlCommand(cmdSeleciona, conexaobd.pgsqlConnection))
                    {
                       NpgsqlDataReader reader = Adpt.ExecuteReader();

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
                    }
                }
            }
            catch(NpgsqlException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexaobd.pgsqlConnection.Close();
            }
        }

       public string GetRegistroPorIdProdutoLaticinio(int id)
        {
            ProdutoLaticinio produtoLaticinio = new ProdutoLaticinio();
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {
                    // abre a conexão com o PgSQL e define a instrução SQL
                    conexaobd.pgsqlConnection.Open();
                    string cmdSeleciona = $"Select (desnatado) from produtoslaticinio where idproduto_fk={id}";

                    using (NpgsqlCommand Adpt = new NpgsqlCommand(cmdSeleciona, conexaobd.pgsqlConnection))
                    {
                        NpgsqlDataReader reader = Adpt.ExecuteReader();

                        if (reader.HasRows && reader.Read())
                            return reader.GetString(0);
                                
                    }
                }   
            }
            catch(NpgsqlException ex)
            {
                throw ex;
            }
            catch( Exception ex)
            {
                throw ex ;
            }
            finally
            {
                conexaobd.pgsqlConnection.Close();
            }
            return null;
        }


       public int InserirRegistroProdutoLaticinio(string nome, double preco, string marca, string tipo, bool desnatado)
        {
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {

                    // Abra a conexão com o PgSQL
                    conexaobd.pgsqlConnection.Open();

                    string cmdInserir = String.Format("Insert into produtos(nome, preco, marca, tipo)" +
                        $" values('{nome}', '{preco}', '{marca}', '{tipo}') RETURNING id");


                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdInserir, conexaobd.pgsqlConnection))
                    {
                        int prodID = (int) pgsqlcommand.ExecuteScalar();

                        string cmdInserirProdutoLaticinio = String.Format("" +
                            $"insert into produtoslaticinio(desnatado, idproduto_fk) values ('{desnatado}', {prodID})");

                        using (NpgsqlCommand pgsqlcommandProdutoLaticinio = new NpgsqlCommand(
                            cmdInserirProdutoLaticinio, conexaobd.pgsqlConnection))
                        {
                            pgsqlcommandProdutoLaticinio.ExecuteNonQuery();
                        }


                        return prodID;
                    }
                }
            }
            catch(NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexaobd.pgsqlConnection.Close();
            }
        }


        public void AtualizarRegistroProdutoLaticinio(int id, string nome, double preco, string marca, bool desnatado)
            {
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {


                    //Abra a conexão com o PgSQL
                    conexaobd.pgsqlConnection.Open();

                    string cmdAtualiza = String.Format($"Update produtos Set nome = '{nome}', preco = {preco}, marca = '{marca}' Where id = {id}");

                    string cmdAtualizaProdutoLaticinio = String.Format($"UPDATE produtoslaticinio SET desnatado = '{desnatado}' WHERE idproduto_fk = {id}");


                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdAtualiza, conexaobd.pgsqlConnection))
                    {
                        pgsqlcommand.ExecuteNonQuery();
                    }

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdAtualizaProdutoLaticinio, conexaobd.pgsqlConnection))
                    {
                        pgsqlcommand.ExecuteNonQuery();
                    }


                }
            }
            catch(NpgsqlException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexaobd.pgsqlConnection.Close();
            }
        }


       public void DeletarRegistroProdutoLaticinio(int id)
        {
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {
                    //abre a conexao
                    conexaobd.pgsqlConnection.Open();

                    string cmdDeletarProdutoLaticinio = String.Format(
                        $"Delete from produtosLaticinio Where idproduto_fk = '{id}' RETURNING id;" +
                        $" DELETE FROM produtos WHERE id = {id}");

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdDeletarProdutoLaticinio, conexaobd.pgsqlConnection))
                    {
                        pgsqlcommand.ExecuteNonQuery();
                    }
                }
            }
            catch(NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexaobd.pgsqlConnection.Close();
            }
        }


    }
}
