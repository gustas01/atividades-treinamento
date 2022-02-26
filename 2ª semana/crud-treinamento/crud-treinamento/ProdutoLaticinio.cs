using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    internal class ProdutoLaticinio : Produto
    {

        private bool desnatado { get; set; }

        public ProdutoLaticinio()
        {

        }

        public ProdutoLaticinio(string nome, float preco, string marca, bool desnatado)
        {
            this.nome = nome;
            this.preco = preco;
            this.marca = marca;
            this.desnatado = desnatado;
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

        public bool Desnatado
        {
            get { return this.desnatado; }
            set { this.desnatado = value; }
        }


    }
}
