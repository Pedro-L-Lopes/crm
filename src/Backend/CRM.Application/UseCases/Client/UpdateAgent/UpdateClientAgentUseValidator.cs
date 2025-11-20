using CRM.Communication.Requests.Client;
using CRM.Exceptions;
using FluentValidation;

namespace CRM.Application.UseCases.Client.UpdateAgent;

public class UpdateClientAgentUseValidator : AbstractValidator<RequestClientUpdateAgentJson>
{
    public UpdateClientAgentUseValidator()
    {
        RuleFor(c => c.AgentId)
            .NotEmpty()
            .WithMessage(ResourceMessageException.AGENT_EMPTY);            
    }


}
