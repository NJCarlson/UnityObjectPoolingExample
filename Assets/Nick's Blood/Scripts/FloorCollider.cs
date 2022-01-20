using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FloorCollider : MonoBehaviour
{
    
//    public string[] BloodSplatterKeys;
//    private ObjectPooler objectPooler;
//    private List<ParticleCollisionEvent> collisionEvents;
//    private int prevEvents = 0;
//    private int numCollisionEvents = 0;
//    // Start is called before the first frame update
//    void Start()
//    {
//        collisionEvents = new List<ParticleCollisionEvent>();
//        objectPooler = FindObjectOfType<ObjectPooler>();
//        BloodSplatterKeys = new string[5];
//        BloodSplatterKeys[0] = "BloodSplatter1";
//        BloodSplatterKeys[1] = "BloodSplatter2";
//        BloodSplatterKeys[2] = "BloodSplatter3";
//        BloodSplatterKeys[3] = "BloodSplatter4";
//        BloodSplatterKeys[4] = "BloodSplatter5";
//
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//
//    }
//
//    void OnParticleCollision(GameObject other)
//    {
//        // Debug.Log("PARTICLEHIT");
//        if (other.tag == "Blood")
//        {
//            ParticleSystem PSystem = other.GetComponent<ParticleSystem>();
//
//            prevEvents = numCollisionEvents;
//            numCollisionEvents = PSystem.GetCollisionEvents(this.gameObject, collisionEvents);
//
//            int count = numCollisionEvents - prevEvents;
//            for (int i = 0; i < count; i++)
//            {
//
//                if (Random.Range(1, 4) % 3 == 0)
//                {
//                    int randomSprite = Random.Range(0, BloodSplatterKeys.Length);
//                    objectPooler.SpawnObject(BloodSplatterKeys[randomSprite], collisionEvents[i].intersection);
//                    // Debug.Log( "i = " + i + " Sprite " + randomSprite + " spawned. prevEvents = " + prevEvents + " curEvents = " + numCollisionEvents);
//                }
//            }
//        }
//    }
//

}
