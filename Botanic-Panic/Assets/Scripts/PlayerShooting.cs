using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public void shoot()
    {
        Debug.Log("Disparo");
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}