using GalaSoft.MvvmLight.Command;
using proj1.Data;
using proj1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        private Fornecedor _fornecedorSelecionado;
        public Fornecedor FornecedorSelecionado
        {
            get => _fornecedorSelecionado;
            set
            {                
                    _fornecedorSelecionado = value;
                    OnPropertyChanged(nameof(FornecedorSelecionado));

                    
                
            }
        }

        public ICommand SalvarCommand { get; }

        public ICommand NovoProdutoCommand { get; }
        public ICommand AtualizarCommand { get; }
        public ICommand RemoverProdutoCommand { get; }


        public ProdutoViewModel()
        {
            CarregarFornecedores();
            CarregarDados();

            SalvarCommand = new RelayCommand(SalvarProduto);
            NovoProdutoCommand = new RelayCommand(NovoProduto);
            AtualizarCommand = new RelayCommand(AtualizarProduto);
            RemoverProdutoCommand = new RelayCommand(RemoverProduto);

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

        private void SalvarProduto()
        {
            try
            {

                if (FornecedorSelecionado != null)
                    ProdutoSelecionado.FornecedorID = FornecedorSelecionado.ID;

                if (ProdutoSelecionado.ID == 0)
                {
                    _context.Produtos.Add(ProdutoSelecionado);
                }
                else
                {
                    _context.Entry(ProdutoSelecionado).State = EntityState.Modified;
                                                        
                }
                                
                _context.SaveChanges();          
                CarregarDados();

                ProdutoSelecionado = new Produto();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}");
            }
        }
        private void NovoProduto()
        {
            ProdutoSelecionado = new Produto();
          
        }

        private void AtualizarProduto()
        {
            if ( ProdutoSelecionado?.ID <= 0)
            {
                MessageBox.Show("Selecione um produto válido!");
                return;
            }
            try
            {
                if (FornecedorSelecionado != null)
                   ProdutoSelecionado.FornecedorID = FornecedorSelecionado.ID;

                _context.Entry(ProdutoSelecionado).State = EntityState.Modified;
                _context.SaveChanges();
                CarregarDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar!");
            }
        }

        private void RemoverProduto()
        {
            if (MessageBox.Show("Confirmar exclusão?", "Atenção", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _context.Produtos.Remove(ProdutoSelecionado);
                    _context.SaveChanges();
                    Produtos.Remove(ProdutoSelecionado);

                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Erro ao remover: Produto esta vinculado");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)=>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
    }

}