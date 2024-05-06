using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coche : MonoBehaviour
{
    public GameObject pit;
    public GameObject adios;
    public bool llegado = false;
    public bool irse = false;

    public List<GameObject> cocheSprite;
    public Spawner spawnerScript;
    [SerializeField] Animator anim;
    [SerializeField] Averia scriptAveria;
    // Start is called before the first frame update
    void Start()
    {
        pit = GameObject.Find("PitStop");
        adios = GameObject.Find("Destruccion");
        GameObject spawner = GameObject.Find("Spawner");
        spawnerScript = spawner.GetComponent<Spawner>();
        RellenoCasillas();
        anim = GameObject.Find("padrebarreras").GetComponent<Animator>();
      

        //cocheSprite = GameObject.FindGameObjectsWithTag("Sprite");
    }

    // Update is called once per frame
    void Update()
    {
        if (!llegado)
        {
           
            StartCoroutine(PitStop());
        }
        if (llegado==false)
        {
            anim.SetBool("Subir", true);
        }
        else
        {
            anim.SetBool("Subir", false);
        }
        //Vector3 position = transform.position;
        //if (Input.GetKeyDown(KeyCode.Space) && llegado)
        //{
        //    //Debug.Log("Espacio");
        //    irse = true;

        //}

        if (irse)
        {
            anim.SetBool("Subir", true);
            transform.position = Vector3.MoveTowards(transform.position, adios.transform.position, 10 * Time.deltaTime);
            if (transform.position == adios.transform.position)
            {
                if (spawnerScript.cochesPasados ==4)
                {
                    //stop
                    Debug.Log(spawnerScript.cochesPasados);

                }
                else
                {
                    Debug.Log(spawnerScript.cochesPasados);
                    if (scriptAveria.sumaPiensaTareas==0)
                     cocheSprite[spawnerScript.cochesPasados].GetComponent<Image>().color = Color.green;
                    else
                    {
                        cocheSprite[spawnerScript.cochesPasados].GetComponent<Image>().color = Color.red;
                    }
                    
                        
                    

                }

                spawnerScript.cochesPasados++;
                Destroy(this.gameObject);
            }
        }
        //transform.position = Transform.forward 
    }

    IEnumerator PitStop()
    {
        transform.position = Vector3.MoveTowards(transform.position, pit.transform.position, 10 * Time.deltaTime);
        yield return new WaitForSeconds(4f);
        //QUE NOOOOOOOO
        llegado = true;
    }
    void RellenoCasillas()
    {
        for (int i = 0; i < cocheSprite.Count; i++)
        {
            cocheSprite[i] = GameObject.Find("coche" + (i + 1));
        }


    }
    public void MandarCoche()
    {
        if (llegado)
        {
            irse = true;
        }

    }
}
