using System;
using FinancialTracker.Core.Commons.Utils;

namespace FinancialTracker.Core.Commons
{
    public abstract class DecoratorAttribute : Attribute
    {
        public Type DecoratorType { get; }

        protected DecoratorAttribute(Type decoratorType)
        {
            Asserts.ThrowIfNull(decoratorType, nameof(decoratorType));
            
            DecoratorType = decoratorType;
        }
    }
}