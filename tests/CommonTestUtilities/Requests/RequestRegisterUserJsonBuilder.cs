using CRM.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        var faker = new Faker();

        return new RequestRegisterUserJson
        {
            TenantId = Guid.Parse("3da2e7eb-18d0-4e79-ab1c-15e2a4dba86d"),
            Name = faker.Person.FullName,
            Email = faker.Internet.Email(),
            Password = "lapdsadp@&2A",
            ConfirmPassword = "lapdsadp@&2A",
            Role = "agent",
            IsActive = true
        };
    }
}
