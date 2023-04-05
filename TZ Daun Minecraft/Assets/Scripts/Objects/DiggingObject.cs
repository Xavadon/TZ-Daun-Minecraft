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
    [SerializeField] private ParticleSystem _destroyEffectPrefab;

    private AudioSource _audioSource;
    public bool CanBeDigged;

    public static event Action<int> OnDig;

    private void Awake()
    {
        _destroyEffectPrefab = Instantiate(_destroyEffectPrefab, transform.position, Quaternion.identity);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _digSound;
    }

    public void Dig()
    {
        _destroyEffectPrefab.transform.position = transform.position;
        _destroyEffectPrefab.Play();

        transform.position += Vector3.down * 2;
        CanBeDigged = false;
        OnDig?.Invoke(_waveNumber);
        _audioSource.Play();
    }
}
