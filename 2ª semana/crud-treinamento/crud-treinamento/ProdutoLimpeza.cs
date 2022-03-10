using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class ProdutoLimpeza : Produto
    {
        private String cheiro;
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

        public String Nome
        {
            get { return this.nome; }
            set{ this.nome = value; }
        }

        public double Preco
        {
            get { return this.preco; }
            set{ this.preco = value; }
        }


        public String Marca
        {
            get { return this.marca; }
            set{ this.marca = value; }
        }

        public String Cheiro
        {
            get { return this.cheiro; }
            set { this.cheiro = value; }
        }


    }
}
