using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    private GameState gameState;
    private GameObject enemy;
    private Rigidbody2D rb;
    public float force;
    public string aimTag;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GameObject.FindGameObjectWithTag(aimTag);
        Vector3 direction = enemy.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot-180);
    }

    // Update is called once per frame
    void Update()
    {
        gameState = GameManager.Instance.gameState;
        timer += Time.deltaTime;
        if (timer>3)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(aimTag))
        {
            other.gameObject.GetComponent<Health>().health -= 1;
            Destroy(gameObject);

            BossManager boss = other.gameObject.GetComponent<BossManager>();
            
            if (boss != null)
            {                
                if (gameState==GameState.Level1)
                {
                    if (other.gameObject.GetComponent<Health>().health < 1)
                    {

                        boss.ToLevel2();
                    }
                    else
                    {
                        boss.hit();
                    }
                }
                else if (gameState == GameState.Level2)
                {
                    if (other.gameObject.GetComponent<Health>().health < 1)
                    {
                        boss.gameOver();
                    }
                }
            }
        }
    }
}
