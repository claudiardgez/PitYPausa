using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovSucio : MonoBehaviour
{
   
   
    Vector2 movimiento;
    public float velocidad;
    Animator anim;
    [SerializeField] GameObject Jugador;
    Rigidbody rb;
    [SerializeField] InputAction movTeclado;
    [SerializeField]  CanvasMnager scriptCanvas;
    PlayerInput controladorMando;
    [SerializeField] int indiceJugador;
  
    [SerializeField] Animator animJugador;
    AnimatorStateInfo estadoActual;
    //  [SerializeField] AudioSource source;
    // Start is called before the first frame update

    //--------------------------------------------controles para teclado--------------------------------------//
 
    Vector2 movimientoTeclado;
    public InputAction interactuarTeclado;
    public InputAction queSeVayaCocheTeclado;


    private void OnEnable()
    {
        movTeclado.Enable();
        interactuarTeclado.Enable();
        queSeVayaCocheTeclado.Enable();
    }
    void Start()
    {
        anim = Jugador.GetComponent<Animator>();
        rb=GetComponent<Rigidbody>();
        controladorMando=GetComponentInParent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {


        estadoActual = anim.GetCurrentAnimatorStateInfo(0);
        // movimiento = mapaAccion.Accion.Movimiento.ReadValue<Vector2>();
        // transform.Translate(new Vector3(movimiento.x, 0, movimiento.y).normalized * velocidad * Time.deltaTime * -1);
        if (scriptCanvas.jugarConMando)
        {
            controladorMando.enabled = true;

            if (estadoActual.IsName("CajaCambio") || estadoActual.IsName("EcharGasofa"))
            {
                //No te muevas
            }
            else
            {
             rb.velocity = new Vector3(movimiento.x, 0, movimiento.y).normalized * velocidad * -1;
               
            }


        }
        else
        {

          movimientoTeclado = movTeclado.ReadValue<Vector2>();
            controladorMando.enabled = false;
            if (estadoActual.IsName("CajaCambio")||estadoActual.IsName("EcharGasofa"))
            {
                //No te muevas
            }
            else
            {
                 rb.velocity = new Vector3(movimientoTeclado.x, 0,movimientoTeclado.y).normalized * velocidad * -1;

            }
          

        }


        if (movimiento.x!=0||movimiento.y!=0||movimientoTeclado.x!=0||movimientoTeclado.y!=0)
        {
            anim.SetBool("Movimiento", true);
          //  source.Play();
          //  Debug.Log("me muevo");
        }
        else
        {
            anim.SetBool("Movimiento", false);
          //  source.Stop();
            //  Debug.Log("no me muevo");
        }
    }
    public void Movimiento(InputAction.CallbackContext context)
    {
        if (scriptCanvas.jugarConMando)
        {
         movimiento= context.ReadValue<Vector2>();

        }


    }
}
