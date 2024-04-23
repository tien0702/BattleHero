using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TT;
using UnityEngine;

public class GameSceneService : MonoBehaviour, IGameService
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    static void BeforeSceneLoad()
    {
        var gameSceneManager = GameObject.FindAnyObjectByType<GameSceneService>();
        ServiceLocator.Current.Register(gameSceneManager);

        ServiceLocator.Current.Get<GlobalData>().MapName = "1vs1";
    }

    private void Awake()
    {
        if (PhotonNetwork.IsConnected)
        {
            string mapName = PhotonNetwork.CurrentRoom.CustomProperties["MapName"] as string;
            GameObject mapObject = Resources.Load("Prefabs/Maps/" + mapName) as GameObject;
            Instantiate(mapObject);
            Transform standingObject = mapObject.transform.Find("StandingPositions");
            List<Transform> standingPositions = GameObjectUtilities.GetChildren(standingObject);
        }
        else
        {
            string mapName = ServiceLocator.Current.Get<GlobalData>().MapName;
            GameObject mapObject = Resources.Load("Prefabs/Maps/" + mapName) as GameObject;
            GameObject map = Instantiate(mapObject);
            map.name = "Enviroment";
            Transform standingObject = mapObject.transform.Find("StandingPositions");
            List<Transform> standingPositions = GameObjectUtilities.GetChildren(standingObject);

            SpawnPlayer(standingPositions, 0);
        }
    }

    void SpawnPlayer(List<Transform> standingPositions, int index)
    {
        GameObject hero = new GameObject();
        var player = hero.AddComponent<PlayerController>();
        hero.name = "Player";
        var rb = hero.AddComponent<Rigidbody>();
        rb.mass = 0.5f;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        hero.transform.position = standingPositions[index].position;

        EntityInfo info = new EntityInfo();
        info.Type = "Hero";
        info.Name = "test";
        info.Level = 1;
        player.SetInfo(info);

        var model = Resources.Load("Prefabs/Heros/Beatrix");
        Instantiate(model, hero.transform.position, standingPositions[index].rotation, hero.transform);
        //PhotonNetwork.Instantiate("Prefabs/Heros/Beatrix", standingPositions[index].position, standingPositions[index].rotation);
    }
}
