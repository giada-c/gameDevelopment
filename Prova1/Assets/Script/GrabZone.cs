using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabZone : MonoBehaviour
{
    // Start is called before the first frame update

    int maxItem = 3;
    private Inventario inventario;

    [SerializeField]
    private Transform portaOggetto;

    [SerializeField]
    private GameObject drawInventory;

    public float force = 50;
    private  GameObject player;
    private GameObject raccolto = null;
    private bool possoRaccogliere = false;
    private GameObject possibileRaccolta;

    public List<RawImage> image = new List<RawImage>();
    public Texture2D tx;

    bool visibile = false;
    private void Start()
    {
        inventario = new Inventario(maxItem);
        tx = Resources.Load<Texture2D>("Texture/Cassa1");
        for (int i = 0; i < maxItem; i++)
        {
            image.Add(drawInventory.transform.GetChild(i).GetComponent<RawImage>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Lancia
        if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.Joystick1Button2))
        {
            Debug.Log("YEEEET");
            if (inventario.getState() != InventoryState.EMPTY)
            {
                Debug.Log("YEEEET1");
                GameObject g = inventario.removeItem();
                if (g != null)
                {
                    g.SetActive(true);
                    g.GetComponent<ComportamentoOggettoLanciabile>().lancia(transform.forward, force);
                   
                    
                }
                
            }
            
        }

        if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.Joystick1Button2)) {
            Debug.Log("RACCOLTA");
            if (possoRaccogliere  && inventario.getState() != InventoryState.FULL)
            {
                Debug.Log("RACCOLTA1");
                possibileRaccolta.GetComponent<ComportamentoOggettoLanciabile>().SetPortaOggetto(portaOggetto);
                GameObject g = possibileRaccolta;
                inventario.addItem(g);
                g.SetActive(false);
                //Destroy(possibileRaccolta);
            }
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (visibile)
            {
                for (int i = 0; i < maxItem; i++)
                {
                    image[i].enabled = true;
                    image[i].texture = tx;
                }
                visibile = !visibile;
            }
            else
            {
                for (int i = 0; i < maxItem; i++)
                {

                    image[i].enabled = false;
                }
                visibile = !visibile;

            }
        }
            

        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "raccoglibile" && other.gameObject.GetComponent<ComportamentoOggettoLanciabile>().isTerra() )
        {
            possoRaccogliere = true;
            possibileRaccolta = other.gameObject;
            
        }
    }
    void OnTriggerExit(Collider other)
    {
            possoRaccogliere = false;
            possibileRaccolta = null;
        
    }
}
