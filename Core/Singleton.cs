namespace Utilities.Core
{
    public class Singleton<T> where T : Singleton<T>, new()
    {

        private static T _instance = null;

        /// <summary>
        /// 初始化Instance時執行
        /// </summary>
        protected virtual void OnCreateInst() { }

        /// <summary>
        /// 回收Instance時執行
        /// </summary>
        protected virtual void OnDestoryInst() { }

        public static T Instance
        {
            get
            {
                //已有Instance
                if (_instance != null)
                    return _instance;

                //找不到Instance 建立新的Instance
                InitInst();

                return _instance;
            }
        }

        public static void InitInst()
        {
            if (_instance == null)
            {
                _instance = new T();
                _instance.OnCreateInst();
            }
        }

        /// <summary>
        /// Instance 是否存在
        /// </summary>
        /// <value></value>
        public static bool IsExist {
            get {
                 return (_instance != null);
            } 
        }

        public static void DestroyInst()
        {
            if (_instance == null)
                return;

            _instance.OnDestoryInst();

            _instance = null;
        }
    }
}
