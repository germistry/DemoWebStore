namespace WebStore.Domain.Infrastructure
{
    public static class DecimalExtensions
    {
        public static string GetValueAsString(this decimal value) =>
            $"${value:N2}";

        public static int GetValueAsInt(this decimal value) =>
            (int)(value * 100);
    }
}
