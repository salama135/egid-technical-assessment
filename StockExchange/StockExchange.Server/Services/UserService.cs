using StockExchange.Server.DAL;
using StockExchange.Server.DTOs;

namespace StockExchange.Server.Services
{
    public interface IUserService
    {
        List<UserDto> GetMembers();
        UserDto GetMembersById();
    }

    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<UserDto> GetMembers()
        {
            return _unitOfWork.UserRepository.GetUsers().Select(u => new UserDto()
            {
                Id = u.Id,
                Name = u.Name, 
            }).ToList();
        }

        public UserDto GetMembersById()
        {
            throw new NotImplementedException();
        }
    }
}
