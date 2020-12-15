using System.Collections;

namespace Utilities.Core {
    public interface ICoroutineManager {
        void StartCoroutine(IEnumerator enumerator);
    }

    public class CoroutineManager : MonoBehaviourBase, ICoroutineManager {
        void ICoroutineManager.StartCoroutine(IEnumerator enumerator) {
            StartCoroutine(enumerator);
        }
    }
}
