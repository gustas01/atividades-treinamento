﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class DAL
    {
        private ConexaoBD conexaobd;
        private DALLIMPEZA modelLimpeza;
        private DALBEBIDA modelBebida;
        private DALLATICINIO modelLaticinio;

        public DAL() {
            modelLimpeza = new DALLIMPEZA();
            modelBebida = new DALBEBIDA();
            modelLaticinio = new DALLATICINIO();
            conexaobd = new ConexaoBD();
        }


        public List<Produto> GetTodosRegistrosProduto()
        {
            
            List<Produto> produtoList = new List<Produto>();

            try
            {
                modelLimpeza.GetTodosRegistrosProdutoLimpeza(produtoList);
                modelBebida.GetTodosRegistrosProdutoBebida(produtoList);
                modelLaticinio.GetTodosRegistrosProdutoLaticinio(produtoList);
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
                
            }
            return produtoList;
        }

       public DataTable GetRegistroPorIdProduto(int id)
        {
            DataTable dt = new DataTable();

            try
            {
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {
                    //Abra a conexão com o PgSQL
                    npgsqlConnection.Open();
                    string cmdSeleciona = "Select * from produtos Where id = " + id;

                    using (NpgsqlDataAdapter Adpt = new NpgsqlDataAdapter(cmdSeleciona, conexaobd.pgsqlConnection))
                    {
                        Adpt.Fill(dt);
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
            return dt ;
        }


       public int InserirRegistroProduto(string nome, double preco, string marca, string tipo)
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


        public void AtualizarRegistroProduto(int id, string nome, double preco, string marca)
            {
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {


                    //Abra a conexão com o PgSQL
                    conexaobd.pgsqlConnection.Open();

                    string cmdAtualiza = String.Format($"Update produtos Set nome = '{nome}', preco = '{preco}', "+
                                            $"marca = '{marca}' Where id = '{id}'");


                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdAtualiza, conexaobd.pgsqlConnection))
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


       public void DeletarRegistroProduto(int id)
        {
            try
            {
                using (conexaobd.pgsqlConnection = new NpgsqlConnection(conexaobd.connString))
                {
                    //abre a conexao
                    conexaobd.pgsqlConnection.Open();

                    string cmdDeletar = String.Format($"Delete from produtos Where id = '{id}'");

                    using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(cmdDeletar, conexaobd.pgsqlConnection))
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
