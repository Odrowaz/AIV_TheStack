using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int score;
    private AudioSource _audioSource;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private float H, S, V;

    public int Score
    {
        get => score;
        set => score = value;
    }

    public AudioSource AudioSource
    {
        get => _audioSource;
    }

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        Color.RGBToHSV(_meshRenderer.material.color, out H, out S, out V);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_rigidbody.linearVelocity, Vector3.zero) < 0.1f)
        {
            S = Mathf.Lerp(S, 0.2f, Time.deltaTime * 0.5f);
        }
        else
        {
            S = Mathf.Lerp(S, 1, Time.deltaTime * 10f);
        }

        _meshRenderer.material.color = Color.HSVToRGB(H, S, V);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Plane")
        {
            AudioSource.Play();
        }
    }
}
