using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Asset {
    public interface IAssetBundleManager {
        void StartSequence();
        void LoadPrefab(string prefabName);
        GameObject GetPrefab(string prefabName);
        void DebugLog();
    }
    public class AssetBundleManager : IAssetBundleManager {
        Dictionary<string, GameObject> prefabDic;
        //TODO Bundle流程 暫時使用Load

        void IAssetBundleManager.StartSequence() {
            prefabDic = new Dictionary<string, GameObject>();
        }
        void IAssetBundleManager.LoadPrefab(string prefabName) {
            var tmpprefab = Resources.Load<GameObject>(prefabName);
            prefabDic.Add(prefabName, GameObject.Instantiate<GameObject>(tmpprefab, null));
        }
        GameObject IAssetBundleManager.GetPrefab(string prefabName){
            if(prefabDic.ContainsKey(prefabName)){
                return prefabDic[prefabName];
            }
            return default(GameObject);
        }
        void IAssetBundleManager.DebugLog() {
            
        }
    }
}
