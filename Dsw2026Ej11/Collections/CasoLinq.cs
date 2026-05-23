using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Dsw2026Ej11.Collections
{
    /*
     * Para cada punto crear un método que permita:
     * 1. Obtener el primer libro (GetPrimero)
     * 2. Obtener el último libro (GetUltimo)
     * 3. Obtener la suma de precios (GetTotalPrecios)
     * 4. Obtener el promedio de precios (GetPromedioPrecios)
     * 5. Obtener la lista de libros con Id mayor a 15 (GetListById)
     * 6. Obtener una lista de cada libro con su título y precio en formato moneda (GetLibros) (debe retornar una lista de string)
     * 7. Obtener el libro con el precio más alto (GetMayorPrecio)
     * 8. Obtener el libro con el precio más bajo (GetMenorPrecio)
     * 9. Obtener los libros cuyo precio sea mayor al promedio (GetMayorPromedio)
     * 10. Obtener los libros ordenados por título de forma descendente
     * En todos los casos debe aplicarse LINQ
     */
    public static class CasoLinq
    {
        // 1. Obtener el primer libro
        public static T? GetPrimero<T>(IEnumerable<T> libros)
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            return libros.FirstOrDefault();
        }

        // 2. Obtener el último libro
        public static T? GetUltimo<T>(IEnumerable<T> libros)
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            return libros.LastOrDefault();
        }

        // 3. Obtener la suma de precios
        public static decimal GetTotalPrecios(IEnumerable<dynamic> libros)
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            return libros.Sum(l => (decimal)l.Precio);
        }

        // 4. Obtener el promedio de precios
        public static decimal GetPromedioPrecios(IEnumerable<dynamic> libros)
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            return libros.Any() ? libros.Average(l => (decimal)l.Precio) : 0m;
        }

        // 5. Obtener la lista de libros con Id mayor a 15
        public static List<T> GetListById<T>(IEnumerable<T> libros) where T : class
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            // Se asume que T tiene una propiedad pública int Id
            return libros
                .Where(l =>
                {
                    var prop = l.GetType().GetProperty("Id");
                    if (prop == null) return false;
                    var val = prop.GetValue(l);
                    return val is int i && i > 15;
                })
                .ToList();
        }

        // 6. Obtener una lista de cada libro con su título y precio en formato moneda (retorna List<string>)
        public static List<string> GetLibros(IEnumerable<dynamic> libros, CultureInfo? culture = null)
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            culture ??= CultureInfo.CurrentCulture;
            return libros
                .Select(l => $"{l.Titulo} - {((decimal)l.Precio).ToString("C", culture)}")
                .ToList();
        }

        // 7. Obtener el libro con el precio más alto
        public static T? GetMayorPrecio<T>(IEnumerable<T> libros) where T : class
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            return libros
                .OrderByDescending(l =>
                {
                    var prop = l.GetType().GetProperty("Precio");
                    return prop != null ? Convert.ToDecimal(prop.GetValue(l)) : decimal.MinValue;
                })
                .FirstOrDefault();
        }

        // 8. Obtener el libro con el precio más bajo
        public static T? GetMenorPrecio<T>(IEnumerable<T> libros) where T : class
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            return libros
                .OrderBy(l =>
                {
                    var prop = l.GetType().GetProperty("Precio");
                    return prop != null ? Convert.ToDecimal(prop.GetValue(l)) : decimal.MaxValue;
                })
                .FirstOrDefault();
        }

        // 9. Obtener los libros cuyo precio sea mayor al promedio
        public static List<T> GetMayorPromedio<T>(IEnumerable<T> libros) where T : class
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            var precios = libros
                .Select(l =>
                {
                    var prop = l.GetType().GetProperty("Precio");
                    return prop != null ? Convert.ToDecimal(prop.GetValue(l)) : (decimal?)null;
                })
                .Where(d => d.HasValue)
                .Select(selector: d => d.Value)
                .ToList();

            if (!precios.Any()) return new List<T>();

            var promedio = precios.Average();

            return libros
                .Where(l =>
                {
                    var prop = l.GetType().GetProperty("Precio");
                    return prop != null && Convert.ToDecimal(prop.GetValue(l)) > promedio;
                })
                .ToList();
        }

        // 10. Obtener los libros ordenados por título de forma descendente
        public static List<T> GetOrdenadosDescPorTitulo<T>(IEnumerable<T> libros) where T : class
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            return libros
                .OrderByDescending(l =>
                {
                    var prop = l.GetType().GetProperty("Titulo");
                    return prop != null ? (prop.GetValue(l)?.ToString() ?? string.Empty) : string.Empty;
                })
                .ToList();
        }
    }
}
