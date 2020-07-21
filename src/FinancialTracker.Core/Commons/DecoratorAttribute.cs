using System;

namespace FinancialTracker.Core.Commons
{
    public abstract class DecoratorAttribute : Attribute
    {
        public Type DecoratorType { get; }

        protected DecoratorAttribute(Type decoratorType)
        {
            DecoratorType = decoratorType ?? throw new ArgumentNullException(nameof(decoratorType));
        }
    }
}