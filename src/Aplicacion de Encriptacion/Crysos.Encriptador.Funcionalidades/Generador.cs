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
        /// <summary>
        /// Objeto de encriptación asimétrica RSA
        /// </summary>
        private static RSACryptoServiceProvider encriptador = null;

        /// <summary>
        /// Método que inicializa el objeto encriptador asimetrico RSA
        /// </summary>
        /// <param name="TamanoLlave">Tamaño para los bytes de la llave</param>
        public static void iniciarEncriptadorRSA(Int32 TamanoLlave)
        {
            encriptador = new RSACryptoServiceProvider(TamanoLlave);
        }

        /// <summary>
        /// Método que inicializa el objeto encriptador asimetrico RSA
        /// utilizando el tamaño de llave por defecto de la API de .Net
        /// </summary>
        public static void iniciarEncriptadorRSA()
        {
            encriptador = new RSACryptoServiceProvider();
        }

        /// <summary>
        /// Método para la encriptación asimétrica
        /// de una cadena de texto con llave pública
        /// </summary>
        /// <param name="TextoAEncriptar">Cadena que contiene el texto a encriptar</param>
        /// <param name="LlavePublicaXML">Llave publica exportada en formato XML</param>
        /// <param name="TamanoLlave">Entero que determina el tamaño de la llave</param>
        /// <returns>Cadena con el texto encriptado</returns>
        public static string EncriptarConLlavePublica(string TextoAEncriptar, string LlavePublicaXML, int TamanoLlave)
        {
            iniciarEncriptadorRSA(TamanoLlave);
            encriptador.FromXmlString(LlavePublicaXML);
            if (encriptador != null)
            {
                /// Byte array que almacenará el texto a crifrar en bytes. Tiene en cuenta tildes
                byte[] textoSinCrifrar = Encoding.UTF8.GetBytes(TextoAEncriptar);

                /// Byte array que almacenará el texto crifrado en bytes
                /// Se usa Optimal Asymmetric Encryption Padding (OAEP)
                /// como esquema de relleno
                byte[] textoCifrado = encriptador.Encrypt(textoSinCrifrar, false);

                /// Retorna la cadena de texto cifrada.
                return Convert.ToBase64String(textoCifrado);
            }
            return null;
        }

        /// <summary>
        /// Método que retorna el contenido sin cifrado cuyo 
        /// contenido ha sido asimétricamente cifrado con llave pública
        /// </summary>
        /// <param name="TextoADesencriptar">Cadena que contiene el texto cifrado</param>
        /// <param name="LlavePrivadaPublicaXML">Llave privada y publica de encriptacion en formato
        /// XML</param>
        /// <param name="TamanoLlave">Entero que determina el tamaño de la llave</param>
        /// <returns>Texto sin encriptado asimétrico</returns>
        public static string DesencriptarConLlavePrivada(string TextoADesencriptar, string LlavePrivadaPublicaXML, int TamanoLlave)
        {
            iniciarEncriptadorRSA(TamanoLlave);
            encriptador.FromXmlString(LlavePrivadaPublicaXML);
            if (encriptador != null)
            {
                /// Byte array que almacenará el texto cifrado en bytes.
                byte[] textoCrifrado = Convert.FromBase64String(TextoADesencriptar);

                /// Byte array que almacenará el texto sin crifrado en bytes
                /// Se usa Optimal Asymmetric Encryption Padding (OAEP)
                /// como esquema de relleno
                byte[] textoSinCifrado = encriptador.Decrypt(textoCrifrado, false);

                /// Retorna la cadena de texto cifrada. Tiene en cuenta tildes
                return Encoding.UTF8.GetString(textoSinCifrado);
            }
            return null;
        }

        /// <summary>
        /// Método para la encriptación asimétrica
        /// de una cadena de texto con llave privada
        /// </summary>
        /// <param name="TextoAEncriptar">Cadena que contiene el texto a encriptar</param>
        /// <param name="LlavePrivadaPublicaXML">Llave publica y privada exportadas
        /// en formato XML</param>
        /// <returns>Cadena con el texto encriptado</returns>
        public static string EncriptarConLlavePrivada(string TextoAEncriptar, string LlavePrivadaPublicaXML)
        {
            iniciarEncriptadorRSA();
            if (encriptador != null)
            {
                try
                {
                    /// Se instancia el objeto de encriptación
                    /// se obtiene a continuación los parmetros 
                    /// de encriptación de la llave privada
                    /// en instancias de la clase BigInteger
                    encriptador.FromXmlString(LlavePrivadaPublicaXML);
                    RSAParameters parametros = encriptador.ExportParameters(true);
                    BigInteger exponentePrivado = new BigInteger(parametros.D);
                    BigInteger exponente = new BigInteger(parametros.Exponent);
                    BigInteger modulo = new BigInteger(parametros.Modulus);


                    /// Byte array que almacenará el texto a crifrar en bytes. Tiene en cuenta tildes
                    byte[] textoSinCrifrar = Encoding.UTF8.GetBytes(TextoAEncriptar);
                    /// Se instancia un objeto BigInteger con los bytes del texto a encriptar
                    BigInteger bigIntegerTexto = new BigInteger(textoSinCrifrar);
                    /// Se instancia un objeto BigInteger con los bytes del texto encriptado
                    /// Encriptando con la fórmula: (textoEncriptar ^ D) % Modulus
                    /// Que aplica para la encriptación por llave privada
                    BigInteger bigIntegerTextoEncriptado = bigIntegerTexto.modPow(exponentePrivado, modulo);

                    /// Byte array que almacenará el texto crifrado en bytes
                    byte[] textoCifrado = bigIntegerTextoEncriptado.getBytes();

                    /// Retorna la cadena de texto cifrada.
                    return Convert.ToBase64String(textoCifrado);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }

        /// <summary>
        /// Método que retorna el contenido sin cifrado cuyo 
        /// contenido ha sido asimétricamente cifrado con llave privada
        /// </summary>
        /// <param name="TextoADesencriptar">Cadena que contiene el texto cifrado</param>
        /// <param name="LlavePrivadaPublicaXML">Llave publica de encriptacion en 
        /// formato XML</param>
        /// <returns>Texto sin encriptado asimétrico</returns>
        public static string DesencriptarConLlavePublica(string TextoADesencriptar, string LlavePublicaXML)
        {
            iniciarEncriptadorRSA();
            if (encriptador != null)
            {
                try
                {
                    /// Se instancia el objeto de encriptación
                    /// se obtiene a continuación los parmetros 
                    /// de encriptación de la llave pública
                    /// en instancias de la clase BigInteger
                    encriptador.FromXmlString(LlavePublicaXML);
                    RSAParameters parametros = encriptador.ExportParameters(false);
                    BigInteger modulo = new BigInteger(parametros.Modulus);
                    BigInteger exponente = new BigInteger(parametros.Exponent);

                    /// Byte array que almacenará el texto cifrado en bytes.
                    byte[] textoCrifrado = Convert.FromBase64String(TextoADesencriptar);
                    /// Se instancia un objeto BigInteger para el texto encriptado
                    BigInteger bigIntegerTextoEncriptado = new BigInteger(textoCrifrado);
                    /// Se instancia un objeto BigInteger con los bytes del texto desencriptado
                    /// desencriptando con la fórmula: (textoEncriptado ^ E) % Modulus
                    /// Que aplica para la encriptación por llave privada
                    BigInteger bigIntegerTextoDesencriptado = bigIntegerTextoEncriptado.modPow(exponente, modulo);

                    /// Byte array que almacenará el texto sin crifrado en bytes
                    byte[] textoSinCifrado = bigIntegerTextoDesencriptado.getBytes();

                    /// Retorna la cadena de texto cifrada. Tiene en cuenta tildes
                    return Encoding.UTF8.GetString(textoSinCifrado);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }

        /// <summary>
        /// Metodo para obtener la llave publica o privada del objeto Encriptador
        /// </summary>
        /// <param name="TipoLlave">variable bool para determinar el tipo de llave
        /// True, incluye la llave privada y la pública
        /// False, solo la pública</param>
        /// <returns>Llave pública o la privada en formato string</returns>
        public static string ObtenerLlavePublicaPrivada(bool TipoLlave)
        {
            return encriptador.ToXmlString(TipoLlave);
        }
    }
}