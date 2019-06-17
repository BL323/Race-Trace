using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace RaceTrace.Tests.Attributes
{
    public class DomainAutoDataAttribute : AutoDataAttribute
    {
        public DomainAutoDataAttribute()
            : base(() => new Fixture()
                .Customize(new AutoMoqCustomization()))
        {
        }
    }
}
