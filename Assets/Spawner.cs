
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject prefabCoche;

    public List<GameObject> cocheSprite;
    public int cochesPasados = 0;
    public int cochesAux = 0;
    Coroutine llamada;
    [SerializeField] CanvasMnager Canvasmanager;

    // Start is called before the first frame update
    void Start()
    {
            LlamadaCorrutina();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Canvasmanager.puedoEmpezarAjugar)
        //{
        //    Debug.Log("comienzooo");
        //    StartCoroutine(SpawnCoche());

        //}
    }

    IEnumerator SpawnCoche()
    {
        int aux = 0;
        //cochesPasados = 0;
        cochesAux = 0;
        cochesPasados = 0;
        while (aux <= 3)
        {
            //Debug.Log("Corrutina");
            yield return new WaitForSeconds(1f);
            if (aux == 0)
            {
                Instantiate(prefabCoche, transform.position, Quaternion.Euler(0, -90, 0));
                //cochesAux;
                aux++;
            }
            else if (cocheSprite[cochesAux].GetComponent<Image>().color == Color.red|| cocheSprite[cochesAux].GetComponent<Image>().color == Color.green)
            {
                Instantiate(prefabCoche, transform.position, Quaternion.Euler(0, -90, 0));
                cochesAux++;
                aux++;
            }
            //Debug.Log(aux + " AUXXX CORRUTINA DE MIERDA");
        }
        // cocheCount++;
    }
    public void desocuparCasilla()
    {

        for (int i = 0; i < cocheSprite.Count; i++)
        {
            cocheSprite[i].GetComponent<Image>().color = Color.white;
        }
    }
    void LlamadaCorrutina()
    {
        if (llamada!=null)//si tengo una ocrrutina activa..
        {
            StopAllCoroutines();//paralas todas
        }
        llamada = StartCoroutine(SpawnCoche());

    }
}
