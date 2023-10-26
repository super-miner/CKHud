using UnityEngine;

namespace CKHud.Common {
    /// <summary>
    /// A system for having nicer console logs.
    /// </summary>
    public class LogSystem {
        public static LogSystem instance = null;
        
        public readonly string modName = "";
        
        /// <summary>
        /// The constructor for the LogSystem class. Use LogSystem.Create instead.
        /// </summary>
        /// <param name="modName">The name of your mod.</param>
        public LogSystem(string modName) {
            this.modName = modName;
        }

        /// <summary>
        /// Creates the LogSystem singleton if none exist.
        /// </summary>
        /// <param name="modName">The name of your mod.</param>
        public static void Create(string modName) {
            instance = instance ?? new LogSystem(modName);
        }

        /// <summary>
        /// Logs a message to the console.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void Log(object message) {
            Debug.Log("[" + modName + "]: " + message);
        }
    }
}