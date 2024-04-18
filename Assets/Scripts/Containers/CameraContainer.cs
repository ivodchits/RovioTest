using UnityEngine;

namespace Containers
{
    [CreateAssetMenu(fileName = "CameraContainer", menuName = "Containers/Camera")]
    public class CameraContainer : ScriptableObject
    {
        public CameraController CameraController { get; private set; }

        public void Initialize(CameraController cameraController)
        {
            CameraController = cameraController;
        }
    }
}