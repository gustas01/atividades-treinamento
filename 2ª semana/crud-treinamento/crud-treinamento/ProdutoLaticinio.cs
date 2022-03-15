using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class ProdutoLaticinio : Produto, IProduto
    {

        private bool desnatado;

        public ProdutoLaticinio()
        {

        }

        public ProdutoLaticinio(string nome, double preco, string marca, bool desnatado)
        {
            this.nome = nome;
            this.preco = preco;
            this.marca = marca;
            this.desnatado = desnatado;
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

        public bool Desnatado
        {
            get { return this.desnatado; }
            set { this.desnatado = value; }
        }

    }
}
