using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject Manager;
    private Animator anim;
    void Awake()
    {        
        anim = GetComponent<Animator>();
    }
    public void ToLevel2()
    {
        GameManager gameManager = Manager.gameObject.GetComponent<GameManager>();
        gameManager.gameChanged("Level2");
        anim.SetTrigger("death");
        this.gameObject.GetComponent<Health>().health = 5;
    }
    public void gameOver()
    {
        GameManager gameManager = Manager.gameObject.GetComponent<GameManager>();
        gameManager.gameChanged("Ended");
        anim.SetTrigger("leave");
        disappearAsync();
    }
    async Task disappearAsync()
    {
        await Task.Delay(2200);
        Destroy(gameObject);
    }
    public void hit()
    {
        anim.SetTrigger("hit");
    }
}
