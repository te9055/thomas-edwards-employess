using System;
using System.Windows;
using static SirmaHomeworkTask.Program;


namespace SirmaHomeworkTask
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FileNameTextBox.Text = "yyyy-MM-dd";
        }


        private void OpenFileViewer(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog { };

            var showDialog = dlg.ShowDialog();

            if (showDialog != true) return;

            var filename = dlg.FileName;
            var result = App(filename, FileNameTextBox.Text);
            if (result == null) return;
            DataGridXaml.Items.Clear();
            DataGridXaml.Items.Add(result);

        }
    }
}