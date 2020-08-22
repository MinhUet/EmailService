using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace BVMinh.EmailService.Common.Utilities
{
    public class Utility
    {
        
        public static string GetEntityName<T>()
        {
            return Activator.CreateInstance<T>().GetType().Name;
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static byte[] SerializeToByteArray(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] byteArray) where T : class
        {
            if (byteArray == null)
            {
                return null;
            }
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(byteArray, 0, byteArray.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = (T)binForm.Deserialize(memStream);
                return obj;
            }
        }

        public static void CopyObject<T>(object sourceObject, ref T destObject)
        {
            if (sourceObject == null || destObject == null)
            {
                return;
            }

            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //  Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //  If there is none, skip
                if (targetObj == null)
                    continue;
                //set value
                targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
            }
        }
    }
}
