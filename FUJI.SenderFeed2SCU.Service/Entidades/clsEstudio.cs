using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUJI.SenderFeed2SCU.Service.Entidades
{
    public class clsEstudio
    {
        public int intDetEstudioID { get; set; }
        public int intEstudioID { get; set; }
        public int intModalidadID { get; set; }
        public string vchPathFile { get; set; }
        public int intEstatusID { get; set; }
        public bool bitUrgente { get; set; }
        public int intSecuencia { get; set; }
        public DateTime datFecha { get; set; }
        public int URGENTES { get; set; }

        public clsEstudio()
        {
            intDetEstudioID = int.MinValue;
            intEstudioID = int.MinValue;
            intModalidadID = int.MinValue;
            vchPathFile = string.Empty;
            intEstatusID = int.MinValue;
            bitUrgente = false;
            intSecuencia = int.MinValue;
            datFecha = DateTime.MinValue;
            URGENTES = int.MinValue;
        }
    }
}
