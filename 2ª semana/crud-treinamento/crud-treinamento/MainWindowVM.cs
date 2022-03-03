using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace crud_treinamento
{
    internal class MainWindowVM : INotifyPropertyChanged
    {

        public ObservableCollection<Produto> produtos { get; set; }
        public ObservableCollection<Produto> produtosFiltrados { get; set; }
        

        public ICommand adiciona { get; set; }
        public ICommand remove { get; set; }
        public ICommand update { get; set; }
        public ICommand filtrar { get; set; }


        public Produto produtoSelecionado { get; set; }

        public string dadoFiltro { get; set; }
        public string dadoTipoCreate { get; set; }



        public MainWindowVM()
        {
            produtos = new ObservableCollection<Produto>();
            produtosFiltrados = new ObservableCollection<Produto>();
            

            adiciona = new RelayCommand((object objeto) =>
            {
                switch (dadoTipoCreate)
                {
                    case "Limpeza":
                        CreateWindowProdutoLimpeza windowlimpeza = new CreateWindowProdutoLimpeza();

                        ProdutoLimpeza produtoLimpeza = new ProdutoLimpeza();
                        windowlimpeza.DataContext = produtoLimpeza;
                        windowlimpeza.ShowDialog();

                        produtoLimpeza.Tipo = "Limpeza";
                        produtos.Add(produtoLimpeza);
                        produtosFiltrados.Add(produtoLimpeza);
                        
                       
                        break;

                    case "Bebida":
                        CreateWindowProdutoBebida windowBebida = new CreateWindowProdutoBebida();

                        ProdutoBebida produtoBebida = new ProdutoBebida();
                        windowBebida.DataContext = produtoBebida;
                        windowBebida.ShowDialog();

                        produtoBebida.Tipo = "Bebida";
                        produtos.Add(produtoBebida);
                        produtosFiltrados.Add(produtoBebida);
                        break;

                    case "Laticinio":
                        CreateWindowProdutoLaticinio windowLaticinio = new CreateWindowProdutoLaticinio();

                        ProdutoLaticinio produtoLaticinio = new ProdutoLaticinio();
                        windowLaticinio.DataContext = produtoLaticinio;
                        windowLaticinio.ShowDialog();

                        produtoLaticinio.Tipo = "Laticinio";
                        produtos.Add(produtoLaticinio); 
                        produtosFiltrados.Add(produtoLaticinio);
                        break;
                }
                
            });

            update = new RelayCommand((object objeto) =>
            {
               

                Produto produtoAux = produtoSelecionado;

                switch (produtoSelecionado.Tipo)
                {
                    case "Limpeza":
                        CreateWindowProdutoLimpeza windowLimpeza = new CreateWindowProdutoLimpeza();

                        windowLimpeza.DataContext = produtoSelecionado;
                        windowLimpeza.ShowDialog();
                        break;

                    case "Bebida":
                        CreateWindowProdutoLimpeza windowBebida = new CreateWindowProdutoLimpeza();

                        windowBebida.DataContext = produtoSelecionado;
                        windowBebida.ShowDialog();
                        break;

                    case "Laticinio":
                        CreateWindowProdutoLimpeza windowLaticinio = new CreateWindowProdutoLimpeza();

                        windowLaticinio.DataContext = produtoSelecionado;
                        windowLaticinio.ShowDialog();
                        break;

                }

            });

            remove = new RelayCommand((object objeto) =>
            {
                produtos.Remove(produtoSelecionado);
                produtosFiltrados.Remove(produtoSelecionado);
            });

            filtrar = new RelayCommand((object objeto) =>
            {
                


                for (int i = produtosFiltrados.Count-1; i >= 0; i--)
                {
                    produtosFiltrados.Remove(produtosFiltrados[i]);
                }
               
                switch (dadoFiltro)
                {
                    case "Limpeza":

                        for (int i = 0; i < produtos.Count; i++)
                            if (produtos[i].Tipo == "Limpeza")
                                produtosFiltrados.Add(produtos[i]);
                            //produtos[i].Tipo == "Limpeza" ? produtosFiltrados.Add(produtos[i]) : false;

                           

                        break;

                    case "Bebida":
                        for (int i = 0; i < produtos.Count; i++)
                            if (produtos[i].Tipo == "Bebida")
                                produtosFiltrados.Add(produtos[i]);

                        
     
                        break;

                    case "Laticinio":
                        for (int i = 0; i < produtos.Count; i++)
                            if (produtos[i].Tipo == "Laticinio")
                                produtosFiltrados.Add(produtos[i]);

                        
                        break;

                    default:
                        for (int i = 0; i < produtos.Count; i++)
                            produtosFiltrados.Add(produtos[i]);
                        
                        break;
                }
            });
            
            
        }





        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
