using MediatR;

namespace HealthCore.Application.Features.Users.Commands.ChangePassword;

public record ChangePasswordCommand(Guid Id, string CurrentPassword, string NewPassword) : IRequest;

public record ChangePasswordDto(string CurrentPassword, string NewPassword);
