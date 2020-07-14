using System;

namespace FinancialTracker.Core.Commons
{
    public class DecoratorAttribute : Attribute
    {
        public Type DecoratorType { get; }

        public DecoratorAttribute(Type decoratorType)
        {
            DecoratorType = decoratorType ?? throw new ArgumentNullException(nameof(decoratorType));
        }
    }
}