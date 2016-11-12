using UnityEngine;
using System.Collections;
using System;

public class HumanIAControler : AbstractIAControler {

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    public int wasHit = -1;
    private float timeUntilZombie = 0;
    

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();

        if(wasHit >= 0)
        {
            timeUntilZombie += Time.deltaTime;

            if(timeUntilZombie > 30)
            {
                GameObject newZombie = Instantiate(zombiePrefab);
                newZombie.transform.position = target.transform.position;
                newZombie.GetComponent<ZombieFaction>().faction = (uint)wasHit;
                Destroy(gameObject);
            }
        }
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
            hits = Array.FindAll(hits, (i) => i.transform.gameObject.tag == "Zombie" && i.distance < visionDistance);
            if (hits.Length > 0)
            {
                
                Vector3 bary = new Vector3();
                foreach (RaycastHit hit in hits)
                {
                    bary += /*hit.distance * */(hit.transform.position - transform.position);
                }
                //Debug.Log(bary);
                bary.Normalize();
                GameObject t = new GameObject();
                t.transform.position = transform.position - (bary * 10);
                if(target != null)
                {
                    Destroy(target.gameObject);
                }
                target = t.transform;
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
