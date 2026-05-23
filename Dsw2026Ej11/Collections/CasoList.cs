using Dsw2026Ej11.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dsw2026Ej11.Collections
{
    public class CasoList
    {
        private readonly List<Alumno> _alumnos = new();
        public void AgregarAlumno(Alumno alumno)
        {
            if (alumno == null) throw new ArgumentNullException(nameof(alumno));
            _alumnos.Add(alumno);
        }
        public List<Alumno> ObtenerAlumnos()
        {
            return new List<Alumno>(_alumnos);
        }
        public Alumno? BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return null;
            return _alumnos.FirstOrDefault(a =>
                string.Equals(a.Nombre, nombre, StringComparison.OrdinalIgnoreCase));
        }
        public bool EliminarAlumno(Alumno alumno)
        {
            if (alumno == null) return false;
            return _alumnos.Remove(alumno);
        }
        public bool EliminarEnPosicion(int index)
        {
            if (index < 0 || index >= _alumnos.Count) return false;
            _alumnos.RemoveAt(index);
            return true;
        }
    }
}
