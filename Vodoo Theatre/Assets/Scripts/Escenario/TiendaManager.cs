using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TiendaManager : MonoBehaviour
{
    public ShopObj[] shopObj;
    public Transform[] Zonas;
    public GameObject[] Objetos;
    public GameObject[] SpawnTienda;
    public bool TiendaActiva = false;
    public GameObject Foco;
    Light2D SpriteFoco;
    [Range (0, 1)] public float t = 1;
    public float currentIntensity;
    public AnimationCurve curve;
    public float intensidad;
    public int Índice = 0;

    public void Start()
    {
        SpriteFoco = Foco.GetComponent<Light2D>();
        currentIntensity = SpriteFoco.intensity;
    }
    public void ActivarTienda()
    {
        for (int i = 0; i < Zonas.Length; i++)
        {
            shopObj[i].ActivarCollider();
            int tipodeobjeto = Random.Range(0, Objetos.Length);
            SpawnTienda[i] = Instantiate(Objetos[tipodeobjeto], Zonas[i].position, Quaternion.identity);
            shopObj[i].ObjetoDeLaZona = SpawnTienda[i].gameObject;
        }
        TiendaActiva = true;
    }
    public void DesactivarTienda()
    {
        for (int i = 0; i < SpawnTienda.Length; i++)
        {
            if (SpawnTienda[i] != null)
            {
                shopObj[i].DesactivarCollider();
                Destroy(SpawnTienda[i]);
            }
        }
        
        TiendaActiva = false;
    }

    private void Update()
    {
        intensidad = SpriteFoco.intensity;
        if (TiendaActiva == true)
        {
           SpriteFoco.intensity= currentIntensity;
            t = 0;


            if (GameObject.FindGameObjectsWithTag("ObjTienda").Length != 3)
            {
                DesactivarTienda();
            }
           
        }

        else
        {
            float ValorMax = currentIntensity;
            float ValorMin = 0;

            if (t < 1)
            {
                t += 1 * Time.deltaTime;
            }
            SpriteFoco.intensity = Mathf.Lerp(ValorMax, ValorMin, curve.Evaluate(t));
        }
       

    }



}
