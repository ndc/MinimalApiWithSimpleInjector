using ApplicationService.Product;

namespace ApplicationService;

public class LogProductGetHandler<Command, Result>(
    ICommandHandler<Command, Result> original
    ) : ICommandHandler<Command, Result> where Command : ICommand<Result>
{
    public async Task<Result> HandleAsync(Command command)
    {
        var result = await original.HandleAsync(command);
        await Task.Delay(1);
        return result;
    }
}
