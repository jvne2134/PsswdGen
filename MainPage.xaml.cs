using System.Security.Cryptography;
using System.Text;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace PsswdGn
{
    public partial class MainPage : ContentPage
    {

        private const string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        private const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Digits = "0123456789";
        private const string Symbols = "!@#$%^&*()-_=+";

        public bool alfanumericos;
        public bool numericos;
        public bool simbolos;

        readonly double sliderIncrement = 1;

        public double sliderCorrectedValue;

        Slider slider = new()
        {
            Maximum = 32, // It is important to set the maximum value first.
            Minimum = 0
        };

        public MainPage()
        {
            InitializeComponent();

            alfanumericos = false;
            numericos = false;
            simbolos = false;

            Slider_Size.Text = "";

        }

        private void GenPassword(object sender, EventArgs e)
        {
            var validChars = new StringBuilder();
            if (alfanumericos)
            {
                validChars.Append(LowercaseLetters);
            }
            if (alfanumericos)
            {
                validChars.Append(UppercaseLetters);
            }
            if (numericos)
            {
                validChars.Append(Digits);
            }
            if (simbolos)
            {
                validChars.Append(Symbols);
            }

            if (validChars.Length == 0)
            {
                DisplayAlert("Alert", "Tamaño de contraseña igual a cero", "OK");
                return;
            }

            if (alfanumericos == false && numericos == false && simbolos == false)
            {
                DisplayAlert("Alert", "No ha elegido ningun parametro", "OK");
                return;
            }

            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[Convert.ToInt16(sliderCorrectedValue)];
                rng.GetBytes(bytes);
                var password = new char[Convert.ToInt16(sliderCorrectedValue)];

                for (int i = 0; i < Convert.ToInt16(sliderCorrectedValue); i++)
                {
                    password[i] = validChars[bytes[i] % validChars.Length];
                }

                PsswdField.Text = new string(password);
            }
        }

        private void ToggleTextBox(object sender, EventArgs e)
        {
            PsswdField.IsPassword = !PsswdField.IsPassword;
        }

        private void Alfanumericos(object sender, CheckedChangedEventArgs e)
        {
            alfanumericos = !alfanumericos;
        }

        private void Numericos(object sender, CheckedChangedEventArgs e)
        {
            numericos = !numericos;
        }

        private void Simbolos(object sender, CheckedChangedEventArgs e)
        {
            simbolos = !simbolos;
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

            // Recognize the sender as a Slider object.
            Slider slider = (Slider)sender;

            sliderCorrectedValue = (int)Math.Round(slider.Value);

            Slider_Size.Text = sliderCorrectedValue.ToString();

        }

        private void CopyToClipboard(object sender, EventArgs e)
        {
            Clipboard.Default.SetTextAsync(PsswdField.Text);

            ClipboardBtn.BackgroundColor = Color.FromArgb("cc99c5");

        }

    }


}