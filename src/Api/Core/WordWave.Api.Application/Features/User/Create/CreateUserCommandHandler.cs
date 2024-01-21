using AutoMapper;
using MediatR;
using WordWave.Api.Application.Interfaces.Repository;
using WordWave.Api.Common;
using WordWave.Api.Common.Events.User;
using WordWave.Api.Common.Infrastructure;
using WordWave.Api.Common.Infrastructure.Exceptions;
using WordWave.Api.Common.ViewModels.RequestModels;

namespace WordWave.Api.Application.Features.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existsUser = await _userRepository.GetSingleAsync(
            i => i.EmailAddress == request.EmailAddress);

        if (existsUser is not null)
            throw new DatabaseValidationException("User already exists!");
        
        var dbUser = _mapper.Map<Domain.Models.User>(request);
        var rows = await _userRepository.AddAsync(dbUser);
        // Email Changed/Created
        if (rows > 0)
        {
            var @event = new UserEmailChangedEvent()
            {
                OldEmailAddress = null,
                NewEmailAddress = dbUser.EmailAddress
            };

            QueueFactory.SendMessageToExchange(exchangeName: WordWaweConstants.UserExchangeName,
                exchangeType: WordWaweConstants.DefaultExchangeType,
                queueName: WordWaweConstants.UserEmailChangedQueueName,
                obj: @event);
        }

        return rows > 0;
        
    }
}