namespace API.Dtos;
public class DetalleMovimientoDto
{
    public int Id { get; set; }
    public int Cantidad { get; set; }
    public double Precio { get; set; }
    public int IdMedicamentoFk { get; set; }
    public MedicamentoDto Medicamento { get; set; }
    public int IdMovimientoMedicamentoFk { get; set; }
    public MovimientoMedicamentoDto MovimientoMedicamento { get; set; }
}
