using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject newrespawnpoint;
    bool PlayerPassed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true && PlayerPassed== false)
        {
            PlayerPassed = true;
            GameObject currentspawnpoint = GameObject.FindWithTag("Respawn");
            currentspawnpoint.SetActive(false);
            newrespawnpoint.SetActive(true);
        }
    }
}
