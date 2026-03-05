using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameObject PrefabMurciélago;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl)) 
        {
            // 1. Obtener la posición del ratón en la pantalla
            Vector3 mousePos = Input.mousePosition;


            // 2. Convertir la posición de la pantalla a coordenadas del mundo
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            if (Input.GetMouseButtonDown(2))
            {
                Instantiate(PrefabMurciélago, new Vector3 (mousePos.x, mousePos.y, 0), Quaternion.identity);
            }
        }
    }
}
