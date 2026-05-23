using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Dsw2026Ej11.Collections
{

    public static class CasoLinq
    {
        public static T? GetPrimero<T>(IEnumerable<T> libros)
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            return libros.FirstOrDefault();
        }

        public static T? GetUltimo<T>(IEnumerable<T> libros)
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            return libros.LastOrDefault();
        }

        public static decimal GetTotalPrecios(IEnumerable<dynamic> libros)
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            return libros.Sum(l => (decimal)l.Precio);
        }

        public static decimal GetPromedioPrecios(IEnumerable<dynamic> libros)
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            return libros.Any() ? libros.Average(l => (decimal)l.Precio) : 0m;
        }


        public static List<T> GetListById<T>(IEnumerable<T> libros) where T : class
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
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

        public static List<string> GetLibros(IEnumerable<dynamic> libros, CultureInfo? culture = null)
        {
            if (libros == null) throw new ArgumentNullException(nameof(libros));
            culture ??= CultureInfo.CurrentCulture;
            return libros
                .Select(l => $"{l.Titulo} - {((decimal)l.Precio).ToString("C", culture)}")
                .ToList();
        }

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
