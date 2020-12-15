using Utilities.Core;

namespace Utilities.UI {
    public interface IUGIBase{
        void InitUI(ManagerBase manager);
    }
    public abstract class UGUIBase : Utilities.Core.MonoBehaviourBase, IUGIBase{
        //TODO 介面UI繼承

        void IUGIBase.InitUI(ManagerBase manager){
            InitUI(manager);
        }

        protected virtual void InitUI(ManagerBase mamager){
        }

        protected virtual void OnShowUI(){

        }
    }
}
