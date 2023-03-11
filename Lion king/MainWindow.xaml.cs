using Lion_king.DAL;
using Lion_king.Models;
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

namespace Lion_king
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        DbRepository db = new();

        #region Hämta information
        //hämta djur (m. art och klass)
        private async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var animals = await db.GetAnimals();
            listbox.ItemsSource = animals;
        }

        //hämta klass
        private async void btnClass_Click(object sender, RoutedEventArgs e)
        {
            var classes = await db.GetClass();
            listbox.ItemsSource = classes;
        }

        //hämta art (m. klass)
        private async void btnSpecies_Click(object sender, RoutedEventArgs e)
        {
            var species = await db.GetSpecies();
            listbox.ItemsSource = species;
        }
        #endregion


        #region Lägg till i databas
        //lägg till klass
        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string thisClass = txtboxClass.Text;

            var newClass = new Class()
            {
                Class_name = thisClass
            };

            try
            {
                await db.AddClass(newClass);

                MessageBox.Show($"Du har nu lagt till en ny klass.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //lägg till art
        private async void btnAddSpecies_Click(object sender, RoutedEventArgs e)
        {
            string thisSpecie = txtBox1.Text;

            var newSpecie = new Species()
            {
                Common_name = thisSpecie,
            };

            try
            {
                await db.AddSpecies(newSpecie);

                MessageBox.Show($"Du har nu lagt till en ny art.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //lägg till djur
        private async void btnAddAnimal_Click(object sender, RoutedEventArgs e)
        {
            //string name = txtBox1.Text;
            //string species = txtBox2.Text;

            //var animal = new Animal()
            //{
            //    Name = name,
            //    Species = "Masai Lion"
            //};

            //try
            //{
            //    await db.AddAnimal(animal);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        #endregion



        private async void btnnewSpecie_Checked(object sender, RoutedEventArgs e)
        {
            var classes = await db.GetClass();
            lstboxClass.ItemsSource = classes;
        }



        private void listbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listbox.SelectedItem is Animal selected)
            {
                MessageBox.Show(selected.Animal_id.ToString());
            }
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //var search = txtBox1.Text;

            //var searchanimals = await db.GetAnimalByName();

            //listbox.ItemsSource = searchanimals;
        }
    }
}
