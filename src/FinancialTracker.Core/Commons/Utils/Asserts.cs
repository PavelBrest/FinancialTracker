using System;
using System.Threading;

namespace FinancialTracker.Core.Commons.Utils
{
    public static partial class Asserts
    {
        public static bool IsCancellationRequested(CancellationToken cancellationToken, out Result result)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                result = Result.Ok();
                return false;
            }

            var exception = new OperationCanceledException(cancellationToken);
            result = Result.Failed(exception);
            return true;
        }

        public static void ThrowIfNull<TObject>(TObject @object, string? paramName = null)
            where TObject : class
        {
            if (@object == null) throw new ArgumentNullException(paramName);
        }
    }
}
