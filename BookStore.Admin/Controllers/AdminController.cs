using BookStore.Admin.Entity;
using BookStore.Admin.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        public ResponseEntity response;

        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
            response = new ResponseEntity();
        }

        [HttpPost]
        [Route("Register")]
        public ResponseEntity Register(AdminEntity newAdmin)
        {
            AdminEntity admin = _adminServices.Register(newAdmin);
            if (admin != null)
            {
                response.Data = admin;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
            }
            return response;
        }

        [HttpPost]
        [Route("Login")]
        public ResponseEntity Login(string email, string password)
        {
            string result = _adminServices.Login(email, password);
            if (!result.IsNullOrEmpty())
            {
                response.Data = result;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
            }
            return response;
        }
    }
}
