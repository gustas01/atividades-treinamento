using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class ProdutoLimpeza : Produto, IProduto
    {
        private string cheiro;
        private int idLimpeza;
        public ProdutoLimpeza()
        {

        }

        public ProdutoLimpeza(string nome, double preco, string marca, string cheiro)
        {
            this.nome = nome;
            this.preco = preco;
            this.marca = marca;
            this.cheiro = cheiro;

        }

        public string Nome
        {
            get { return this.nome; }
            set{ this.nome = value; }
        }

        public double Preco
        {
            get { return this.preco; }
            set{ this.preco = value; }
        }


        public string Marca
        {
            get { return this.marca; }
            set{ this.marca = value; }
        }

        public string Cheiro
        {
            get { return this.cheiro; }
            set { this.cheiro = value; }
        }

        public int IdLimpeza
        {
            get { return this.idLimpeza; }
            set { this.idLimpeza = value; }
        }

    }
}
