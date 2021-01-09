using System.Reflection;

namespace SebaFixes.utils
{
    public class SFReflect
    {
        public static void Run(string methodName, object instance)
        {
            SFReflect.Run(methodName, instance, new object[] { });
        }
        
        public static void Run(string methodName, object instance, object[] parametersArray)
        {
            instance.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Invoke(instance, parametersArray);
        }
        
        public static T Run<T>(string methodName, object instance, object[] parameters)
        {
            return (T) instance.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Invoke(instance, parameters);
        }
        
        public static T Get<T>(string fieldName, object instance)
        {
            return (T) instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(instance);
        }
        
        public static void Set<T>(string fieldName, object instance, T value)
        {
            instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).SetValue(instance, (object) value);
        }
    }
}