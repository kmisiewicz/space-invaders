using System.Collections;
using System.Collections.Generic;
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