﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EjercicioPeliculasBddEfCore.Domain.Entities
{
    public partial class Actore
    {
        public Actore()
        {
            Actuaciones = new HashSet<Actuacione>();
        }

        public int IdActor { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaNac { get; set; }
        public string Sexo { get; set; }

        public virtual ICollection<Actuacione> Actuaciones { get; set; }
    }
}