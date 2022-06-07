using BookStoreAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStoreAPI.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignupAsync(SignupModel requestModel);
            Task<string> LoginAsync(SignInModel signInModel);
    }
}
