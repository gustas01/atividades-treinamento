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

        public ObservableCollection<ProdutoLimpeza> produtosLimpeza { get; set; }
        public ICommand adiciona { get; set; }
        public ICommand remove { get; set; }
        public ICommand update { get; set; }
        public ProdutoLimpeza produtoSelecionado { get; set; }

        public MainWindowVM()
        {
            produtosLimpeza = new ObservableCollection<ProdutoLimpeza>();


            adiciona = new RelayCommand((object objeto) =>
            {
                CreateWindow window = new CreateWindow();

                ProdutoLimpeza produto = new ProdutoLimpeza();
                window.DataContext = produto;
                window.ShowDialog();


                produtosLimpeza.Add(produto);               
            });

            update = new RelayCommand((object objeto) =>
            {
                CreateWindow window = new CreateWindow();

                window.DataContext = produtoSelecionado;
                window.ShowDialog();
                ProdutoLimpeza produtoAux = produtoSelecionado;
                produtosLimpeza.Remove(produtoSelecionado);

                produtosLimpeza.Add(produtoAux);
            });

            remove = new RelayCommand((object objeto) =>
            {
                produtosLimpeza.Remove(produtoSelecionado);
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
