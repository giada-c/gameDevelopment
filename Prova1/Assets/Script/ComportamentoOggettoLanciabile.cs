using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamentoOggettoLanciabile : MonoBehaviour
{
   
    public int type;
   

    public int danno;

    private bool terra=true;

    private Transform portaOggetto;


    public float GroundDistance = 0.0f;
    public LayerMask Ground;
    public Transform _groundChecker;

    public GameObject barraVita;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        terra = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        float vita = barraVita.GetComponent<BarraVitaOggettoLanciabile>().GetHealth();
        if (vita <= 0 && terra)
        {
            Destroy(gameObject);
        }
    }

   public int getDanno()
    {
        return danno;
    }
    public bool isTerra()
    {
        return terra;
    }
    public void SetPortaOggetto(Transform p)
    {
        portaOggetto = p;
    }

    public void lancia(Vector3 tra, float force)
    {
        terra = false;
        Debug.Log("yeet");
        gameObject.GetComponent<Renderer>().enabled = true;
        this.transform.position = portaOggetto.transform.position;
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce((tra) * 30, ForceMode.Impulse);
        barraVita.gameObject.GetComponent<BarraVitaOggettoLanciabile>().TakeDamage(1);


    }

    public int getType()
    {
        return type;
    }
}
