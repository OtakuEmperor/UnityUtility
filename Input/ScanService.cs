using UnityEngine;
using Utilities.Update;

namespace Utilities.Input {

    public enum ScanFlags {
        None = 0,
        Key = 1 << 1,
        JoystickButton = 1 << 2,
        JoystickAxis = 1 << 3,
        MouseAxis = 1 << 4,
    }

    public enum MoveType {
        Right = 0,
        Left = 1,
        Up = 2,
        Down =3
    }

    public struct ScanResult {
        public ScanFlags ScanFlags;
        public KeyCode Key;
        public int Joystick;
        public int JoystickAxis;
        public float JoystickAxisValue;
        public int MouseAxis;
        public object UserData;
    }

    public delegate bool ScanHandler(ScanResult result);

    public interface IScanService {
        float GameTime { get; set; }
        bool IsScanning { get; set; }
        void StartScan(ScanFlags flag, ScanHandler scanHandler);
        void StopScan(ScanFlags flags);
    }

    public class ScanService : ITimer, IScanService {
        /// <summary>
        /// 正在監聽的輸入種類
        /// </summary>
        private ScanFlags m_scanFlags;
        private ScanResult m_scanResult;
        private ScanHandler m_scanHandler;
        private string[] m_rawMouseAxes;
        private object m_scanUserData;
        private IUpdateManager updateManager;

        public float GameTime { get; set; }
        public bool IsScanning { get; set; }

        public const int MAX_MOUSE_AXES = 4;

        public ScanService(IUpdateManager updateManager) {
            m_rawMouseAxes = new string[MAX_MOUSE_AXES];
            m_rawMouseAxes[0] = "right";
            m_rawMouseAxes[1] = "left";
            m_rawMouseAxes[2] = "up";
            m_rawMouseAxes[3] = "down";
            this.updateManager = updateManager;
        }

        void IScanService.StartScan(ScanFlags flag, ScanHandler scanHandler) {
            m_scanFlags |= flag;
            updateManager.RegisterUpdate(this);
            this.m_scanHandler = scanHandler;
            IsScanning = true;
        }

        void IScanService.StopScan(ScanFlags flag) {
            m_scanFlags ^= flag;
            if (m_scanFlags == ScanFlags.None) {
                updateManager.UnRegisterUpdate(this);
                IsScanning = false;
            }
        }

        void ITimer.OnUpdate(float deltaTime) {
            // float timeout = GameTime - m_scanStartTime;
            // if(Input.GetKeyDown(m_cancelScanKey) || timeout >= m_scanTimeout)
            // {
            //     Stop();
            //     return;
            // }

            bool success = false;
            // if (IsScanning && !success && HasFlag(ScanFlags.JoystickAxis)) {
            //     success = ScanJoystickAxis();
            // }
            if (IsScanning && !success && HasFlag(ScanFlags.MouseAxis)) {
                success = ScanMouseAxis();
            }

            IsScanning = IsScanning && !success;
        }

        private bool ScanMouseAxis() {
            for (int i = 0; i < m_rawMouseAxes.Length; i++) {
                if (Mathf.Abs(UnityEngine.Input.GetAxis(m_rawMouseAxes[i])) > 0.0f) {
                    m_scanResult.ScanFlags = ScanFlags.MouseAxis;
                    m_scanResult.Key = KeyCode.None;
                    m_scanResult.Joystick = -1;
                    m_scanResult.JoystickAxis = -1;
                    m_scanResult.JoystickAxisValue = 0.0f;
                    m_scanResult.MouseAxis = i;
                    m_scanResult.UserData = m_scanUserData;
                    if(m_scanHandler(m_scanResult))
                    {
                        m_scanHandler = null;
                        m_scanResult.UserData = null;
                        m_scanFlags = ScanFlags.None;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 判斷是否有在監聽指定輸入種類
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        private bool HasFlag(ScanFlags flag) {
            return ((int)m_scanFlags & (int)flag) != 0;
        }
    }
}
