using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSalida : MonoBehaviour
{
   public void SalirDelTutorial()
    {
        SceneManager.LoadScene(0);
    }
    public void EntrarAlTutorial()
    {
        SceneManager.LoadScene(2);
    }
}
