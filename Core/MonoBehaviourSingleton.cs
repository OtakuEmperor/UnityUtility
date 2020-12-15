using UnityEngine;

namespace Utilities.Core{
    public class MonoBehaviourSingleton<T> : Utilities.Core.MonoBehaviourBase where T : MonoBehaviourSingleton<T> {
        private static T inst = null;
        private static GameObject instGameObject = null;

        protected virtual void Init() { }

        public virtual string Name {
            get {
                return typeof(T).ToString();
            }
        }

        protected virtual void OnDestroyInst() { }

        public static T Inst {
            get {
                if (inst != null)
                    return inst;

                GameObject obj = GetGameObject(typeof(T).ToString());
                inst = GetComponent(obj);
                inst.Init();

                return inst;
            }
        }

        private static T GetComponent(GameObject obj) {
            if (inst == null) {
                inst = obj.GetComponent<T>();
                if (inst == null) {
                    inst = obj.AddComponent<T>();
                }
            }
            return inst;
        }

        private static GameObject GetGameObject(string objName) {
            if (instGameObject == null) {
                instGameObject = GameObject.Find(objName);
                if (instGameObject == null) {
                    instGameObject = new GameObject(objName);
                    DontDestroyOnLoad(instGameObject);
                }
            }

            return instGameObject;
        }

        public static bool IsExist { get { return (inst != null); } }

        public static void DestroyInst() {
            if (inst == null)
                return;

            GameObject.Destroy(inst.gameObject);

            inst.OnDestroyInst();
            inst = null;
        }
    }
}