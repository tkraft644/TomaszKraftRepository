using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MiodyKraftowe.Controllers
{
    public static class Dodatki
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                string[] extensions = { "jpg", "png", "jpeg", "webp" };
                bool result = extensions.Any(x => extension.EndsWith(x));

                if (!result)
                {
                    return new ValidationResult("Dozwolone są tylko pliki .jpg, .jpeg .png lub .webp");
                }
            }

            return ValidationResult.Success;
        }
    }
    public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;
    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }
    protected override ValidationResult IsValid(
    object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
           if (file.Length > _maxFileSize)
            {
                return new ValidationResult(GetErrorMessage());
            }
        }

        return ValidationResult.Success;
    }
    public string GetErrorMessage()
    {
        return $"Maksymalna wielość pliku to { _maxFileSize} bitów.";
    }
}
}

