using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject UIToEnable;
    [SerializeField] private int[] _pickaxePrice = new int[8];
    [SerializeField] private float[] _pickaxeSpeed = new float[8];
    [SerializeField] private TextMeshProUGUI _pickaxePriceText;
    [SerializeField] private TextMeshProUGUI _pickaxeLevelText;
    [SerializeField] private int[] _bootsPrice = new int[8];
    [SerializeField] private TextMeshProUGUI _bootsPriceText;
    [SerializeField] private TextMeshProUGUI _bootsLevelText;

    private int _pickaxeLevel = 0;
    private int _bootsLevel = 0;

    public static event Action<int> OnBuy;
    public static event Action<float> OnPickaxeUpgradeBought;
    public static event Action OnBootsUpgradeBought;

    private void OnValidate()
    {
        for (int i = 0; i < _pickaxePrice.Length; i++)
        {
            if (_pickaxePrice[i] < 0) _pickaxePrice[i] = 0;
        }
        
        for (int i = 0; i < _bootsPrice.Length; i++)
        {
            if (_bootsPrice[i] < 0) _bootsPrice[i] = 0;
        }
    }

    private void Awake()
    {
        _pickaxePriceText.text = _pickaxePrice[0].ToString();
        _pickaxeLevelText.text = 0.ToString();
        _bootsPriceText.text = _bootsPrice[0].ToString();
        _bootsLevelText.text = 0.ToString();
        UIToEnable.SetActive(false);
        Debug.Log(_pickaxePrice.Length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMoney playerMoney))
        {
            UIToEnable.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerMoney playerMoney))
        {
            UIToEnable.SetActive(false);
        }
    }

    public void BuyPickaxeUpgrade()
    {
        if (_pickaxeLevel >= _pickaxePrice.Length) return;
        if (PlayerMoney.Money < _pickaxePrice[_pickaxeLevel]) return;

        OnBuy?.Invoke(_pickaxePrice[_pickaxeLevel]);
        OnPickaxeUpgradeBought?.Invoke(_pickaxeSpeed[_pickaxeLevel]);

        _pickaxeLevel++;

        if (_pickaxeLevel < _pickaxePrice.Length) _pickaxePriceText.text = _pickaxePrice[_pickaxeLevel].ToString();
        else _pickaxePriceText.text = "Max";

        _pickaxeLevelText.text = "lvl " + _pickaxeLevel;
    }
    
    public void BuyBootsUpgrade()
    {
        if (_bootsLevel >= _bootsPrice.Length) return;
        if (PlayerMoney.Money < _bootsPrice[_bootsLevel]) return;

        OnBuy?.Invoke(_bootsPrice[_bootsLevel]);
        OnBootsUpgradeBought?.Invoke();

        _bootsLevel++;

        if (_bootsLevel < _bootsPrice.Length) _bootsPriceText.text = _bootsPrice[_bootsLevel].ToString();
        else _bootsPriceText.text = "Max";

        _bootsLevelText.text = "lvl " + _bootsLevel;
    }
}
