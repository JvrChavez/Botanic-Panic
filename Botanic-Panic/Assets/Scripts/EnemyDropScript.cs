using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    public string aimTag;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (transform.position.y < 0)
        {
            player = GameObject.FindGameObjectWithTag(aimTag);
            Vector3 direction = player.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot - 90);
        }
        else
        {
            rb.velocity = new Vector2(0, -force);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
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

            CupheadPlayerManager player = other.gameObject.GetComponent<CupheadPlayerManager>();
            if (player != null)
            {
                if (other.gameObject.GetComponent<Health>().health < 1)
                {
                    player.gameOver();
                }
                else
                {
                    player.hit(); // Llama al método en el objeto "player"
                }                                
            }
        }
    }
}
