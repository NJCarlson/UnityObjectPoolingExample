using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDestroyTimer : MonoBehaviour
{
    [SerializeField]
    float LifeSpan = 3.0f;
    private float LifeTimer = 0.0f;

    private Rigidbody rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody>();
        LifeTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        LifeTimer += Time.deltaTime;
        if (LifeTimer >= LifeSpan)
        {
            if (rbody != null)
            {
                rbody.velocity = Vector3.zero;
            }

            this.gameObject.SetActive(false);
            LifeTimer = 0.0f;
        }
    }
}
