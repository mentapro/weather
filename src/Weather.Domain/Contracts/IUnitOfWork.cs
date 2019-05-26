using System;
using System.Threading;
using System.Threading.Tasks;

namespace Weather.Domain.Contracts
{
	public interface IUnitOfWork : IDisposable
	{
		Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
	}
}