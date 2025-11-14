using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;
    public Transform[] spawnPoints;

    // List of active player that join the game
    public List<PlayerController> players;

    // State machine variable to orchestrate the game loop
    public GamePhase phase = GamePhase.starting;

    public Timer timer;

    // The Player who has won the round
    public PlayerController victor;

    public void Awake()
    {
        // Singleton initialization
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        // Disable timer script until all players join
        timer.enabled = false;
    }

    // A Message that listens to the PlayerInputManager runs when a player joins
    public void OnPlayerJoined(PlayerInput input)
    {
        // Using the amount of joined players select the next spawn position
        Transform nextSpawnPosition = spawnPoints[players.Count];
        // Move player to their spawn position
        input.transform.position = nextSpawnPosition.position;
        // Add the player to the joined players list
        players.Add(input.GetComponent<PlayerController>());
        // If all players have joined, start the timer
        if (players.Count == 2)
            timer.enabled = true;
    }

    // Used by the timer to end the game if nobody jumps in time
    public void KillAll()
    {
        // Loop through the players and tell them to die
        foreach (PlayerController player in players)
        {
            player.Die();
        }
        // End the round
        StartCoroutine(EndRound());
    }

    // Only one player can jump per round & only if the game has started
    public bool CanPlayerJump()
    {
        return victor == null && phase == GamePhase.started;
    }

    // If the player can jump we call this function and end the round
    public void OnPlayerJumped(PlayerController player)
    {
        // Save the jumping player as the victor
        victor = player;
        // Kill the other player
        players.Find((p) => p != victor).Die();
        // End the round
        StartCoroutine(EndRound());
    }

    // Used by both round outcomes to end & reset the game
    public IEnumerator EndRound()
    {
        // Start the ending phase (pauses timer) & prevents jumping
        phase = GamePhase.ending;
        // Give the players time to Jump & Die
        yield return new WaitForSeconds(3);
        // Start resetting the game
        phase = GamePhase.starting;
        // Reset timer
        timer.CountDown();
        // Clear victor and reset the players
        victor = null;
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