using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.InputSystem;


public class CanvasMnager : MonoBehaviour
{
    [SerializeField] Transform jugadorASeguir1;
    [SerializeField] Transform jugadorASeguir2;
    [SerializeField] GameObject textoVisiblesobreCabezaTexto;
    [SerializeField] GameObject textoVisiblesobreCabezaTexto2;
    [SerializeField] GameObject controlesTexto;
    [SerializeField]GameObject tituloPITYPAUSA;
    [SerializeField]GameObject botonesControles;
    
    [SerializeField] TMP_Text nombreJugador1 ;
    [SerializeField] TMP_Text nombreJugador2;
    bool nombrePuesto1, nombrePuesto2;
  public  bool puedoEmpezarAjugar;
    public bool jugarConMando;
    public bool escogidoControl;
  //  [SerializeField] PlayerInputManager manager;
    [SerializeField] GameObject[] prefabsJugadores;
    // Start is called before the first frame update
    void Start()
    {
        controlesTexto.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        textoVisiblesobreCabezaTexto.transform.position = new Vector3(jugadorASeguir1.position.x, jugadorASeguir1.position.y + 4, jugadorASeguir1.position.z -0.1f);
        textoVisiblesobreCabezaTexto2.transform.position = new Vector3(jugadorASeguir2.position.x, jugadorASeguir2.position.y + 4, jugadorASeguir2.position.z - 0.1f);

        if (nombrePuesto1&&nombrePuesto2&&escogidoControl)
        {
            controlesTexto.SetActive(true);
            tituloPITYPAUSA.SetActive(false);
            botonesControles.SetActive(false);

        }
         
    }
   public void ponerNombreAjugador1(string nombre)
    {

        nombreJugador1.text = nombre;
       

    }
    public void finalPonerleNombre()
    {
        
        nombrePuesto1 = true;
        //manager.playerPrefab = prefabsJugadores[0];
      
    }
    public void ponerNombreAjugador2(string nombre)
    {

        nombreJugador2.text = nombre;
       

    }
    public void finalPonerleNombre2()
    {
        nombrePuesto2 = true;

    }
    public void jugarConMandoo()
    {
        escogidoControl = true;
        jugarConMando = true;
    }
    public void jugarConTeclado()
    {
        escogidoControl = true;
        jugarConMando = false;

    }
    public void Jugar()
    {

        puedoEmpezarAjugar = true;
    }

}
