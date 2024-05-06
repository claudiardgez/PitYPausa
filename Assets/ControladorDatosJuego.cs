using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ControladorDatosJuego : MonoBehaviour
{ 
public GUI gui;
public string archivoDeGuardado;
public DatosJuego datosJuego = new DatosJuego();

    public List<TextMeshProUGUI> rankingText;


//public float lifes, currentHealth, currentEnergy;
private void Awake()
{
    archivoDeGuardado = Application.dataPath + "/datos juego.json";

    GameObject guiObj = GameObject.Find("Trigger");
  //  gui = guiObj.GetComponent<GUI>();

    //GuardarDatos();
}

private void Start()
{
    //CargarDatos(); 
}

private void Update()
{
        if (Input.GetKeyDown(KeyCode.E))
        {
            CargarDatos();
        }
    }

private void CargarDatos()
{
    if (File.Exists(archivoDeGuardado))
    {
        string contenido = File.ReadAllText(archivoDeGuardado);
        datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);

            for (int i = 0; i < datosJuego.rankings.Count; i++)
            {
                Debug.Log(datosJuego.rankings[i]);
            }
            gui.minutos1 = datosJuego.minutos1;
            gui.minutos2 = datosJuego.minutos2;
            gui.minutos3 = datosJuego.minutos3;
            gui.minutos4 = datosJuego.minutos4;
            gui.minutosTotal = datosJuego.minutosTotales;

            gui.segundos1 = datosJuego.segundos1;
            gui.segundos2 = datosJuego.segundos2;
            gui.segundos3 = datosJuego.segundos3;
            gui.segundos4 = datosJuego.segundos4;
            gui.segundosTotal = datosJuego.segundosTotales;
            for (int i = 0; i < rankingText.Count; i++)
            {

            }
        }
    else
    {
        Debug.Log("No existe");
    }
}
public void GuardarDatos()
{
        DatosJuego nuevosDatos = new DatosJuego()
        {
            player1 = gui.player1,
            player2 = gui.player2,

            minutos1 = gui.minutos1,
            minutos2 = gui.minutos2,
            minutos3 = gui.minutos3,
            minutos4 = gui.minutos4,

            segundos1 = gui.segundos1,
            segundos2 = gui.segundos2,
            segundos3 = gui.segundos3,
            segundos4 = gui.segundos4,

            minutosTotales = gui.minutosTotal,
            segundosTotales = gui.segundosTotal,
    };


    string cadenaJSON = JsonUtility.ToJson(nuevosDatos);

    File.WriteAllText(archivoDeGuardado, cadenaJSON);

    Debug.Log("Archivo Guardado");
}
}
