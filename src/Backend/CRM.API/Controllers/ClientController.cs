using Azure.Core;
using CRM.API.Attributes;
using CRM.Application.UseCases.Client.Delete;
using CRM.Application.UseCases.Client.GetClient;
using CRM.Application.UseCases.Client.GetClientsPaged;
using CRM.Application.UseCases.Client.Register;
using CRM.Application.UseCases.Client.UpdateAgent;
using CRM.Application.UseCases.Client.UpdateClient;
using CRM.Application.UseCases.User.Profile;
using CRM.Application.UseCases.User.Register;
using CRM.Communication.Requests;
using CRM.Communication.Requests.Client;
using CRM.Communication.Responses;
using CRM.Communication.Responses.Client;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[AuthenticatedUser]
public class ClientController : CRMBaseController
{
    /// <summary>
    /// Registra um novo cliente
    /// </summary>
    /// <param name="request">Dados do cliente</param>
    /// <param name="useCase">Caso de uso para registrar cliente</param>
    /// <returns>Cliente criado</returns>
    /// <response code="201">Cliente criado com sucesso</response>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseClientJson), StatusCodes.Status201Created)]
    [AuthorizedByRole(Role.superAdmin, Role.owner, Role.admin, Role.agent)]
    public async Task<IActionResult> Register([FromBody] RequestClientJson request, [FromServices] IRegisterClientUseCase useCase)
    {

        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    /// <summary>
    /// Lista clientes com paginação, filtros e ordenação
    /// </summary>
    /// <param name="useCase">Use case injetado</param>
    /// <param name="request">Parâmetros de busca (query string)</param>
    /// <returns>Lista paginada de clientes</returns>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     GET /api/clients?page=1&amp;pageSize=10&amp;tenantId=1&amp;name=João&amp;orderBy=name&amp;descending=false
    /// 
    /// Filtros disponíveis:
    /// - tenantId: ID do tenant
    /// - agentId: ID do agente
    /// - name: Nome do cliente (busca parcial)
    /// - phone: Telefone (busca parcial)
    /// - document: Documento (busca parcial)
    /// - type: Tipo do cliente
    /// - gender: Gênero do cliente
    /// 
    /// Ordenação disponível:
    /// - orderBy: name, phone, document, type, gender, createdAt
    /// - descending: true/false (padrão: false)
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ResponsePagedListJson<ResponseShortClientJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IGetClientsPagedUseCase useCase,
        [FromQuery] RequestClientQuery request)
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }

    /// <summary>
    /// Obtém um cliente pelo ID
    /// </summary>
    /// <param name="useCase">Caso de uso para buscar cliente</param>
    /// <param name="id">ID do cliente</param>
    /// <returns>Dados do cliente</returns>
    /// <response code="200">Cliente encontrado</response>
    /// <response code="404">Cliente não encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseClientJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [AuthenticatedUser]
    public async Task<IActionResult> GetClient([FromServices] IGetClientsUseCase useCase, [FromRoute] Guid id)
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    /// <summary>
    /// Remove um cliente pelo ID
    /// </summary>
    /// <param name="useCase">Caso de uso para deletar cliente</param>
    /// <param name="id">ID do cliente</param>
    /// <response code="204">Cliente removido com sucesso</response>
    /// <response code="404">Cliente não encontrado</response>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
       [FromServices] IDeleteClientUseCase useCase,
       [FromRoute] Guid id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    /// <summary>
    /// Atualiza os dados de um cliente
    /// </summary>
    /// <param name="useCase">Caso de uso para atualizar cliente</param>
    /// <param name="id">ID do cliente</param>
    /// <param name="request">Dados atualizados</param>
    /// <response code="204">Cliente atualizado com sucesso</response>
    /// <response code="404">Cliente não encontrado</response>
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateClientUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] RequestClientJson request)
    {
        await useCase.Execute(request, id);

        return NoContent();
    }


    /// <summary>
    /// Atualiza o agente responsável por um cliente
    /// </summary>
    /// <param name="useCase">Caso de uso para atualizar agente</param>
    /// <param name="id">ID do cliente</param>
    /// <param name="request">Dados do agente</param>
    /// <response code="204">Agente atualizado com sucesso</response>
    /// <response code="404">Cliente não encontrado</response>
    [HttpPut]
    [Route("agent/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAgent(
        [FromServices] IUpdateClientAgentUseCase useCase,
        [FromRoute] string id,
        [FromBody] RequestClientUpdateAgentJson request)
    {
        await useCase.Execute(request, id);

        return NoContent();
    }
}
