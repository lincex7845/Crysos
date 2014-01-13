using System;
using System.Security.Cryptography;
using System.Text;

namespace Crysos.Encriptador.Funcionalidades
{
    /// <summary>
    /// Clase que provee las funcionalidades de Encriptación de Crysos
    /// </summary>
    public static class Generador
    {
        private static RSACryptoServiceProvider encriptador = null;

        public static RSACryptoServiceProvider Encriptador
        {
          get 
          {
              if (Generador.encriptador == null)
                  Generador.encriptador = new RSACryptoServiceProvider();
              return Generador.encriptador; 
          }
          set { Generador.encriptador = value; }
        }

        /// <summary>
        /// Método que inicializa el objeto encriptador asimetrico
        /// </summary>
        /// <param name="llavePublica">Cadena de la llave  publica en formato XML</param>
        public static void iniciarEncriptadorRSA(string llavePublica)
        {
            if (!(String.IsNullOrEmpty(llavePublica)) && !(String.IsNullOrWhiteSpace(llavePublica)))
            {
                Encriptador.FromXmlString(llavePublica);
            }
        }

        /// <summary>
        /// Método para la encriptación asimétrica
        /// de una cadena de texto
        /// </summary>
        /// <param name="textoAEncriptar">Cadena que contiene el texto a encriptar</param>
        /// <returns>Cadena con el texto encriptado</returns>
        public static string Encriptar(string textoAEncriptar)
        {
            if (Encriptador != null)
            {
                /// Byte array que almacenará el texto a crifrar en bytes. Tiene en cuenta tildes
                byte[] textoSinCrifrar = Encoding.UTF8.GetBytes(textoAEncriptar);

                /// Byte array Byte array que almacenará el texto crifrado en bytes
                /// No se usa Optimal Asymmetric Encryption Padding (OAEP)
                /// como esquema de relleno
                byte[] textoCifrado = Encriptador.Encrypt(textoSinCrifrar, false);

                /// Retorna la cadena de texto cifrada. Tiene en cuenta tildes
                return Encoding.UTF8.GetString(textoCifrado);
            }
            return null;
        }

        /// <summary>
        /// Método que retorna el contenido sin cifrado cuyo 
        /// contenido ha sido asimétricamente cifrado
        /// </summary>
        /// <param name="textoADesencriptar">Cadena que contiene el texto cifrado</param>
        /// <returns>Texto sin encriptado asimétrico</returns>
        public static string Desencriptar(string textoADesencriptar)
        {
            if (Encriptador != null)
            {
                /// Byte array que almacenará el texto cifrado en bytes. Tiene en cuenta tildes
                byte[] textoCrifrado = Encoding.UTF8.GetBytes(textoADesencriptar);

                /// Byte array que almacenará el texto sin crifrado en bytes
                /// No se usa Optimal Asymmetric Encryption Padding (OAEP)
                /// como esquema de relleno
                byte[] textoSinCifrado = Encriptador.Decrypt(textoCrifrado, false);

                /// Retorna la cadena de texto cifrada. Tiene en cuenta tildes
                return Encoding.UTF8.GetString(textoSinCifrado);
            }
            return null;
        }

        /// <summary>
        /// Método para la encriptación asimétrica
        /// de una cadena de texto
        /// </summary>
        /// <param name="textoAEncriptar">Byte array que contiene el texto a encriptar</param>
        /// <returns>Byte array del texto encriptado</returns>
        public static byte[] EncriptarTextoEnByte(byte[] textoAEncriptar)
        {
            if (Encriptador != null)
            {
                /// Byte array que almacenará el texto crifrado en bytes
                /// No se usa Optimal Asymmetric Encryption Padding (OAEP)
                /// como esquema de relleno
                return Encriptador.Encrypt(textoAEncriptar, false);
            }
            return null;
        }

        /// <summary>
        /// Método que retorna el contenido sin cifrado 
        /// </summary>
        /// <param name="textoADesencriptar">Cadena que contiene el texto cifrado</param>
        /// <returns>Texto sin encriptado asimétrico</returns>
        public static byte[] DesencriptarTextoEnByte(byte[] textoADesencriptar)
        {
            if (Encriptador != null)
            {
                /// Byte array que almacenará el texto sin crifrado en bytes
                return Encriptador.Decrypt(textoADesencriptar, false);
            }
            return null;
        }

        /// <summary>
        /// Metodo para obtener la llave publica del objeto Encriptador
        /// </summary>
        /// <returns>Llave pública en formato string</returns>
        public static string ObtenerLlavePublica()
        {
            return Encriptador.ToXmlString(false);
        }
    }
}
