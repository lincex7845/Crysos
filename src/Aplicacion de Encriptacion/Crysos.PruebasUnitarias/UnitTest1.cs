using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crysos.Encriptador.Funcionalidades;
using System.Text;
using System.Security.Cryptography;

namespace Crysos.PruebasUnitarias
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDesencriptar()
        {
            string llavePublica = "<RSAKeyValue><Modulus>ssFQ+v/YJzttB8I+RBTfCcdpdnD2Rl9uGlApslCbE2mfac9M2+sehXgqKlVK/S0bbLaYBF2GDw9cmumn0FROSy8hrI3RlpnpY33ff5tPRgGZh/0zG9d+SGMgtY8BsMjyjpPr0AYf4o2N3W4BrxIKJNVi1lpmpjbJQvMRhmr7atU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            string cadenaEncriptada = "KYVIK3qJXB7vzeYd/kN8Atth1OCYfFsyFBCXlF7eHNYKa8ELtJHelwLfx29T1aQt14i8kklWlps8qk64xO6+ffxKYdEyBa32a+obkUW/sSrM0awVwO3qjhQQ5y6SZE0cUU7LE9E0pU3diSrBJ/dFKxt/dADAx5wLEy5IgAcRyt4=";
            string esperado = "la ñañ sú3lo$#%#& fahsñvnds bfdsmovsd-goeir3q45t$#%#/{}[]~^";
            string textoDesencriptado = Generador.DesencriptarConLlavePublica(cadenaEncriptada, llavePublica);
            Assert.AreEqual(esperado, textoDesencriptado, "Listo");

        }

        [TestMethod]
        public void TestEncriptar()
        {
            string texto = "la ñañ sú3lo$#%#& fahsñvnds bfdsmovsd-goeir3q45t$#%#/{}[]~^";
            string esperado = "KYVIK3qJXB7vzeYd/kN8Atth1OCYfFsyFBCXlF7eHNYKa8ELtJHelwLfx29T1aQt14i8kklWlps8qk64xO6+ffxKYdEyBa32a+obkUW/sSrM0awVwO3qjhQQ5y6SZE0cUU7LE9E0pU3diSrBJ/dFKxt/dADAx5wLEy5IgAcRyt4=";
            string llavePrivada = "<RSAKeyValue><Modulus>ssFQ+v/YJzttB8I+RBTfCcdpdnD2Rl9uGlApslCbE2mfac9M2+sehXgqKlVK/S0bbLaYBF2GDw9cmumn0FROSy8hrI3RlpnpY33ff5tPRgGZh/0zG9d+SGMgtY8BsMjyjpPr0AYf4o2N3W4BrxIKJNVi1lpmpjbJQvMRhmr7atU=</Modulus><Exponent>AQAB</Exponent><P>4HV/yhecxKBrRFmll5WePQh7odWrZat/OZ2pZe7VLVgC5QwOH/s7v75V3lDkPwy1QnFmCzjLge3MURKAW3ZYZw==</P><Q>y9+0icpEhD/2T1dZZgvauZU0IXYF6nKUbKeLdT/kOV1oCvkMddYgwLnMRtvvaDTN64SlfpBWH9q+UQlu/DYNYw==</Q><DP>luGlEx4oPWxwbrOsQmdKxVAsey78Vg2gKgS3WFPhbOeamokt/YWONmglpJnPtCpAtfcwVx7IfgBxtZWwPssgxQ==</DP><DQ>C6mXZU55zurtxyojBhBlibo8SjG7MuctEG4hLyrhflqWihInIVKHex7lzaPlNRvYL8HdybiuBJJ50p7sh2b8Kw==</DQ><InverseQ>fXYlCToaCE0bhNPI8NvO7b//ESIFCxzbx4owvpdpwt3pD0zM8gIhCyusq6xyq43kockQdTM3Xpkq9kaCfESlnQ==</InverseQ><D>D5fJwsi8PW6IzBiiH4SQkNpPyqX/najnwleEI6ALaa3uMCfbT63FW6WH9r8PoQvPwh51c/VdOIVsXGkNxfWstJXlKdxSmtxG7wn+D3hxt5qsgwfzYLlkEjBNQioC46iH+8q+2SHtqkeMQeo7AymKodX90/1PAvM6fQQ26tGjiXs=</D></RSAKeyValue>";
            string textoEncriptado = Generador.EncriptarConLlavePrivada(texto, llavePrivada);
            Assert.AreEqual(esperado, textoEncriptado, "Listo");
        }
    }
}
