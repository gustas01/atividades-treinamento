using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class Produto
    {
        protected string nome { get; set; }
        protected float preco { get; set; }
        protected string marca { get; set; }
        protected string tipo { get; set; }

        public Produto()
        {

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

        public String Tipo
        {
            get { return this.tipo; }
            set { this.tipo = value; }
        }
    }
}
