using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum GameState { Ready, Level1, Level2, Ended };
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState gameState = GameState.Level1;
    private void Awake()
    {
        Instance = this;
    }
    public void gameChanged(string gameName)
    {
        switch (gameName)
        {
            case "Level1":
                gameState = GameState.Level1;
                break;
            case "Level2":
                gameState = GameState.Level2;
                break;
            case "Ended":
                gameState=GameState.Ended;
                break;
        }
    }
}
