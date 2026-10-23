using MediatR;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
namespace KooliProjekt.Application.Features.Users
{
    public class GetUserQuery : IRequest<OperationResult<UserDto>>
    {
        public int Id { get; set; }
    }
}
