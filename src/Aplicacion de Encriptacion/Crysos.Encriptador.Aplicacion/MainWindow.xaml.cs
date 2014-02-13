using Crysos.Encriptador.Funcionalidades;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

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

        /// <summary>
        /// Manejador de evento para el boton de encriptación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEncriptar_Click(object sender, RoutedEventArgs e)
        {
            if (this.TextoEncriptar.Text.Equals(""))
            {
                MessageBox.Show("Debe proveer un texto a encriptar");
            }
            else
            {
                /// Se dispone un objeto OpenFileDialog para
                /// abrir el archivo que contiene la llave pública
                OpenFileDialog abrirArchivo = new OpenFileDialog();
                abrirArchivo.FileName = "";
                abrirArchivo.Title = "Abrir archivo de llaves";
                abrirArchivo.Filter = "Archivo de llaves ( *.keys )|*.keys";
                string llaveEnArchivo = null;
                if (abrirArchivo.ShowDialog() ==  true)
                {
                    if (File.Exists(abrirArchivo.FileName))
                    {
                        StreamReader lectorArchivo = null;
                        try
                        {
                            lectorArchivo = new StreamReader(abrirArchivo.FileName, true);
                            llaveEnArchivo = lectorArchivo.ReadToEnd();
                            if (!String.IsNullOrEmpty(llaveEnArchivo) && !String.IsNullOrWhiteSpace(llaveEnArchivo))
                            {
                                /// Se obtiene a continuación la etiqueta 
                                /// <tamanoLlave> y su valor para determinar 
                                /// el tamaño de la llave generada
                                //Int32 tamanoLlave = 0;
                                //string tamanoLlaveArchivo = llaveEnArchivo.Substring(0, llaveEnArchivo.IndexOf("</tamanoLlave>") + 14);
                                /// Una vez identificada la etiqueta y su valor, se suprime de la cadena de la llave
                                /// pública exportada a XML
                                //llaveEnArchivo = llaveEnArchivo.Replace(tamanoLlaveArchivo, "");
                                /// Se deja simplemente el número correspodiente al tamaño de la llave
                                //tamanoLlaveArchivo = tamanoLlaveArchivo.Replace("<tamanoLlave>", "").Replace("</tamanoLlave>", "");
                                //bool conversion = Int32.TryParse(tamanoLlaveArchivo, out tamanoLlave);
                                //if (conversion)
                                //{
                                    /// Se llama al método de encriptación de la biblioteca de funcionalidades 
                                    /// pasándole como parámetros: el texto a encriptar, la llave pública en formato XML y
                                    /// el tamaño de la llave. Retorna una cadena encriptada
                                    //string textoEncriptado = Generador.Encriptar(this.TextoEncriptar.Text, llaveEnArchivo, tamanoLlave);
                                    string textoEncriptado = Generador.EncriptarConLlavePrivada(this.TextoEncriptar.Text, llaveEnArchivo);
                                    this.TextoEncriptado.Text = textoEncriptado;
                                    MessageBox.Show("El texto fue encriptado");
                                //}
                                //else
                                //{
                                //    MessageBox.Show("Debe proveer un archivo de llaves generado por este programa");
                                //}
                            }
                            else
                            {
                                MessageBox.Show("Debe proveer una llave pública válida");
                            }
                        }
                        #pragma warning disable
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocurrió un error al abrir el archivo de llave pública");
                        }
                        finally
                        {
                            lectorArchivo.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Manejador de evento para el botón desencriptar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDesencriptar_Click(object sender, RoutedEventArgs e)
        {
            if (this.TextoEncriptado1.Text.Equals(""))
            {
                MessageBox.Show("Debe proveer un texto a desencriptar");
            }
            else
            {
                /// Se dispone de un objeto OpenFileDialog 
                /// para abrir el archivo que contiene las llaves públicas
                /// y privadas para desencriptar
                OpenFileDialog abrirArchivo = new OpenFileDialog();
                abrirArchivo.FileName = "";
                abrirArchivo.Title = "Abrir archivo de llave pública";
                abrirArchivo.Filter = "Archivo de llave pública ( *.pubk )|*.pubk";
                string llavesEnArchivo = null;
                if (abrirArchivo.ShowDialog() == true)
                {
                    if (File.Exists(abrirArchivo.FileName))
                    {
                        StreamReader lectorArchivo = null;
                        try
                        {
                            lectorArchivo = new StreamReader(abrirArchivo.FileName, true);
                            llavesEnArchivo = lectorArchivo.ReadToEnd();
                            if (!String.IsNullOrEmpty(llavesEnArchivo) && !String.IsNullOrWhiteSpace(llavesEnArchivo))
                            {
                                /// Se obtiene a continuación la etiqueta 
                                /// <tamanoLlave> y su valor para determinar 
                                /// el tamaño de la llave generada
                                //Int32 tamanoLlave = 0;
                                //string tamanoLlaveArchivo = llavesEnArchivo.Substring(0, llavesEnArchivo.IndexOf("</tamanoLlave>") + 14);
                                /// Una vez identificada la etiqueta y su valor, se suprime de la cadena de la llave
                                /// pública exportada a XML
                                //llavesEnArchivo = llavesEnArchivo.Replace(tamanoLlaveArchivo, "");
                                /// Se deja simplemente el número correspodiente al tamaño de la llave
                                //tamanoLlaveArchivo = tamanoLlaveArchivo.Replace("<tamanoLlave>", "").Replace("</tamanoLlave>", "");
                                //bool conversion = Int32.TryParse(tamanoLlaveArchivo, out tamanoLlave);
                                //if (conversion)
                                //{
                                    /// Se llama al método de desencriptación de la biblioteca de funcionalidades 
                                    /// pasándole como parámetros: el texto a desencriptar, las llaves pública y privada en formato XML y
                                    /// el tamaño de la llave. Retorna una cadena desencriptada
                                    string textoDesencriptado = Generador.DesencriptarConLlavePublica(this.TextoEncriptado1.Text, llavesEnArchivo);
                                    this.TextoDesencriptado.Text = textoDesencriptado;
                                    MessageBox.Show("El texto fue desencriptado");
                                //}
                                //else
                                //{
                                //    MessageBox.Show("Debe proveer un archivo de llaves generado por este programa");
                                //}
                            }
                            else
                            {
                                MessageBox.Show("Debe proveer una llave pública y una llave privada válidas");
                            }
                        }
                        #pragma warning disable
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocurrió un error al abrir el archivo de llaves");
                        }
                        finally
                        {
                            lectorArchivo.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Manejador de evento para el botón de 
        /// generación de llaves pública y privada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGenerarLlaves_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Generador.iniciarEncriptadorRSA(1024);
                Generador.iniciarEncriptadorRSA();
                string llavePrivadaYPublica = /*"<tamanoLlave>" + 1024.ToString() + "</tamanoLlave>" +*/ Generador.ObtenerLlavePublicaPrivada(true);
                string llavePublica = /*"<tamanoLlave>" + 1024.ToString() + "</tamanoLlave>" +*/ Generador.ObtenerLlavePublicaPrivada(false);
                if (!GuardarArchivo("Guardar Llave pública y privada", "Archivo de llaves  ( *.keys )|*.keys", llavePrivadaYPublica))
                {
                    MessageBox.Show("No es posible guardar los archivos de llaves.");
                }
                else
                {
                    if (!GuardarArchivo("Guardar Llave pública", "Archivo de llave publica ( *.pubk )|*.pubk", llavePublica))
                    {
                        MessageBox.Show("No es posible guardar el archivo de la llave pública.");
                    }
                }
            }
            #pragma warning disable
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al generar las llaves.");
            }
        }

        /// <summary>
        /// Método para guardar archivos de llaves de encriptación RSA
        /// </summary>
        /// <param name="mensaje">Mensaje para el título del objeto SaveFileDialog</param>
        /// <param name="tipoArchivo">Filtro para guardar la llave con su respectiva extensión</param>
        /// <param name="llave">Llave de encriptación RSA a guardar</param>
        /// <returns></returns>
        private bool GuardarArchivo(string mensaje, string tipoArchivo, string llave)
        {
            bool retorno = false;
            SaveFileDialog guardarArchivo = new SaveFileDialog();
            guardarArchivo.Title = mensaje;
            guardarArchivo.Filter = tipoArchivo;
            guardarArchivo.FileName = "";
            if (guardarArchivo.ShowDialog() == true)
            {
                StreamWriter escritorArchivo = new StreamWriter(guardarArchivo.FileName, false);
                try
                {                    
                    if (llave != null)
                    {
                        escritorArchivo.Write(llave);
                        retorno = true;
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                }
                finally
                {
                    escritorArchivo.Close();
                }
            }
            return retorno;
        }
    }
}