namespace API.Dtos;
public class MovimientoMedicamentoDto
{
    public int Id { get; set; }
    public DateOnly Fecha { get; set; }
    public int IdTipoMovimientoFk { get; set; }
    public TipoMovimientoDto TipoMovimiento { get; set; }
}
