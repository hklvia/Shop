using DAL;
using IBLL;
using MODEL;

namespace BLL
{
    public class LoginBLL : BaseBLL<Admin, LoginDAL>, ILoginBLL
    {
    }
}
