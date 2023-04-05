using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class DiggingObject : MonoBehaviour
{
    [SerializeField] private int _waveNumber;
    [SerializeField] private AudioClip _digSound;

    private AudioSource _audioSource;
    public bool CanBeDigged;

    public static event Action<int> OnDig;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _digSound;
    }

    public void Dig()
    {
        transform.position += Vector3.down * 2;
        CanBeDigged = false;
        OnDig?.Invoke(_waveNumber);
        _audioSource.Play();
    }
}
