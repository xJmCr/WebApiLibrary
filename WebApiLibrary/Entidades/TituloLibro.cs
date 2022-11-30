namespace WebApiLibrary.Entidades
{
    public class TituloLibro
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public List<Libro> libros { get; set; }
    }
}
