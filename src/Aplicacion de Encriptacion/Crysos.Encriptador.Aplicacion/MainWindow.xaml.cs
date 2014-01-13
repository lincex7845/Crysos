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
using Crysos.Encriptador.Funcionalidades;

namespace Crysos.Encriptador.Aplicacion
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

        private void BtnGenerarLlavePublica_Click(object sender, RoutedEventArgs e)
        {
            this.TextLlavePublica.Text = Generador.ObtenerLlavePublica();
        }

        private void BtnEncriptar_Click(object sender, RoutedEventArgs e)
        {
            if (this.TextLlavePublica.Text.Equals(""))
            {
                MessageBox.Show("Debe generar la llave pública");
            }
            else
            {
                if (this.TextoEncriptar.Text.Equals(""))
                {
                    MessageBox.Show("Debe proveer un texto a encriptar");
                }
                else
                {
                    //Generador.iniciarEncriptadorRSA(this.TextLlavePublica.Text);
                    byte[] textoSinEncriptar = Encoding.UTF8.GetBytes(this.TextoEncriptar.Text);
                    this.TextoEncriptado.Text = Convert.ToBase64String(Generador.EncriptarTextoEnByte(textoSinEncriptar));
                    MessageBox.Show("El texto ha sido encriptado");
                }
            }
        }

        private void BtnDesencriptar_Click(object sender, RoutedEventArgs e)
        {
            if (this.TextLlavePublica.Text.Equals(""))
            {
                MessageBox.Show("Debe generar la llave pública");
            }
            else
            {
                if (this.TextoEncriptado1.Text.Equals(""))
                {
                    MessageBox.Show("Debe proveer un texto a desencriptar");
                }
                else
                {
                    //Generador.iniciarEncriptadorRSA(this.TextLlavePublica.Text);
                    byte[] textoEncriptado = Convert.FromBase64String(this.TextoEncriptado1.Text);
                    this.TextoDesencriptado.Text = Encoding.UTF8.GetString(Generador.DesencriptarTextoEnByte(textoEncriptado));
                    MessageBox.Show("El texto ha sido desencriptado");
                }
            }
        }
    }
}
