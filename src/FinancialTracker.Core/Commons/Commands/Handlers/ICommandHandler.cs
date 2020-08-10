namespace FinancialTracker.Core.Commons.Commands.Handlers
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Result Execute(TCommand command);
    }
}
