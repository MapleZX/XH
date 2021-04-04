using System;
using System.Reflection;
using System.Collections.Generic;

namespace _Deprecated_
{
    public static class AttributeExtension
    {
        public static void InitializationAttribute(this IAttributeCollection collection)
        {
            Type collectionType = collection.GetType();
 
            var binding = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            FieldInfo[] info = collectionType.GetFields(binding);

            var attrType = typeof(CharacterObjectAttribute);
            
            Dictionary<string, object> objectDic = new Dictionary<string, object>();
            collection.Initialization(objectDic);

            foreach (var fo in info)
            {
                bool isCharacterAttribute = Attribute.IsDefined(fo, attrType);
                if (isCharacterAttribute)
                {
                    CharacterObjectAttribute attr = (CharacterObjectAttribute)Attribute.GetCustomAttribute(fo, attrType);
                    var key = attr.parameter;
                    if (objectDic.ContainsKey(key))
                    {
                        var value = objectDic[key];
                        fo.SetValue(collection, value);
                    }
                }
            }
        }
    }  
}