﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EjercicioPeliculasBddEfCore.Domain.Entities;

namespace EjercicioPeliculasBddEfCore.Repository
{
    public partial class PeliculasBDDContext : DbContext
    {
        public PeliculasBDDContext()
        {
        }

        public PeliculasBDDContext(DbContextOptions<PeliculasBDDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actore> Actores { get; set; }
        public virtual DbSet<Actuacione> Actuaciones { get; set; }
        public virtual DbSet<Directore> Directores { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<Pelicula> Peliculas { get; set; }
        public virtual DbSet<PeliculasGenero> PeliculasGeneros { get; set; }
        public virtual DbSet<Productora> Productoras { get; set; }
        public virtual DbSet<Ranking> Rankings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actore>(entity =>
            {
                entity.HasKey(e => e.IdActor)
                    .HasName("PK__Actores__F86BE71724D08A36");

                entity.Property(e => e.IdActor)
                    .ValueGeneratedNever()
                    .HasColumnName("id_actor");

                entity.Property(e => e.FechaNac)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nac");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre");

                entity.Property(e => e.Sexo)
                    .HasColumnName("sexo");
            });

            modelBuilder.Entity<Actuacione>(entity =>
            {
                entity.HasKey(e => e.IdActuacion)
                    .HasName("PK__Actuacio__9D26834341868FA9");

                entity.Property(e => e.IdActuacion)
                    .ValueGeneratedNever()
                    .HasColumnName("id_actuacion");

                entity.Property(e => e.IdActor).HasColumnName("id_actor");

                entity.Property(e => e.IdPelicula).HasColumnName("id_pelicula");

                entity.Property(e => e.Papel)
                    .HasColumnName("papel");

                entity.HasOne(d => d.IdActorNavigation)
                    .WithMany(p => p.Actuaciones)
                    .HasForeignKey(d => d.IdActor)
                    .HasConstraintName("FK__Actuacion__id_ac__44FF419A");

                entity.HasOne(d => d.IdPeliculaNavigation)
                    .WithMany(p => p.Actuaciones)
                    .HasForeignKey(d => d.IdPelicula)
                    .HasConstraintName("FK__Actuacion__id_pe__45F365D3");
            });

            modelBuilder.Entity<Directore>(entity =>
            {
                entity.HasKey(e => e.IdDirector)
                    .HasName("PK__Director__6B65E2A2C7832B3E");

                entity.Property(e => e.IdDirector)
                    .ValueGeneratedNever()
                    .HasColumnName("id_director");

                entity.Property(e => e.FechaNac)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nac");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.IdGenero)
                    .HasName("PK__Generos__99A8E4F9FA0925D6");

                entity.Property(e => e.IdGenero)
                    .ValueGeneratedNever()
                    .HasColumnName("id_genero");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.HasKey(e => e.IdPelicula)
                    .HasName("PK__Pelicula__B5017F4DF667C10F");

                entity.Property(e => e.IdPelicula)
                    .ValueGeneratedNever()
                    .HasColumnName("id_pelicula");

                entity.Property(e => e.AñoEstreno).HasColumnName("año_estreno");

                entity.Property(e => e.Clasificacion)
                    .HasColumnName("clasificacion");

                entity.Property(e => e.IdDirector).HasColumnName("id_director");

                entity.Property(e => e.IdProductora).HasColumnName("id_productora");

                entity.Property(e => e.Recaudacion)
                    .HasColumnType("decimal(18, 3)")
                    .HasColumnName("recaudacion");

                entity.Property(e => e.Titulo)
                    .HasColumnName("titulo");

                entity.HasOne(d => d.IdDirectorNavigation)
                    .WithMany(p => p.Peliculas)
                    .HasForeignKey(d => d.IdDirector)
                    .HasConstraintName("FK__Peliculas__id_di__46E78A0C");

                entity.HasOne(d => d.IdProductoraNavigation)
                    .WithMany(p => p.Peliculas)
                    .HasForeignKey(d => d.IdProductora)
                    .HasConstraintName("FK__Peliculas__id_pr__47DBAE45");
            });

            modelBuilder.Entity<PeliculasGenero>(entity =>
            {
                entity.HasKey(e => e.IdPeliculaGenero)
                    .HasName("PK__Pelicula__021DFEC065BF7A29");

                entity.ToTable("Peliculas_Generos");

                entity.Property(e => e.IdPeliculaGenero)
                    .ValueGeneratedNever()
                    .HasColumnName("id_pelicula_genero");

                entity.Property(e => e.IdGenero).HasColumnName("id_genero");

                entity.Property(e => e.IdPelicula).HasColumnName("id_pelicula");

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.PeliculasGeneros)
                    .HasForeignKey(d => d.IdGenero)
                    .HasConstraintName("FK__Peliculas__id_ge__48CFD27E");

                entity.HasOne(d => d.IdPeliculaNavigation)
                    .WithMany(p => p.PeliculasGeneros)
                    .HasForeignKey(d => d.IdPelicula)
                    .HasConstraintName("FK__Peliculas__id_pe__49C3F6B7");
            });

            modelBuilder.Entity<Productora>(entity =>
            {
                entity.HasKey(e => e.IdProductora)
                    .HasName("PK__Producto__0E435047725C9227");

                entity.Property(e => e.IdProductora)
                    .ValueGeneratedNever()
                    .HasColumnName("id_productora");

                entity.Property(e => e.Fundacion)
                    .HasColumnType("date")
                    .HasColumnName("fundacion");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Ranking>(entity =>
            {
                entity.HasKey(e => e.IdRanking)
                    .HasName("PK__Rankings__2001139949C059B4");

                entity.Property(e => e.IdRanking)
                    .ValueGeneratedNever()
                    .HasColumnName("id_ranking");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdPelicula).HasColumnName("id_pelicula");

                entity.Property(e => e.Ranking1).HasColumnName("ranking");

                entity.HasOne(d => d.IdPeliculaNavigation)
                    .WithMany(p => p.Rankings)
                    .HasForeignKey(d => d.IdPelicula)
                    .HasConstraintName("FK__Rankings__id_pel__4AB81AF0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}