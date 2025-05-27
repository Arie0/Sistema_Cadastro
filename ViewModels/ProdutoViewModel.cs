using GalaSoft.MvvmLight.Command;
using proj1.Data;
using proj1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace proj1.ViewModels
{
    public class ProdutoViewModel : INotifyPropertyChanged
    {
        private CadastroContext _context = new CadastroContext();

        public ObservableCollection<Produto> Produtos { get; set; }
        public Produto ProdutoSelecionado { get; set; }

        public ICommand AdicionarProdutoCommand { get; }
        public ICommand AtualizarProdutoCommand { get; }
        public ICommand RemoverProdutoCommand { get; }

        public ProdutoViewModel()
        {
            Produtos = new ObservableCollection<Produto>(_context.Produtos.Include(p => p.FornecedorID).ToList());

            AdicionarProdutoCommand = new RelayCommand(AdicionarProduto);
            AtualizarProdutoCommand = new RelayCommand(AtualizarProduto, () => ProdutoSelecionado != null);
            RemoverProdutoCommand = new RelayCommand(RemoverProduto, () => ProdutoSelecionado != null);
        }

        private void AdicionarProduto()
        {
            var novoProduto = new Produto { Nome = "Novo Produto", Preco = 10, Quantidade = 5, FornecedorID = 1 };
            _context.Produtos.Add(novoProduto);
            _context.SaveChanges();
            Produtos.Add(novoProduto);
        }

        private void AtualizarProduto()
        {
            _context.Entry(ProdutoSelecionado).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private void RemoverProduto()
        {
            _context.Produtos.Remove(ProdutoSelecionado);
            _context.SaveChanges();
            Produtos.Remove(ProdutoSelecionado);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
