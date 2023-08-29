using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
//Cambiar el logo, el splashscreen y revisar los TODO
namespace Notas_Ua
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            nota1.TextChanged += OnEntryTextChanged;
            nota2.TextChanged += OnEntryTextChanged;
            nota3.TextChanged += OnEntryTextChanged;
            MainPage.ShowToastAsync("Aplicacion en desarrollo",10);
        }
        private static async void ShowToastAsync(string text, int FontSize)//Utilizar para notificaciones de alerta leve
        {
            var toast = Toast.Make(text, CommunityToolkit.Maui.Core.ToastDuration.Short, FontSize);
            await toast.Show();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nota1.Text) || string.IsNullOrEmpty(nota2.Text) || string.IsNullOrEmpty(nota3.Text))
            {
                ShowToastAsync("Rellene todos los campos de notas", 10);
            }
            else
            {
                int notap1 = Convert.ToInt32(nota1.Text);
                int notap2 = Convert.ToInt32(nota2.Text);
                int notap3 = Convert.ToInt32(nota3.Text);

                if ((notap1 >= 91 && notap1 <= 100) &&
    (notap2 >= 91 && notap2 <= 100) &&
    (notap3 >= 91 && notap3 <= 100))
                {
                    await DisplayAlert("Materia Exonerada 👍🏻", "Cuando alcanzas 91 o mas en tus tres parciales exoneras la materia y no rendis la final 😉","OK");
                    return;
                }

                int[] notasParciales = { notap1, notap2, notap3 };

                Array.Sort(notasParciales);

                if ((notasParciales[0] >= 91 && notasParciales[0] <= 100) &&
    (notasParciales[1] >= 91 && notasParciales[1] <= 100) &&
    (notasParciales[2] >= 91 && notasParciales[2] <= 100) &&
    (notasParciales[2] - notasParciales[0] == 2))
                {
                    await DisplayAlert("Materia Exonerada 👍🏻", "Cuando alcanzas 91 o mas en tus tres parciales exoneras la materia y no rendis la final 😉", "OK");
                    return;
                }

                int sumaMejoresNotas = notasParciales[1] + notasParciales[2];
                int promedioActual = sumaMejoresNotas / 2;

                int[] notasObjetivo = { 60, 71, 81, 91 };
                string[] nombresNotas = { "2", "3", "4", "5" };

                string resultado = "Promedio actual en los 2 mejores parciales: " + promedioActual.ToString("D0") + "\n\n";
                //TODO: transformar las notas, promedio y notas objetivo en un objeto
                for (int i = 0; i < notasObjetivo.Length; i++)
                {
                    int notaNecesaria = (int)Math.Ceiling((notasObjetivo[i] - (promedioActual * 0.4)) / 0.6);

                    resultado += "Para nota " + nombresNotas[i] + ", necesita ";

                    if (notaNecesaria <= 100)
                    {
                        resultado += notaNecesaria.ToString("D0") + " en el examen final.\n";
                    }
                    else
                    {
                        resultado += "--- en el examen final.\n";
                    }
                }
                await DisplayAlert("Resultados", resultado, "OK");//TODO: Cambiar DisplayAlert por un Popup
            }
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is Entry entry)
            {
                entry.Text = new string(entry.Text.Where(c => char.IsDigit(c)).ToArray());
            }
        }

        private void Popup_Clicked(object sender, EventArgs e)//Funcion para mostrar el popup, la clase <Popup> deberia recibir un objeto con las notas y el promedio
        {
            this.ShowPopup(new PopupPage());
        }
        
        private void Limpiar_Clicked(object sender, EventArgs e)
        {
            nota1.Text = string.Empty;
            nota2.Text = string.Empty;
            nota3.Text = string.Empty;
        }
    }
}