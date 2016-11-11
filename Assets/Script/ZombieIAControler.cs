﻿using UnityEngine;
using System.Collections;
using System;

public class ZombieIAControler : AbstractIAControler {

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();
    }

    protected override void Attack()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            int angle = 25;
            Vector3 pushForce;
            do
            {
                pushForce = GetBallisticVelocity((target.position - transform.position).normalized * 10000f, angle);
                angle--;
            } while (float.IsNaN(pushForce.x) || float.IsNaN(pushForce.y) || float.IsNaN(pushForce.z));

            if (angle <= 0) return;
            target.gameObject.GetComponent<Rigidbody>().AddForce(pushForce, ForceMode.Impulse);
            //target.GetComponent<BarbieLife>().decreaseLife(1);

            /*SoundOnAttack soa = GetComponent<SoundOnAttack>();

            if (soa != null)
            {
                soa.Play();
            }*/
        }
    }

    protected override void ComputeIA()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, visionDistance, transform.forward);

        if (hits.Length > 0)
        {
            hits = Array.FindAll(hits, (i) => i.transform.gameObject.tag == "Human" && i.distance < visionDistance);
            if (hits.Length > 0)
            { 
                Array.Sort(hits, (i1, i2) => i1.distance.CompareTo(i2.distance));
                target = hits[0].transform;
                seeTargetable = true;
            }
            else
            {
                if (target != null && seeTargetable)
                {
                    target = null;
                    seeTargetable = false;
                }
            }
        }

        if (target == null)
        {
            GameObject t = new GameObject();
            t.transform.position = new Vector3((float)(randEngine.NextDouble() - 0.5) * roamingDistance, 0, (float)(randEngine.NextDouble() - 0.5) * roamingDistance) + transform.position;
            target = t.transform;
            seeTargetable = false;
        }
        else
        {
            float distance = Vector3.Distance(transform.position, target.position);

            UpdateNavMesh();

            if (distance < minDistance)
            {
                if (!seeTargetable)
                {
                    Destroy(target.gameObject);
                }
                target = null;
            }
        }
    }
}