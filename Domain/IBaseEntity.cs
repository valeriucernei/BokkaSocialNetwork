namespace Domain;

public interface IBaseEntity
{
    public byte[]? RowVersion { get; set; }
}