using UnityEngine;

public class RisingFloor : MonoBehaviour
{
    [Header("è„è∏ë¨ìxÅi1ïbÇ†ÇΩÇËÅj")]
    public float riseSpeed = 1.0f;

    void Update()
    {
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;
    }
}
