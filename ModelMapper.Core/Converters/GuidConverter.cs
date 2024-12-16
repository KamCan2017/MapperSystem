using ModelMapper.Core.Interfaces;
using NLog;

namespace ModelMapper.Core.Converters
{

    /// <summary>
    /// The Guid converter
    /// </summary>
    public class GuidConverter : BaseConverter, ITypeConverter<string,Guid>
    {
        /// <summary>
        /// Gets the type of the target.
        /// </summary>
        /// <value>
        /// The type of the target.
        /// </value>
        public (Type, Type) SourceTargetTypes => (typeof(string), typeof(Guid)); 

        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <returns>The convertion method</returns>
        public Func<string, Guid> GetMethod()
        {
            Func<object, Guid> func = (input) =>
            {
                if (!Guid.TryParse(input?.ToString(), out Guid result))
                {
                    LogManager.GetCurrentClassLogger().Warn($"Convert {input} to Guid type failed");
                    return Guid.Empty;
                }
                return result;
            };

            return func;
        }

        public override object Invoke(object obj)
        {
            return GetMethod().Invoke(obj?.ToString()!);
        }
    }
}
