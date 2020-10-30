using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxItem=10;
    public int timeBetweenSpawn;
    public List<GameObject> PrefabList = new List<GameObject>();

    public Transform limitSx;
    public Transform limitDx;
    public Transform limitUp;
    public Transform limitLow;

    public List<GameObject> spawnedItem = new List<GameObject>();


    private int countItem = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedItem.Count < maxItem)
        {
            int i = Random.Range(0, PrefabList.Count);

            Vector3 position = new Vector3(Random.Range(limitSx.position.x, limitDx.position.x), 1f, Random.Range(limitUp.position.z, limitLow.position.z));
            spawnedItem.Add(Instantiate(PrefabList[i], position, Quaternion.identity));
            
        }
        foreach (GameObject s in spawnedItem)
        {
            if (s == null)
                spawnedItem.Remove(s);
        }
    }


}
