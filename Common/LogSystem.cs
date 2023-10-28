using System.Runtime.CompilerServices;
using UnityEngine;

namespace CKHud.Common {
    /// <summary>
    /// A system for having nicer console logs.
    /// </summary>
    public static class LogSystem {
        /// <summary>
        /// Logs a message to the console.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="callerMember">The name of the member calling the function.</param>
        public static void Log(object message, [CallerMemberName] string callerMember = "Unknown") {
            Debug.Log("[" + callerMember + "]: " + message);
        }
    }
}