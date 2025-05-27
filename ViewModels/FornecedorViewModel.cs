using GalaSoft.MvvmLight.Command;
using proj1.Data;
using proj1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace proj1.ViewModels
{
    public class FornecedorViewModel : INotifyPropertyChanged
    {
        private CadastroContext _context = new CadastroContext();

        public ObservableCollection<Fornecedor> Fornecedores { get; set; }
        
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
                    CommandManager.InvalidateRequerySuggested(); 
                }
            }
        }

        public ICommand AdicionarFornecedorCommand { get; }
        public ICommand AtualizarFornecedorCommand { get; }
        public ICommand RemoverFornecedorCommand { get; }


        public FornecedorViewModel()
        {
            Fornecedores = new ObservableCollection<Fornecedor>(_context.Fornecedor.ToList());

            AdicionarFornecedorCommand = new RelayCommand(AdicionarFornecedor);
            AtualizarFornecedorCommand = new RelayCommand(AtualizarFornecedor, () => FornecedorSelecionado != null);
            RemoverFornecedorCommand = new RelayCommand(RemoverFornecedor, () => FornecedorSelecionado != null);
        }

        private void AdicionarFornecedor()
        {
            try
            {
                var novoFornecedor = new Fornecedor { Nome = "Novo Fornecedor", CNPJ = "12345678901234" };
                _context.Fornecedor.Add(novoFornecedor);
                _context.SaveChanges();
                Fornecedores.Add(novoFornecedor);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar: {ex.Message}");
            }
        }
        private void AtualizarFornecedor()
        {
            _context.Entry(FornecedorSelecionado).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private void RemoverFornecedor()
        {
            _context.Fornecedor.Remove(FornecedorSelecionado);
            _context.SaveChanges();
            Fornecedores.Remove(FornecedorSelecionado);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
