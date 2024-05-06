using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Character : MonoBehaviour
{
   
    [SerializeField] Transform target;
    Vector3 direccionGiro;
    float velocidadRotacion;
   
    Coroutine llamada;
    [SerializeField] LayerMask mascaraObjeto;
    [SerializeField] LayerMask mascaraEstanteria;
    [SerializeField] LayerMask coche;
    [SerializeField] Transform posTpOverlap;
    [SerializeField] float radioOverlap;
    [SerializeField] float velocidadMovimiento;
    bool tengoAlgoAgarrado;
    GameObject ObjetoAgarrado;
    UbicacionesEstanteria scripUbicacionEstanteria;
    [SerializeField] GameObject bidonmano;
    [SerializeField] GameObject infladorMano;
    [SerializeField] GameObject ruedaCargada;
    [SerializeField] GameObject cajaCambioMano;
    [SerializeField] GameObject aleronDelantero;
    [SerializeField] GameObject aleronTrasero;
    [SerializeField] GameObject retrovisor;
    [SerializeField] GameObject pistolaCaja;
   

    Animator anim;
    [SerializeField] MovSucio scripTarget;
    [SerializeField] CanvasMnager scriptCanvasManager;
    GameObject auxiliar;
    bool sigo;

    [SerializeField] GUI scriptGui;
    [SerializeField] Spawner scripSpawmer;
    [SerializeField] AudioClip[] audios;
    
   

  
     //[SerializeField] AudioSource source;
   
    

   
    // Start is called before the first frame update
    void Start()
    {
        scripUbicacionEstanteria = GameObject.Find("PosicionesEstanteria").GetComponent<UbicacionesEstanteria>();
        anim = GetComponent<Animator>();
        bidonmano.SetActive(false);
        infladorMano.SetActive(false);
        ruedaCargada.SetActive(false);
        cajaCambioMano.SetActive(false);
        aleronDelantero.SetActive(false);
        aleronTrasero.SetActive(false);
        retrovisor.SetActive(false);
       
        pistolaCaja.SetActive(false);
        //source = GetComponent<AudioSource>();

        sigo = true;


       

    }

   

    // public int GetPlayer
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(posTpOverlap.position,0.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        direccionGiro = target.position - transform.position;//DIRECCION A GIRAR DESDE EL TARGET AL JUGADOR
        llamadoCorrutina();
       

        if (scriptCanvasManager.jugarConMando==false)
        {
           // Debug.Log("no juego con mando");
            if (scripTarget.interactuarTeclado.WasPressedThisFrame())
            {
                interaccion();
            }
            if (scripTarget.queSeVayaCocheTeclado.WasPressedThisFrame())
            {
                if (scriptGui.cocheActual.GetComponent<Coche>().llegado)
                {
                    scriptGui.cocheActual.GetComponent<Coche>().irse = true;
                    // scripSpawmer.desocuparCasilla();
                }

            }

        }






    }
    void llamadoCorrutina()
    {
        if (llamada!=null)
        {
            StopAllCoroutines();
        }
       
       
        llamada= StartCoroutine(MoverObjetivoCaidaGranada(target.position, velocidadMovimiento));

        


    }


    IEnumerator MoverObjetivoCaidaGranada( Vector3 destino, float vel)
    {
        if (sigo)
        {
            while (transform.position != destino)//mientras el enemigo no haya llegado al punto de destino...
            {
                //source.Play();
                OrientarseHaciaEnemigo(direccionGiro, 0.3f);
                transform.position = Vector3.MoveTowards(transform.position, destino, vel * Time.deltaTime);//se mueve hacia el punto de destino
                yield return new WaitForEndOfFrame();


            }
            if (transform.position==destino)
            {
               // source.Stop();
            }
            

        }
        




    }
    void OrientarseHaciaEnemigo(Vector3 target, float Smoth)
    {

        //Arcotangente,convierte la rotacion en grados ,para saber que rotacion ponerle a mi personaje
        float angulo = Mathf.Atan2(target.x, target.z) * Mathf.Rad2Deg;//el calculo me lo dan en radios ...Y LO TENGO QUE CONVERTIR A RADIANES
        float anguloSuave = Mathf.SmoothDampAngle(transform.eulerAngles.y, angulo, ref velocidadRotacion, Smoth);//vamos a crear una interpolacion entre el angulo al que estamos mirando y al angulo hacia el que vamos a mirar , con una velocidad de rotacion y un  Smootheado


        transform.eulerAngles = new Vector3(0, anguloSuave, 0);

    }
    public void Overlap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {


            interaccion();

                    
            

        }


    }
    void interaccion()
    {


        if (tengoAlgoAgarrado == false)
        {




            Collider[] coll = Physics.OverlapSphere(posTpOverlap.position, 0.5f, mascaraObjeto);
            if (coll.Length > 0)
            {
                Debug.Log("entro");

                if (tengoAlgoAgarrado == false)
                {

                    if (coll[0].transform.gameObject.name == "Espejos")
                    {
                        Debug.Log("cojo");
                        ObjetoAgarrado = coll[0].transform.gameObject;
                        ObjetoAgarrado.transform.position = posTpOverlap.position;
                        ObjetoAgarrado.transform.SetParent(transform);
                        ObjetoAgarrado.GetComponent<Collider>().enabled = false;
                        anim.SetBool("Agarrar", true);
                        tengoAlgoAgarrado = true;

                    }
                    else if (coll[0].transform.gameObject.name == "Bidon")
                    {
                        ObjetoAgarrado = coll[0].transform.gameObject;
                        ObjetoAgarrado.SetActive(false);
                        anim.SetBool("AgarreEspecial", true);
                        tengoAlgoAgarrado = true;
                        ActivarAgarre(bidonmano);
                    }
                    else if (coll[0].transform.gameObject.name == "Inflador")
                    {
                        ObjetoAgarrado = coll[0].transform.gameObject;
                        ObjetoAgarrado.SetActive(false);
                        anim.SetBool("AgarreEspecial", true);
                        tengoAlgoAgarrado = true;
                        ActivarAgarre(infladorMano);
                    }
                    else if (coll[0].transform.gameObject.name == "Rueda")
                    {
                        ObjetoAgarrado = coll[0].transform.gameObject;
                        //  ObjetoAgarrado.SetActive(false);
                        anim.SetBool("Agarrar", true);
                        tengoAlgoAgarrado = true;
                        ActivarAgarre(ruedaCargada);

                    }
                    else if (coll[0].transform.gameObject.name == "CajaCambio")
                    {
                        ObjetoAgarrado = coll[0].transform.gameObject;
                        ObjetoAgarrado.SetActive(false);
                        anim.SetBool("AgarreEspecial", true);
                        tengoAlgoAgarrado = true;
                        ActivarAgarre(cajaCambioMano);
                    }
                    else if (coll[0].transform.gameObject.name == "AleronDelantero")
                    {
                        ObjetoAgarrado = coll[0].transform.gameObject;
                        ObjetoAgarrado.SetActive(false);
                        anim.SetBool("Agarrar", true);
                        tengoAlgoAgarrado = true;
                        ActivarAgarre(aleronDelantero);
                    }
                    else if (coll[0].transform.gameObject.name == "AleronTrasero")
                    {
                        ObjetoAgarrado = coll[0].transform.gameObject;
                        ObjetoAgarrado.SetActive(false);
                        anim.SetBool("Agarrar", true);
                        tengoAlgoAgarrado = true;
                        ActivarAgarre(aleronTrasero);
                    }
                    else if (coll[0].transform.gameObject.name == "espejo")
                    {
                        ObjetoAgarrado = coll[0].transform.gameObject;
                        ObjetoAgarrado.SetActive(false);
                        // anim.SetBool("Agarrar", true);
                        tengoAlgoAgarrado = true;
                        ActivarAgarre(retrovisor);
                    }





                }






            }

        }
        else
        {
            Collider[] coll = Physics.OverlapSphere(posTpOverlap.position, 0.5f, mascaraEstanteria);
            if (coll.Length > 0)
            {
                //  Debug.Log("salgo");
                anim.SetBool("Agarrar", false);

                if (ObjetoAgarrado.name == "CajaDeCambios")
                {
                    if (coll[0].transform.gameObject.name == "EstanteriaCajaDeCambios")
                    {


                        DejarObjeto(1);
                    }
                }
                else if (infladorMano.activeSelf == true)
                {
                    if (coll[0].transform.gameObject.name == "EstanteriaInflador")
                    {
                        ObjetoAgarrado.SetActive(true);
                        ActivarAgarre(infladorMano);
                        anim.SetBool("AgarreEspecial", false);


                        tengoAlgoAgarrado = false;
                        infladorMano.SetActive(false);


                    }
                    else if (coll[0].transform.gameObject.name == "RuedasDesinfladas")
                    {
                        anim.SetBool("Inflar", true);
                        coll[0].gameObject.SetActive(false);
                        scriptGui.cocheActual.GetComponent<Averia>().sumaPiensaTareas--;//me quito una tarea de la lista
                    }



                }
                else if (ObjetoAgarrado.name == "Espejos")
                {
                    if (coll[0].transform.gameObject.name == "EstanteriaEspejos")
                    {


                        DejarObjeto(3);
                    }
                }
                else if (bidonmano.activeSelf == true)
                {


                    if (coll[0].transform.gameObject.name == "EstanteriaBidon")
                    {
                        ObjetoAgarrado.SetActive(true);
                        ActivarAgarre(bidonmano);
                        anim.SetBool("AgarreEspecial", false);
                        DejarObjeto(4);
                    }
                    else if (coll[0].transform.gameObject.name == "Deposito")
                    {
                        scripTarget.velocidad = 0;
                        sigo = false;
                        anim.SetBool("Gasolina", true);
                        Invoke("finalGasofa", 4);
                        coll[0].transform.gameObject.SetActive(false);
                       


                    }
                }
                else if (ruedaCargada.activeSelf == true)
                {

                    if (coll[0].transform.gameObject.name == "EstanteriaRueda")
                    {

                        anim.SetBool("Agarrar", false);
                        tengoAlgoAgarrado = false;
                        ruedaCargada.SetActive(false);

                    }
                    else if (coll[0].transform.gameObject.name == "RuedaVacia")
                    {
                        ruedaCargada.SetActive(false);
                        GameObject ruedaBunea = coll[0].transform.Find("RuedaBuena").gameObject;
                        ruedaBunea.GetComponent<MeshRenderer>().enabled = true;
                        scriptGui.cocheActual.GetComponent<Averia>().sumaPiensaTareas--;//me quito una tarea de la lista
                        tengoAlgoAgarrado = false;
                    }
                }
                else if (cajaCambioMano.activeSelf == true)
                {
                    if (coll[0].transform.gameObject.name == "EstanteriaCajaDeCambio")
                    {

                        anim.SetBool("AgarreEspecial", false);
                        tengoAlgoAgarrado = false;
                        cajaCambioMano.SetActive(false);
                        ObjetoAgarrado.SetActive(true);

                    }
                    else if (coll[0].transform.gameObject.name == "RuedaPinchada")
                    {
                        scripTarget.velocidad = 0;
                        auxiliar = coll[0].transform.parent.gameObject;
                        anim.SetTrigger("Pistola");
                    }
                    else if (coll[0].transform.gameObject.name == "AleronRotoTrasero")
                    {

                        auxiliar = coll[0].transform.parent.gameObject;
                        coll[0].transform.gameObject.SetActive(false);

                    }
                    else if (coll[0].transform.gameObject.name == "AleronRotoFrontal")
                    {

                        auxiliar = coll[0].transform.parent.gameObject;
                        coll[0].transform.gameObject.SetActive(false);

                    }
                }
                else if (aleronDelantero.activeSelf == true)
                {
                    if (coll[0].transform.gameObject.name == "EstanteriaAleronDelantero")
                    {

                        anim.SetBool("Agarrar", false);
                        tengoAlgoAgarrado = false;
                        aleronDelantero.SetActive(false);
                        ObjetoAgarrado.SetActive(true);

                    }
                    else if (coll[0].transform.gameObject.name == "AleronFrontalVacio")
                    {
                        ObjetoAgarrado.SetActive(true);
                        aleronDelantero.SetActive(false);
                        GameObject aleronBueno = coll[0].transform.Find("AleronFrontalBueno").gameObject;
                        Debug.Log(aleronBueno);
                        aleronBueno.GetComponent<MeshRenderer>().enabled = true;
                        scriptGui.cocheActual.GetComponent<Averia>().sumaPiensaTareas--;//me quito una tarea de la lista
                        tengoAlgoAgarrado = false;
                    }
                }
                else if (aleronTrasero.activeSelf == true)
                {
                    if (coll[0].transform.gameObject.name == "EstanteriaAleronTrasero")
                    {

                        anim.SetBool("Agarrar", false);
                        tengoAlgoAgarrado = false;
                        aleronTrasero.SetActive(false);
                        ObjetoAgarrado.SetActive(true);

                    }
                    else if (coll[0].transform.gameObject.name == "AleronVacio")
                    {
                        ObjetoAgarrado.SetActive(true);
                        aleronTrasero.SetActive(false);
                        GameObject aleronBueno = coll[0].transform.Find("AleronTraseroBueno").gameObject;
                        Debug.Log(aleronBueno);
                        aleronBueno.GetComponent<MeshRenderer>().enabled = true;
                        scriptGui.cocheActual.GetComponent<Averia>().sumaPiensaTareas--;//me quito una tarea de la lista
                        tengoAlgoAgarrado = false;
                    }

                }
                else if (retrovisor.activeSelf == true)
                {
                    if (coll[0].transform.gameObject.name == "EstanteriaEspejo")
                    {

                        // anim.SetBool("Agarrar", false);
                        tengoAlgoAgarrado = false;
                        retrovisor.SetActive(false);
                        ObjetoAgarrado.SetActive(true);

                    }
                    else if (coll[0].transform.gameObject.name == "RetrovisorRoto")
                    {


                        ObjetoAgarrado.SetActive(true);
                        retrovisor.SetActive(false);
                        GameObject retrovisorBueno = coll[0].transform.Find("RetrovisorBueno").gameObject;
                        retrovisorBueno.GetComponent<MeshRenderer>().enabled = true;
                        coll[0].transform.GetComponent<MeshRenderer>().enabled = false;
                        coll[0].transform.GetComponent<Collider>().enabled = false;
                        scriptGui.cocheActual.GetComponent<Averia>().sumaPiensaTareas--;//me quito una tarea de la lista
                        tengoAlgoAgarrado = false;

                    }
                }




            }


        }







    }
    public void llegoCoche(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (scriptGui.cocheActual.GetComponent<Coche>().llegado)
            {
                scriptGui.cocheActual.GetComponent<Coche>().irse = true;
              // scripSpawmer.desocuparCasilla();
            }

        }


    }
  
   
    void DejarObjeto(int index)
    {
        ObjetoAgarrado.transform.SetParent(null);
        ObjetoAgarrado.transform.eulerAngles = new Vector3(0, 0, 0);
        ObjetoAgarrado.transform.position = scripUbicacionEstanteria.posicionesEstanteria[index].position;//1 es la ubicacion de la estanteria de cajaDeCambios
        ObjetoAgarrado.GetComponent<Collider>().enabled = true;
        tengoAlgoAgarrado = false;

    }
    void ActivarAgarre(GameObject objetoActivar)
    {
        if (objetoActivar.activeSelf)
        {
              objetoActivar.SetActive(false);

        }
        else
        {
            objetoActivar.SetActive(true);
        }

    }
    void desactivarMove()
    {
        

        if (scripTarget.enabled)
        {
            scripTarget.enabled = false;
            anim.SetBool("AgarreEspecial", false);
            pistolaCaja.SetActive(true);
            cajaCambioMano.SetActive(false);
        }
      

    }
    void finalGasofa()
    {
        scriptGui.cocheActual.GetComponent<Averia>().sumaPiensaTareas--;//me quito una tarea de la lista


    }
    
    void activarMove()
    {
        GameObject hijo = auxiliar.transform.Find("RuedaPinchada").gameObject;
        hijo.SetActive(false);
        Collider coll = auxiliar.GetComponent<Collider>();
        Debug.Log(coll);
        coll.enabled = true;
      
        pistolaCaja.SetActive(false);
        cajaCambioMano.SetActive(true);
        scripTarget.velocidad = 9;
        scripTarget.enabled = true;

    }
    void desactivarMOVE()
    {
        sigo = false;
        scripTarget.enabled = false;


    }
    void activa()
    {
        sigo = true;
        scripTarget.velocidad = 9;
        scripTarget.enabled = true;

    }
    void terminoAnimacionDeGasolina()
        {


        sigo = true;
       
       
        anim.SetBool("Gasolina", false);
    }
    public void AudioCorrer()
    {

        //source.Play();
    }
   





}
