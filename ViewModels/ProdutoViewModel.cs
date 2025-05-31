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
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace proj1.ViewModels
{
    public class ProdutoViewModel : INotifyPropertyChanged
    {
        private CadastroContext _context = new CadastroContext();

        private ObservableCollection<Fornecedor> _fornecedores;
        public ObservableCollection<Fornecedor> Fornecedores
        {
            get => _fornecedores;
            set
            {
                _fornecedores = value;
                OnPropertyChanged(nameof(Fornecedores));
            }
        }

        private ObservableCollection<Produto> _produtos;
        public ObservableCollection<Produto> Produtos
        {
            get => _produtos;
            set
            {
                _produtos = value;
                OnPropertyChanged(nameof(Produtos));
            }
        }

        private Produto _produtoSelecionado;
        public Produto ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set
            {
                _produtoSelecionado = value;
                OnPropertyChanged(nameof(ProdutoSelecionado));
           
              
            }
        }

        private Fornecedor _fornecedorSelecionado;
        public Fornecedor FornecedorSelecionado
        {
            get => _fornecedorSelecionado;
            set
            {
                if (_fornecedorSelecionado != value)
                {
                    _fornecedorSelecionado = value;
                    OnPropertyChanged(nameof(FornecedorSelecionado));
                }
            }
        }



        public ICommand AdicionarProdutoCommand { get; }
        public ICommand AtualizarProdutoCommand { get; }
        public ICommand RemoverProdutoCommand { get; }

        public ICommand LimparProdutoCommand { get; }

        public ProdutoViewModel()
        {
            CarregarFornecedores();
            CarregarDados();

            AdicionarProdutoCommand = new RelayCommand(AdicionarProduto);
            AtualizarProdutoCommand = new RelayCommand(AtualizarProduto, () => ProdutoSelecionado != null);
            RemoverProdutoCommand = new RelayCommand(RemoverProduto, () => ProdutoSelecionado != null);
            LimparProdutoCommand = new RelayCommand(LimparCampos);
        }

        private void CarregarFornecedores()
        {
            Fornecedores = new ObservableCollection<Fornecedor>(_context.Fornecedores.ToList());
        }

        private void CarregarDados()
        {
      
            Produtos = new ObservableCollection<Produto>(
                _context.Produtos.Include(p => p.Fornecedor).ToList());

        
        }

        private void AdicionarProduto()
        {
            if (ProdutoSelecionado == null)
            {
                ProdutoSelecionado = new Produto();
            }

            try
            {
                _context.Produtos.Add(ProdutoSelecionado);
                _context.SaveChanges();
                Produtos.Add(ProdutoSelecionado);

                
                ProdutoSelecionado = new Produto();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar: {ex.Message}");
            }
        }
        

        private void AtualizarProduto()
        {
            if (ProdutoSelecionado == null) return;

            try
            {
                if (FornecedorSelecionado != null)
                {
                    ProdutoSelecionado.FornecedorID = FornecedorSelecionado.ID;
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private void RemoverProduto()
        {
            if (ProdutoSelecionado != null)
            {
                _context.Produtos.Remove(ProdutoSelecionado);
                _context.SaveChanges();
                Produtos.Remove(ProdutoSelecionado);
            }
        }

        private void LimparCampos()
        {
            ProdutoSelecionado = new Produto();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}