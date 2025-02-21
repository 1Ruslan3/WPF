using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
                //ShowResults("Execution completed successfully.\n\n" + GetRegisterValues());
            }
            catch (Exception ex)
            {
                //ShowResults($"Error: {ex.Message}");
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
                        using (var reader = new BinaryReader(stream))
                        {
                            while (reader.BaseStream.Position != reader.BaseStream.Length)
                            {
                                instructions.Add(reader.ReadUInt32());
                            }
                        }
                        _vm.Execute(_compiler.LoadInstructionsIntoMemoryStream(instructions));
                        //ShowResults("Execution completed successfully.\n\n" + GetRegisterValues());
                    }
                }
                catch (Exception ex)
                {
                    //ShowResults($"Error: {ex.Message}");
                }
            }
        }

        private string GetRegisterValues()
        {
            var result = new StringBuilder();
            result.AppendLine("Register Values:");
            for (int i = 0; i < _vm.Registers.Length; i++)
            {
                if (_vm.Registers[i] != 0)
                {
                    result.AppendLine($"R{i}: {_vm.Registers[i]}");
                }
            }
            return result.ToString();
        }

        //private void ShowResults(string result)
        //{
        //    var outputWindow = new Output(result);
        //    outputWindow.Show();
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
