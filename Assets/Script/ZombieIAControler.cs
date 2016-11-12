using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

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
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Entity e = target.GetComponent<Entity>();
            e.Hit();
            HumanIAControler ia = GetComponentInChildren<HumanIAControler>();
            if(ia != null)
            {
                ia.wasHit = (int)GetComponent<ZombieFaction>().faction;
            }
            if (e.PV == 0)
            {
                Vector3 newpos;
                NavMeshHit closestHit;
                if (NavMesh.SamplePosition(target.position, out closestHit, 500, 1))
                {
                    newpos = closestHit.position;
                }
                else
                {
                    newpos = target.position;
                }
                GameObject newZombie = Instantiate(zombiePrefab, newpos, Quaternion.identity) as GameObject;
                newZombie.transform.position = target.transform.position;
                newZombie.GetComponent<ZombieFaction>().SetFaction(gameObject.GetComponent<ZombieFaction>().faction);
                FactionManager.Instance.RemoveEntity(target.gameObject);
                FactionManager.Instance.AddEntity(gameObject.GetComponent<ZombieFaction>().faction, newZombie);
                if (target.gameObject.GetComponent<ZombieFaction>() != null && target.gameObject.GetComponent<ZombieFaction>().IsBoss)
                {
                    FactionManager.Instance.Wololo(target.gameObject, gameObject.GetComponent<ZombieFaction>().faction);
                }
                if (target.gameObject.GetComponentInChildren<PlayerControler>())
                {
                    SceneManager.LoadScene("GameOver");
                }
                Destroy(target.gameObject);
                target = null;
            }

            /*int angle = 25;
            Vector3 pushForce;
            do
            {
                pushForce = GetBallisticVelocity((target.position - transform.position).normalized, angle);
                angle--;
            } while (float.IsNaN(pushForce.x) || float.IsNaN(pushForce.y) || float.IsNaN(pushForce.z));

            if (angle <= 0) return;
            target.gameObject.GetComponent<Rigidbody>().AddForce(pushForce, ForceMode.Impulse);*/
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
        if (!seeTargetable)
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, visionDistance, transform.forward);

            if (hits.Length > 0)
            {
                hits = Array.FindAll(hits, (i) => ((i.transform.gameObject.tag == "Human") || (i.transform.gameObject.tag == "Zombie" && i.transform.gameObject.GetComponent<ZombieFaction>().faction != gameObject.GetComponent<ZombieFaction>().faction)) && i.distance < visionDistance);
                if (hits.Length > 0)
                {
                    Array.Sort(hits, (i1, i2) => -i1.distance.CompareTo(i2.distance));
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
        }

        if (target == null)
        {
            GameObject t = new GameObject();
            t.transform.position = new Vector3((float)(randEngine.NextDouble() - 0.5) * roamingDistance, transform.position.y, (float)(randEngine.NextDouble() - 0.5) * roamingDistance) + transform.position;
            target = t.transform;
            seeTargetable = false;
        }
        else
        {
            float distance = Vector3.Distance(transform.position, target.position);

            UpdateNavMesh();

            if(seeTargetable)
            {
                if (distance < attackDistance && !target.GetComponent<Entity>().isHit)
                {
                    Attack();
                }
            }
            else
            {
                if (distance < minDistance)
                {
                    Destroy(target.gameObject);
                    target = null;
                }
            }

            
        }
    }
}
