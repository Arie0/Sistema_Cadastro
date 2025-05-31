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

        public ICommand AdicionarFornecedorCommand { get; }
        public ICommand AtualizarFornecedorCommand { get; }
        public ICommand RemoverFornecedorCommand { get; }
        public ICommand LimparCamposCommand { get; }

        public FornecedorViewModel()
        {
            CarregarFornecedores();

            AdicionarFornecedorCommand = new RelayCommand(AdicionarFornecedor, PodeSalvar);
            AtualizarFornecedorCommand = new RelayCommand(AtualizarFornecedor, () => FornecedorSelecionado != null);
            RemoverFornecedorCommand = new RelayCommand(RemoverFornecedor, () => FornecedorSelecionado != null);
            LimparCamposCommand = new RelayCommand(LimparCampos);

            FornecedorSelecionado = new Fornecedor(); 
        }

        private void CarregarFornecedores()
        {
            Fornecedores = new ObservableCollection<Fornecedor>(_context.Fornecedores.ToList());
        }

      
        private void AdicionarFornecedor()
        {
            FornecedorSelecionado = new Fornecedor();
        }

        private bool PodeSalvar(object obj) => FornecedorSelecionado != null;

        private void Salvar(object obj)
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
            try
            {
                _context.Fornecedores.Remove(FornecedorSelecionado);
                _context.SaveChanges();
                Fornecedores.Remove(FornecedorSelecionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao remover: {ex.Message}");
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
