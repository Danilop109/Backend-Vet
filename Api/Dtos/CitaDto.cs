namespace API.Dtos;
public class CitaDto
{
    public int Id { get; set; }
    public DateOnly Fecha { get; set; }
    public DateTime Hora { get; set; }
    public string Motivo { get; set; }
    public int IdMascotaFk { get; set; }
    public MascotaDto Mascota { get; set; }
    public int IdVeterinarioFk { get; set; }
    public VeterinarioDto Veterinario { get; set; }
}
