using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Tcc.Api.Configurations;
using Tcc.Api.Data.VO;
using Tcc.Api.Repository;
using Tcc.Api.Services;

namespace Tcc.Api.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _tokenConfiguration;

        private IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration tokenConfiguration, IUserRepository userRepository, ITokenService tokenService)
        {
            _tokenConfiguration = tokenConfiguration;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            //valida senha dada pelo cliente no banco
            var user = _userRepository.ValidateCredenctials(userCredentials);
            if (user == null) return null;

            //gera o token temporario
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };

            var accessToken = _tokenService.GenerateAcessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();


            // setando ao banco o refresh gerado
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_tokenConfiguration.DaysToExpiry);

            //atualizando dados do usuario no repositorio
            _userRepository.RefreshUserInfo(user);

            // tempo de expiração deste token
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);

            // gerando dados que serão passados  para o controller
            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;

            var user = _userRepository.ValidateCredenctials(username);

            if (user == null || user.RefreshToken != refreshToken || 
                user.RefreshTokenExpiryTime <= DateTime.Now)
                return null;

            accessToken = _tokenService.GenerateAcessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _userRepository.RefreshUserInfo(user);

            // tempo de expiração deste token
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);

            // gerando dados que serão passados  para o controller
            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );

        }

        public bool RevokeToken(string userName)
        {
            return _userRepository.RevokeToken(userName);
        }
    }
}
