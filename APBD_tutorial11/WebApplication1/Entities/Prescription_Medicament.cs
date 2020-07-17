using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class Prescription_Medicament
    {
        public int IdMedicament { get; set; }

        public int IdPrescription { get; set; }

        public int Dose { get; set; }

        public string Details { get; set; }

        public virtual Medicament Medicament { get; set; }

        public virtual Prescription Prescription { get; set; }
    }
}
