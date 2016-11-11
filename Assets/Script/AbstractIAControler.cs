using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Assertions;

public abstract class AbstractIAControler : MonoBehaviour {

    protected System.Random randEngine;

    protected bool seeTargetable = false;

    public float visionDistance = 100;
    public float roamingDistance = 10;
    public float maxDistance = 10;
    public float minDistance = 0.1f;
    
    public float attackDistance;

    protected float distanceBetweenTarget = float.MaxValue;

    protected Transform target;

    private NavMeshPath path;
    private float elapsed = 0.0f;

    public bool hasSawTarget = false;

    protected NavMeshAgent agent;

    private bool stop = false;

    // Use this for initialization
    protected virtual void Start ()
    {
        randEngine = new System.Random();
        path = new NavMeshPath();
        elapsed = 0.0f;

        agent = GetComponent<NavMeshAgent>();

        SanitizeAttribute();
    }
	
	// Update is called once per frame
	protected virtual void Update ()
    {

        if (!stop)
        {
            ComputeIA();
        }
        else
        {
            agent.Stop();
        }
    }

    public void Stop()
    {
        stop = true;
    }

    protected abstract void ComputeIA();

    protected virtual void SanitizeAttribute()
    {
        Assert.IsTrue(minDistance < maxDistance);
        Assert.IsTrue(attackDistance >= minDistance && attackDistance < maxDistance);
    }

    protected void UpdateNavMesh()
    {
        // Update the way to the goal every second.
        elapsed += Time.deltaTime;
        if (elapsed > 0.1f)
        {
            elapsed -= 1.0f;
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
            agent.SetPath(path);
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    }
    
    protected Vector3 GetBallisticVelocity(Vector3 targetVec, float angle)
    {
        Vector3 dir = targetVec - transform.position;

        float h = dir.y;

        dir.y = 0;

        float dist = dir.magnitude;

        float a = angle * Mathf.Deg2Rad;

        dir.y = dist * Mathf.Tan(a);

        dist += h / Mathf.Tan(a);

        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));

        return vel * dir.normalized;
    }
    
    protected abstract void Attack();
}
