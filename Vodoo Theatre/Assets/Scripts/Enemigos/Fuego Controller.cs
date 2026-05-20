using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuegoController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [Range(0, 1)] public float t;
    public AnimationCurve curve;
    PlayerHealth playerHealth;
    public int daño = 1;
    void Start()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
       playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

   
    void Update()
    {
        if (t < 1)
        {
            t += 1*Time.deltaTime;
        }
        spriteRenderer.color = Color.Lerp(Color.yellow, Color.red, curve.Evaluate(t));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.PerderVida(daño * playerHealth.MultiplicadorDeDaño);
            }
        }
    }
}
