namespace Gestion_RRHH_Server.Models
{
    public class Permiso
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? Motivo { get; set; }
        public DateTime Fecha { get; set; }
        public string? Hora { get; set; }
        public int CantidadHoras { get; set; }
        public string? Aprobado { get; set; }//Si, No
        public string? Observaciones { get; set; }
        public DateTime FechaAprueba { get; set; }
    }
}
