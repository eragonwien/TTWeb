﻿namespace TTWeb.BusinessLogic.Models.Account
{
    public class LoginTokenModel
    {
        public TokenModel AccessToken { get; set; } = new TokenModel();
        public TokenModel RefreshToken { get; set; } = new TokenModel();

        public LoginTokenModel Reset()
        {
            AccessToken = new TokenModel();
            RefreshToken = new TokenModel();
            return this;
        }
    }
}