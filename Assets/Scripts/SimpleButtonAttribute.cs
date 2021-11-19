using UnityEngine;

namespace KM.Utility
{
    public class SimpleButtonAttribute : PropertyAttribute
    {
        public string MethodName;

        public SimpleButtonAttribute(string method)
        {
            MethodName = method;
        }
    }
}