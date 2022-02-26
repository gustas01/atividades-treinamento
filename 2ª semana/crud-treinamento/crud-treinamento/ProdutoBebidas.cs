using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    internal class ProdutoBebidas : Produto
    {

        private bool alcoolica;
        private bool habilitado;

        public ProdutoBebidas()
        {

        }

        public ProdutoBebidas(string nome, float preco, string marca, bool alcoolica)
        {
            this.nome = nome;
            this.preco = preco;
            this.marca = marca;
            this.alcoolica = alcoolica;
        }

        public String Nome
        {
            get { return this.nome; }
            set { this.nome = value; }
        }



        public float Preco
        {
            get { return this.preco; }
            set { this.preco = value; }
        }


        public String Marca
        {
            get { return this.marca; }
            set { this.marca = value; }
        }

        public bool Alcoolica
        {
            get { return this.alcoolica; }
            set { this.alcoolica = value; }
        }

        public bool Habilitado
        {
            get { return this.habilitado; }
            set { this.habilitado = value; }
        }


    }
}

