using Dsw2026Ej11.Collections;
using System.Reflection;

namespace Dsw2026Ej11.Tests;


internal class Ejemplos
{
    public static void EjemploList()
    {
        var alumnos = new List<string>();

        alumnos.Add("Marta");
        alumnos.Add("Juan");
        alumnos.Add("Lucía");

        Console.WriteLine("Alumnos iniciales:");
        foreach (var alumno in alumnos)
            Console.WriteLine(alumno);

        var nombreBuscar = "Juan";
        var encontrado = alumnos.Find(a => string.Equals(a, nombreBuscar, StringComparison.OrdinalIgnoreCase));
        Console.WriteLine($"\nBuscar '{nombreBuscar}': {(encontrado != null ? encontrado : "No existe")}");

        nombreBuscar = "Pedro";
        encontrado = alumnos.Find(a => string.Equals(a, nombreBuscar, StringComparison.OrdinalIgnoreCase));
        Console.WriteLine($"\nBuscar '{nombreBuscar}': {(encontrado != null ? encontrado : "No existe")}");

        alumnos.Remove("Juan");
        Console.WriteLine("\nDespués de eliminar 'Juan':");
        foreach (var alumno in alumnos)
            Console.WriteLine(alumno);

        if (alumnos.Count > 0)
            alumnos.RemoveAt(0);

        Console.WriteLine("\nDespués de eliminar el primer elemento:");
        foreach (var alumno in alumnos)
            Console.WriteLine(alumno);
    }

    public static void EjemploDictionary()
    {
        var alumnos = new Dictionary<int, string>
        {
            [101] = "Ana Pérez",
            [102] = "Luis Gómez",
            [103] = "María Fernández"
        };

        Console.WriteLine("Alumnos (inicial):");
        foreach (var kvp in alumnos)
        {
            Console.WriteLine($"Clave: {kvp.Key}, Alumno: {kvp.Value}");
        }

        int claveExistente = 102;
        if (alumnos.TryGetValue(claveExistente, out var alumnoEncontrado))
        {
            Console.WriteLine($"\nBúsqueda: clave {claveExistente} -> {alumnoEncontrado}");
        }
        else
        {
            Console.WriteLine($"\nBúsqueda: clave {claveExistente} -> No existe");
        }

        int claveInexistente = 999;
        if (alumnos.TryGetValue(claveInexistente, out var alumnoNo))
        {
            Console.WriteLine($"\nBúsqueda: clave {claveInexistente} -> {alumnoNo}");
        }
        else
        {
            Console.WriteLine($"\nBúsqueda: clave {claveInexistente} -> No existe");
        }

        int claveAEliminar = 101;
        if (alumnos.Remove(claveAEliminar))
        {
            Console.WriteLine($"\nSe eliminó la clave {claveAEliminar}.");
        }
        else
        {
            Console.WriteLine($"\nNo se pudo eliminar la clave {claveAEliminar} (no existe).");
        }

        Console.WriteLine("\nAlumnos (final):");
        foreach (var kvp in alumnos)
        {
            Console.WriteLine($"Clave: {kvp.Key}, Alumno: {kvp.Value}");
        }
    }

    public static void EjemploLinq()
    {
        var tipo = typeof(CasoLinq);
        var metodos = tipo.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.DeclaredOnly)
                          .OrderBy(m => m.Name)
                          .ToArray();

        if (metodos.Length == 0)
        {
            Console.WriteLine("No se encontraron métodos públicos estáticos en CasoLinq.");
            return;
        }

        foreach (var metodo in metodos)
        {
            Console.WriteLine("=== Ejecutando: {0} ===", metodo.Name);
            try
            {
                var parametros = metodo.GetParameters();

                if (parametros.Length == 0)
                {
                    var resultado = metodo.Invoke(null, null);
                    if (metodo.ReturnType != typeof(void))
                        Console.WriteLine("Resultado: {0}", resultado ?? "null");
                }
                else
                {
                    object?[] args = parametros.Select(p =>
                    {
                        var t = p.ParameterType;
                        if (t.IsByRef) t = t.GetElementType()!;
                        if (!t.IsValueType) return null;
                        return Activator.CreateInstance(t);
                    }).ToArray();

                    try
                    {
                        var resultado = metodo.Invoke(null, args);
                        if (metodo.ReturnType != typeof(void))
                            Console.WriteLine("Resultado: {0}", resultado ?? "null");
                    }
                    catch (TargetInvocationException tie)
                    {
                        Console.WriteLine("Error al invocar con valores por defecto: {0}", tie.InnerException?.Message ?? tie.Message);
                        Console.WriteLine("Omitiendo invocación de {0} que requiere parámetros específicos.", metodo.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ejecutar {0}: {1}", metodo.Name, ex.InnerException?.Message ?? ex.Message);
            }

            Console.WriteLine();
        }
    }
}
