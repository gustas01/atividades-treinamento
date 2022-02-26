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
        List<Produto> produtosListaFiltrados;


        public ICommand adiciona { get; set; }
        public ICommand remove { get; set; }
        public ICommand update { get; set; }
        public ICommand filtrar { get; set; }


        public Produto produtoSelecionado { get; set; }

        public string dadoFiltro { get; set; }                                                                                                       




        public MainWindowVM()
        {
            produtos = new ObservableCollection<Produto>();
            produtosFiltrados = new ObservableCollection<Produto>();

            adiciona = new RelayCommand((object objeto) =>
            {
                CreateWindow window = new CreateWindow();

                Produto produto = new Produto();
                window.DataContext = produto;
                window.ShowDialog();


                produtos.Add(produto);
                produtosFiltrados.Add(produto);
            });

            update = new RelayCommand((object objeto) =>
            {
                CreateWindow window = new CreateWindow();

                window.DataContext = produtoSelecionado;
                window.ShowDialog();
                Produto produtoAux = produtoSelecionado;
                produtos.Remove(produtoSelecionado);
                produtosFiltrados.Remove(produtoSelecionado);

                produtos.Add(produtoAux);
                produtosFiltrados.Add(produtoAux);

            });

            remove = new RelayCommand((object objeto) =>
            {
                produtos.Remove(produtoSelecionado);
                produtosFiltrados.Remove(produtoSelecionado);
            });

            filtrar = new RelayCommand((object objeto) =>
            {
                produtosListaFiltrados = new List<Produto>();


                for (int i = 0; i < produtos.Count; i++)
                {
                    if (!produtosListaFiltrados.Contains(produtos[i]))
                        produtosListaFiltrados.Add(produtos[i]);

                    if (!produtosFiltrados.Contains(produtos[i]))
                        produtosFiltrados.Add(produtos[i]);
                    
                }
               
                switch (dadoFiltro)
                {
                    case "Limpeza":
                        produtosListaFiltrados = produtosListaFiltrados.Where(prod => prod.Tipo == "Limpeza").ToList();

                            for(int j = produtosFiltrados.Count-1; j >=0; j--)
                                if (!produtosListaFiltrados.Contains(produtosFiltrados[j]))
                                    produtosFiltrados.Remove(produtosFiltrados[j]);
                        break;

                    case "Bebidas":
                        produtosListaFiltrados = produtosListaFiltrados.Where(prod => prod.Tipo == "Bebidas").ToList();

                        for (int j = produtosFiltrados.Count - 1; j >= 0; j--)
                            if (!produtosListaFiltrados.Contains(produtosFiltrados[j]))
                                produtosFiltrados.Remove(produtosFiltrados[j]);
                        break;

                    case "Laticinios":
                        produtosListaFiltrados = produtosListaFiltrados.Where(prod => prod.Tipo == "Laticinios").ToList();

                        for (int j = produtosFiltrados.Count - 1; j >= 0; j--)
                            if (!produtosListaFiltrados.Contains(produtosFiltrados[j]))
                                produtosFiltrados.Remove(produtosFiltrados[j]);
                        break;

                    default:
                        for (int i = 0; i < produtos.Count; i++)
                         if (!produtosFiltrados.Contains(produtos[i]))
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
