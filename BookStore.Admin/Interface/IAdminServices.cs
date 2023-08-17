using BookStore.Admin.Entity;

namespace BookStore.Admin.Interface;

public interface IAdminServices
{
    AdminEntity Register(AdminEntity entity);
    string Login(string email, string password);
}
