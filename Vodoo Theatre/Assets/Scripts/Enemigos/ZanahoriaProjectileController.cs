using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZanahoriaProjectileController : MonoBehaviour
{
    public float Speed = 16;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-Speed, 0), ForceMode2D.Impulse);
        StartCoroutine(DestroyProjectile());
    }

   IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().PerderVida(1);
            Destroy(this.gameObject);
        }
       
    }
}
