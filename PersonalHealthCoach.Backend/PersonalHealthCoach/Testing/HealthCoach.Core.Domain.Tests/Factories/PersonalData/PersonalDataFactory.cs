using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCoach.Core.Domain.Tests.Factories
{
    public static class PersonalDataFactory
    {
        public static PersonalData Any() => PersonalData.Create(Guid.NewGuid(),
        PersonalDataConstants.MinimumDateOfBirth,
        70,
        170,
        null,
        null,
        "Slabire",
        null).Value;

    }
}
