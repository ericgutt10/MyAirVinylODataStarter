using AirVinyContext.Entities;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AirVinylRepository;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}

public class Repository<T>(MyAirVinylCtx dbContext) : RepositoryBase<T>(dbContext), IRepository<T> where T : class
{
}

//public class RepositoryFactory<TEntity> : ContextFactoryRepositoryBaseOfT<TEntitiy, IDbContextFactory<MyAirVinylCtx>>
//      where TEntity : class
//{
//    public RepositoryFactory<TEntity>(IDbContextFactory<MyAirVinylCtx> contextFactory)
//        : base(contextFactory)
//    {
//    }
//}