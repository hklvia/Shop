namespace VMODEL
{
    public class TokenVModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        //public long Expire { get; set; }
        public long TokenExpire { get; set; }
        public long RefreshTokenExpire { get; set; }
    }
}
