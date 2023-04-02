using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCoach.Core.Business
{
    public record RetrieveLatestPersonalDataCommand(Guid id): IRequest<Result<PersonalData>>;
}
