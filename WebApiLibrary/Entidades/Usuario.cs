namespace WebApiLibrary.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nombre { get; set; }
        public string Ap_Paterno { get; set; }
        public string Ap_Materno { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public bool Estado { get; set; }


        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }

        public List<Prestamo> Prestamos { get; set; }
    }
}