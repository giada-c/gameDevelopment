using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
public enum InventoryState { 
FULL, EMPTY, OTHER
}
public class Inventario
{
    
    private Stack<GameObject> inventario = new Stack<GameObject>();
    private int maxItem=2;
    public Inventario(int mi) {
        maxItem = mi;
    }
    public void addItem(GameObject g) {
        if (inventario.Count < maxItem)
            inventario.Push(g);
        

    }
    public GameObject removeItem() {
        
        if (inventario.Count > 0)
        {

            return inventario.Pop();  
        }
        return null;

    }

    public void print()
    {
        foreach (GameObject go in inventario)
        {
            Debug.Log("-" + go + "-");
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
