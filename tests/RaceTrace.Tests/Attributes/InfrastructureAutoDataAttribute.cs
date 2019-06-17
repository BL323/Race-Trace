// Copyright (c) Red Bull Technology Ltd. All rights reserved.

using AutoFixture.Xunit2;

namespace RaceTrace.Tests.Attributes
{
    public class InfrastructureAutoDataAttribute : InlineAutoDataAttribute
    {
        public InfrastructureAutoDataAttribute(params object[] objects)
            : base(new AutoDataAttribute(), objects)
        {
        }
    }
}