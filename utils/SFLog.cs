using System.Security.AccessControl;
using UnityEngine;

namespace SebaFixes.utils
{
    public class SFLog
    {
        private static string prefix = "SF";
        private static bool enabled = true;
        public static void log(string message)
        {
            if (enabled)
            {
                Debug.Log($"[{prefix}]({getCallingClass()}->{getCallingMethod()}) {message}");
            }
        }

        private static string getCallingClass()
        {
            var strace = new System.Diagnostics.StackTrace();
            var frame = strace.GetFrames()[2];
            return frame.GetMethod().DeclaringType.ToString();
        }

        private static string getCallingMethod()
        {
            var strace = new System.Diagnostics.StackTrace();
            var frame = strace.GetFrames()[2];
            return frame.GetMethod().Name;
        }

        public static void disableLogger()
        {
            enabled = false;
        }
    }
}