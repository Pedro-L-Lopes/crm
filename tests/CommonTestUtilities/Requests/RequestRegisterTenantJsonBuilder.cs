using Bogus;
using CRM.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestRegisterTenantJsonBuilder
{
    public static RequestRegisterTenantJson Build()
    {
        var faker = new Faker();

        return new RequestRegisterTenantJson
        {
            Name = faker.Person.FullName,
            Email = faker.Internet.Email(),
            Type = "agency",
            Phone = faker.Phone.PhoneNumber(),
            IsActive = true,
            PlanExpiration = DateTime.UtcNow.AddDays(30),
            PlanId = Guid.Parse("4ddb302d-b8b4-11f0-b283-00155d1ee4e1"),

        };
    }
}
