using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MurciélagoEnemigoController : MonoBehaviour
{
    Rigidbody2D rb;
    public Rigidbody2D RbPlayer;
    public float speed = 5f;
    Vector2 dirección;
    public GameObject zonaDetección;
    bool PlayerInAtackZone= false;
    public bool moving;
    public float Posx;
    public float Posy;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerInAtackZone && moving == false)
        {
            moving = true;
             Posx = Random.Range(-15, 15);
             Posy = Random.Range(0, 1.5F);

            dirección = new Vector2(Posx,Posy);

            rb.velocity = (dirección - rb.position) * speed ;  
        }
        else if (dirección == rb.position)
        {
            rb.velocity = new Vector2(0,0);
            moving = false;
        }
       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            PlayerInAtackZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == false)
        {

            PlayerInAtackZone = false;
        }
    }
}






















// Vector3 playerPos = RbPlayer.position;


//Vector2 dirección = new Vector2
//(
//playerPos.x - transform.position.x,
//0

//);


//if (playerPos.x - rb.position.x < 0.5)
//{
//    rb.velocity = dirección.normalized * speed;
//}
//else if (playerPos.x - rb.position.x < 0.5)
//{
//    rb.position = new Vector2(playerPos.x, rb.position.y);

//}