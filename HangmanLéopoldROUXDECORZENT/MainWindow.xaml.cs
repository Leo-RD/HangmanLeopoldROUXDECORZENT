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

namespace HangmanLéopoldROUXDECORZENT
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string GuessLetter;
        private int vies = 7;
        private string randomWord;

        public MainWindow()
        {
            InitializeComponent();
            setupGame();
        }

        public TextBox HiddenWordTextBox { get; private set; }

        private void setupGame()
        {
            string filePath = "../../Ressources/mots5lettres.txt"; // Chemin du fichier .txt
            List<string> list = new List<string>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    list.Add(line.Trim().ToLower());
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Le fichier 'mots.txt' n'a pas été trouvé.");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
                return;
            }

            Random rnd = new Random();
            int randomIndex = rnd.Next(0, list.Count);
            randomWord = list[randomIndex].ToUpper();
            int wordLength = randomWord.Length;
            string hiddenWord = new string('*', wordLength);
            DisplayHiddenWord(hiddenWord);
            UpdateLifeCounter(); // Call the UpdateLifeCounter method
        }

        public void runGame()
        {
            if (GuessLetter != null)
            {
                if (randomWord.Contains(GuessLetter.ToUpper()))
                {
                    for (int i = 0; i < randomWord.Length; i++)
                    {
                        if (randomWord[i].ToString().ToUpper() == GuessLetter.ToUpper())
                        {
                            if (TB_Display != null)
                            {
                                TB_Display.Text = TB_Display.Text.Remove(i, 1).Insert(i, GuessLetter.ToUpper());
                            }
                        }
                    }
                }
                else
                {
                    vies--;
                    UpdateLifeCounter(); // Call the UpdateLifeCounter method
                }

                if (vies == 0)
                {
                    MessageBox.Show("Défaite !");
                }

                if (TB_Display != null && !TB_Display.Text.Contains("*"))
                {
                    MessageBox.Show("Victoire !");
                }
            }
            DisplayHiddenWord(TB_Display.Text);
        }

        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string btnContent = btn.Content.ToString();
            btn.IsEnabled = false;
            GuessLetter = btn.Content.ToString();
            runGame();
        }

        private void DisplayHiddenWord(string hiddenWord)
        {
            TB_Display.Text = hiddenWord;
        }

        private void UpdateLifeCounter()
        {
            // Mettre à jour l'image en fonction des vies restantes
            string imagePath = $"../../Ressources/images/{8 - vies}.png"; // 1.png pour 7 vies, 2.png pour 6 vies, etc.
            lifeImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
        }


    }
}
