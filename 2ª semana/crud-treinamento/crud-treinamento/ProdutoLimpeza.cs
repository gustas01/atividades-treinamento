using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class ProdutoLimpeza : Produto
    {
        private String marca { get; set; }
        
        public ProdutoLimpeza()
        {

        }

        public ProdutoLimpeza(string nome, float preco, string marca)
        {
            this.nome = nome;
            this.preco = preco;
            this.marca = marca;
        }

        public String Nome
        {
            get { return this.nome; }
            set{ this.nome = value; }
        }



        public float Preco
        {
            get { return this.preco; }
            set{ this.preco = value; }
        }


        public String Marca
        {
            get { return this.marca; }
            set{ this.marca = value; }
        }

        
    }
}
