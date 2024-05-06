using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Averia : MonoBehaviour
{
    [SerializeField] List<string> stringErrores ;
    [SerializeField] List<GameObject> piezasDañadas ;
    [SerializeField] List<GameObject> piezasBuenas;
    int nuRan1, nuRan2, nuRan3, nuRan4;
   public  int sumaPiensaTareas;



    [SerializeField] Coche scriptCoche;
    
    
    
    

    
    
    // Start is called before the first frame update
    void Start()
    {
      



        for (int i = 0; i < piezasBuenas.Count; i++)
        {
            nuRan1 = Random.Range(0, piezasBuenas.Count);//escojo un numero random para los problemas
            nuRan2 = Random.Range(0, piezasBuenas.Count);//escojo un numero random para los problemas
            nuRan3 = Random.Range(0, piezasBuenas.Count);//escojo un numero random para los problemas
            nuRan4 = Random.Range(0, piezasBuenas.Count);//escojo un numero random para los problemas



            if (i==nuRan1|| i == nuRan2|| i == nuRan3||i==nuRan4)
            {
                piezasBuenas[i].SetActive(false);
                piezasDañadas[i].SetActive(true);
                sumaPiensaTareas++;

            }
          
          else  
            {
                piezasBuenas[i].SetActive(true);
                piezasDañadas[i].SetActive(false);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sumaPiensaTareas==0)
        {
            scriptCoche.irse = true;
           

        }
       
    }
}
