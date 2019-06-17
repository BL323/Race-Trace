using System.Linq;
using Dawn;

namespace Core.Domain
{
    /// <summary>
    /// Represents the three character driver code.
    /// </summary>
    public sealed class DriverCode
    {
        /// <summary>
        /// Gets the three character driver code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="DriverCode"/> class.
        /// </summary>
        /// <param name="code">The three character code.</param>
        public DriverCode(string code)
        {
            Code = Guard.Argument(code, nameof(code))
                .NotNull()
                .Length(3)
                .Require(ValidCharacters, s => "Driver code consists of three upper case alphabetical characters.");
        }

        private bool ValidCharacters(string characters)
        {
            return characters.All(x => char.IsLetter(x) && char.IsUpper(x));
        }

        /// <summary>
        /// Equates to other objects.
        /// </summary>
        /// <param name="obj">The object to check equality.</param>
        /// <returns><see langword="bool"/> for indicating matched equality.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is DriverCode driverCode))
                return false;

            return Code == driverCode.Code;
        }

        /// <summary>
        /// Overrides default hash code implementation.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + Code.GetHashCode();
                return hash;
            }
        }
    }
}
