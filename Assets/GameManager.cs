using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController playerPrefab;
    public Transform[] spawnPoints;
    public List<PlayerController> players;
    public GamePhase phase = GamePhase.starting;
    public Timer timer;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        timer.enabled = false;
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        Transform nextSpawnPosition = spawnPoints[players.Count];
        input.transform.position = nextSpawnPosition.position;
        players.Add(input.GetComponent<PlayerController>());
        if (players.Count == 2)
            timer.enabled = true;
    }

    public void KillAll()
    {
        foreach (PlayerController player in players)
        {
            player.Die();
        }
        StartCoroutine(EndRound());
    }

    public IEnumerator EndRound()
    {
        phase = GamePhase.ending;
        yield return new WaitForSeconds(3);
        phase = GamePhase.starting;
        timer.CountDown();
        for (int i = 0; i < players.Count; i++)
        {
            players[i].Revive(spawnPoints[i].position);
        }
    }
}

public enum GamePhase
{
    starting,
    started,
    ending
}