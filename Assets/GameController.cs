using UnityEngine;
using TMPro;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class GameController : MonoBehaviour
{
    public GameObject[] players = new GameObject[4]; //4 Players max seems a good limit
  

    public int[] lives = new int[4]; // Integers for storing how many lives the players have left

    public int playerCount = 2; //Total amount of players
    public int totalPlayers = -1;

    public Color[] playerColors = new Color[4];

    public int turn = 0; //Int to track what players turn it is to protect the bubble

    public float turnTime = 15; //How long a players turn is

    public float turnTimer = 0; //Timer for tracking turns

    [Header("UI")] //References to the games ui so we can update it, going to address these in arrays cause I assume it would be faster
    public GameObject[] uiGameObject = new GameObject[4];

    public TMP_Text[] LivesText = new TMP_Text[4];

    public Transform[] turnHeader = new Transform[4];

    public Bubble bubble; //Reference to the bubble to perform functions, bubble also has a reference to this (thats makes them co dependant and is bad practice)


    void Start()
    {
        for(int i = 0; i < playerCount; ++i)
        {

            turnHeader[i] = uiGameObject[i].transform.Find("TurnHeader"); //Get reference to ui for showcasing the current turn

            turnHeader[i].localScale = new Vector2 (0, 0);

            Transform liveUI = uiGameObject[i].transform.Find("Lives Text"); //Find child transform of the UI
            LivesText[i] = liveUI.GetComponent<TMP_Text>(); 
            Debug.Log("Loop ran");
        }

        StartCoroutine(ScaleBox(turnHeader[0]));


    }

    void Update()
    {
        if (turnTimer < turnTime) {
            turnTimer += Time.deltaTime; //Increment timer by how much time has passed since last frame
        }
        else
        {
            ChangeTurn(turn);
           
            turnTimer = 0; //Reset timer
            turn += 1;



            if (turn > playerCount - 1) // Reset turn to first player if on last player
            {
                turn = 0;
            }

            bubble.SwitchBubble(turn);

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            DecreaseLives (turn);
        }


    }

    void ChangeTurn(int currentTurn)
    {
        turnHeader[currentTurn].localScale = new Vector2 (0, 0);
        currentTurn += 1;
        if (currentTurn > playerCount - 1) // Reset turn to first player if on last player
        {
            currentTurn = 0;
        }

        //  turnHeader[currentTurn].localScale = new Vector2(1, 1);
        StartCoroutine(ScaleBox(turnHeader[currentTurn]));
    }

    IEnumerator ScaleBox(Transform box)
    {
        while(box.localScale.y < 1) {
            box.localScale = new Vector2(1, box.localScale.y + 0.05f);

            yield return new WaitForSeconds(0.05f);
        }


        box.localScale = new Vector3(1, 1);
    }

  public void BubbleHitFloor()
    {
        DecreaseLives(turn);
    }


    void DecreaseLives(int playerId)
    {
        lives[playerId] -= 1;

        Transform lifeIcon;

        if (lives[playerId] < 1)
        {
            lifeIcon = uiGameObject[playerId].transform.Find("FirstLife"); 
        }
        else if (lives[playerId] < 2){
            lifeIcon = uiGameObject[playerId].transform.Find("SecondLife");
        }
        else{
            lifeIcon = uiGameObject[playerId].transform.Find("ThirdLife");
        }

        if (lives[playerId] < 0)
        {
            LivesText[playerId].text = "GAME OVER";

            return;
        }
        

        lifeIcon.localScale = new Vector3(0, 0);

        //   LivesText[playerId].text = "LIVES: " + lives[playerId];
    }

    public void ReceivePlayer(GameObject player)
    {
        totalPlayers += 1;
        players[totalPlayers] = player;
        SpriteRenderer spriteRender = player.GetComponent<SpriteRenderer>();
        spriteRender.color = playerColors[totalPlayers];

        spriteRender.sortingOrder = 6;
    }


}
