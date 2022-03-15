using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class ProdutoBebida : Produto, IProduto
    {

        private bool alcoolica;

        public ProdutoBebida()
        {

        }

        public ProdutoBebida(string nome, double preco, string marca, bool alcoolica)
        {
            this.nome = nome;
            this.preco = preco;
            this.marca = marca;
            this.alcoolica = alcoolica;
        }

        public string Nome
        {
            get { return this.nome; }
            set { this.nome = value; }
        }



        public double Preco
        {
            get { return this.preco; }
            set { this.preco = value; }
        }


        public string Marca
        {
            get { return this.marca; }
            set { this.marca = value; }
        }

        public bool Alcoolica
        {
            get { return this.alcoolica; }
            set { this.alcoolica = value; }
        }

    }
}

