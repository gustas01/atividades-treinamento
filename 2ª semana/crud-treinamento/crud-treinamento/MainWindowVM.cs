using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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




        //SELEÇÃO DO BANCO DE DADOS
        IConexaoBD connectBD = new ConexaoMRDB();



        public MainWindowVM()
        {
            try
            {

                List<IDALProdutos> dALProdutos = new List<IDALProdutos>();
                modelProdutos = new DAL(connectBD);
                modelProdutosLimpeza = new DALLIMPEZA(connectBD);
                modelProdutosBebida = new DALBEBIDA(connectBD);
                modelProdutosLaticinio = new DALLATICINIO(connectBD);



                produtos = modelProdutos.GetTodosRegistrosProduto(new List<IDALProdutos>()
                {
                    modelProdutosLimpeza,
                    modelProdutosBebida,
                    modelProdutosLaticinio
                } );

                produtosFiltrados = new ObservableCollection<Produto>(produtos);
                int prodID;
                Dictionary<string, string> props = new Dictionary<string, string>();

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

                        
                            props.Clear();

                            //props.Add("id", produtoLimpeza.Id.ToString());
                            props.Add("nome", produtoLimpeza.Nome);
                            props.Add("preco", produtoLimpeza.Preco.ToString());
                            props.Add("marca", produtoLimpeza.Marca);
                            props.Add("tipo", produtoLimpeza.Tipo);
                            props.Add("cheiro", produtoLimpeza.Cheiro);

                            prodID = modelProdutosLimpeza.InserirRegistro(props);

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

                       
                            props.Clear();
                            //props.Add("id", produtoBebida.Id.ToString());
                            props.Add("nome", produtoBebida.Nome);
                            props.Add("preco", produtoBebida.Preco.ToString());
                            props.Add("marca", produtoBebida.Marca);
                            props.Add("tipo", produtoBebida.Tipo);
                            props.Add("alcoolica", produtoBebida.Alcoolica.ToString());

                            prodID = modelProdutosBebida.InserirRegistro(props);

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

                       
                            props.Clear();
                            //props.Add("id", produtoLaticinio.Id.ToString());
                            props.Add("nome", produtoLaticinio.Nome);
                            props.Add("preco", produtoLaticinio.Preco.ToString());
                            props.Add("marca", produtoLaticinio.Marca);
                            props.Add("tipo", produtoLaticinio.Tipo);
                            props.Add("desnatado", produtoLaticinio.Desnatado.ToString());

                            prodID = modelProdutosLaticinio.InserirRegistro(props);

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

                            props.Clear();
                            props.Add("id", prodLimp.Id.ToString());
                            props.Add("nome", prodLimp.Nome);
                            props.Add("preco", prodLimp.Preco.ToString());
                            props.Add("marca", prodLimp.Marca);
                            props.Add("cheiro", prodLimp.Cheiro);

                            modelProdutosLimpeza.AtualizarRegistro(props);

                            break;

                        case "Bebida":
                            CreateWindowProdutoBebida windowBebida = new CreateWindowProdutoBebida();

                            ProdutoBebida prodBeb = (ProdutoBebida) produtoSelecionado;
                            windowBebida.DataContext = prodBeb;
                            windowBebida.ShowDialog();

                            props.Clear();
                            props.Add("id", prodBeb.Id.ToString());
                            props.Add("nome", prodBeb.Nome);
                            props.Add("preco", prodBeb.Preco.ToString());
                            props.Add("marca", prodBeb.Marca);
                            props.Add("alcoolica", prodBeb.Alcoolica.ToString());

                            modelProdutosBebida.AtualizarRegistro(props);

                            break;

                        case "Laticinio":
                            CreateWindowProdutoLaticinio windowLaticinio = new CreateWindowProdutoLaticinio();

                            ProdutoLaticinio prodLat = (ProdutoLaticinio) produtoSelecionado;
                            windowLaticinio.DataContext = prodLat;
                            windowLaticinio.ShowDialog();

                            props.Clear();
                            props.Add("id", prodLat.Id.ToString());
                            props.Add("nome", prodLat.Nome);
                            props.Add("preco", prodLat.Preco.ToString());
                            props.Add("marca", prodLat.Marca);
                            props.Add("desnatado", prodLat.Desnatado.ToString());

                            modelProdutosLaticinio.AtualizarRegistro(props);
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
                            modelProdutosLimpeza.DeletarRegistro(produtoSelecionado.Id);
                            break;

                            case "Bebida":
                                modelProdutosBebida.DeletarRegistro(produtoSelecionado.Id);
                                break;

                            case "Laticinio":
                                modelProdutosLaticinio.DeletarRegistro(produtoSelecionado.Id);
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

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
