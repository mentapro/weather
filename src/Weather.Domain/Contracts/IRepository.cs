namespace Weather.Domain.Contracts
{
	public interface IRepository
	{
		IUnitOfWork UnitOfWork { get; }
	}
}