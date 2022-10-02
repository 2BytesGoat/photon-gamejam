using UnityEngine;

public class Camera : MonoBehaviour {
    private Transform camera_target;

    public Transform target {
        get { return camera_target; }
        set { camera_target = value; }
    }

    void LateUpdate() {
    }
}
