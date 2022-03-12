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
        
        public List<Produto> produtos { get; set; }
        public ObservableCollection<Produto> produtosFiltrados { get; set; }


        public ICommand adiciona { get; set; }
        public ICommand remove { get; set; }
        public ICommand update { get; set; }
        public ICommand filtrar { get; set; }



        public Produto produtoSelecionado { get; set; }
        public DAL modelProdutos { get; set; }
        public DALLIMPEZA modelProdutosLimpeza { get; set; }
        public DALBEBIDA modelProdutosBebida { get; set; }
        public DALLATICINIO modelProdutosLaticinio { get; set; }
        public string dadoFiltro { get; set; }
        public string dadoTipoCreate { get; set; }



        public MainWindowVM()
        {
            modelProdutos = new DAL();
            modelProdutosLimpeza = new DALLIMPEZA();
            modelProdutosBebida = new DALBEBIDA();
            modelProdutosLaticinio = new DALLATICINIO();

            produtos = modelProdutos.GetTodosRegistrosProduto();
            produtosFiltrados = new ObservableCollection<Produto>(produtos);
            int prodID;

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

                        prodID = modelProdutosLimpeza.InserirRegistroProdutoLimpeza(produtoLimpeza.Nome, produtoLimpeza.Preco, produtoLimpeza.Marca,
                            produtoLimpeza.Tipo, produtoLimpeza.Cheiro);

                        produtoLimpeza.Id = prodID;
                        produtos.Add(produtoLimpeza);
                        produtosFiltrados.Add(produtoLimpeza);
                        
                       
                        break;

                    case "Bebida":
                        CreateWindowProdutoBebida windowBebida = new CreateWindowProdutoBebida();

                        ProdutoBebida produtoBebida = new ProdutoBebida();
                        windowBebida.DataContext = produtoBebida;
                        windowBebida.ShowDialog();

                        produtoBebida.Tipo = "Bebida";

                        prodID = modelProdutosBebida.InserirRegistroProdutoBebida(produtoBebida.Nome, produtoBebida.Preco, produtoBebida.Marca,
                            produtoBebida.Tipo, produtoBebida.Alcoolica);

                        produtoBebida.Id = prodID;

                        produtos.Add(produtoBebida);
                        produtosFiltrados.Add(produtoBebida);
                        break;

                    case "Laticinio":
                        CreateWindowProdutoLaticinio windowLaticinio = new CreateWindowProdutoLaticinio();

                        ProdutoLaticinio produtoLaticinio = new ProdutoLaticinio();
                        windowLaticinio.DataContext = produtoLaticinio;
                        windowLaticinio.ShowDialog();

                        produtoLaticinio.Tipo = "Laticinio";

                        prodID = modelProdutosLaticinio.InserirRegistroProdutoLaticinio(produtoLaticinio.Nome, produtoLaticinio.Preco, produtoLaticinio.Marca,
                            produtoLaticinio.Tipo, produtoLaticinio.Desnatado);

                        produtoLaticinio.Id = prodID;
                        produtos.Add(produtoLaticinio); 
                        produtosFiltrados.Add(produtoLaticinio);
                        break;
                }
                
            });

            update = new RelayCommand((object objeto) =>
            {

                switch (produtoSelecionado?.Tipo)
                {
                    case "Limpeza":
                        CreateWindowProdutoLimpeza windowLimpeza = new CreateWindowProdutoLimpeza();
                        
                        ProdutoLimpeza prodLimp = (ProdutoLimpeza) produtoSelecionado;
                        windowLimpeza.DataContext = prodLimp;
                        windowLimpeza.ShowDialog();

                        
                        
                        modelProdutosLimpeza.AtualizarRegistroProdutoLimpeza(prodLimp.Id, prodLimp.Nome, prodLimp.Preco,
                            prodLimp.Marca, prodLimp.Cheiro);

                        break;

                    case "Bebida":
                        CreateWindowProdutoBebida windowBebida = new CreateWindowProdutoBebida();

                        ProdutoBebida prodBeb = (ProdutoBebida) produtoSelecionado;
                        windowBebida.DataContext = prodBeb;
                        windowBebida.ShowDialog();

                        modelProdutosBebida.AtualizarRegistroProdutoBebida(prodBeb.Id, prodBeb.Nome, prodBeb.Preco,
                            prodBeb.Marca, prodBeb.Alcoolica);

                        break;

                    case "Laticinio":
                        CreateWindowProdutoLaticinio windowLaticinio = new CreateWindowProdutoLaticinio();

                        ProdutoLaticinio prodLat = (ProdutoLaticinio) produtoSelecionado;
                        windowLaticinio.DataContext = prodLat;
                        windowLaticinio.ShowDialog();

                        modelProdutosLaticinio.AtualizarRegistroProdutoLaticinio(prodLat.Id, prodLat.Nome, prodLat.Preco, prodLat.Marca, prodLat.Desnatado);
                        break;
                    default:
                        return;

                }
               
            });

            remove = new RelayCommand((object objeto) =>
            {
                if(produtoSelecionado != null)
                {
                    switch (produtoSelecionado.Tipo)
                    {
                        case "Limpeza":
                        modelProdutosLimpeza.DeletarRegistroProdutoLimpeza(produtoSelecionado.Id);
                        break;

                        case "Bebida":
                            modelProdutosBebida.DeletarRegistroProdutoBebida(produtoSelecionado.Id);
                            break;

                        case "Laticinio":
                            modelProdutosLaticinio.DeletarRegistroProdutoLaticinio(produtoSelecionado.Id);
                            break;
                    }

                    produtos.Remove(produtoSelecionado);
                    produtosFiltrados.Remove(produtoSelecionado);
                }
            });

            filtrar = new RelayCommand((object objeto) =>
            {
                


                for (int i = produtosFiltrados.Count-1; i >= 0; i--)
                {
                    produtosFiltrados.Remove(produtosFiltrados[i]);
                }


                if (dadoFiltro == "TODOS" || dadoFiltro == null)
                    for (int i = 0; i < produtos.Count; i++)
                        produtosFiltrados.Add(produtos[i]);
                else
                    for (int i = 0; i < produtos.Count; i++)
                        if (produtos[i].Tipo == dadoFiltro)
                            produtosFiltrados.Add(produtos[i]);
                    //produtosFiltrados = new ObservableCollection<Produto>(produtos.Where(el => el.Tipo == dadoFiltro).ToList());
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
