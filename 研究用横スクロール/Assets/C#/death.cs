using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [Header("¶‘¶ŠÔi•bj")]
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
