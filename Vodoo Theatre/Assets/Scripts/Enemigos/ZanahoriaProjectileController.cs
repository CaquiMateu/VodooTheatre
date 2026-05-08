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
       
    }

    // Update is called once per frame
    void Update()
    {

        rb.transform.position += new Vector2(-rb.position.x * Speed, 0);

    }
}
