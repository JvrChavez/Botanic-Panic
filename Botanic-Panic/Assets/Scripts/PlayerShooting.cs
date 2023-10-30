using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet,bullet2;
    public Transform bulletPos;
    public void shootBlue()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
    public void shootRed()
    {
        Instantiate(bullet2, bulletPos.position, Quaternion.identity);
    }
}
