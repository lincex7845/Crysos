using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Crysos.ServicioVerificacion
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICrysos
    {
        //[WebInvoke(Method="POST", ResponseFormat=WebMessageFormat.Json, 
        //    UriTemplate="Registrar/", RequestFormat=WebMessageFormat.Json)]
        //[OperationContract]
        //HttpResponseMessage Registrar(string llavePublica);

        //[WebInvoke(Method="POST", ResponseFormat=WebMessageFormat.Json, 
        //    UriTemplate="Validar/", RequestFormat=WebMessageFormat.Json)]
        //[OperationContract]
        //HttpResponseMessage Validar(SolicitudVerificacion solicitud);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Registrar", RequestFormat = WebMessageFormat.Json,
            BodyStyle=WebMessageBodyStyle.Bare)]
        [OperationContract]
        int Registrar(string llavePublica);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Validar", RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        int Validar(SolicitudVerificacion solicitud);
    }

    public struct Llave
    {
        public string ID;
        public string LlavePublica;
    }

    [DataContract]
    public class SolicitudVerificacion
    {
        [DataMember]
        public string  Id;
        [DataMember]
        public string Texto;
        [DataMember]
        public string TextoEncriptado;
    }
}