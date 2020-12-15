using System.Collections.Generic;
using UnityEngine;
using Utilities.Core;

namespace Utilities.Update {
    public interface IUpdateManager {
        void RegisterUpdate(ITimer needUpdateScript);
        void UnRegisterUpdate(ITimer needUpdateScript);
        void RegisterLateUpdate(ILateTimer needLateUpdateScript);
        void UnRegisterLateUpdate(ILateTimer needLateUpdateScript);
    }

    public interface ITimer {
        void OnUpdate(float deltaTime);
    }

    public interface ILateTimer {
        void OnLateUpdate(float deltaTime);
    }

    public class UpdateManager : MonoBehaviourBase, IUpdateManager {
        /// <summary>
        /// 更新list
        /// </summary>
        private List<ITimer> _timerUpdateList = new List<ITimer>();
        /// <summary>
        /// 更新list
        /// </summary>
        private List<ILateTimer> _timerLateUpdateList = new List<ILateTimer>();
        /// <summary>
        /// 註冊Update事件
        /// </summary>
        void IUpdateManager.RegisterUpdate(ITimer needUpdateScript) {
            _timerUpdateList.Add(needUpdateScript);
        }
        /// <summary>
        /// 取消註冊Update事件
        /// </summary>
        void IUpdateManager.UnRegisterUpdate(ITimer needUpdateScript) {
            _timerUpdateList.Remove(needUpdateScript);
        }

        /// <summary>
        /// 註冊LateUpdate事件
        /// </summary>
        void IUpdateManager.RegisterLateUpdate(ILateTimer needUpdateScript) {
            _timerLateUpdateList.Add(needUpdateScript);
        }

        /// <summary>
        /// 取消註冊LateUpdate事件
        /// </summary>
        void IUpdateManager.UnRegisterLateUpdate(ILateTimer needUpdateScript) {
            _timerLateUpdateList.Remove(needUpdateScript);
        }


        private void Update() {
            for (int i = 0; i < _timerUpdateList.Count; i++)
            {
                _timerUpdateList[i].OnUpdate(Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            for (int i = 0; i < _timerUpdateList.Count; i++)
            {
                _timerLateUpdateList[i].OnLateUpdate(Time.deltaTime);
            }
        }
    }
}