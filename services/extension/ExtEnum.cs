using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.services.extension
{
    public static class ExtEnum
    {
        public static string GetCustomDescription(this Enum value)
        {
            var attr = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(EnumCustomAttributesDescription), false) as EnumCustomAttributesDescription[];
            return attr.Length > 0 ? attr[0].Description : "";
        }
    }

    public class EnumCustomAttributesDescription : Attribute
    {
        public EnumCustomAttributesDescription(string description)
        {
            Description = description;
        }
        public string Description { get; set; }
    }
}