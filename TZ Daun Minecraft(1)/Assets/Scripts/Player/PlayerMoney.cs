using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;

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

    private void IncreaseMoney(int value)
    {
        Money++;
        UpdateUI();
    }

    private void DecreaseMoney(int value)
    {
        if ((Money - value) > 0)
        {
            Money -= value;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (_moneyText != null) _moneyText.text = Money.ToString();
    }
}
