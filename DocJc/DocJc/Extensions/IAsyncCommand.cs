namespace DocJc.Extensions
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    public interface IAsyncCommand<TResult> : ICommand
    {
        Task<TResult> ExecuteAsync(object parameter);
    }
}
