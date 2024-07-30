namespace Gestion_RRHH_Server.Models
{
    public class UsuarioDTO
    {
        public string Nombre { get; set; }=string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public string Seccion { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
