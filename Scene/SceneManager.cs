namespace Utilities.Scene {
    public interface ISceneManager {
        void StartSequence();
        void LoadScene(string name);
    }
    public class SceneManager : ISceneManager {
        void ISceneManager.StartSequence() {
        }
        void ISceneManager.LoadScene(string name) {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name);
        }
    }
}