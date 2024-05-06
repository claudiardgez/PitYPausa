using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasNombre : MonoBehaviour
{
    string player1;
    string player2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void lectura(string nombre)
    {
        player1 = nombre;
        Debug.Log(nombre);
    }
}
