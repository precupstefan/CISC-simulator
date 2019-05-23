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

namespace CISC_simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MPM mpms = new MPM();
        ushort[] memory = new ushort[65536];
        Registers registers = new Registers();
        private String selectedFile = "";
//        private string something = "ANA ARE MERE";
//
//        public string smth
//        {
//            get => something;
//            set
//            {
//                something = value;
//                
//            }
//        }

        public MainWindow()
        {
            InitializeComponent();
            Log("Application started");
            DataContext = registers;
            
        }

        public void Log(string message)
        {
            var time = DateTime.Now.ToLongTimeString();
            Console.Inlines.Add(time + " : " + message + Environment.NewLine);
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() != true) return;
            selectedFile = fileDialog.FileName;
            SelectedFileLabel.Content = fileDialog.SafeFileName;
            Log($"File '{fileDialog.SafeFileName}' has been selected");
        }

        private void AssembleButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFile != null)
            {
                Log("Initializing Assembly process");
                Assembler assembler = new Assembler(memory);
                assembler.ReadFromFile(selectedFile);
                Log("Successfully read contents of file");
                Log("Parsing...");
                assembler.Assemble();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}