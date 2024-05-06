using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Iniciojuego : MonoBehaviour
{
    [SerializeField] GameObject[] ObjetosAFrezear;
    [SerializeField] CanvasMnager controladorComienzo;
    [SerializeField] Spawner spawner;
    [SerializeField] Animator AnimCamara;
    [SerializeField] MovSucio scripjugador1;
    [SerializeField] MovSucio scripjugador2;
    [SerializeField] GameObject CanvasInicio;
    [SerializeField] GameObject CanvasJuego;
    

    
    // Start is called before the first frame update
    void Start()
    {
        spawner.enabled = false;
        scripjugador1.enabled = false;
        scripjugador2.enabled = false;
        CanvasJuego.SetActive(false);
        CanvasInicio.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (controladorComienzo.puedoEmpezarAjugar)
        {
            spawner.enabled = true;
            AnimCamara.SetBool("ComienzoAjugar", true);
            Invoke("cambioCamarayCanva", 0.5F);
           
        }
        else
        {
            spawner.enabled = false;
            AnimCamara.SetBool("ComienzoAjugar",false);
            

        }
        
    }
    void cambioCamarayCanva()
    {
        CanvasInicio.SetActive(false);
        CanvasJuego.SetActive(true);
        scripjugador1.enabled = true;
        scripjugador2.enabled = true;
        
    }
}
