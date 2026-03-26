using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    bool esInvulnerable = false;
    float InvulTime = 1f;
    public int currentHealth = 0;
    public int MaxHealth = 3;
    public Transform healthIconLayout;

    void Start()
    {
        currentHealth = MaxHealth;
    }
    private void Update()
    {
       
    }


    public void PerderVida(int Damage, Vector3 position = default)
    {
        if (esInvulnerable == true) { return; }

        if (currentHealth != 0)
        {
            currentHealth -= Damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("Fallesí");
                Respawn();
            }
            UpdateHealIcons();
            StartCoroutine(InvulnerableTimer());
        }

    }

    IEnumerator InvulnerableTimer()
    {
        esInvulnerable = true;
        gameObject.layer = LayerMask.NameToLayer("Enemigo");
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(InvulTime);
        esInvulnerable = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Heal(int health)
    {
        if (currentHealth != 0)
        {

        }
    }

    void UpdateHealIcons()
    {
        for (int i = 0; i < healthIconLayout.childCount; i++)
        {
            if (i > currentHealth - 1)
            {
                healthIconLayout.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                healthIconLayout.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void Respawn()
    {
        currentHealth = MaxHealth;
        SceneManager.LoadScene(0);
    }



}

