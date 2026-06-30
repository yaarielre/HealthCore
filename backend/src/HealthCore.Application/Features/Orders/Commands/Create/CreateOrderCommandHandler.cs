using AutoMapper;
using MediatR;
using HealthCore.Application.Features.Orders.DTOs;
using HealthCore.Domain.Entities;
using HealthCore.Domain.Interfaces;

namespace HealthCore.Application.Features.Orders.Commands.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(request.Dto.PatientId)
            ?? throw new KeyNotFoundException($"Paciente con Id '{request.Dto.PatientId}' no encontrado.");

        var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.Dto.DoctorId)
            ?? throw new KeyNotFoundException($"Doctor con Id '{request.Dto.DoctorId}' no encontrado.");

        var orderType = await _unitOfWork.OrderTypes.GetByIdAsync(request.Dto.OrderTypeId)
            ?? throw new KeyNotFoundException($"Tipo de orden con Id '{request.Dto.OrderTypeId}' no encontrado.");

        var orderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid():N}"[..20];

        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = orderNumber,
            PatientId = request.Dto.PatientId,
            DoctorId = request.Dto.DoctorId,
            MedicalConsultationId = request.Dto.MedicalConsultationId,
            OrderTypeId = request.Dto.OrderTypeId,
            Priority = request.Dto.Priority,
            Notes = request.Dto.Notes
        };

        foreach (var itemDto in request.Dto.Items)
        {
            order.OrderItems.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ItemName = itemDto.ItemName,
                Description = itemDto.Description,
                Quantity = itemDto.Quantity,
                UnitPrice = itemDto.UnitPrice
            });
        }

        await _unitOfWork.Orders.AddAsync(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderDto>(order) with
        {
            PatientName = $"{patient.FirstName} {patient.LastName}",
            DoctorName = $"{doctor.FirstName} {doctor.LastName}",
            OrderTypeName = orderType.Name,
            ItemCount = order.OrderItems.Count
        };
    }
}
