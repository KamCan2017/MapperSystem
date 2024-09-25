using ModelMapper.Core.Interfaces;
using NLog;

namespace ModelMapper.Core.Converters
{

    /// <summary>
    /// The Guid converter
    /// </summary>
    public class GuidConverter : ITypeConverter
    {
        /// <summary>
        /// Gets the type of the target.
        /// </summary>
        /// <value>
        /// The type of the target.
        /// </value>
        public Type TargetType => typeof(Guid);

        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <returns>The convertion method</returns>
        public Func<object, object> GetMethod()
        {
            Func<object, object> func = (input) =>
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
    }
}
