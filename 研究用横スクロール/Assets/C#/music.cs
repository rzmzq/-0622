using UnityEngine;

public class RandomPitchBGM : MonoBehaviour
{
    [Header("再生するBGM")]
    public AudioClip bgm;

    [Header("ピッチの最小値")]
    public float minPitch = 0.9f;

    [Header("ピッチの最大値")]
    public float maxPitch = 1.1f;

    [Header("ピッチを変える間隔（秒）")]
    public float changeInterval = 3f;

    AudioSource audioSource;
    float timer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.Play();

        ChangePitch();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f;
            ChangePitch();
        }
    }

    void ChangePitch()
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
    }
}
