using System;
using UnityEngine;

public class DestroyOnFall : MonoBehaviour
{
    [SerializeField] private AudioClip destroySound;

    public AudioClip DestroySound
    {
        get { return destroySound; }
        set { destroySound = value; }
    }

    private AudioSource _audioSource;

    private bool _destroyed = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!_destroyed && transform.position.y < -10)
        {
            ManageDestroy();
        }
    }

    void ManageDestroy()
    {
        _destroyed = true;
        _audioSource.PlayOneShot(destroySound, 0.4f);
        Camera.main.GetComponent<CameraShake>().ShakeDuration = 0.1f;
        Destroy(gameObject, destroySound.length);
    }
}
