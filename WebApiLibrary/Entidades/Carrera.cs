namespace WebApiLibrary.Entidades
{
    public class Carrera
    {
        public int Id { get; set; }
        public string Nombre_Carrera { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
