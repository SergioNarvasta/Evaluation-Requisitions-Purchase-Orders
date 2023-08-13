using HDProjectWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace HDProjectWeb.Services
{
    public class UsuarioStore : IUserStore<_Login>, IUserEmailStore<_Login>, IUserPasswordStore<_Login>
    {
        private readonly IRepositorioUsuario repositorioUsuario;

        public UsuarioStore(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario;
        }

        public async Task<IdentityResult> CreateAsync(_Login user, CancellationToken cancellationToken)
        {
            user.Id = await repositorioUsuario.CrearUsuario(user);
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(_Login user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {            
        }

        public Task<_Login> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(); //Buscar por Email
        }

        public Task<_Login> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<_Login> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(_Login user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();//Obtener usuario por email
        }

        public Task<bool> GetEmailConfirmedAsync(_Login user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(_Login user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(_Login user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(_Login user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Token);
        }

        public Task<string> GetUserIdAsync(_Login user, CancellationToken cancellationToken)
        {
           return Task.FromResult(user.Id.ToString());  
        }

        public Task<string> GetUserNameAsync(_Login user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Nombre);
        }

        public Task<bool> HasPasswordAsync(_Login user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(_Login user, string email, CancellationToken cancellationToken)
        {    
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(_Login user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(_Login user, string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(_Login user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(_Login user, string passwordHash, CancellationToken cancellationToken)
        {
           user.Token=passwordHash;
           return Task.CompletedTask;  
        }

        public Task SetUserNameAsync(_Login user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(_Login user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
