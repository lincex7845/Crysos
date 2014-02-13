using System.ServiceModel;
using System.ServiceModel.Web;

namespace Crysos.ServicioVerificacion
{
    /// <summary>
    /// Interfaz para la implementación de los métodos del servicio
    /// </summary>
    [ServiceContract]
    public interface IService
    {
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/Registrar/", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        string Registrar(string LlavePrivada);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/Validar/", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool Validar(SolicitudVerificacion solicitud);

        [WebInvoke(
            Method="POST", ResponseFormat=WebMessageFormat.Json,
            UriTemplate="/Hearbeat/")]
        [OperationContract]
        bool Heartbeat();
    }

    /// <summary>
    /// Clase que permite estructurar el archivo
    /// que almacenará las llaves públicas
    /// </summary>
    public class Llave
    {
        public string ID { get; set; }
        public string LlavePublica { get; set; }
    }

    /// <summary>
    /// Estructura para recibir la solicitud de
    /// verificación de textos cifrados 
    /// </summary>
    public struct SolicitudVerificacion
    {
        public string CadenaEncriptada;
        public string CadenaVerificacion;
        public string IDLlavePublica;
    }
}