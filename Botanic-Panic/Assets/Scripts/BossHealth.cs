using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHealth : MonoBehaviour
{
    public GameObject spriteHealth;
    private Animator anim;
    public float health;
    private float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        anim = spriteHealth.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 4)
        {
            anim.Play("health4");
        }
        else if (health == 3)
        {
            anim.Play("health3");
        }
        else if (health == 2)
        {
            anim.Play("health2");
        }
        else if (health == 1)
        {
            anim.Play("health1");
        }
        else if (health < 1)
        {
            anim.Play("health0");
        }
    }
}
