using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController playerPrefab;
    public Transform[] spawnPoints;
    public List<PlayerController> players;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        Transform nextSpawnPosition = spawnPoints[players.Count];
        input.transform.position = nextSpawnPosition.position;
        players.Add(input.GetComponent<PlayerController>());
    }
}
