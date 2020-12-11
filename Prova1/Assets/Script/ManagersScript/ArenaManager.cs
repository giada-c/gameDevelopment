using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour, IGameManager
{

    public ManagerStatus _Status { get; set; } = ManagerStatus.SHUTDOWN;
    public int nPlayers = 1;
    public int nItems = 20;
    public int nEnemies = 10;

    //PLayer
    [Space] 
    [Header("PLAYER")]
    public List<Transform> spawnAreaPlayer= new List<Transform>();
    public List<GameObject> players= new List<GameObject>();
    public GameObject playerPrefab;

    //Item 
    [Space]
    [Header("ITEM")]
    public List<GameObject> spawnedItem = new List<GameObject>();
    [Tooltip("SX, DX, UP, DOWN")]
    public List<Transform> AreaSpawnItem = new List<Transform>();//Sx,dx,up,down
    public List<GameObject> itemPrefabList = new List<GameObject>();

    //Enemy
    [Space]
    [Header("ENEMY")]
    public List<GameObject> spawnedEnemies = new List<GameObject>();
    [Tooltip("SX, DX, UP, DOWN")]
    public List<Transform> AreaSpawnEnemies = new List<Transform>();//Sx,dx,up,down
    public List<GameObject> enemiesPrefabList = new List<GameObject>();
    public void Startup()
    {
        _Status = ManagerStatus.INITIALIZING;
        posizionaPlayer();
        posizionaItems();
        posizionaEnemies();
        _Status = ManagerStatus.STARTED;
    }

    void Update()
    {
        controlloItems();
       controlloEnemies();
    }

    void posizionaPlayer()
    {
       playerPrefab = Resources.Load<GameObject>("Player/Player-health");
        for (int i = 0; i < nPlayers; i++)
        { 
            GameObject t;
            t= Instantiate(playerPrefab, spawnAreaPlayer[i].transform.position, spawnAreaPlayer[i].transform.rotation);
            players[i] = t;
        }
    }

    void posizionaItems()
    {
        itemPrefabList.Add(Resources.Load<GameObject>("Item/Item0"));
        itemPrefabList.Add(Resources.Load<GameObject>("Item/Item1"));
        itemPrefabList.Add(Resources.Load<GameObject>("Item/Item2"));
        itemPrefabList.Add(Resources.Load<GameObject>("Item/Item3"));
        itemPrefabList.Add(Resources.Load<GameObject>("Item/Item4"));
        while (spawnedItem.Count < nItems)
            creaItem();

    }
    void controlloItems() {
        if (spawnedItem.Count < nItems)
            creaItem();

        foreach (GameObject s in spawnedItem){
            if (s == null)
                spawnedItem.Remove(s);
        }
    }
    void creaItem()
    {
        int r = Random.Range(0, itemPrefabList.Count);
        Vector3 position = new Vector3(Random.Range(AreaSpawnItem[0].position.x, AreaSpawnItem[1].position.x), 1f, Random.Range(AreaSpawnItem[2].position.z, AreaSpawnItem[3].position.z));
        spawnedItem.Add(Instantiate(itemPrefabList[r], position, Quaternion.identity));
    }

    void posizionaEnemies()
    {
        enemiesPrefabList.Add(Resources.Load<GameObject>("Enemy/Enemy0"));
        enemiesPrefabList.Add(Resources.Load<GameObject>("Enemy/Enemy1"));
        enemiesPrefabList.Add(Resources.Load<GameObject>("Enemy/Enemy2"));
        for (int i = 0; i < nEnemies; i++)
            creaEnemy();
    }
    void controlloEnemies()
    {
        if (spawnedEnemies.Count < nEnemies)
            creaEnemy();

        foreach (GameObject s in spawnedEnemies){
            if (s == null)
                spawnedEnemies.Remove(s);
        }
    }

    void creaEnemy()
    {
        int r = Random.Range(0, enemiesPrefabList.Count);
        Vector3 position = new Vector3(Random.Range(AreaSpawnEnemies[0].position.x, AreaSpawnEnemies[1].position.x), 1f, Random.Range(AreaSpawnEnemies[2].position.z, AreaSpawnEnemies[3].position.z));
        spawnedEnemies.Add(Instantiate(enemiesPrefabList[r], position, Quaternion.identity));
    }

}
