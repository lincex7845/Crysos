using System;
using System.Net.Http;
using System.Web.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CsvHelper;
using System.IO;
using Crysos.Encriptador.Funcionalidades;

namespace Crysos.ServicioVerificacion
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Crysos : ICrysos
    {
        //public HttpResponseMessage Registrar(string llavePublica)
        //{
        //    StreamWriter escritor = null;
        //    try
        //    {
        //        string rutaArchivo = Path.Combine(HostingEnvironment.MapPath("~/App_Data"), "Llaves.csv");
        //        escritor = new StreamWriter(rutaArchivo);
        //        CsvWriter escritorCSV = new CsvWriter(escritor);
        //        escritorCSV.Configuration.HasHeaderRecord = true;
        //        Llave nuevaLlave = new Llave()
        //        {
        //            ID = new Guid().ToString(),
        //            LlavePublica = llavePublica
        //        };
        //        escritorCSV.WriteRecord<Llave>(nuevaLlave);
        //        return new HttpResponseMessage()
        //        {
        //            StatusCode = System.Net.HttpStatusCode.Created,
        //            Content = new StringContent(String.Concat(nuevaLlave.ID))
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new System.ServiceModel.FaultException(ex.Message);
        //    }
        //    finally
        //    {
        //        if (escritor != null)
        //            escritor.Close();
        //    }
        //}

        //public HttpResponseMessage Validar(SolicitudVerificacion solicitud)
        //{
        //    StreamReader lector = null;
        //    try
        //    {
        //        string rutaArchivo = Path.Combine(HostingEnvironment.MapPath("~/App_Data"), "Llaves.csv");
        //        lector = new StreamReader(rutaArchivo);
        //        CsvReader lectorCSV = new CsvReader(lector);
        //        lectorCSV.Configuration.HasHeaderRecord = true;
        //        IEnumerable<Llave> llaves = lectorCSV.GetRecords<Llave>();
        //        Llave llaveAProbar = llaves.Where(z => z.ID.Equals(solicitud.id)).FirstOrDefault();
        //        if(!String.IsNullOrEmpty(llaveAProbar.ID) || !String.IsNullOrWhiteSpace(llaveAProbar.ID))
        //        {
        //            Generador.iniciarEncriptadorRSA(llaveAProbar.LlavePublica);
        //            string resultado = Generador.Desencriptar(solicitud.textoEncriptado);
        //            if(resultado.Equals(solicitud.texto))
        //            {
        //                return new HttpResponseMessage()
        //                {
        //                    StatusCode = System.Net.HttpStatusCode.OK,
        //                    Content = new StringContent(String.Concat("El texto se encriptó con la llave solicitada"))
        //                };
        //            }
        //            else
        //            {
        //                return new HttpResponseMessage()
        //                {
        //                    StatusCode = System.Net.HttpStatusCode.BadRequest,
        //                    Content = new StringContent(String.Concat("El texto no se encriptó con la llave solicitada"))
        //                };
        //            }
        //        }
        //        else
        //        {
        //            return new HttpResponseMessage()
        //            {
        //                StatusCode = System.Net.HttpStatusCode.NotFound,
        //                Content = new StringContent(String.Concat("La llave solicitada no ha sido registrada"))
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new System.ServiceModel.FaultException(ex.Message);
        //    }
        //    finally
        //    {
        //        if (lector != null)
        //            lector.Close();
        //    }
        //}

        public int Registrar(string llavePublica)
        {
            StreamWriter escritor = null;
            try
            {
                string rutaArchivo = Path.Combine(HostingEnvironment.MapPath("~/App_Data"), "Llaves.csv");
                escritor = new StreamWriter(rutaArchivo);
                CsvWriter escritorCSV = new CsvWriter(escritor);
                escritorCSV.Configuration.HasHeaderRecord = true;
                Llave nuevaLlave = new Llave()
                {
                    ID = new Guid().ToString(),
                    LlavePublica = llavePublica
                };
                escritorCSV.WriteRecord<Llave>(nuevaLlave);
                return 201;
            }
            catch (Exception ex)
            {
                throw new System.ServiceModel.FaultException(ex.Message);
            }
            finally
            {
                if (escritor != null)
                    escritor.Close();
            }
        }

        public int Validar(SolicitudVerificacion solicitud)
        {
            StreamReader lector = null;
            try
            {
                string rutaArchivo = Path.Combine(HostingEnvironment.MapPath("~/App_Data"), "Llaves.csv");
                lector = new StreamReader(rutaArchivo);
                CsvReader lectorCSV = new CsvReader(lector);
                lectorCSV.Configuration.HasHeaderRecord = true;
                IEnumerable<Llave> llaves = lectorCSV.GetRecords<Llave>();
                Llave llaveAProbar = llaves.Where(z => z.ID.Equals(solicitud.Id)).FirstOrDefault();
                if (!String.IsNullOrEmpty(llaveAProbar.ID) || !String.IsNullOrWhiteSpace(llaveAProbar.ID))
                {
                    Generador.iniciarEncriptadorRSA(llaveAProbar.LlavePublica);
                    string resultado = Generador.Desencriptar(solicitud.TextoEncriptado);
                    if (resultado.Equals(solicitud.Texto))
                    {
                        return 200;
                    }
                    else
                    {
                        return 400;
                    }
                }
                else
                {
                    return 404;
                }
            }
            catch (Exception ex)
            {
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
