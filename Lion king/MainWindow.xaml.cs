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
using static System.Net.Mime.MediaTypeNames;

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
        private async void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            if (radioNewClass.IsChecked == false && btnnewSpecie.IsChecked == false && btnnewAnimal.IsChecked == false) 
            {
                MessageBox.Show("Du måste välja typ av kategori för att lägga till ett nytt objekt.");
            }

            if (radioNewClass.IsChecked == true)
            {
                string thisClass = txtBox2.Text;

                var newClass = new Class()
                {
                    Class_name = thisClass
                };

                try
                {
                    await db.AddClass(newClass);

                    MessageBox.Show($"Du har nu lagt till {newClass} som en ny klass.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (btnnewSpecie.IsChecked == true)
            {
                string specieName = txtBox1.Text;
                string latinName = txtBox2.Text;

                var chosenClass = (Class)cbo.SelectedItem;

                var newSpecie = new Species()
                {
                    Common_name = specieName,
                    Latin_name = latinName,
                    Class = chosenClass
                };

                try
                {
                    await db.AddSpecies(newSpecie);

                    MessageBox.Show($"Du har nu lagt till {newSpecie} som en ny art.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (btnnewAnimal.IsChecked == true)
            {
                string name = txtBox1.Text;

                var chosenSpecie = (Species)cbo.SelectedItem;

                var animal = new Animal()
                {
                    Name = name,
                    Species = chosenSpecie
                };

                try
                {
                    await db.AddAnimal(animal);
                    MessageBox.Show($"Du har nu lagt till {animal} som ett nytt djur.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //lägg till art
        private async void btnAddSpecies_Click(object sender, RoutedEventArgs e)
        {
            var thisSpecie = txtBox1.Text;
            var latin = txtBox2.Text;

            var classs = (Class)cbo.SelectedItem;
            
                var newSpecie = new Species()
                {
                    Common_name = thisSpecie,
                    Latin_name = latin,
                    Class = classs
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

        #endregion



        private async void btnnewSpecie_Checked(object sender, RoutedEventArgs e)
        {
            txtBox1.Clear();
            txtBox2.Clear();

            var classes = await db.GetClass();
            cbo.ItemsSource = classes;

            lblAdd1.Content = "Namn på art *";
            lblAdd1.Visibility = Visibility.Visible;
            lblAdd2.Content = "Latinskt namn";
            lblAdd2.Visibility = Visibility.Visible;
            lblAdd3.Content = "Välj djurklass *";
            lblAdd3.Visibility = Visibility.Visible;

            txtBox1.Visibility = Visibility.Visible;
            txtBox2.Visibility= Visibility.Visible;
            cbo.Visibility = Visibility.Visible;
        }

        private async void btnnewAnimal_Checked(object sender, RoutedEventArgs e)
        {
            txtBox1.Clear();
            txtBox2.Clear();

            var species = await db.GetSpecies();
            cbo.ItemsSource = species;

            lblAdd1.Content = "Namn på djur";
            lblAdd1.Visibility= Visibility.Visible;
            lblAdd3.Content = "Välj djurart *";
            lblAdd3.Visibility= Visibility.Visible;

            cbo.Visibility= Visibility.Visible;
            txtBox1.Visibility = Visibility.Visible;

            txtBox2.Visibility = Visibility.Hidden;
            lblAdd2.Visibility = Visibility.Hidden;
        }

        private void radioNewClass_Checked(object sender, RoutedEventArgs e)
        {
            txtBox1.Clear();
            txtBox2.Clear();

            lblAdd2.Content = "Namn på klass *";
            lblAdd2.Visibility= Visibility.Visible;
            txtBox2.Visibility= Visibility.Visible;

            txtBox1.Visibility = Visibility.Hidden;
            cbo.Visibility = Visibility.Hidden;
            lblAdd1.Visibility = Visibility.Hidden;
            lblAdd3.Visibility = Visibility.Hidden;
        }



        private void listbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listbox.SelectedItem is Animal selected)
            {
                MessageBox.Show(selected.Animal_id.ToString());
            }
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Class classs = (Class)listbox.SelectedItem;
            //Species species = (Species)listbox.SelectedItem;

            if (listbox.SelectedItem == classs)
            {
                try
                {
                    await db.DeleteClass(classs);

                    MessageBox.Show($"Du har nu tagit bort {classs.Class_name} som klass.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //if (listbox.SelectedItem == species)
            //{
            //    try
            //    {
            //        await db.DeleteSpecies(species);

            //        MessageBox.Show($"Du har nu tagit bort {species.Common_name} som klass.");
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}

        }

    }
}
