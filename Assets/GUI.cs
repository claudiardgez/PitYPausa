using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GUI : MonoBehaviour
{
    public float segundos;
    public int segundosAux;
    public int minutos;
    public TextMeshProUGUI segundosText;
    public TextMeshProUGUI minutosText;
    public List<TextMeshProUGUI> listaText;
    public List<TextMeshProUGUI> rankingText;
    public GameObject rankingPestana;
    public List<string> listaTextOrig;
    public List<string> lista;
    public List<string> listaNombresPlayer1;
    public List<string> listaNombresPlayer2;
    public List<bool> listaBool;
    public List<int> listaNumeros;
    public bool startTimer = false;

    public int coche;

    public ControladorDatosJuego guardarScript;
    public int minutos1, minutos2, minutos3, minutos4, segundos1, segundos2, segundos3, segundos4, minutosTotal, segundosTotal;
    public string player1, player2;
    //public List<string> rankings;
    public List<int> minutosRanking; 
    public List<int> minutosRankingOrdenar; 
    public List<int> segundosRankingOrdenar; 
    public List<int> segundosRanking;

    public GameObject cocheActual;
    public GameObject canvaMenu;


    float timer;
    // Start is called before the first frame update
    void Start()
    {

       // Debug.Log("termino espera");
        for (int i = 0; i < lista.Count; i++)
        {
            listaBool.Add(false);
        }
        for (int i = 0; i < lista.Count; i++)
        {
            listaNumeros.Add(0);
        }
        for (int i = 0; i < lista.Count; i++)
        {
            listaTextOrig.Add(lista[i]);
        }
      //  GameObject guardarObj = GameObject.Find("GameManager");
       // guardarScript = guardarObj.GetComponent<ControladorDatosJuego>();
        rankingPestana.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       // scriptCoche = GameObject.Find("Coche F1").GetComponent<Coche>();
        if (startTimer)
        {
            segundos += Time.deltaTime;
            segundosAux = ((int)segundos);

            if (segundosAux<10)
            {
                segundosText.text = "0"+segundosAux.ToString();
            }
            else
            {
                segundosText.text = segundosAux.ToString();
            }
            if (segundosAux == 60)
            {
                minutos++;
                segundos = 0;
                segundosText.text = "00";
            }
            if (minutos<10)
            {
                minutosText.text = "0"+minutos.ToString();
            }
            else
            {
                minutosText.text = minutos.ToString();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coche"))
        {
            Debug.Log("coll");
            // other.transform.gameObject.GetComponent<Coche>().llegado = true;
            cocheActual = other.transform.gameObject;
            startTimer = true;
            coche++;
            Debug.Log(coche);
            int auxNumeroErrores = Random.Range(3, 6);
            for (int i = 0; i < auxNumeroErrores; i++)
            {
                int aux = Random.Range(0, lista.Count);
                listaText[i].text = lista[aux];
                lista.RemoveAt(aux);
                listaBool[aux] = true;
                listaNumeros[aux]++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("coche"))
        {
          Debug.Log("estoy saliendo");
            if (coche == 1)
            {
                minutos1 = minutos;
                segundos1 = segundosAux;
               
            }
            else if (coche == 2)
            {
                minutos2 = minutos;
                segundos2 = segundosAux;
                
            }
            else if (coche == 3)
            {
                minutos3 = minutos;
                segundos3 = segundosAux;
              
            }
            else if (coche == 4)
            {
                minutos4 = minutos;
                segundos4 = segundosAux;

                segundosTotal = (segundos1 + segundos2 + segundos3 + segundos4);
                minutosTotal = (minutos1 + minutos2 + minutos3 + minutos4)*60;
                segundosTotal = segundosTotal + minutosTotal;
                segundosRanking.Add(segundosTotal);
                segundosRankingOrdenar.Add(segundosTotal);
                Debug.Log(segundosTotal);
                segundosRanking.Sort();
               
                coche = 0;

                //for (int i = 0; i < segundosRanking.Count; i++)
                //{
                //    for (int j = 0; j < segundosRanking.Count; j++)
                //    {
                //        if (segundosRanking[i] == segundosRanking[j])
                //        {
                //            segundosRanking[j]++;
                //        }
                //    }
                //}

               

                StartCoroutine(Ending());

            }
            startTimer = false;
            minutos = 0;
            segundosAux = 0;
            segundos = 0;
            segundosText.text = "00";
            minutosText.text = "00";
            for (int i = 0; i < listaText.Count; i++)
            {
                listaText[i].text = " ";
            }
            for (int i = 0; i < listaBool.Count; i++)
            {
                listaBool[i] = false;
            }
            for (int i = 0; i < listaNumeros.Count; i++)
            {
                listaNumeros[i] = 0;
            }
            lista.RemoveRange(0, lista.Count);
            for (int i = 0; i < listaTextOrig.Count; i++)
            {
                lista.Add(listaTextOrig[i]);
            }

        }
            //Debug.Log("Salgo del trigger");
    }

    IEnumerator Ending()
    {
        //bloquear movimiento, hacer animacion de victoriaaaa
        yield return new WaitForSeconds(5f);
        rankingPestana.SetActive(true);
       
        for (int i = 0; i < rankingText.Count; i++)
        {
            if (segundosRanking.Count>=0)
            {
                Debug.Log("A");
                for (int j = 0; j < segundosRankingOrdenar.Count; j++)
                {
                    Debug.Log("b");
                    if (segundosRanking[i] == segundosRankingOrdenar[j])
                    {
                        Debug.Log("PONER COSAS RANKING");
                        //rankingText[i].text = listaNombresPlayer1[j] + " , "+ listaNombresPlayer2[j]+ " || TIEMPO TRANSCURRIDO: "+ segundosRanking[i].ToString() + "s.";
                        rankingText[i].text = " || TIEMPO TRANSCURRIDO: "+ segundosRanking[i].ToString() + "s.";
                    }
                }
                //rankingText[i].text = segundosRanking[i].ToString();
            }
            else
            {
                rankingText[i].text = "";
            }
        }
    }

   
}
