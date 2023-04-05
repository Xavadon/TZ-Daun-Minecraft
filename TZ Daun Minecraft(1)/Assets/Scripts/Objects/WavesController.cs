using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Threading.Tasks;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    [SerializeField] private DiggingObject[] _firstWave;
    [SerializeField] private DiggingObject[] _secondWave;
    [SerializeField] private BigDiggingObject _bigDiamondsBlock;
    [SerializeField] private TextMeshProUGUI _levelCounterText;

    private DiggingObject[,] objectsArray = new DiggingObject[2,64];
    private bool _firstWaveDigged;
    private bool _secondWaveDigged;
    private int _levelCounter;

    private void OnEnable()
    {
        DiggingObject.OnDig += CheckWave;
    }

    private void OnDisable()
    {
        DiggingObject.OnDig -= CheckWave;
    }

    private void Start()
    {
        Initialize();
        //DigBlocks();
    }

    private void Initialize()
    {
        for (int i = 0; i < _firstWave.Length; i++)
        {
            objectsArray[0, i] = _firstWave[i];
            objectsArray[0, i].CanBeDigged = true;
        }
        
        for (int i = 0; i < _secondWave.Length; i++)
        {
            objectsArray[1, i] = _secondWave[i];
            objectsArray[1, i].CanBeDigged = false;
        }
    }

    private void CheckWave(int waveNumber)
    {
        for (int i = 0; i < objectsArray.GetLength(1); i++)
        {
            if (objectsArray[waveNumber, i].CanBeDigged == true) return;
        }

        if (waveNumber == 0) _firstWaveDigged = true;
        if (waveNumber == 1) _secondWaveDigged = true;
        _levelCounter++;

        SetCurrentWave();
        MoveDiamondsBlockDown();
        UpdateUI();
    }

    private void SetCurrentWave()
    {
        if (_firstWaveDigged)
        {
            _firstWaveDigged = false;

            for (int i = 0; i < objectsArray.GetLength(1); i++)
            {
                objectsArray[1, i].CanBeDigged = true;
            }
        }

        if (_secondWaveDigged)
        {
            _secondWaveDigged = false;

            for (int i = 0; i < objectsArray.GetLength(1); i++)
            {
                objectsArray[0, i].CanBeDigged = true;
            }
        }
    }

    private void MoveDiamondsBlockDown()
    {
        _bigDiamondsBlock.transform.position += Vector3.down;
    }

    private void UpdateUI()
    {
        if (_levelCounterText != null) _levelCounterText.text = "lvl " + _levelCounter;
    }

    private async void DigBlocks()
    {
        for (int i = 0; i < objectsArray.GetLength(0); i++)
        {
            for (int j = 0; j < objectsArray.GetLength(1); j++)
            {
                await Task.Delay((int)(Time.fixedDeltaTime * 100));
                if (objectsArray[i, j].CanBeDigged)
                    objectsArray[i, j].Dig();
            }
        }

        DigBlocks();
    }
}
