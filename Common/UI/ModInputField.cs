using UnityEngine;

namespace CKHud.Common.UI {
    /// <summary>
    /// An extension of the RadicalMenuOptionTextInput class from the base game.
    /// </summary>
    public class ModInputField : RadicalMenuOptionTextInput {
        public Vector3 textBoxSize = Vector3.one;

        protected string oldText = "";
        
        /// <summary>
        /// The overridden Awake method.
        /// </summary>
        protected override void Awake() {
            maxWidth = textBoxSize.x;
            
            base.Awake();

            float xOffset = 0.0f;
            if (pugText.style.horizontalAlignment == PugTextStyle.HorizontalAlignment.left) {
                xOffset = textBoxSize.x / 2.0f;
            }
            else if (pugText.style.horizontalAlignment == PugTextStyle.HorizontalAlignment.right) {
                xOffset = -textBoxSize.x / 2.0f;
            }
            
            float yOffset = 0.0f;
            if (pugText.style.verticalAlignment == PugTextStyle.VerticalAlignment.bottom) {
                yOffset = textBoxSize.x / 2.0f;
            }
            else if (pugText.style.verticalAlignment == PugTextStyle.VerticalAlignment.top) {
                yOffset = -textBoxSize.x / 2.0f;
            }
            
            clickCollider.center = new Vector3(xOffset, yOffset, 0.0f);
            clickCollider.size = textBoxSize;

            oldText = pugText.textString;
        }

        /// <summary>
        /// The overridden Update method.
        /// </summary>
        protected override void Update() {
            base.Update();

            if (oldText != pugText.textString) {
                OnTextChange(oldText, ref pugText.textString);
                pugText.Render(true);
                oldText = pugText.textString;
            }
        }

        /// <summary>
        /// A callback type function for when the text in the text box changes.
        /// </summary>
        /// <param name="oldText">The previous text.</param>
        /// <param name="newText">The text after the changes have been applied.</param>
        protected virtual void OnTextChange(string oldText, ref string newText) {
            
        }
    }
}