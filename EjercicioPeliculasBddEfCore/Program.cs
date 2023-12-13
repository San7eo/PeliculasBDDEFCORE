using EjercicioPeliculasBddEfCore.Domain.Entities;
using EjercicioPeliculasBddEfCore.Repository;
using Microsoft.EntityFrameworkCore;

namespace EjercicioPeliculasBddEfCore
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var options = new DbContextOptionsBuilder<PeliculasBDDContext>();

            string StringConnection = @"Data Source=localhost;Initial Catalog=EJEMPLOCDASQL;Integrated Security=True;Trust Server Certificate=True";

            options.UseSqlServer(StringConnection);

            var context = new PeliculasBDDContext(options.Options);

            //1. listar directores sin peliculas, mostrar nombre de director
            var DirectoresSinPeliculas = context.Directores
                                                            .Where(w => !w.Peliculas.Any())
                                                            .ToList();
            foreach (var director in DirectoresSinPeliculas)
            {
                Console.WriteLine($"El director {director.Nombre} no tienen peliculas ");
            }
            Console.ReadKey();
            Console.WriteLine("\n");

            //2. listar productoras sin peliculas, mostrar nombre de productora
            var ProductorasSinPeliculas = context.Productoras
                                                            .Where(w => !w.Peliculas.Any())
                                                            .GroupBy(gb => gb.Nombre)
                                                            .Select(s => s.First())
                                                            .ToList();
            foreach (var productora in ProductorasSinPeliculas)
            {
                Console.WriteLine($"La productora {productora.Nombre} no tienen peliculas ");
            }
            Console.ReadKey();
            Console.WriteLine("\n");
            //3. listar actores agrupados por sexo, mostrar cantidad de actores para cada sexo

            var ActoresPorSexo = context.Actores
                                                .GroupBy(gb => gb.Sexo)
                                                .Select(grupo => new
                                                {

                                                    Sexo = grupo.Key,
                                                    CantidadActores = grupo.Count()

                                                }).ToList();
            foreach (var sexo in ActoresPorSexo)
            {
                Console.WriteLine($"Sexo: {sexo.Sexo}, Cantidad de Actores: {sexo.CantidadActores}");
            }

            Console.ReadKey();
            Console.WriteLine("\n");
            //4. listar actores sin actuaciones, mostrar nombre del actor
            var ActoresSinActuaciones = context.Actores
                                                        .Where(w => !w.Actuaciones.Any())
                                                        .ToList();
            foreach (var actor in ActoresSinActuaciones)
            {
                Console.WriteLine($"El actor {actor.Nombre} no tienen actuaciones ");
            }
            Console.ReadKey();
            Console.WriteLine("\n");
            //*5. listar cantidad peliculas por productora con recaudacion total > $300.000.000

            var PeliculasPorProductora = context.Peliculas
                                                          .GroupBy(gb => gb.IdProductora)
                                                          .Select(grupo => new
                                                          {
                                                              Idproductora = grupo.Key,
                                                              NombreProductora = grupo.First().IdProductoraNavigation.Nombre,
                                                              CantidadPeliculas = grupo.Count(),
                                                              RecaudacionTotal = grupo.Sum(sum => sum.Recaudacion.Value)
                                                          })
                                                          .Where(w => w.RecaudacionTotal >= 300000000m)
                                                          .ToList();
            foreach (var pelicula in PeliculasPorProductora)
            {
                Console.WriteLine($"La productora {pelicula.NombreProductora} tiene {pelicula.CantidadPeliculas} peliculas sumando una recaudacion de {pelicula.RecaudacionTotal}.");
            }
            Console.ReadKey();
            Console.WriteLine("\n");
            // OPCION CON JOIN
            //var PeliculasPorProductora2 = context.Peliculas
            //                                                .Join(
            //                                                        context.Productoras,
            //                                                        pelicula => pelicula.IdProductora,
            //                                                        productora => productora.IdProductora,
            //                                                        (pelicula, productora) => new { Pelicula = pelicula, Productora = productora }
            //                                                      )
            //                                                .GroupBy(gb => gb.Productora.Nombre)
            //                                                .Select(grupo => new
            //                                                {
            //                                                    NombreProductora = grupo.Key,
            //                                                    CantidadPeliculas = grupo.Count(),
            //                                                    RecaudacionTotal = grupo.Sum(sum => sum.Pelicula.Recaudacion.Value)
            //                                                })
            //                                                .Where(w => w.RecaudacionTotal > 300000000)
            //                                                .ToList();
            //foreach (var pelicula in PeliculasPorProductora2)
            //{
            //    Console.WriteLine($"La productora {pelicula.NombreProductora} tiene {pelicula.CantidadPeliculas} peliculas sumando una recaudacion de {pelicula.RecaudacionTotal}.");
            //}
            //Console.ReadKey();
            //Console.WriteLine("\n");

            /*6. listar los directores con mayor recaudación */

            var DirectoresConMayorRecaudacion = context.Peliculas
                                                                 .Join(
                                                                          context.Directores,
                                                                          pelicula => pelicula.IdDirector,
                                                                          director => director.IdDirector,
                                                                          (pelicula, director) => new {Pelicula = pelicula, Directore = director}

                                                                    )
                                                                 .GroupBy(gb => gb.Directore.Nombre)
                                                                 .Select(grupo => new
                                                                 {
                                                                     NombreDirector = grupo.Key,
                                                                     RecaudacionTotal = grupo.Sum(sum => sum.Pelicula.Recaudacion.Value)
                                                                 })
                                                                 .OrderByDescending(ob => ob.RecaudacionTotal)
                                                                 .ToList();
            foreach (var director in DirectoresConMayorRecaudacion)
            {
                Console.WriteLine($"El director {director.NombreDirector} tiene una recaudación total acumulada de {director.RecaudacionTotal:C}.");
            }

            Console.ReadKey();
            Console.WriteLine("\n");

            /*7. listar los directores con mayor recaudación en los años 80*/

            var DirectoresConMayorRecaudacionEn80 = context.Peliculas
                                                     .Where(pelicula => pelicula.AñoEstreno >= 1980 && pelicula.AñoEstreno <= 1989)
                                                     .Join(
                                                              context.Directores,
                                                              pelicula => pelicula.IdDirector,
                                                              director => director.IdDirector,
                                                              (pelicula, director) => new { Pelicula = pelicula, Directore = director }

                                                        )
                                                     .GroupBy(gb => gb.Directore.Nombre)
                                                     .Select(grupo => new
                                                     {
                                                         NombreDirector = grupo.Key,
                                                         RecaudacionTotal = grupo.Sum(sum => sum.Pelicula.Recaudacion.Value)
                                                     })
                                                     .OrderByDescending(ob => ob.RecaudacionTotal)
                                                     .ToList();
            foreach (var director in DirectoresConMayorRecaudacionEn80)
            {
                Console.WriteLine($"El director {director.NombreDirector} tiene una recaudación total acumulada de {director.RecaudacionTotal:C}.");
            }

            Console.ReadKey();
            Console.WriteLine("\n");

            /*8. listar las 10 peliculas con mayor ranking*/

            var PeliculaConMayorRanking = context.Peliculas
                                                           .OrderByDescending(ob => ob.Rankings.Max(ranking => ranking.Ranking1))
                                                           .Take(10)
                                                           .Select(s => new
                                                           {
                                                               TituloPelicula = s.Titulo,
                                                               Ranking = s.Rankings.Max(ranking => ranking.Ranking1)
                                                           })
                                                           .ToList();
            Console.WriteLine("Las 10 películas con mayor ranking son:\n");
            foreach (var pelicula in PeliculaConMayorRanking)
            {
                Console.WriteLine($"Título: {pelicula.TituloPelicula}, Ranking: {pelicula.Ranking}");
            }
 
            Console.ReadKey();

            Console.WriteLine("\n");

            /*9. listar los directores con 2 o mas peliculas con ranking = 5*/

            var DirectoresConRanking = context.Directores
                                                             .Where(director => director.Peliculas
                                                             .Count(pelicula => pelicula.Rankings.Any(ranking => ranking.Ranking1 == 5)) >= 2)
                                                             .ToList();
            Console.WriteLine("Directores con 2 o más películas con ranking = 5:\n");
            foreach (var director in DirectoresConRanking)
            {
                Console.WriteLine($"Director: {director.Nombre}");
            }

            Console.ReadKey();

            Console.WriteLine("\n");

            /*10. listar las productoras con su promedio de rankings de peliculas*/

            // Recuperar datos de la base de datos
            var datosDesdeDB = context.Productoras
                .Select(productora => new
                {
                    NombreProductora = productora.Nombre,
                    Rankings = productora.Peliculas
                        .SelectMany(pelicula => pelicula.Rankings)
                        .Where(ranking => ranking.Ranking1.HasValue)
                        .Select(ranking => ranking.Ranking1.Value)
                })
                .ToList();

            // Calcular promedio en memoria
            var productorasConPromedioGeneral = datosDesdeDB
                .GroupBy(productora => productora.NombreProductora)
                .Select(grupo => new
                {
                    NombreProductora = grupo.Key,
                    PromedioGeneral = grupo.SelectMany(productora => productora.Rankings).DefaultIfEmpty(0).Average()
                })
                .ToList();

            Console.WriteLine("Promedio general de rankings por productora:\n");
            foreach (var produ in productorasConPromedioGeneral)
            {
                Console.WriteLine($"Productora: {produ.NombreProductora}, Promedio General de Rankings: {produ.PromedioGeneral:F2}");
            }
            Console.ReadKey();

            Console.WriteLine("\n");






        }

    }
}
