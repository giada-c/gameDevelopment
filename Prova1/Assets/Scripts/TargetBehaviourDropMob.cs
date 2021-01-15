using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;


public class TargetBehaviourDropMob : MonoBehaviour
{
    public float hitTime = 2f;
    public int life = 5;
    public GameObject objectToDrop;

    public AudioSource audioSource;
    public MusicManager audioManager;

    public Renderer[] targetRenderers;
    private bool _isHit = false;
    private bool _isBleeding = false;
    private float _hitTimer = 0f;

    private bool isOnDestroy = false;

    void Awake()
    {
        if (targetRenderers == null || targetRenderers.Length == 0)
        {
            targetRenderers = GetComponentsInChildren<MeshRenderer>();
        }
    }


    IEnumerator beforeDestroy(float audioLenght)
    {
        yield return new WaitForSeconds(audioLenght);
        Debug.Log("MUORI e SPARISCI");
        Instantiate(objectToDrop, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Update()
    {
        //Conrol if the mob still alive
        if (life <= 0 && !isOnDestroy)
        {
            isOnDestroy = true;

                //audioSource.PlayOneShot(audioSource.clip, audioManager.ambientVolume);
            //sStartCoroutine(beforeDestroy(audioSource.clip.length));
            StartCoroutine(beforeDestroy(1));
        }


        if (_isHit && !_isBleeding) // Register hit and start bleeding state
        {
            _isHit = false;
            _isBleeding = true;
            /*foreach (var r in targetRenderers)
            {
                foreach (var m in r.materials)
                {
                    m.color = Color.red;
                }
            }*/
            _hitTimer = hitTime;
        }
        else if (_hitTimer > 0f) // While bleeding
        {
            _hitTimer -= Time.deltaTime;
        }
        else if (_isBleeding) // Stop bleeding and return to normal state
        {
            _isBleeding = false;
            /*foreach (var r in targetRenderers)
            {
                foreach (var m in r.materials)
                {
                    m.color = Color.white;
                }
            }*/
        }
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "raccoglibile")
        {

            Debug.Log("Danno");
            danno(collision.gameObject.GetComponent<ComportamentoOggettoLanciabile>().getDanno());
        }
    }

    public void danno(int danno)
    {
        life -= danno;
       // barraVita.GetComponent<BarraVitaPlayer>().TakeDamage(danno);
        Messenger<string>.Broadcast(GameEvent.ENEMY_HIT, GameEvent.ENEMY_HIT, MessengerMode.DONT_REQUIRE_LISTENER);

       // Debug.Log(audioManager.getVolumeAmbient());
        //Debug.Log(audioManager.ambientVolume);
        //if (audioManager.ambientOn) 
           // audioSource.PlayOneShot(audioSource.clip, audioManager.ambientVolume);           


    }

    public void setAudioManager( MusicManager audio)
    {
        audioManager = audio;
    }
}