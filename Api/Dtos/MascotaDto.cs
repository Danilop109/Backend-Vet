namespace API.Dtos;
public class MascotaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public DateOnly FechaNacimiento { get; set; }
    public int IdPropietarioFk { get; set; }
    public int IdEspecieFk { get; set; }
    public int IdRazaFk { get; set; }
}
