﻿using AuthAPI.Services.Models;

namespace AuthAPI.Services
{
    public interface IAuthService
    {
        ServiceResult<AuthenticatedUserModel> Login (LoginModel loginModel);
    }
}
