using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CanvasMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject controles;
    public GameObject jugabilidad;
    public GameObject canvas;
    public GameObject canvasJogo;

    string player1;
    string player2;
    public GameObject spawner;
    public GUI gui;
    bool pasarNombre1 = false;
    bool pasarNombre2 = false;
    bool paroEltiempo;

    // Start is called before the first frame update
    void Start()
    {
        GameObject guiObj = GameObject.Find("Trigger");
        spawner.SetActive(false);
        gui = guiObj.GetComponent<GUI>();
        gui.enabled = false;
        //Time.timeScale = 0;
        paroEltiempo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (paroEltiempo)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void LeerInputsNombres(string s)
    {
        player1 = s;
            //gui.listaNombresPlayer1.Add(s);
            pasarNombre1 = true;
        //Debug.Log(gui.listaNombresPlayer1[gui.listaNombresPlayer1.Count]);
        Debug.Log(player1);
    }

    public void LeerInputsNombres2(string s)
    {
        player2 = s;
        //gui.listaNombresPlayer2.Add(s);
        pasarNombre2 = true;
        Debug.Log(player2);
        //Debug.Log(gui.listaNombresPlayer2[gui.listaNombresPlayer2.Count]);
    }
    public void ComoJugar()
    {
        if (pasarNombre1&&pasarNombre2)
        {
            menu.SetActive(false);
            controles.SetActive(true);
            jugabilidad.SetActive(false);
        }
    }

    public void PasarPagina()
    {
        menu.SetActive(false);
        controles.SetActive(false);
        jugabilidad.SetActive(true);
    }

    public void Jugar()
    {
        canvas.SetActive(false);
        canvasJogo.SetActive(true);
        paroEltiempo = false;

        spawner.SetActive(true);
        gui.enabled = true;
    }

    public void VueltaAmenu()
    {
        SceneManager.LoadScene(0);
        //canvasJogo.SetActive(true);
        //Time.timeScale = 0;
    }
}
