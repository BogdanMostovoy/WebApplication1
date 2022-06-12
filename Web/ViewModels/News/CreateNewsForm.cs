using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace Web.ViewModels.News;

public class CreateNewsForm
{
    [MinLength(1, ErrorMessage = "Длинна не меньше 1-го символов")]
    [MaxLength(512, ErrorMessage = "Длинна не больше 512-ми символов")]
    public string Title { get; set; }
    
    [MinLength(1, ErrorMessage = "Длинна не меньше 1-го символов")]
    [MaxLength(2048, ErrorMessage = "Длинна не больше 2048-ми символов")]
    public string Description { get; set; }
    
    [Required]
    [MaxLength(10, ErrorMessage = "Максимум 10 изображений")]
    [MinLength(1, ErrorMessage = "Минимум одно изображение")]
    public List<IFormFile> Images { get; set; }

    public CreateNewsForm(HttpRequest request)
    {
        var form = request.Form;

        Images = new List<IFormFile>(form.Files);

        foreach (var prop in typeof(CreateNewsForm).GetProperties()) {
            string propName = Dashify(prop.Name);
            var curVal = form[propName];
            if (curVal.Count > 0) {
                prop.SetValue(this, To(curVal[0], prop.PropertyType), null);
            }
        }
    }

    public CreateNewsForm()
    {
        
    }
    
    private object To(IConvertible obj, Type t)
    {
        var u = Nullable.GetUnderlyingType(t);
        if (u != null) 
            return obj == null ? GetDefaultValue(t) : Convert.ChangeType(obj, u);

        return Convert.ChangeType(obj, t);
    }

    private object GetDefaultValue(Type t) =>
        t.GetTypeInfo().IsValueType ? Activator.CreateInstance(t) : null;
    

    private string Dashify(string source)
    {
        var result = "";
        var chars = source.ToCharArray();
        for (int i = 0; i < chars.Length; ++i) {
            var c = chars[i];
            if (i > 0 && char.IsUpper(c)) {
                result += '-';
            }
            result += char.ToLower(c);
        }
        return result;
    }
}