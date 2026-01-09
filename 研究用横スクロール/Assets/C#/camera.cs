using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] Transform target;   // 追従対象
    [SerializeField] float fixedY = 0f;   // カメラのY座標（固定）

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = new Vector3(
            target.position.x,
            fixedY,
            transform.position.z
        );
    }
}
