using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject[] UIToEnable = new GameObject[2];
    [SerializeField] private int[] _pickaxePrice = new int[8];
    [SerializeField] private float[] _pickaxeSpeed = new float[8];
    [SerializeField] private TextMeshProUGUI[] _pickaxePriceText = new TextMeshProUGUI[2];
    [SerializeField] private TextMeshProUGUI[] _pickaxeLevelText = new TextMeshProUGUI[2];
    [SerializeField] private int[] _bootsPrice = new int[8];
    [SerializeField] private TextMeshProUGUI[] _bootsPriceText = new TextMeshProUGUI[2];
    [SerializeField] private TextMeshProUGUI[] _bootsLevelText = new TextMeshProUGUI[2];

    private int _pickaxeLevel = 0;
    private int _bootsLevel = 0;
    private int _indexUI;

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

        Initialize();
    }

    private void Initialize()
    {
        if (_pickaxePriceText[_indexUI] != null) _pickaxePriceText[_indexUI].text = _pickaxePrice[0].ToString();
        if (_pickaxeLevelText[_indexUI] != null) _pickaxeLevelText[_indexUI].text = 0.ToString();
        if (_bootsPriceText[_indexUI] != null) _bootsPriceText[_indexUI].text = _bootsPrice[0].ToString();
        if (_bootsLevelText[_indexUI] != null) _bootsLevelText[_indexUI].text = 0.ToString();
        UIToEnable[_indexUI].SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMoney playerMoney))
        {
            UIToEnable[_indexUI].SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerMoney playerMoney))
        {
            UIToEnable[_indexUI].SetActive(false);
        }
    }

    public void BuyPickaxeUpgrade()
    {
        if (_pickaxeLevel >= _pickaxePrice.Length) return;
        if (PlayerMoney.Money < _pickaxePrice[_pickaxeLevel]) return;

        OnBuy?.Invoke(_pickaxePrice[_pickaxeLevel]);
        OnPickaxeUpgradeBought?.Invoke(_pickaxeSpeed[_pickaxeLevel]);

        _pickaxeLevel++;

        if (_pickaxeLevel < _pickaxePrice.Length) _pickaxePriceText[_indexUI].text = _pickaxePrice[_pickaxeLevel].ToString();
        else _pickaxePriceText[_indexUI].text = "Max";

        _pickaxeLevelText[_indexUI].text = "lvl " + _pickaxeLevel;
    }
    
    public void BuyBootsUpgrade()
    {
        if (_bootsLevel >= _bootsPrice.Length) return;
        if (PlayerMoney.Money < _bootsPrice[_bootsLevel]) return;

        OnBuy?.Invoke(_bootsPrice[_bootsLevel]);
        OnBootsUpgradeBought?.Invoke();

        _bootsLevel++;

        if (_bootsLevel < _bootsPrice.Length) _bootsPriceText[_indexUI].text = _bootsPrice[_bootsLevel].ToString();
        else _bootsPriceText[_indexUI].text = "Max";

        _bootsLevelText[_indexUI].text = "lvl " + _bootsLevel;
    }
}
