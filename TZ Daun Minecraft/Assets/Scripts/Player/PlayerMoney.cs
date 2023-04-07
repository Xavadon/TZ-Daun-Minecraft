using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _moneyText = new TextMeshProUGUI[2];

    private int _indexUI;

    public static int Money { get; private set; }

    private void OnEnable()
    {
        DiggingObject.OnDig += IncreaseMoney;
        Shop.OnBuy += DecreaseMoney;
    }

    private void OnDisable()
    {
        DiggingObject.OnDig -= IncreaseMoney;
        Shop.OnBuy -= DecreaseMoney;
    }

    private void Start()
    {
        switch (YandexSDK.YaSDK.instance.currentPlatform)
        {
            case YandexSDK.Platform.desktop:
                _indexUI = 0;
                break;
            case YandexSDK.Platform.phone:
                _indexUI = 1;
                break;
            default:
                _indexUI = 1;
                break;
        }
    }

    private void IncreaseMoney(int value)
    {
        Money++;
        UpdateUI();
    }

    private void DecreaseMoney(int value)
    {
        if ((Money - value) >= 0)
        {
            Debug.Log(value);
            Money -= value;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (_moneyText != null) _moneyText[_indexUI].text = Money.ToString();
    }
}
