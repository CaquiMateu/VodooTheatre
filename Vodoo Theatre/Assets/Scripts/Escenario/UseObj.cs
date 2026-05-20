using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UseObj : MonoBehaviour
{
    public SpawnEnemigos SpawnEnemigos;
    public GameManger gameManager;
    public UnityEvent OnTriggerEnter;
    public UnityEvent OnTriggerExit;
    public UnityEvent Acción;
    public PlayerController playerController;
    PlayerHealth playerHealth;

    private void Start()
    {
        
       
    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerController == null)
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
        }
        if (playerHealth == null)
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        }
    }

    public void curar()
    {
        playerHealth.Heal(1);
    }
    public void AumentoVidaMax()
    {
        playerHealth.MaxHealth += 1;
        playerHealth.Heal(1);
    }
    public void AumentoDañoBase()
    {
        playerController.DañoAtaque += 1;
        playerHealth.MultiplicadorDeDaño ++;
    }







}
