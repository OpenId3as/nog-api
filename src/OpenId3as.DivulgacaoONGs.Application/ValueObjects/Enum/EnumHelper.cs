using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Application.ValueObjects.Enum
{
    public class EnumHelper<T>
    {
        public static string GetEnumDescription(string value)
        {
            Type type = typeof(T);
            var name = System.Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }

        public static string GetEnumDescriptionByValue(int value)
        {
            Type type = typeof(T);
            var name = System.Enum.GetName(type, value);

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }

        public static string GetEnumIdByDescription(string description)
        {
            Type type = typeof(T);
            var name = System.Enum.GetName(type, description);

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }

        public static List<EnumTextValue> GetListTextValueByEnum()
        {
            var enumTextValue = new List<EnumTextValue>();

            var properties = typeof(T).GetEnumNames();
            foreach (var property in properties)
            {
                enumTextValue.Add(new EnumTextValue()
                {
                    Text = GetEnumDescription(property),
                    Value = Convert.ToString((int)System.Enum.Parse(typeof(T), property))
                });
            }
            return enumTextValue.OrderBy(x => x.Text).ToList();
        }

        public class EnumTextValue
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }
    }
}
