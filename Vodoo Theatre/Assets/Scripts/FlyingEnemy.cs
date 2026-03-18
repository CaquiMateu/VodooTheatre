using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    private AIDestinationSetter setter;
    private AIPath Path;

    public Transform[] puntos;
    public int ÍndiceActual = 0;

    public bool chasing = false;
    public float attackDinstance = 2;
    public GameObject BalaPrefab;
    public float RecargaBala = 1;
    public float VelocidadBala = 3;
    public float Temporizador;
    private float TiempoParaAtacar;

    private void Start()
    {
        setter = GetComponent<AIDestinationSetter>();
        Path = GetComponent<AIPath>();
        setter.target = puntos[ÍndiceActual];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            chasing = true;
            Debug.Log("Goo Goo Gaa Gaaa");
            setter.target = collision.transform;
        }
    }
    void Update()
    {
        if (setter.target.position.x > transform.position.x ) 
        {
            this.transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3 (-1,1,1);
        }
        if (chasing == true)
        {
            if (Time.time >= TiempoParaAtacar)
            {
                shoot();
            }
        }
        
        if (Vector3.Distance(transform.position, puntos[ÍndiceActual].position) <= 0.1f && chasing==false)
        {
            ÍndiceActual = Random.Range(0, puntos.Length);

            setter.target = puntos[ÍndiceActual];

            Path.endReachedDistance = attackDinstance;
        }
    }

    void shoot()
    {
        GameObject Bala = Instantiate(BalaPrefab, new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity);
        Vector3 PlayerDirection = setter.target.position - transform.position;
        Bala.GetComponent<Rigidbody2D>().AddForce(PlayerDirection.normalized * VelocidadBala, ForceMode2D.Impulse);
    }
}
