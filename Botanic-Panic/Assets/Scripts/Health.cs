using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
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
        if (health == 5)
        {
            anim.ResetTrigger("health1");
            anim.SetTrigger("health5");
        }
        else if (health == 4)
        {
            anim.ResetTrigger("health5");
            anim.SetTrigger("health4");
        }
        else if (health == 3)
        {
            anim.ResetTrigger("health4");
            anim.SetTrigger("health3");
        }
        else if (health == 2)
        {
            anim.ResetTrigger("health3");
            anim.SetTrigger("health2");
        }
        else if (health == 1)
        {
            anim.ResetTrigger("health2");
            anim.SetTrigger("health1");
        }
        else if (health < 1)
        {
            anim.ResetTrigger("health1");
            anim.SetTrigger("health0");
        }
    }
}
