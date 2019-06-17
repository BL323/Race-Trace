using System;
using System.Linq;
using Core.Domain;

namespace RaceTrace.Tests.Domain.Generators
{
    public class DriverCodeGenerator
    {
        private const string ValidCodeCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly Random Random = new Random();

        internal DriverCode Generate()
        {
            return new DriverCode(RandomCharacters(3, ValidCodeCharacters));
        }

        private static string RandomCharacters(int length, string characters)
        {
            var codeCharacters = Enumerable.Range(1, length)
                .Select(x => characters[Random.Next(characters.Length)])
                .ToArray();

            return new string(codeCharacters);
        }
    }
}
