using GalaSoft.MvvmLight.Command;
using proj1.Data;
using proj1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
        private readonly CadastroContext _context = new CadastroContext();

        private ObservableCollection<Fornecedor> _fornecedores;
        public ObservableCollection<Fornecedor> Fornecedores
        {
            get => _fornecedores;
            private set
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
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand NovoFornecedorCommand { get; }
        public ICommand AtualizarFornecedorCommand { get; }
        public ICommand RemoverFornecedorCommand { get; }


        public FornecedorViewModel()
        {
            CarregarFornecedores();

            SalvarCommand = new RelayCommand(SalvarFornecedor);
            NovoFornecedorCommand = new RelayCommand(NovoFornecedor);
            AtualizarFornecedorCommand = new RelayCommand(AtualizarFornecedor, () => FornecedorSelecionado != null);
            RemoverFornecedorCommand = new RelayCommand(RemoverFornecedor, () => FornecedorSelecionado != null);

            FornecedorSelecionado = new Fornecedor(); 
        }

        

        private void SalvarFornecedor()
        {
            try
            {
                if (FornecedorSelecionado.ID == 0)
                {
                    _context.Fornecedores.Add(FornecedorSelecionado);
                    
                }
                else
                {
                    _context.Entry(FornecedorSelecionado).State = EntityState.Modified;
                }

                _context.SaveChanges();
                CarregarFornecedores();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        private void CarregarFornecedores()
        {
            Fornecedores = new ObservableCollection<Fornecedor>(_context.Fornecedores.ToList());
        }

      
        private void NovoFornecedor()
        {
            FornecedorSelecionado = new Fornecedor();

        }


      
        private void AtualizarFornecedor()
        {
            try
            {
                _context.Entry(FornecedorSelecionado).State = EntityState.Modified;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar: {ex.Message}");
            }
        }

        private void RemoverFornecedor()
        {
            if (MessageBox.Show("Confirmar exclusão?", "Atenção", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _context.Fornecedores.Remove(FornecedorSelecionado);
                    _context.SaveChanges();
                    Fornecedores.Remove(FornecedorSelecionado);
                    
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Erro ao remover: Fornecedor possui relacionamentos");
                }
            }
        }
            
       
        private void LimparCampos()
        {
            FornecedorSelecionado = new Fornecedor();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
