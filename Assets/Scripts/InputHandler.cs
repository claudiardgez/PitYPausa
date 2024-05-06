using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputHandler : MonoBehaviour
{

    private PlayerInput playerInput;
    private Character scriptJugador;
    // Start is called before the first frame update
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        Character script = FindObjectOfType<Character>();
        int index = playerInput.playerIndex;
       
        //scriptJugador=script

           
           


    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
