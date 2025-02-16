using System;
using Unity.Collections;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private int collisions = 0;
    [SerializeField] private int score = 0;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
        Block block = collision.collider.GetComponent<Block>();
        score += block.Score;
        _audioSource.Play();
        GameManager.instance.SetScore(score);
    }

    private void OnCollisionExit(Collision collision)
    {
        collisions--;
        score -= collision.gameObject.GetComponent<Block>().Score;
        GameManager.instance.SetScore(score);
    }
}
