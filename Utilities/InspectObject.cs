using GlobalLogic.Helper;
using System;
using System.Collections.Generic;

namespace GlobalLogic.Utilities
{
    public class InspectObject
    {
        static ILogger logger = new LogService(typeof(InspectObject));
        /// <summary>
        /// This method shall return the collection of properties belongs to the object that is passed as an parameter.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<PropertyDetail> GetPropertyValues(object obj)
        {
            try
            {
                var propList = new List<PropertyDetail>();
                foreach (var prop in obj.GetType().GetProperties())
                {
                    var value = prop.GetValue(obj, null);

                    if (prop.PropertyType.IsArray)
                    {
                        var arr = value as object[];
                        for (var i = 0; i < arr.Length; i++)
                        {
                            if (arr[i].GetType().IsPrimitive)
                            {
                                propList.Add(new PropertyDetail() { Name = prop.Name + i.ToString(), Value = arr[i].ToString() });
                            }
                            else
                            {
                                var lst = GetPropertyValues(arr[i]);
                                if (lst != null && lst.Count > 0)
                                    propList.AddRange(lst);
                            }
                        }
                    }
                    else
                    {
                        if (prop.PropertyType.IsPrimitive || value.GetType() == typeof(string))
                        {
                            propList.Add(new PropertyDetail() { Name = prop.Name, Value = value.ToString() });
                        }
                        else
                        {
                            var lst = GetPropertyValues(value);
                            if (lst != null && lst.Count > 0)
                                propList.AddRange(lst);
                        }
                    }
                }
                return propList;
            }
            catch (Exception ex)
            {
                logger.Error(String.Format("Error in iterating the properties of object {0}. Exception:{1} StackTrace:{2}",obj.ToString() ,ex.Message, ex.StackTrace));
                throw;
            }
        }

    }
    public class PropertyDetail
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
