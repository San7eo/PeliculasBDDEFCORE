﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EjercicioPeliculasBddEfCore.Domain.Entities
{
    public partial class Productora
    {
        public Productora()
        {
            Peliculas = new HashSet<Pelicula>();
        }

        public int IdProductora { get; set; }
        public string Nombre { get; set; }
        public DateTime? Fundacion { get; set; }

        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}