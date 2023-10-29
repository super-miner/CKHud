using UnityEngine;

namespace CKHud.Common {
	public static class Util {
		public static Transform GetChildByName(Transform parent, string name) {
			for (int i = 0; i < parent.childCount; i++) {
				Transform child = parent.GetChild(i);
				if (child.name == name) {
					return child;
				}
			}

			return null;
		}
	}
}