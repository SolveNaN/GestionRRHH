namespace Gestion_RRHH_Client.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Cedula { get; set; }
        public string? Direccion { get; set; }
        public string? Departamento { get; set; }
        public string? Seccion { get; set; }
        public string? Rol { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
