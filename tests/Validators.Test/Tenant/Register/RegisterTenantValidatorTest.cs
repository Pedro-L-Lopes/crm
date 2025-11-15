using CommonTestUtilities.Requests;
using CRM.Application.UseCases.Tenant.Register;
using Shouldly;

namespace Validators.Test.Tenant.Register;

public class RegisterTenantValidatorTest
{
    [Fact(DisplayName = "Deve validar corretamente quando todos os campos estão preenchidos")]
    public void Success()
    {
        // Arrange
        var validator = new RegisterTenantValidator();
        var request = RequestRegisterTenantJsonBuilder.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeTrue("todos os campos obrigatórios foram preenchidos corretamente");

        request.Name.ShouldNotBeNullOrWhiteSpace("o nome do tenant deve estar preenchido");
        request.Email.ShouldNotBeNullOrWhiteSpace("o e-mail deve estar preenchido");
        request.Phone.ShouldNotBeNullOrWhiteSpace("o telefone deve estar preenchido");
        request.Type.ShouldNotBeNullOrWhiteSpace("o tipo deve estar definido");
        request.PlanId.ShouldNotBe(Guid.Empty, "o plano deve ser informado");
        request.PlanExpiration.ShouldBeGreaterThan(DateTime.UtcNow, "a data de expiração deve ser futura");
    }

    [Fact(DisplayName = "Deve retornar erro quando algum campo obrigatório está inválido ou vazio")]
    public void Error()
    {
        // Arrange
        var validator = new RegisterTenantValidator();
        var request = RequestRegisterTenantJsonBuilder.Build();

        // Tornando os campos inválidos
        request.Name = string.Empty;
        request.Email = "email-invalido";
        request.Phone = "";
        request.Type = "";
        request.PlanId = Guid.Empty;
        request.PlanExpiration = DateTime.UtcNow.AddDays(-10); // data no passado

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse("existem campos obrigatórios vazios ou inválidos");

        result.Errors.ShouldContain(e => e.PropertyName == "Name");
        result.Errors.ShouldContain(e => e.PropertyName == "Email");
        result.Errors.ShouldContain(e => e.PropertyName == "Phone");
        result.Errors.ShouldContain(e => e.PropertyName == "Type");
        result.Errors.ShouldContain(e => e.PropertyName == "PlanId");
        result.Errors.ShouldContain(e => e.PropertyName == "PlanExpiration");
    }
}
