using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class GameManager : MonoBehaviour, IGameService
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    static void BeforeSceneLoad()
    {
        ServiceLocator.Initialize();
        DataService dataService = new DataService();
        ServiceLocator.Current.Register<DataService>(dataService);

        var playerButton = GameObject.FindAnyObjectByType<PlayerButtonController>();
        ServiceLocator.Current.Register<PlayerButtonController>(playerButton);
    }

    private void Awake()
    {
        if (ServiceLocator.Current.Get<GameManager>() == null)
        {
            ServiceLocator.Current.Register<GameManager>(this);
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
