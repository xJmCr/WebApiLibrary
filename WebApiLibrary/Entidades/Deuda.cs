namespace WebApiLibrary.Entidades
{
    public class Deuda
    {
        public int Id { get; set; }
        public float Monto { get; set; }

        public int PrestamoId { get; set; }
        public Prestamo Prestamo { get; set; }
    }
}
