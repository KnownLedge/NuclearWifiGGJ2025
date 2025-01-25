using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] players = new GameObject[3]; //4 Players max seems a good limit
    public GameObject bubble; //Reference to bubble object

    public int[] lives = new int[3]; // Integers for storing how many lives the players have left

    public int playerCount = 2; //Total amount of players

    public int turn = 0; //Int to track what players turn it is to protect the bubble

    public float turnTime = 15; //How long a players turn is

    public float turnTimer = 0; //Timer for tracking turns

    void Start()
    {
        
    }

    void Update()
    {
        if (turnTimer < turnTime) {
            turnTimer += Time.deltaTime; //Increment timer by how much time has passed since last frame
        }
        else
        {
            turn += 1;
            if(turn > playerCount - 1) // Reset turn to first player if on last player
            {
                turn = 0;
            }
        }
                      
    }

    void DecreaseLives(int playerId)
    {
        lives[playerId] -= 1;
    }

}
