namespace UnitOfWork.Contract;

public interface IKey <T>
{
    public T Id { get; set; }
}