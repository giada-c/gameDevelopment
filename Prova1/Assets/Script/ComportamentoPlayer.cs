using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamentoPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject barraVita;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        
       
        //Debug.Log("OnTriggerEnter-->"+ other.gameObject.tag);
        if (other.gameObject.tag == "raccoglibile")
        {
            Debug.Log(other.gameObject.tag + "Terra-- >" + other.gameObject.GetComponent<ComportamentoOggettoLanciabile>().isTerra());
            if (!other.gameObject.GetComponent<ComportamentoOggettoLanciabile>().isTerra())
                danno(other.gameObject.GetComponent<ComportamentoOggettoLanciabile>().getDanno());
        }
        if (other.gameObject.tag == "Damage") {
            danno(7);
        }
    }
    public void danno(int danno)
    {
        Debug.Log("DANNO");
        barraVita.GetComponent<BarraVitaPlayer>().TakeDamage(danno);
        //GetComponent<Material>().color = Color.red;
    }
}
