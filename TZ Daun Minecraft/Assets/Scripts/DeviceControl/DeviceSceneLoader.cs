using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeviceSceneLoader : MonoBehaviour
{
    private void Start()
    {
        switch (YandexSDK.YaSDK.instance.currentPlatform)
        {
            case YandexSDK.Platform.desktop:
                break;
            case YandexSDK.Platform.phone:
                SceneManager.LoadScene(1);
                break;
            default:
                SceneManager.LoadScene(1);
                break;
        }
    }
}
