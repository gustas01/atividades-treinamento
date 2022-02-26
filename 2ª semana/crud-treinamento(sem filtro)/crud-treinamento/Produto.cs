using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public abstract class Produto
    {
        protected string nome { get; set; }
        protected float preco { get; set; }

        public Produto()
        {

        }
    }
}
