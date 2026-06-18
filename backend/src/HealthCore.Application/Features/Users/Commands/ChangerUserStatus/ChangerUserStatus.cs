using HealthCore.Domain.Enums;
using MediatR;

namespace HealthCore.Application.Features.Users.Commands.ChangeUserStatus;

public record ChangeUserStatusCommand(Guid Id, AccountStatus NewStatus) : IRequest<bool>;
