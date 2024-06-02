﻿using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.LoginUser;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _authService.ComputeSha256Hash(request.Password);

        var user = await _userRepository.GetByEmailAndPasswordAsync(request.Email, passwordHash) 
            ?? throw new Exception("Usuário não encontrado");

        var token = _authService.GenerateJwtToken(user.Email, user.Role);

        return new LoginUserViewModel(user.Email, token);
    }
}
