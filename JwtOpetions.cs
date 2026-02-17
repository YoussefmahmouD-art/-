namespace مشروع_قبل_الشغل
{
    public class JwtOpetions
    {
        public string Issure { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SigningKey { get; set; } = string.Empty;
        public int LifeTime { get; set; }
    }

}
