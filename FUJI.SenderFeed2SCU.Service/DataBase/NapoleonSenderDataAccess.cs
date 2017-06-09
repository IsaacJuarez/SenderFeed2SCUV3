using FUJI.SenderFeed2SCU.Service.Entidades;
using FUJI.SenderFeed2SCU.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUJI.SenderFeed2SCU.Service.DataBase
{
    public class NapoleonSenderDataAccess
    {
        public static NAPOLEONEntities NapoleonDA;

        public List<clsEstudio> getEstudiosEnviar(int id_Sitio)
        {
            Log.EscribeLog("Leyendo del Sitio: " + id_Sitio.ToString());
            List<clsEstudio> lstEst = new List<clsEstudio>();
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    var query = (from item in NapoleonDA.stp_getEstudiosEnviar(id_Sitio)
                                 select item).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (var est in query)
                            {
                                clsEstudio mdl = new clsEstudio();
                                mdl.intDetEstudioID = est.intDetEstudioID;
                                mdl.bitUrgente = est.bitUrgente;
                                mdl.datFecha = (DateTime)est.datFecha;
                                mdl.intEstatusID = (int)est.intEstatusID;
                                mdl.intEstudioID = (int)est.intEstudioID;
                                mdl.intModalidadID = est.intSecuencia;
                                mdl.URGENTES = est.URGENTES;
                                mdl.vchPathFile = est.vchPathFile;
                                lstEst.Add(mdl);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.EscribeLog("Existe un error en getEstudiosEnviar: " + e.Message);
            }
            Log.EscribeLog("Archivos a enviar: " + lstEst.Count.ToString());
            return lstEst;
        }

        public bool updateEstatus(int intDetEstudioID)
        {
            bool valido = false;
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    tbl_DET_Estudio det = NapoleonDA.tbl_DET_Estudio.First(x => x.intDetEstudioID == intDetEstudioID);
                    det.intEstatusID = 3;
                    NapoleonDA.SaveChanges();
                }
            }
            catch (Exception eup)
            {
                Log.EscribeLog("Existe un error en updateEstatus:" + eup.Message);
            }
            return valido;
        }

        public static void setService(int id_Servicio, string vchClaveSitio)
        {
            try
            {
                tbl_DET_ServicioSitio mdl = new tbl_DET_ServicioSitio();

                if (id_Servicio > 0)
                {
                    using (NapoleonDA = new NAPOLEONEntities())
                    {
                        if (NapoleonDA.tbl_DET_ServicioSitio.Any(x => x.id_Sitio == id_Servicio))
                        {
                            using (NapoleonDA = new NAPOLEONEntities())
                            {
                                mdl = NapoleonDA.tbl_DET_ServicioSitio.First(x => x.id_Sitio == id_Servicio);
                                mdl.datFechaSCU = DateTime.Now;
                                NapoleonDA.SaveChanges();
                            }
                        }
                        else
                        {
                            using (NapoleonDA = new NAPOLEONEntities())
                            {
                                mdl.id_Sitio = id_Servicio;
                                mdl.datFechaSCU = DateTime.Now;
                                NapoleonDA.tbl_DET_ServicioSitio.Add(mdl);
                                NapoleonDA.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    if (vchClaveSitio != "")
                    {
                        using (NapoleonDA = new NAPOLEONEntities())
                        {
                            tbl_ConfigSitio mdlSitio = new tbl_ConfigSitio();
                            if (NapoleonDA.tbl_ConfigSitio.Any(x => x.vchClaveSitio == vchClaveSitio))
                            {
                                mdlSitio = NapoleonDA.tbl_ConfigSitio.First(x => x.vchClaveSitio == vchClaveSitio);
                                mdl = NapoleonDA.tbl_DET_ServicioSitio.First(x => x.id_Sitio == mdlSitio.id_Sitio);
                                mdl.datFechaSCU = DateTime.Now;
                                NapoleonDA.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception eSS)
            {
                Log.EscribeLog("Existe un error en setService: " + eSS.Message);
                //throw eSS;
            }
        }
    }
}
