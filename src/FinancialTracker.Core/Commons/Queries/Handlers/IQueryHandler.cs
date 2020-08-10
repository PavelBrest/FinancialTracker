namespace FinancialTracker.Core.Commons.Queries.Handlers
{
    public interface IQueryHandler<TReturn, in TQuery> 
        where TQuery : IQuery<TReturn>
    {
        Result<TReturn> Execute(TQuery query);
    }
}
