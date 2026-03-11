using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    private AIDestinationSetter setter;

    public Transform[] puntos;
    public int ÍndiceActual = 0;

    public bool chasing = false;

    private void Start()
    {
        setter = GetComponent<AIDestinationSetter>();
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
        
        if (Vector3.Distance(transform.position, puntos[ÍndiceActual].position) <= 0.01f && chasing==false)
        {
            ÍndiceActual = Random.Range(0, puntos.Length);

            setter.target = puntos[ÍndiceActual];
        }
    }
}
