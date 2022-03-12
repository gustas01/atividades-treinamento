using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace crud_treinamento
{
    public class DALLIMPEZA
    {
        ConexaoPGBD conexaobd = new ConexaoPGBD();

        public DALLIMPEZA() {
        }


        public void GetTodosRegistrosProdutoLimpeza(List<Produto> produtoList)
        {
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {
                    // abre a conexão com o PgSQL e define a instrução SQL
                    conexaobd.pgsqlConnection.Open();

                    string cmdSeleciona = "Select P.id, P.nome, P.preco, P.marca, P.tipo, L.cheiro from produtoslimpeza L " +
                        "INNER JOIN produtos P ON L.idproduto_fk = P.id order by P.id";

                    using (NpgsqlCommand Adpt = new NpgsqlCommand(cmdSeleciona, conexaobd.pgsqlConnection))
                    {
                       NpgsqlDataReader reader = Adpt.ExecuteReader();

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

       public ProdutoLimpeza GetRegistroPorIdProdutoLimpeza(int id)
        {
            ProdutoLimpeza produtoLimpeza = new ProdutoLimpeza();
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {
                    // abre a conexão com o PgSQL e define a instrução SQL
                    conexaobd.pgsqlConnection.Open();
                    string cmdSeleciona = $"Select P.id, P.nome, P.preco, P.marca, P.tipo, L.cheiro from produtoslimpeza L " +
                        $"INNER JOIN produtos P ON L.idproduto_fk = P.id where P.id = {id}";

                    using (NpgsqlCommand Adpt = new NpgsqlCommand(cmdSeleciona, conexaobd.pgsqlConnection))
                    {
                        NpgsqlDataReader reader = Adpt.ExecuteReader();

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


       public int InserirRegistroProdutoLimpeza(string nome, double preco, string marca, string tipo, string cheiro)
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

                        string cmdInserirProdutoLimpeza = String.Format("" +
                            $"insert into produtoslimpeza(cheiro, idproduto_fk) values ('{cheiro}', {prodID})");

                        using (NpgsqlCommand pgsqlcommandProdutoLimpeza = new NpgsqlCommand(
                            cmdInserirProdutoLimpeza, conexaobd.pgsqlConnection))
                        {
                            pgsqlcommandProdutoLimpeza.ExecuteNonQuery();
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


        public void AtualizarRegistroProdutoLimpeza(int id, string nome, double preco, string marca, string cheiro)
            {
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {


                    //Abra a conexão com o PgSQL
                    conexaobd.pgsqlConnection.Open();

                    string cmdAtualiza = String.Format($"Update produtos Set nome = '{nome}', preco = {preco}, marca = '{marca}' Where id = {id}");

                    string cmdAtualizaProdutoLimpeza = String.Format($"UPDATE produtoslimpeza SET cheiro = '{cheiro}' WHERE idproduto_fk = {id}");


                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdAtualiza, conexaobd.pgsqlConnection))
                    {
                        pgsqlcommand.ExecuteNonQuery();
                    }

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdAtualizaProdutoLimpeza, conexaobd.pgsqlConnection))
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


       public void DeletarRegistroProdutoLimpeza(int id)
        {
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {
                    //abre a conexao
                    conexaobd.pgsqlConnection.Open();

                    string cmdDeletarProdutoLimpeza = String.Format(
                        $"Delete from produtoslimpeza Where idproduto_fk = '{id}' RETURNING id;" +
                        $" DELETE FROM produtos WHERE id = {id}");

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdDeletarProdutoLimpeza, conexaobd.pgsqlConnection))
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
