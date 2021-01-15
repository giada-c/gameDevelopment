using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public enum InventoryState { 
FULL, EMPTY, OTHER
}
public class Inventario
{
   public GameObject drawInventory;
    private Stack<GameObject> inventario = new Stack<GameObject>();
    private List<Texture2D> textures = new List<Texture2D>();
    private List<RawImage> image;
    Texture2D bordo, vuoto;
    private bool visibile = false;
    private int maxItem=2;
    public Inventario(int mi,List<RawImage> im, GameObject drawInventory) {
        maxItem = mi;
        image = im;
        for (int i = 0; i < maxItem; i++)
        {
            image.Add(drawInventory.transform.GetChild(i).GetComponent<RawImage>());
            print();
        }


        textures.Add(Resources.Load<Texture2D>("Texture/0b"));
        textures.Add(Resources.Load<Texture2D>("Texture/1b"));
        textures.Add(Resources.Load<Texture2D>("Texture/2b"));
        textures.Add(Resources.Load<Texture2D>("Texture/3b"));
        textures.Add(Resources.Load<Texture2D>("Texture/4b"));
        bordo = Resources.Load<Texture2D>("Texture/bordo");
        vuoto = Resources.Load<Texture2D>("Texture/vuoto");

    }
    public void addItem(GameObject g) {
        if (inventario.Count < maxItem)
        {
            inventario.Push(g);
            print();
        }

    }
    public GameObject removeItem() {
        
        if (inventario.Count > 0)
        {
           GameObject g= inventario.Pop();
            print();
            return g;
        }
        return null;

    }
    public void showInventory()
    {
        visibile = !visibile;
        
            for (int i = 0; i < maxItem; i++)
            {
            if (visibile)
            {
                print();
            }
            else
                image[i].enabled = false;
            }
       
        
    }
    public void print()
    {
        if (visibile)
        {
            for (int i = 0; i < maxItem; i++)
            {
                image[i].enabled = true;
                image[i].texture = bordo;
            }

            for (int i = 0; i < inventario.Count; i++)
            {
                image[i].enabled = true;
                int p = inventario.ToArray()[i].gameObject.GetComponent<ComportamentoOggettoLanciabile>().getType();
                image[i].texture = textures[p];
            }

        }
        else
        {
            for (int i = 0; i < maxItem; i++)
            {
                image[i].enabled = false;
            }

        }
        
    }
    public InventoryState getState()
    {
        if (inventario.Count == 0)
            return InventoryState.EMPTY;
        if (inventario.Count == maxItem)
            return InventoryState.FULL;
        return InventoryState.OTHER;
    }

    
}
