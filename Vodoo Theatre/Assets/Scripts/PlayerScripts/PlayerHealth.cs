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
    Animator animator;
    public bool IsDead = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = MaxHealth;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    private void Update()
    {
        if ( IsDead == true)
        {
            gameObject.layer = LayerMask.NameToLayer("Invunerable");
           
        }
        if (IsDead == true && Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }

       
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
                Muerte();
            }
            UpdateHealIcons();
            StartCoroutine(InvulnerableTimer());
        }

    }

    IEnumerator InvulnerableTimer()
    {
        esInvulnerable = true;
        Time.timeScale = 0.1f;
        gameObject.layer = LayerMask.NameToLayer("Invunerable");
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.03f);
        GetComponent<SpriteRenderer>().color = Color.white;
        Time.timeScale = 1;
        yield return new WaitForSeconds(InvulTime);
        esInvulnerable = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
       
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

    public void Muerte()
    {
        animator.SetBool("IsDead", true);
        IsDead = true;
    }

    public void Respawn()
    {
        currentHealth = MaxHealth;
        SceneManager.LoadScene(0);
    }



}

