using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject drop;
    public Transform dropPos;
    private float timer;
    private GameState gameState;
    // Update is called once per frame
    void Update()
    {
        gameState = GameManager.Instance.gameState;
        if (gameState == GameState.Level1)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                timer = 0;
                shoot();
            }
        }
    }
    void shoot()
    {
        Instantiate(drop,dropPos.position, Quaternion.identity);
    }
}