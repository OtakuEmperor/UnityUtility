using System.Runtime.CompilerServices;
using UnityEngine;

namespace Utilities.Core {
    public class LogManager {
        public static void Log(string data, [CallerMemberName] string callerName = "") {
#if UNITY_EDITOR
            Debug.Log(string.Format("{0}: {1}", callerName, data));
#endif
        }
    }
}
