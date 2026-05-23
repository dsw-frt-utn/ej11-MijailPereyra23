using Dsw2026Ej11.Domain;
using System.Collections.ObjectModel;

namespace Dsw2026Ej11.Collections
{
    public class CasoDictionary
    {
        private readonly Dictionary<int, Alumno> _alumnos = new();

        public CasoDictionary() { }

        public bool AgregarAlumno(Alumno alumno)
        {
            if (alumno == null) throw new ArgumentNullException(nameof(alumno));
            if (_alumnos.ContainsKey(alumno.Legajo)) return false;
            _alumnos.Add(alumno.Legajo, alumno);
            return true;
        }

        public Alumno? BuscarPorLegajo(int legajo)
        {
            _alumnos.TryGetValue(legajo, out var alumno);
            return alumno;
        }

        public IReadOnlyDictionary<int, Alumno> ObtenerDiccionario()
        {
            return new ReadOnlyDictionary<int, Alumno>(_alumnos);
        }

        public bool EliminarPorLegajo(int legajo)
        {
            return _alumnos.Remove(legajo);
        }
    }
}
