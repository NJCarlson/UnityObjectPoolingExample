using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// USED TO SPAWN BLOOD POOL SPRITES UPON PARTICLE COLLISION THROUGH OBJECT POOLER.
/// 
/// NOTE : If you want to attatch this to multiple
/// floor objects, you'll want to comment out line 23, and uncomment line 25. 
/// 
/// This script uses a modified version of the object pooler I previously created.
/// The BloodObjectPooler is meant to be the only object pooler in the scene.
/// It is not ideal to have multiple object poolers running at the same time, so you may want 
/// to use the FloorCollider script instead.
/// 
/// Nick Carlson 12/18/2020
/// </summary>
public class BloodPoolCollider : MonoBehaviour
{
    private BloodObjectPooler bloodObjectPooler;
    private List<ParticleCollisionEvent> collisionEvents;
    private int prevEvents = 0;
    private int numCollisionEvents = 0;
    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        bloodObjectPooler = this.gameObject.GetComponent<BloodObjectPooler>();
        // If you attatch this script to multiple objects, you'll want to use the following instead :
        //bloodObjectPooler = FindObjectOfType<BloodObjectPooler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        // Debug.Log("PARTICLEHIT");
        if (other.tag == "Blood")
        {
            ParticleSystem PSystem = other.GetComponent<ParticleSystem>();

            prevEvents = numCollisionEvents;
            numCollisionEvents = PSystem.GetCollisionEvents(this.gameObject, collisionEvents);

            int count = numCollisionEvents - prevEvents;
            for (int i = 0; i < count; i++)
            {

                if (Random.Range(1, 4) % 3 == 0)
                {
                   
                    bloodObjectPooler.SpawnObject(bloodObjectPooler.getRandomBloodSplatterKey(), collisionEvents[i].intersection);
                    // Debug.Log( "i = " + i + " Sprite " + randomSprite + " spawned. prevEvents = " + prevEvents + " curEvents = " + numCollisionEvents);
                }
            }
        }
    }

 
}
