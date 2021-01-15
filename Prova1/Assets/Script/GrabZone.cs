using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabZone : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject drawInventory;
    public Transform portaOggetto;
    public float force = 50;
    public GameObject player;

   
    private bool possoRaccogliere = false;
    private GameObject possibileRaccolta;

    private bool possoMangiare = false;
    private GameObject possibileMangiata;

    public List<RawImage> image = new List<RawImage>();
    private Inventario inventario;
   
    void Start()
    {
        inventario = new Inventario(3, image, drawInventory);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.Joystick1Button3))
        {
            
             if (inventario.getState() != InventoryState.EMPTY)
             {
                 
                 GameObject g = inventario.removeItem();
                 if (g != null)
                 {
                     g.SetActive(true);
                     g.GetComponent<ComportamentoOggettoLanciabile>().lancia(transform.forward, force);}
                 }
            }

            if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                if (possoRaccogliere && inventario.getState() != InventoryState.FULL)
                {
                    
                    possibileRaccolta.GetComponent<ComportamentoOggettoLanciabile>().SetPortaOggetto(portaOggetto);
                    GameObject g = possibileRaccolta;
                    inventario.addItem(g);
                    g.SetActive(false);
                    //Destroy(possibileRaccolta);
                    possoRaccogliere = false;
                    possibileRaccolta = null;
                }
                if (possoMangiare)
                {
                    player.GetComponent<ComportamentoPlayer>().barraVita.GetComponent<BarraVitaPlayer>().GainHealth(possibileMangiata.GetComponent<Food>().getValue());
                    possoMangiare = false;
                    Destroy(possibileMangiata);
                    possibileMangiata = null;
                }
            }
            if (Input.GetKeyUp(KeyCode.I) ||(Input.GetKeyUp(KeyCode.Joystick1Button5)))
            {
            
            inventario.showInventory();
            }
        
    }
    void OnTriggerEnter(Collider other)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.gameObject.tag == "raccoglibile" && other.gameObject.GetComponent<ComportamentoOggettoLanciabile>().isTerra() )
        {
           
            possoRaccogliere = true;
            possibileRaccolta = other.gameObject;  
        }

        if (other.gameObject.tag == "mangiabile")
        {
            possoMangiare = true;
            possibileMangiata = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
       
            Debug.Log(" NON POSSO RACCOGLIERE");
            possoRaccogliere = false;
            possibileRaccolta = null;

        possoMangiare = false;
        possibileMangiata = null;

    }

    public void setDrawInventory(GameObject d)
    {
        drawInventory = d;
    }
}
