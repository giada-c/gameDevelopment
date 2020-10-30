using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabZone : MonoBehaviour
{
    // Start is called before the first frame update



    public Transform portaOggetto;

    public float force = 50;
   private  GameObject player;
    private GameObject raccolto = null;
    private bool possoRaccogliere = false;
    private GameObject possibileRaccolta;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (raccolto != null && (Input.GetKeyUp(KeyCode.Mouse1)|| Input.GetKeyUp(KeyCode.Joystick1Button2))) {
            //Debug.Log("POSSO LANCIARE");
            //raccolto.gameObject.SetActive(true);
            //Vector3 v = player.GetComponent<Rigidbody>().velocity;
            
            raccolto.GetComponent<ComportamentoOggettoLanciabile>().lancia(transform.forward,force);
           
            

            raccolto = null;
            //Debug.Log("YEEEET");
        }

        else if (possoRaccogliere && (Input.GetKeyDown(KeyCode.Mouse1)|| Input.GetKeyDown(KeyCode.Joystick1Button2)))
        {
            //possibileRaccolta.SetActive(false);
            raccolto = possibileRaccolta;
            raccolto.GetComponent<ComportamentoOggettoLanciabile>().SetPortaOggetto(portaOggetto);
            raccolto.GetComponent<Renderer>().enabled = false;

        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.gameObject.tag == "raccoglibile" && other.gameObject.GetComponent<ComportamentoOggettoLanciabile>().isTerra() )
        {
            //Debug.Log("POSSO RACCOGLIERE");
            possoRaccogliere = true;
            possibileRaccolta = other.gameObject;
            
        }
    }
    void OnTriggerExit(Collider other)
    {
       
            //Debug.Log(" NON POSSO RACCOGLIERE");
            possoRaccogliere = false;
            possibileRaccolta = null;
        
    }
}
