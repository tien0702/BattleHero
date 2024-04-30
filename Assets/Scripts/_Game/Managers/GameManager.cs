using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class GameManager : MonoBehaviour, IGameService
{
    public const string JoyMoveId = "Move";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    static void BeforeSceneLoad()
    {
        ServiceLocator.Initialize();
        DataService dataService = new DataService();
        ServiceLocator.Current.Register(dataService);

        var globalData = new GlobalData();
        ServiceLocator.Current.Register(globalData);

        var playerButton = GameObject.FindAnyObjectByType<PlayerButtonController>();
        ServiceLocator.Current.Register(playerButton);

        var gameManager = GameObject.FindAnyObjectByType<GameManager>();
        ServiceLocator.Current.Register(gameManager);

        Application.targetFrameRate = 1000;
    }

    private void Awake()
    {

    }
}
