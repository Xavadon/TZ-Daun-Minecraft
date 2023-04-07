using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTest : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _device;

    private void FixedUpdate()
    {
        Debug.Log(YandexSDK.YaSDK.instance.currentPlatform);
        _device.text = YandexSDK.YaSDK.instance.currentPlatform.ToString();
    }
}
