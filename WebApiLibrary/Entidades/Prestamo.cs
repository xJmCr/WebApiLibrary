namespace WebApiLibrary.Entidades
{
    public class Prestamo
    {
        public int Id { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaCompromiso { get; set; }
        public DateTime FechaEntrega { get; set; }


        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int LibroId { get; set; }
        public Libro Libro { get; set; }
        public List<Deuda> Deudas { get; set; }
    }
}
