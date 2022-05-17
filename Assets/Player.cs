using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public event Action Dead;

    public AudioSource audioSource;

    public int Score { get; private set; }


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.TryGetComponent<Collectable>(out _))
        {
            Score++;
            audioSource.Play();
        }
        else
        {
            Handheld.Vibrate();
            Destroy(gameObject);
            Dead?.Invoke();
        }
    }
}