using Tcc.Api.Data.VO;
using Tcc.Api.Model;

namespace Tcc.Api.Repository
{
    public interface IUserRepository
    {
        User ValidateCredenctials(UserVO user);

        User ValidateCredenctials(string username);

        bool RevokeToken(string username);

        User RefreshUserInfo(User user);
    }
}
