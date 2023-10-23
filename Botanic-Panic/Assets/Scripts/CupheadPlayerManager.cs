using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CupheadPlayerManager : MonoBehaviour
{
    public GameState gameState;
    public Rigidbody2D rig;
    private Animator anim;
    [SerializeField] private float velocidadPersonaje;
    SpriteRenderer spriteCuphead;
    private float timeJump = 0f;//Tiempo de salto
    private float velocityInputx;       //Velocidad en X
    private bool canDash = true,parryInProcess=false;        //Variable que controla si se puede realizar un dash
    public float dashCooldown = 0.3f;   //Tiempo de espera mínimo entre dash
    private void Awake()
    {
        rig=GetComponent<Rigidbody2D>();
        anim= GetComponentInChildren<Animator>();
        spriteCuphead=GetComponentInChildren<SpriteRenderer>();
    }
    private void FixedUpdate()  //Se mandan llamar las acciones con respectivos metodos
    {
        run();
        jump();
        candash();
        if (Input.GetButton("Dash") && canDash)//If dash
        {
            StartCoroutine(dash());
            canDash = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            anim.Play("hit");
            rig.AddForce(Vector2.up*30);
            GetComponent<BoxCollider2D>().enabled = false;
            SpawnManager.Instance.StopSpawn(); 
        }
    }
    void run()
    {        
        /*float*/velocityInputx = Input.GetAxisRaw("Horizontal");//Se toma el input teclado
        /*Rigidbody2D*/rig.velocity = new Vector2(velocityInputx * velocidadPersonaje, rig.velocity.y);//Se mueve horizontal
        /*Animator*/anim.SetFloat("run", Mathf.Abs(velocityInputx));//Animacion Run  (Abajo) if se invierte la imagen o no
        /*SpriteRenderer*/spriteCuphead.flipX = velocityInputx < 0 ? true : (velocityInputx > 0 ? false : spriteCuphead.flipX);        
    }
    void jump()
    {
        if (Input.GetButton("Jump") && rig != null && rig.velocity.y == 0)//Input del salto y verifica que solo salte una vez
        {
            timeJump = 0f; // Resetea el tiempo cuando se presiona la tecla
            rig.AddForce(Vector2.up * 250); // Aplica una fuerza inicial de salto
        }
        if (Input.GetButton("Jump") && rig != null)//If para incrementar salto si se deja la tecla presionada
        {
            timeJump += Time.deltaTime; // Incrementa el tiempo mientras se mantiene presionada la tecla
            if (timeJump < 1.0f && !parryInProcess)
            {
                rig.AddForce(Vector2.up * 8 * (1.0f - timeJump)); // Aplica una fuerza adicional de salto
            }
        }
        anim.SetFloat("jump", Mathf.Abs(rig.velocity.y));//Animacion de salto
    }
    void candash()
    {
        if (!canDash)
        {
            dashCooldown -= Time.deltaTime;
            if (dashCooldown <= 0 && rig != null && rig.velocity.y == 0)
            {                           //If paso el cooldawn, existe un rig y toco el suelo
                canDash = true;
                dashCooldown = 0.3f;    //Reset coldown
                parryProcess(false);

            }
        }        
    }
    IEnumerator dash()
    {
        anim.Play("dash");
        float dashDuration = 0.21f;   // Duración total del dash en segundos
        float dashForce = 300.0f;   // Ajusta la fuerza de dash
        float loopTime = 0f;
        while (loopTime < dashDuration)
        {               
            float dashSpeed = Mathf.Lerp(0f, 10.0f, loopTime / dashDuration);//Aumenta progresivamene el dash
            Vector2 dashVelocity= spriteCuphead.flipX ? Vector2.left*dashSpeed : Vector2.right*dashSpeed;
            //Vector2 dashVelocity = velocityInputx < 0 ? Vector2.left * dashSpeed : Vector2.right * dashSpeed;

            //spriteCuphead.flipX = velocityInputx < 0 ? true : (velocityInputx > 0 ? false : spriteCuphead.flipX);
            //rig.velocity = dashVelocity;
            rig.AddForce(dashVelocity.normalized * dashForce);
            loopTime += Time.deltaTime;                
            yield return null;// Espera un frame antes de continuar
        }
        rig.velocityX = 0;
    }
    public void parry()//Parry provisional
    {
        rig.velocityY = 0;
        candash();
        rig.velocity = new Vector2(rig.velocity.x, 6);
    }
    public void parryProcess(bool inProcess)
    {
        parryInProcess=inProcess;
    }
}