using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject drop;
    public Transform dropPos;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>2)
        {
            timer = 0;
            shoot();
        }
    }
    void shoot()
    {
        Instantiate(drop,dropPos.position, Quaternion.identity);
    }
}