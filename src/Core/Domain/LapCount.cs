using Dawn;

namespace Core.Domain
{
    /// <summary>
    /// Represents a cumulative race lap count.
    /// </summary>
    public sealed class LapCount
    {
        /// <summary>
        /// Gets the lap count.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="LapCount"/> class.
        /// </summary>
        /// <param name="count">Optionally, the initial lap count, otherwise default = 1.</param>
        public LapCount(int count = 1)
        {
            Count = Guard.Argument(count, nameof(count))
                .Require(x => x >= 1, i => "Lap count must be greater than 0.");
        }

        /// <summary>
        /// Returns the next consecutive lap number.
        /// </summary>
        /// <returns>An instance of the <see cref="LapCount"/> class with count incremented by 1.</returns>
        public LapCount NextLap() => new LapCount(Count + 1);

        /// <summary>
        /// Equates to other objects.
        /// </summary>
        /// <param name="obj">The object to check equality.</param>
        /// <returns><see langword="bool"/> for indicating matched equality.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is LapCount lapCount))
                return false;

            return Count == lapCount.Count;
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
                hash = hash * 23 + Count.GetHashCode();
                return hash;
            }
        }
    }
}
