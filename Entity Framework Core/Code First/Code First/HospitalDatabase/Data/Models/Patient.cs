﻿namespace P01_HospitalDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Patient
    {
        public int PatientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public bool HasInsurance { get; set; }

        public virtual ICollection<PatientMedicament> Prescriptions { get; set; }
            = new HashSet<PatientMedicament>();

        public virtual ICollection<Diagnose> Diagnoses { get; set; }
            = new HashSet<Diagnose>();

        public virtual ICollection<Visitation> Visitations { get; set; }
            = new HashSet<Visitation>();
    }
}
