using System;
using System.Web;

namespace BogusStore.Client.Extensions;

public static class ObjectExtensions
{
    public static string AsQueryString(this object obj)
    {
        var properties = from p in obj.GetType().GetProperties()
                         where p.GetValue(obj, null) != null
                         select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

        return string.Join("&", properties.ToArray());
    }
}
