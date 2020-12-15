using System.Collections.Generic;
using Utilities.Core;
using Utilities.Update;

namespace Utilities.Input {
    public interface IInputManager {
        void StartSequence(Update.IUpdateManager updateManager);
        void RegisterInput(IInputObserver inputObserver);
    }
    public interface IInputObserver {
        void HandleInput(ScanResult result);
    }
    public class InputManager : MonoBehaviourBase, IInputManager {
        private IUpdateManager updateManager;
        private IScanService scanService;
        private List<IInputObserver> handleList;

        void IInputManager.StartSequence(Update.IUpdateManager updateManager) {
            //TODO 跟UpdateManager註冊
            handleList = new List<IInputObserver>();
            this.updateManager = updateManager;
            scanService = new ScanService(updateManager);
            scanService.StartScan(ScanFlags.MouseAxis, ScanHandler);
        }

        void IInputManager.RegisterInput(IInputObserver inputObserver) {
            handleList.Add(inputObserver);
        }

        public bool ScanHandler(ScanResult result) {
            for (int i = 0; i < handleList.Count; i++) {
                handleList[i].HandleInput(result);
            }
            return false;
        }
    }
}
