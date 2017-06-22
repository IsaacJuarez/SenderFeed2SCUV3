using FUJI.SenderFeed2SCU.Service.Extensions;
using FUJI.SenderFeed2SCU.Service.Feed2Service;
using System;
using System.Linq;

namespace FUJI.SenderFeed2SCU.Service.DataBase
{
    public class NapoleonSenderDataAccess
    {
        //public static NAPOLEONEntities NapoleonDA;
        public static NapoleonServiceClient NapoleonDA = new NapoleonServiceClient();

        public ClienteF2CResponse getEstudiosEnviar(int id_Sitio, string vchClaveSitio)
        {
            Log.EscribeLog("Leyendo del Sitio: " + id_Sitio.ToString());
            ClienteF2CResponse response = new ClienteF2CResponse();
            try
            {
                ClienteF2CRequest request = new ClienteF2CRequest();
                request.id_Sitio = id_Sitio;
                //request.id_SitioSpecified = true;
                request.vchClaveSitio = vchClaveSitio;
                request.Token = Security.Encrypt(id_Sitio + "|" + vchClaveSitio);
                response = NapoleonDA.getEstudiosEnviar(request);
                Log.EscribeLog("Archivos a enviar: " + response.lstEstudio.Count().ToString());
            }
            catch (Exception e)
            {
                response.message = e.Message;
                response.valido = false;
                Log.EscribeLog("Existe un error en getEstudiosEnviar: " + e.Message);
            }
            return response;
        }

        public ClienteF2CResponse updateEstatus(int intDetEstudioID, int id_Sitio, string vchClaveSitio)
        {
            ClienteF2CResponse response = new ClienteF2CResponse();
            try
            {
                ClienteF2CRequest request = new ClienteF2CRequest();
                request.intDetEstudioID = intDetEstudioID;
                //request.intDetEstudioIDSpecified = true;
                request.Token = Security.Encrypt(id_Sitio + "|" + vchClaveSitio);
                request.id_Sitio = id_Sitio;
                //request.id_SitioSpecified = true;
                request.vchClaveSitio = vchClaveSitio;
                response = NapoleonDA.updateEstatus(request);
            }
            catch (Exception eup)
            {
                Log.EscribeLog("Existe un error en updateEstatus:" + eup.Message);
            }
            return response;
        }

        public static void setService(int id_Servicio, string vchClaveSitio)
        {
            try
            {
                ClienteF2CRequest request = new ClienteF2CRequest();
                request.id_Sitio = id_Servicio;
                //request.id_SitioSpecified = true;
                request.vchClaveSitio = vchClaveSitio;
                request.Token = Security.Encrypt(id_Servicio + "|" + vchClaveSitio);
                request.tipoServicio = 3;
                //request.tipoServicioSpecified = true;
                NapoleonDA.setService(request);
            }
            catch (Exception eSS)
            {
                Log.EscribeLog("Existe un error en setService: " + eSS.Message);
                //throw eSS;
            }
        }
    }
}
