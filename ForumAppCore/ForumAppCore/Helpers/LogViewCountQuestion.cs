using ForumAppCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ForumAppCore.Helpers
{
    public class LogViewCountQuestion : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            //if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            if (context.ActionArguments.ContainsKey("id"))
            {
                Guid id = Guid.Empty;
                id = Guid.Parse(context.ActionArguments["id"].ToString());
                var repo = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
                var question = await repo.QuestionRepository.GetQuestionAsyncDb(id);
                question.ViewCount++;
                await repo.Complete();
                //add this: services.AddScoped<LogViewCountQuestion>(); [ServiceFilter(typeof(LogViewCountQuestion))] dat truoc action get id
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }
        }
    }
}
