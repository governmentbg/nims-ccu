namespace Eumis.Public.Common.Captcha
{
    public class ApiCaptchaModel
    {
        public string Captcha { get; set; }

        public string CaptchaGuid { get; set; }

        public bool? CaptchaValid { get; set; }
    }
}
