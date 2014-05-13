using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;





namespace CarmelTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String importFile;
        BrushConverter converter;
        List<Button> buttonList;
        Style style1;

        public MainWindow()
        {
            InitializeComponent();
            buttonList = new List<Button>();
            converter = new BrushConverter();
            style1 = new System.Windows.Style();

            maakStyles();
        }

        public void vulButtonArray()
        {
            buttonList[0] = convertButton;
            buttonList[1] = templatesButton;
            buttonList[2] = vleugelButton;
            buttonList[3] = accountsButton;
            buttonList[4] = settingsButton;
        }

        // DUbbelklik event voor importbestand
        private void importTextbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Maak openfiledialog
            OpenFileDialog open = new OpenFileDialog();

            // Zet opties voor filter
            open.Filter = "CSV bestanden (.csv)|*.csv";
            open.Title = "Openen";
            open.FilterIndex = 1;

            // Laat de openFileDialog zien.
            Nullable<bool> result = open.ShowDialog();

            // Kijk of gebruiker op Ok heeft geklikt.
            if (result == true)
            {
                // String naar het te importeren bestand.
                importFile = open.FileName;

                padTextbox.Document.Blocks.Clear();
                padTextbox.Document.Blocks.Add(new Paragraph(new Run(importFile)));
            }
        }

        // Dubbelklik event voor nieuwe locatie pad
        private void RichTextBox_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {

        }


        private void convertButton_Click(object sender, RoutedEventArgs e)
        {
            // Ga naar tab 0 - convert
            tabs.SelectedIndex = 0;
            selecteerButton(0);

            // code hieronder
        }

        private void templatesButton_Click(object sender, RoutedEventArgs e)
        {
            // Ga naar tab 1 - templates
            tabs.SelectedIndex = 1;
            selecteerButton(1);

            // code hieronder
        }

        private void vleugelButton_Click(object sender, RoutedEventArgs e)
        {
            // Ga naar tab 2 - vleugels
            tabs.SelectedIndex = 2;
            selecteerButton(2);

            // code hieronder
        }

        private void accountsButton_Click(object sender, RoutedEventArgs e)
        {
            // Ga naar tab 3 - accounts
            tabs.SelectedIndex = 3;
            selecteerButton(3);

            // code hieronder
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Ga naar tab 4 - settings
            tabs.SelectedIndex = 4;
            selecteerButton(4);

            // code hieronder
        }

        private void converteerButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void selecteerButton(int nummer)
        {


        }

        private void maakStyles()
        {
            Setter setter1 = new Setter();
            setter1.Property = Button.BorderBrushProperty;
            setter1.Value = (Brush)converter.ConvertFromString("#FF551155");

            Setter setter2 = new Setter();
            setter2.Property = Button.BackgroundProperty;
            setter2.Value = (Brush)converter.ConvertFromString("#FF551155");

            style1.Setters.Add(setter1);
            style1.Setters.Add(setter2);
        }
    }
}
