using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_treinamento
{
    public interface IDALProdutos
    {
        void GetTodosRegistros(List<Produto> produtoList);
        

        Produto GetRegistroPorId(int id);
        


        int InserirRegistro(Dictionary<string, string> produto);
        



        void AtualizarRegistro(Dictionary<string, string> produto);
        


        void DeletarRegistro(int id);
        
    }
}
