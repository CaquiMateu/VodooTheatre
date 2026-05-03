 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMóvil : MonoBehaviour
{
 
    public Transform[] puntos;
    public int ÍndiceActual = 0;
    public float Velocidad = 3;
    
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        transform.position= Vector3.MoveTowards(transform.position, puntos[ÍndiceActual].position, Velocidad * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, puntos[ÍndiceActual].position) <= 0.01f)
        {
            if (ÍndiceActual < puntos.Length - 1)
            {
                ÍndiceActual++;
            }
            else if (ÍndiceActual >= puntos.Length - 1)
            {
                ÍndiceActual = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")==true)
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            collision.transform.SetParent(null);
        }
    }
}
