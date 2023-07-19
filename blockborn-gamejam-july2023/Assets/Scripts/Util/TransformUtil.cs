using UnityEngine;

namespace Util {

    public static class TransformUtil {

        public static void DestroyChildren(this Transform transform) {
            while (transform.childCount > 0) {
                Object.Destroy(transform.GetChild(0).gameObject);
                transform.GetChild(0).SetParent(null);
            }
        }

    }

}