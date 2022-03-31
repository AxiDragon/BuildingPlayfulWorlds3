using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarShaderChange : MonoBehaviour
{
    GameObject player;
    Material material;
    Animator anim;
    
    //float proximity, startProximity,
    //float proximityMultiplier = 15f;
    float force;
    float forceMultiplier = 10f;

    //Vector3 referencePosition;

    void Start()
    {
        foreach (GameObject possiblePlayer in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (possiblePlayer.name == "Capsule")
            {
                player = possiblePlayer;
                break;
            }
        }

        //foreach (Transform child in transform)
        //{
        //    if (child.name == "ProximityPoint")
        //    {
        //        referencePosition = child.gameObject.transform.position;
        //        break;
        //    }
        //}

        //referencePosition = GetComponent<Collider>().transform.position;
        material = GetComponent<Renderer>().material;
        anim = GetComponent<Animator>();

        //startProximity = material.GetFloat("_Proximity");
        force = Vector3.Magnitude(transform.lossyScale) * forceMultiplier;
    }

    void FixedUpdate()
    {
        //proximity = 1f / (1f + Vector3.Magnitude(player.transform.position - referencePosition)) * proximityMultiplier;
        //material.SetFloat("_Proximity", proximity);

        //if (name == "SM_Pillar_1")
        //    print(proximity);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            anim.SetTrigger("_PillarHit");

            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;

            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force);
        }
    }
}
