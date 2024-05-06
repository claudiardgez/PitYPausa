using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animacionRuedas : MonoBehaviour
{
    Animator anim;
    Coche coche;

    // Start is called before the first frame update
    void Start()
    {
      coche = GetComponentInParent<Coche>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coche.llegado)
        {
            anim.SetBool("Conduciendo", false);
        }
        else
        {
            anim.SetBool("Conduciendo", true);
        }
    }
}
