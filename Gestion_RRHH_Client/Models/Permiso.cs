namespace Gestion_RRHH_Client.Models
{
    public class Permiso
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Motivo { get; set; }=string.Empty;
        public DateTime Fecha { get; set; }= DateTime.Now;
        public string? Hora { get; set; }
        public int CantidadHoras { get; set; }
        public string Aprobado { get; set; } = "Pendiente";//Si, No
        public string? Observaciones { get; set; }
        public DateTime FechaAprueba { get; set; }=DateTime.Now;
    }
}
