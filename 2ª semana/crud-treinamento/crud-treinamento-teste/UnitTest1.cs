using Xunit;
using crud_treinamento;
using System.Collections.Generic;
using System.Data.Common;
using System;

namespace crud_treinamento_teste
{
    public class UnitTest1
    {     
        [Fact]
        public void BancoForaDoAr()
        {
            Moq.Mock<IConexaoBD> mock = new Moq.Mock<IConexaoBD>();

            mock.Setup(banco => banco.Open()).Throws(new Exception("Falha no Open"));

           
            IDALProdutos produtos = new DALLIMPEZA(mock.Object);

            string msg;

            msg = Assert.Throws<Exception>(() => produtos.GetTodosRegistros(new List<Produto>())).Message;


            Assert.Equal("Falha ao tentar abrir conexao", msg);
            
        }

        [Fact]
        public void FalhaAoInserirNoBanco()
        {
            Moq.Mock<IConexaoBD> mock = new Moq.Mock<IConexaoBD>();

            //mock.Setup(banco => banco.Open()).Throws(new Exception("Falha ao inserir no banco"));

            IDALProdutos prodInsereBebida = new DALBEBIDA(mock.Object);

            Dictionary<string, string> props = new Dictionary<string, string>();

            ProdutoBebida produtoBebida = new ProdutoBebida()
            {
                Nome = "nomeTeste",
                Preco = 10,
                Marca = "marcaTeste",
                Tipo = "tipoTeste",
                Alcoolica = true
            };

            props.Clear();

            props.Add("nome", produtoBebida.Nome);
            props.Add("preco", produtoBebida.Preco.ToString());
            props.Add("marca", produtoBebida.Marca);
            props.Add("tipo", produtoBebida.Tipo);
            props.Add("alcoolica", produtoBebida.Alcoolica.ToString());

            string cmdInserir = String.Format("Insert into produtos(nome, preco, marca, tipo)" +
                    $" values('{props["nome"]}', '{props["preco"]}', '{props["marca"]}', '{props["tipo"]}') RETURNING id");

            mock.Setup(banco => banco.insereProduto(cmdInserir)).Throws(new Exception("Falha ao inserir no banco"));

            string msg = Assert.Throws<Exception>(() => prodInsereBebida.InserirRegistro(props)).Message;

            Assert.Equal("Falha ao inserir no banco", msg);
        }
    }
}