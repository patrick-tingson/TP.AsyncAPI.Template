public class  DataModel 
{
    public string? Id { get; set; }
    public string? Message { get; set; }
    public string? CorrelationId { get; set; }
    public DateTimeOffset? DateTimeRequest { get; set; }
    public DateTimeOffset? DateTimeResponse { get; set; }
}
