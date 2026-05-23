using Dsw2026Ej11.Collections;
using System.Reflection;

namespace Dsw2026Ej11.Tests;


internal class Ejemplos
{
    //Agregar 3 alumnos a la lista
    //Listar por consola los alumnos
    //Buscar por nombre un alumno que exista y mostrar por consola
    //Buscar por nombre un alumno que no exista y mostrar por consola el texto "No existe"
    //Eliminar un alumno y listar por consola los alumnos
    //Eliminar el primer elemento de la lista y listar por consola los alumnos
    public static void EjemploList()
    {
        var alumnos = new List<string>();

        // Agregar 3 alumnos
        alumnos.Add("Marta");
        alumnos.Add("Juan");
        alumnos.Add("Lucía");

        // Listar por consola los alumnos
        Console.WriteLine("Alumnos iniciales:");
        foreach (var alumno in alumnos)
            Console.WriteLine(alumno);

        // Buscar por nombre un alumno que exista y mostrar por consola
        var nombreBuscar = "Juan";
        var encontrado = alumnos.Find(a => string.Equals(a, nombreBuscar, StringComparison.OrdinalIgnoreCase));
        Console.WriteLine($"\nBuscar '{nombreBuscar}': {(encontrado != null ? encontrado : "No existe")}");

        // Buscar por nombre un alumno que no exista y mostrar por consola el texto "No existe"
        nombreBuscar = "Pedro";
        encontrado = alumnos.Find(a => string.Equals(a, nombreBuscar, StringComparison.OrdinalIgnoreCase));
        Console.WriteLine($"\nBuscar '{nombreBuscar}': {(encontrado != null ? encontrado : "No existe")}");

        // Eliminar un alumno y listar por consola los alumnos
        alumnos.Remove("Juan");
        Console.WriteLine("\nDespués de eliminar 'Juan':");
        foreach (var alumno in alumnos)
            Console.WriteLine(alumno);

        // Eliminar el primer elemento de la lista y listar por consola los alumnos
        if (alumnos.Count > 0)
            alumnos.RemoveAt(0);

        Console.WriteLine("\nDespués de eliminar el primer elemento:");
        foreach (var alumno in alumnos)
            Console.WriteLine(alumno);
    }

    //Agregar 3 alumnos al diccionario
    //Listar por consola los alumnos
    //Buscar un alumno por clave y mostrar por consola
    //Buscar un alumno por clave, pero que no exista, y mostrar por consola el texto "No existe"
    //Eliminar un alumno por clave y listar por consola los alumnos
    public static void EjemploDictionary()
    {
        //Agregar 3 alumnos al diccionario
        var alumnos = new Dictionary<int, string>
        {
            [101] = "Ana Pérez",
            [102] = "Luis Gómez",
            [103] = "María Fernández"
        };

        //Listar por consola los alumnos
        Console.WriteLine("Alumnos (inicial):");
        foreach (var kvp in alumnos)
        {
            Console.WriteLine($"Clave: {kvp.Key}, Alumno: {kvp.Value}");
        }

        //Buscar un alumno por clave y mostrar por consola
        int claveExistente = 102;
        if (alumnos.TryGetValue(claveExistente, out var alumnoEncontrado))
        {
            Console.WriteLine($"\nBúsqueda: clave {claveExistente} -> {alumnoEncontrado}");
        }
        else
        {
            Console.WriteLine($"\nBúsqueda: clave {claveExistente} -> No existe");
        }

        //Buscar un alumno por clave, pero que no exista, y mostrar por consola el texto "No existe"
        int claveInexistente = 999;
        if (alumnos.TryGetValue(claveInexistente, out var alumnoNo))
        {
            Console.WriteLine($"\nBúsqueda: clave {claveInexistente} -> {alumnoNo}");
        }
        else
        {
            Console.WriteLine($"\nBúsqueda: clave {claveInexistente} -> No existe");
        }

        //Eliminar un alumno por clave y listar por consola los alumnos
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

    //Realizar una llamada a cada método definido en CasoLinq y mostar por consola según corresponda
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
                    // Intentar invocar proveyendo valores por defecto (null para referencias, default(T) para valores)
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
                        // Mostrar la excepción interna si la hay
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
