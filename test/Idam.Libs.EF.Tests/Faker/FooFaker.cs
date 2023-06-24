﻿using Bogus;
using Idam.Libs.EF.Tests.Entities;

namespace Idam.Libs.EF.Tests.Faker;
public class FooFaker : Faker<Foo>
{
    public FooFaker()
    {
        var random = new Randomizer();

        RuleFor(o => o.Name, f => f.Name.FullName());
        if (random.Number() == 1)
        {
            RuleFor(o => o.Description, f => f.Lorem.Sentence());
        }
    }
}
