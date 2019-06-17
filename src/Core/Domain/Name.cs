using System.Linq;
using Dawn;

namespace Core.Domain
{
    /// <summary>
    /// Represents the name of a person.
    /// </summary>
    public sealed class Name
    {
        /// <summary>
        /// Gets first name.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Gets surname.
        /// </summary>
        public string Surname { get; }

        /// <summary>
        /// Initialises a new instance of <see cref="Name"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="surname">The surname.</param>
        public Name(string firstName, string surname)
        {
            FirstName = Guard.Argument(firstName, nameof(firstName))
                .NotNull()
                .NotEmpty()
                .NotWhiteSpace()
                .Require(ValidName, s => "First name requires capital letter.");
            Surname = Guard.Argument(surname, nameof(surname))
                .NotNull()
                .NotEmpty()
                .NotWhiteSpace()
                .Require(ValidName, s => "Surname requires capital letter.");
        }

        private bool ValidName(string arg)
        {
            var initialCharacter = arg.First();
            return char.IsUpper(initialCharacter) && char.IsLetter(initialCharacter) && arg.All(char.IsLetter);
        }
    }
}
