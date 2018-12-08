using UnityEngine;

namespace StanixGames.Player
{
    [
        RequireComponent(typeof(FOVMesh)),
        RequireComponent(typeof(FieldOfView)),
        RequireComponent(typeof(RotationFollowMouse))
    ]
    public class PlayerFOV : MonoBehaviour
    {
        private RotationFollowMouse mouseRotation;

        private void Start()
        {
            mouseRotation = GetComponent<RotationFollowMouse>();
        }

        private void Update()
        {
            transform.eulerAngles = new Vector3(0, 0, mouseRotation.angle);
        }
    }
}
