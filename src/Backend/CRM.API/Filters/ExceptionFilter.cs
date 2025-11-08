using CRM.Communication.Responses;
using CRM.Exceptions;
using CRM.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace CRM.API.Filters;
public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var request = context.HttpContext.Request;
        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];

        _logger.LogError(context.Exception,
            "Erro ao processar {Method} {Path}. Controller: {Controller}, Action: {Action}, Mensagem: {Message}",
            request.Method,
            request.Path,
            controller,
            action,
            context.Exception.Message);

        if (context.Exception is CRMException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownException(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is InvalidLoginException)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(context.Exception.Message));
        }
        else if (context.Exception is ErrorOnValidationException validationException)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(validationException.ErrorMessages));
        }
        else
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(context.Exception.Message));
        }
    }

    private void ThrowUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

#if DEBUG
        context.Result = new ObjectResult(new ResponseErrorJson(new[] { context.Exception.Message }));
#else
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessageException.UNKNOW_ERROR));
#endif
    }
}

