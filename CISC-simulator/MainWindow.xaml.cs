using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Assembly;
using Microcode;
using Microcode.classes;
using Logger;

namespace CISC_simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MPM mpms = new MPM();
        Registers registers = new Registers();

        private String selectedFile = "";

        private Logger.Logger Logger = global::Logger.Logger.Instance;

        public MainWindow()
        {
            InitializeComponent();
            Logger.SetConsole(Console);
            Logger.Info("Application started");
            DataContext = registers;
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            registers.SP = 44;
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() != true) return;
            selectedFile = fileDialog.FileName;
            SelectedFileLabel.Content = fileDialog.SafeFileName;
            Logger.Info($"File '{fileDialog.SafeFileName}' has been selected");
        }

        private void AssembleButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFile != "")
            {
                Assembler assembler = new Assembler();
                assembler.ReadFromFile(selectedFile);
                assembler.Assemble();
            }
            else
            {
                Logger.Warning("Assembly aborted, there was no file selected");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void StepButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}