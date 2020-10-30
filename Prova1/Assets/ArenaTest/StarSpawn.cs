using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawn : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject Player1;
    GameObject Player2;
    GameObject Player3;
    GameObject Player4;
    public GameObject PlayerPrefab;
    public Transform SpawnPlayer1;
    public Transform SpawnPlayer2;
    public Transform SpawnPlayer3;
    public Transform SpawnPlayer4;
    void Start()
    {


        Player1 = Instantiate(PlayerPrefab, SpawnPlayer1.transform.position, SpawnPlayer1.transform.rotation );
        Player2 = Instantiate(PlayerPrefab, SpawnPlayer2.transform.position,SpawnPlayer2.transform.rotation);
        //Player3 = Instantiate(PlayerPrefab, SpawnPlayer3.transform.position, SpawnPlayer3.transform.rotation );
        //Player4 = Instantiate(PlayerPrefab, SpawnPlayer4.transform.position,SpawnPlayer4.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
