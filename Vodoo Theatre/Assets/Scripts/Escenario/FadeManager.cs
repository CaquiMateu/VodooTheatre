using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeManager : MonoBehaviour
{
    [Header("Referencias")]
    public CanvasGroup canvasGroup;

    [Header("Configuración")]
    public float duracionTransicion = 0.25f;

    [Header("Eventos")]
    public UnityEvent eventoenFade;

    private Coroutine fadeCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        canvasGroup.alpha = 0;
    }

    public void FundirANegro()
    {
        CambiarFade(1);
    }

    public void VolverdesdeNegro()
    {
        CambiarFade(0);
        Debug.Log("Llamado");
    }

    public void InvocarEvento(float retraso)
    {
        StartCoroutine(InvocarEventoCoroutine(retraso));
    }

    public void VolverDeNegroConRetraso(float retraso)
    {

        StartCoroutine(FadeOutConRetraso(retraso));
    }
    private void CambiarFade(float AlphaObjetivo)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeCoroutine(AlphaObjetivo));
    }

    private IEnumerator FadeCoroutine(float AlphaObjetivo)
    {
        float alphainicial = canvasGroup.alpha;

        float Tiempo = 0;

        while (Tiempo < duracionTransicion)
        {
            Tiempo += Time.deltaTime;
            float progreso = Tiempo / duracionTransicion;

            canvasGroup.alpha = Mathf.Lerp(alphainicial, AlphaObjetivo, progreso);

            yield return null;
        }
    }

    private IEnumerator InvocarEventoCoroutine(float retraso)
    {
        yield return new WaitForSeconds(retraso);

        eventoenFade.Invoke();
    }

    private IEnumerator FadeOutConRetraso(float retraso)
    {
        yield return new WaitForSeconds(retraso);

        VolverdesdeNegro();
    }
}
