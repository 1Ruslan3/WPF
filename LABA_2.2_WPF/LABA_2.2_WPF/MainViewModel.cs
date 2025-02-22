using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace LABA_2._2_WPF
{
    class MainViewModel : INotifyPropertyChanged
    {
        private string _code;
        private Compiler _compiler;
        private VirtualMachine _vm;

        public MainViewModel()
        {
            _compiler = new Compiler();
            _vm = new VirtualMachine();
            PlayCommand = new RelayCommand(ExecutePlay);
            OpenFileCommand = new RelayCommand(ExecuteOpenFile);
        }

        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
            }
        }

        public ICommand PlayCommand { get; }
        public ICommand OpenFileCommand { get; }

        private void ExecutePlay(object parameter)
        {
            try
            {
                var instructions = _compiler.Compile(Code);
                _vm.Execute(_compiler.LoadInstructionsIntoMemoryStream(instructions));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExecuteOpenFile(object parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (var stream = openFileDialog.OpenFile())
                    {
                        var instructions = new List<uint>();
                        string[] lines = File.ReadAllLines(openFileDialog.FileName);
                        StringBuilder All_lines = new StringBuilder();
                        foreach (string line in lines)
                        {                            
                            All_lines.AppendLine(line);                            
                        }
                        instructions = _compiler.Compile(All_lines.ToString());
                        _vm.Execute(_compiler.LoadInstructionsIntoMemoryStream(instructions));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
