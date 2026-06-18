using MediatR;

namespace HealthCore.Application.Features.Users.Commands.ChangePassword;

public record ChangePasswordCommand(Guid Id, string NewPassword) : IRequest;

public record ChangePasswordDto(string NewPassword);
