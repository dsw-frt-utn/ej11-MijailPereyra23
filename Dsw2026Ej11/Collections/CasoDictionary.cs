using Dsw2026Ej11.Domain;
using System.Collections.ObjectModel;

namespace Dsw2026Ej11.Collections
{
    // Crear un diccionario donde la clave sea el legajo y el valor el alumno
    // Incluir un método para agregar un alumno al diccionario
    // Incluir un método para buscar un alumno utilizando la clave
    // Incluir un método para retornar el diccionario
    // Incluir un método para eliminar un alumno utilizando la clave
    public class CasoDictionary
    {
        private readonly Dictionary<int, Alumno> _alumnos = new();

        public CasoDictionary() { }

        // Agrega un alumno al diccionario. Devuelve true si se agregó, false si el legajo ya existe.
        public bool AgregarAlumno(Alumno alumno)
        {
            if (alumno == null) throw new ArgumentNullException(nameof(alumno));
            if (_alumnos.ContainsKey(alumno.Legajo)) return false;
            _alumnos.Add(alumno.Legajo, alumno);
            return true;
        }

        // Busca un alumno por legajo. Devuelve null si no se encuentra.
        public Alumno? BuscarPorLegajo(int legajo)
        {
            _alumnos.TryGetValue(legajo, out var alumno);
            return alumno;
        }

        // Retorna una vista de solo lectura del diccionario.
        public IReadOnlyDictionary<int, Alumno> ObtenerDiccionario()
        {
            return new ReadOnlyDictionary<int, Alumno>(_alumnos);
        }

        // Elimina un alumno por legajo. Devuelve true si se eliminó, false si no existía.
        public bool EliminarPorLegajo(int legajo)
        {
            return _alumnos.Remove(legajo);
        }
    }
}
