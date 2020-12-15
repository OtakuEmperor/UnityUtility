using System.Collections;
using UnityEngine;

/// <summary>
/// 管理GameObject層級
/// </summary>
namespace Utilities.Core {
    public static class LayerManager {
        /// <summary>
        /// 遞迴設定物件層級
        /// </summary>
        /// <param name="targetObject"></param>
        /// <param name="layer"></param>
        public static void SetLayerRecursively(GameObject targetObject, int layer) {
            targetObject.layer = layer;
            foreach (Transform trans in targetObject.GetComponentsInChildren<Transform>(true)) {
                trans.gameObject.layer = layer;
            }
        }
    }
}

