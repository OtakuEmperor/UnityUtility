using System.Collections.Generic;

using UnityEngine;

using Utilities.Asset;
using Utilities.Core;

namespace Utilities.UI {
    public interface IUGUIManager {
        void StartSequence(IAssetBundleManager assetBundleManager);
        void ShowUI(string UIName, ManagerBase manager);
    }

    public class UGUIManager : Utilities.Core.MonoBehaviourBase, IUGUIManager {
        private Canvas _canvas;
        private Camera _camera;
        private IAssetBundleManager assetBundleManager;
        private RenderTexture rendertexture;
        private List<GameObject> UIList;

        /// <summary>
        /// 初始化相機
        /// </summary>
        private void InitCamera() {
            float w = Screen.width;
            float h = Screen.height;

            GameObject cameraObjcet = new GameObject("UICamera");
            cameraObjcet.transform.SetParent(transform);
            _camera = cameraObjcet.AddComponent<Camera>();
            _camera.orthographic = true;
            _camera.cullingMask = 1 << (int)ECameraIndex.UI;
            _camera.clearFlags = CameraClearFlags.Depth;
        }

        /// <summary>
        /// 初始化UGUI
        /// </summary>
        private void InitCanvas() {
            GameObject canvasObject = new GameObject("Canvas");
            canvasObject.transform.SetParent(_camera.transform);
            canvasObject.AddComponent<RectTransform>();
            _canvas = canvasObject.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _canvas.worldCamera = _camera;
        }

        private void InitRenderTexture(){
            rendertexture = Resources.Load<RenderTexture>("SpaceRenderTexture");
        }

        void IUGUIManager.StartSequence(IAssetBundleManager assetBundleManager) {
            this.assetBundleManager = assetBundleManager;
            UIList = new List<GameObject>();
            InitCamera();
            InitCanvas();
            InitRenderTexture();
        }

        void IUGUIManager.ShowUI(string UIName, ManagerBase manager) {
            GameObject tmpObject = assetBundleManager.GetPrefab(UIName);
            LayerManager.SetLayerRecursively(tmpObject, (int)ECameraIndex.UI);
            UIList.Add(tmpObject);
            RectTransform tmpRect = tmpObject.GetComponent<RectTransform>();
            tmpObject.transform.SetParent(_canvas.transform);
            tmpObject.transform.localPosition = Vector3.zero;
            tmpObject.transform.localScale = Vector3.one;
            tmpObject.transform.SetAsLastSibling();
            tmpRect.sizeDelta = Vector2.zero;
            tmpObject.GetComponent<IUGIBase>().InitUI(manager);
        }
    }
}
