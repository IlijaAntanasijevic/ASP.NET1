using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Chat
{
    public interface IOpenAIChatQuery : IQuery<OpenAIResponseDto, OpenAIRequestDto>
    {
    }
}
