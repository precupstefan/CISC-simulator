using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using Architecture;
using Architecture.classes;
using Logger;
using Architecture.classes.Bus;

namespace CISC_simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MPM mpms = new MPM();

        private String selectedFile =
            @"C:\Users\psa97\Documents\Facultate\AN3\PROIECT_MIHU\CISC-simulator\whatever.txt.txt";

        private bool assembled = false;

        private Logger.Logger Logger = global::Logger.Logger.Instance;

        public MainWindow()
        {
            InitializeComponent();
            Logger.SetConsole(Console);
            Logger.Info("Application started");
            DataContext = global::Architecture.Registers.Instance;
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
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
                var assembler = new Assembler();
                assembler.ReadFromFile(selectedFile);
                assembler.Assemble();
                assembled = true;
                State.Instance.Halt = false;
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
            if (!IsProgramAssembled()) return;

            Sequencer.Sequencer.Instance.ExecuteCycle();
        }

        private bool IsProgramAssembled()
        {
            if (assembled) return true;
            Logger.Error("NO ASSEMBLY CODE FOUND. PLEASE ASSEMBLE SOME");
            return false;
        }


        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void FullExecutionButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!IsProgramAssembled()) return;

            Sequencer.Sequencer.Instance.ExecuteFullCycle();
        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (!assembled)
            {
                Logger.Error("CAN NOT SAVE UNCOMPILED ASSEMBLY CODE");
                return;
            }

            string path = @"C:\Users\psa97\Desktop\assembly_code.bin";
            try
            {
                using (BinaryWriter binWriter =
                    new BinaryWriter(File.Open(path, FileMode.Create)))
                {
                    var memory = Memory.Instance;
                    int indx = 0;
                    while (memory[indx] != 0)
                    {
                        binWriter.Write(memory[indx]);
                        indx++;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return;
            }

            Logger.Info($"Successfully written code to file to {path}");
        }
    }
}