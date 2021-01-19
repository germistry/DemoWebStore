using System;

namespace WebStore.Domain.Infrastructure
{
    public static class DateTimeExtensions
    {
        public static string GetDateTimeAsString(this DateTime value) =>
            value.ToString("dd/MM/yyyy");

        public static string GetDateTimeAsStringOrNull(this DateTime? value) =>
            value.ToString();
    }
}
