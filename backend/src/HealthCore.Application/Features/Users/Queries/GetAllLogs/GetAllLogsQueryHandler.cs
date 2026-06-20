using HealthCore.Application.Features.Users.DTOs;
using HealthCore.Application.Interfaces;
using MediatR;

namespace HealthCore.Application.Features.Users.Queries.GetAllLogs;

public class GetAllLogsQueryHandler : IRequestHandler<GetAllLogsQuery, IEnumerable<LogDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllLogsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<LogDto>> Handle(GetAllLogsQuery request, CancellationToken cancellationToken)
    {
        var logs = await _unitOfWork.Users.GetLogsAsync();

        return logs.Select(l => new LogDto(
            l.Id,
            l.UserId,
            l.User?.FirstName + " " + l.User?.LastName,
            l.Action,
            l.Module,
            l.Details,
            l.IpAddress,
            l.CreatedAt
        ));
    }
}
