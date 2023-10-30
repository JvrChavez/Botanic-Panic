using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public GameObject drop;
    public Transform[] dropPositions;
    private float timer;
    public  GameState gameState;
    // Update is called once per frame
    void Update()
    {
        gameState = GameManager.Instance.gameState;
        if (gameState==GameState.Level2)
        {
            timer += Time.deltaTime;
            if (timer > 0.5)
            {
                timer = 0;
                shoot();
            }
        }        
    }
    void shoot()
    {
        // Elige una posición aleatoria del array
        int randomIndex = Random.Range(0, dropPositions.Length);
        Transform selectedPosition = dropPositions[randomIndex];

        // Instancia el objeto drop en la posición seleccionada
        Instantiate(drop, selectedPosition.position, Quaternion.identity);
    }

}