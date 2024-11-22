using Ardalis.Specification.EntityFrameworkCore;
using KT.Domain.SeedWork;

namespace KT.Infrastructure.Data
{
    public class KTRepository<T> : RepositoryBase<T>, IReadOnlyRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        private readonly KTDbContext dbContext;

        public KTRepository(KTDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}