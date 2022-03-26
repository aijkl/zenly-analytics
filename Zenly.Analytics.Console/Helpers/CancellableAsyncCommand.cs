using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace Zenly.Analytics.Console.Helpers
{
    public abstract class CancellableAsyncCommand : AsyncCommand
    {
        public abstract Task<int> ExecuteAsync(CommandContext context, CancellationToken cancellation);

        public sealed override async Task<int> ExecuteAsync(CommandContext context)
        {
            using var cancellationSource = new CancellationTokenSource();

            using var sigInt = PosixSignalRegistration.Create(PosixSignal.SIGINT, OnSignal);
            using var sigQuit = PosixSignalRegistration.Create(PosixSignal.SIGQUIT, OnSignal);
            using var sigTerm = PosixSignalRegistration.Create(PosixSignal.SIGTERM, OnSignal);

            var cancellable = ExecuteAsync(context, cancellationSource.Token);
            return await cancellable;

            void OnSignal(PosixSignalContext posixSignalContext)
            {
                posixSignalContext.Cancel = true;
                cancellationSource.Cancel();
            }
        }
    }
}
