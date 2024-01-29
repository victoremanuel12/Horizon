using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.Services
{
    public class LoginAdminService : ILoginAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoginAdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<LoginDto>> LoginAdmin(LoginDto adminData)
        {
            try
            {
                Login usuarioAdminEntity = await _unitOfWork.LoginAdminRepository.GetByExpressionAsync(u => u.Username == adminData.Username);
                if (usuarioAdminEntity == null || usuarioAdminEntity.Password != adminData.Password)
                    return new Result<LoginDto> { Success = false, ErrorMessage = "Dados do usuario administrativo não conferem", StatusCode = 404 };
                var usarioAdminDto = _mapper.Map<LoginDto>(usuarioAdminEntity);
                return new Result<LoginDto> { Success = true, Data = usarioAdminDto, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<LoginDto> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };
            }


        }
    }
}
