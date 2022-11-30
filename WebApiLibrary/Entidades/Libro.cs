using System.Runtime.CompilerServices;

namespace WebApiLibrary.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        public bool Estatus { get; set; }

        public int TituloLibroId { get; set; }
        public TituloLibro TituloLibro { get; set; }

        public List<Prestamo> Prestamos { get; set; }
    }
}
