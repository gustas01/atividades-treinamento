using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class DALBEBIDA
    {
        ConexaoPGBD conexaobd = new ConexaoPGBD();

        public DALBEBIDA() {
        }


        public void GetTodosRegistrosProdutoBebida(List<Produto> produtoList)
        {
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {
                    // abre a conexão com o PgSQL e define a instrução SQL
                    conexaobd.pgsqlConnection.Open();

                    string cmdSeleciona = "Select P.id, P.nome, P.preco, P.marca, P.tipo, B.alcoolica from produtosbebida B " +
                        "INNER JOIN produtos P ON B.idproduto_fk = P.id order by P.id";

                    using (NpgsqlCommand Adpt = new NpgsqlCommand(cmdSeleciona, conexaobd.pgsqlConnection))
                    {
                       NpgsqlDataReader reader = Adpt.ExecuteReader();

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

       public string GetRegistroPorIdProdutoBebida(int id)
        {
            ProdutoBebida produtoBebida = new ProdutoBebida();
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {
                    // abre a conexão com o PgSQL e define a instrução SQL
                    conexaobd.pgsqlConnection.Open();
                    string cmdSeleciona = $"Select P.id, P.nome, P.preco, P.marca, P.tipo, B.alcoolica from produtosbebida B " +
                        $"INNER JOIN produtos P ON B.idproduto_fk = P.id where idproduto_fk={id}";

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


       public int InserirRegistroProdutoBebida(string nome, double preco, string marca, string tipo, bool alcoolica)
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

                        string cmdInserirProdutoBebida = String.Format("" +
                            $"insert into produtosbebida(alcoolica, idproduto_fk) values ({alcoolica}, {prodID})");

                        using (NpgsqlCommand pgsqlcommandProdutoBebida = new NpgsqlCommand(
                            cmdInserirProdutoBebida, conexaobd.pgsqlConnection))
                        {
                            pgsqlcommandProdutoBebida.ExecuteNonQuery();
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


        public void AtualizarRegistroProdutoBebida(int id, string nome, double preco, string marca, bool alcoolica)
            {
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {


                    //Abra a conexão com o PgSQL
                    conexaobd.pgsqlConnection.Open();

                    string cmdAtualiza = String.Format($"Update produtos Set nome = '{nome}', preco = {preco}, marca = '{marca}' Where id = {id}");

                    string cmdAtualizaProdutoBebida = String.Format($"UPDATE produtosbebida SET alcoolica = '{alcoolica}' WHERE idproduto_fk = {id}");


                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdAtualiza, conexaobd.pgsqlConnection))
                    {
                        pgsqlcommand.ExecuteNonQuery();
                    }

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdAtualizaProdutoBebida, conexaobd.pgsqlConnection))
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


       public void DeletarRegistroProdutoBebida(int id)
        {
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {
                    //abre a conexao
                    conexaobd.pgsqlConnection.Open();

                    string cmdDeletarProdutoBebida = String.Format(
                        $"Delete from produtosbebida Where idproduto_fk = '{id}' RETURNING id;" +
                        $" DELETE FROM produtos WHERE id = {id}");

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdDeletarProdutoBebida, conexaobd.pgsqlConnection))
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
