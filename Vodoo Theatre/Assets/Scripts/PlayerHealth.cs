using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int currentHealth = 0;
    public int MaxHealth = 3;
    
    void Start()
    {
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PerderVida(1);
        }
    }

    public void PerderVida(int Damage)
    {
        if (currentHealth != 0) 
        {
            currentHealth -= Damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("Fallesí");
                Respawn();
            }
        }
       
    }

    public void Respawn()
    {
        currentHealth = MaxHealth;
        GameObject respawn = GameObject.FindWithTag("Respawn");
        transform.position = respawn.transform.position;
    }

}
