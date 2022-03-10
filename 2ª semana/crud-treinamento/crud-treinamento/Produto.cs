using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public class Produto
    {
        protected int id { get; set; }
        protected string nome { get; set; }
        protected double preco { get; set; }
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



        public double Preco
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

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}
