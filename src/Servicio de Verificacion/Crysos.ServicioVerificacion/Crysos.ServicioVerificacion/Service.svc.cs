using Crysos.Encriptador.Funcionalidades;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Web;
using System.Web.Hosting;

namespace Crysos.ServicioVerificacion
{
    /// <summary>
    /// Clase que instancia el servicio REST
    /// y contiene la lógica de negocio de sus
    /// métodos
    /// </summary>
    public class Service : IService
    {
        /// <summary>
        /// Método que permite registrar una llave pública
        /// </summary>
        /// <param name="LlavePublica">
        /// Cadena de texto de la llave pública exportada 
        /// a XML
        /// </param>
        /// <returns>
        /// Cadena correspondiente al id de la llave registrada
        /// con código HTTP 201 si se guardó correctamente
        /// en caso contrario HTTP 500
        /// </returns>
        public string Registrar(string LlavePublica)
        {
            StreamWriter escritor = null;
            try
            {
                string rutaArchivo = Path.Combine(HostingEnvironment.MapPath("~/App_Data"), "Llaves.csv");
                escritor = new StreamWriter(rutaArchivo, true);
                CsvWriter escritorCSV = new CsvWriter(escritor);
                escritorCSV.Configuration.HasHeaderRecord = true;
                
                /// Inicializa el objeto de encriptación con el tamaño de llave por defecto
                Generador.iniciarEncriptadorRSA();
                Llave nuevaLlave = new Llave()
                {
                    ID = Guid.NewGuid().ToString(),
                    LlavePublica = LlavePublica
                };

                /// Guarda las llaves en el archivo
                /// Se retorna el ID con Código Http 201
                escritorCSV.WriteRecord<Llave>(nuevaLlave);
                WebOperationContext contexto = WebOperationContext.Current;
                contexto.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.Created;
                return nuevaLlave.ID;
            }
            catch (Exception ex)
            {
                /// Si se presentan problemas al escribir el archivo
                /// se retorna Código Http 500
                WebOperationContext contexto = WebOperationContext.Current;
                contexto.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                throw new System.ServiceModel.FaultException(ex.Message);
            }
            finally
            {
                if (escritor != null)
                    escritor.Close();
            }
        }

        /// <summary>
        /// Método para probar la disponibilidad del servicio
        /// </summary>
        /// <returns>
        /// True si está disponible
        /// </returns>
        public bool Heartbeat()
        {
            return true;
        }

        /// <summary>
        /// Método para validar si un texto
        /// </summary>
        /// <param name="solicitud">
        /// Estructura de datos compuesta de la siguiente forma
        /// 1. Cadena encriptada
        /// 2. Cadena a verficar con la cadena encriptada
        /// 3. Cadena correspondiente al id de la posible llave
        /// pública que corresponde a la llave privada que encriptó
        /// la cadena
        /// </param>
        /// <returns>
        /// 1. True, si la cadena de verificación corresponde a la cadena encriptada
        /// (por tanto fue encriptado con la correspondiente llave privada de la llave
        /// pública solicitada)
        /// con código HTTP 200.
        /// 2. False, si la cadena de verificación no corresponde a la cadena encriptada
        /// (por tanto no fue encriptado con la correspondiente llave privada de la llave
        /// pública solicitada)
        /// con código HTTP 400
        /// </returns>
        public bool Validar(SolicitudVerificacion solicitud)
        {
            StreamReader lector = null;
            try
            {
                if (!String.IsNullOrEmpty(solicitud.IDLlavePublica) && !String.IsNullOrWhiteSpace(solicitud.IDLlavePublica)
                    && !String.IsNullOrEmpty(solicitud.CadenaEncriptada) && !String.IsNullOrWhiteSpace(solicitud.CadenaEncriptada)
                    && !String.IsNullOrWhiteSpace(solicitud.CadenaVerificacion) && !String.IsNullOrEmpty(solicitud.CadenaVerificacion))
                {
                    string rutaArchivo = Path.Combine(HostingEnvironment.MapPath("~/App_Data"), "Llaves.csv");
                    lector = new StreamReader(rutaArchivo);
                    CsvReader lectorCSV = new CsvReader(lector);
                    lectorCSV.Configuration.HasHeaderRecord = true;
                    IEnumerable<Llave> llaves = lectorCSV.GetRecords<Llave>();
                    Llave llave = null;
                    while (lectorCSV.Read())
                    {
                        var item = lectorCSV.GetRecord<Llave>();
                        llave = (Llave)item;
                        if(llave.ID.Equals(solicitud.IDLlavePublica))
                            break;
                    }
                    string llavePublica = llave.LlavePublica;
                    
                    string textoDesencriptado = Generador.DesencriptarConLlavePublica(solicitud.CadenaEncriptada, llavePublica);
                    if (textoDesencriptado.Equals(solicitud.CadenaVerificacion))
                    {
                        /// La cadena encriptada corresponde a la cadena de verificación
                        WebOperationContext contexto = WebOperationContext.Current;
                        contexto.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
                        return true;
                    }
                    else
                    {
                        /// La cadena encriptada no corresponde a la cadena de verificación
                        WebOperationContext contexto = WebOperationContext.Current;
                        contexto.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        return false;
                    }
                }
                else
                {
                    /// Los datos de prueba no son válidos
                    /// se retorna Código Http 400
                    WebOperationContext contexto = WebOperationContext.Current;
                    contexto.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return false;
                }
            }
            catch (Exception ex)
            {
                /// Error interno del servidor a procesar la solicitud
                /// Se retorna Código Http 500
                WebOperationContext contexto = WebOperationContext.Current;
                contexto.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                throw new System.ServiceModel.FaultException(ex.Message);
            }
            finally
            {
                if (lector != null)
                    lector.Close();
            }
        }
    }
}