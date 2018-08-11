using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    private GameObject wayPointsGO;
    private Vector3[] wayPoints;

    [SerializeField]
    private float minTimeAtWayPoint = 2f, maxTimeAtWayPoint = 5f;

    private NPCDialogue npcDialogue;
    private NavMeshAgent agent;
    private CapsuleCollider capCollider;
    CapsuleCollider agentCollider;

    private int currentWayPoint = 0;

    private float changeWayPointTimer = 0f;

    private float timeUntilChangeWayPoint = 1f;

    Vector3 lastAgentVelocity = Vector3.zero;
    NavMeshPath lastAgentPath;

    bool wasTalking = false;

    // Use this for initialization
    void Start ()
    {
        agent = transform.GetChild(0).GetComponent<NavMeshAgent>();
        npcDialogue = GetComponent<NPCDialogue>();

        capCollider = GetComponent<CapsuleCollider>();
        agentCollider = transform.GetChild(0).GetComponent<CapsuleCollider>();

        capCollider.radius = agentCollider.radius * 1.5f;
        capCollider.height = agentCollider.height; 

        wayPointsGO = transform.GetChild(1).gameObject;
        wayPoints = new Vector3[wayPointsGO.transform.childCount];

        for(int i=0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = wayPointsGO.transform.GetChild(i).position;
        }

        timeUntilChangeWayPoint = Random.Range(minTimeAtWayPoint, maxTimeAtWayPoint);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!npcDialogue.IsTalking())
        {
            if (wasTalking == true)
            {
                ResumeAgent();
                wasTalking = false;
            }

            Move();
        }
        else
        {
            PauseAgent();
            wasTalking = true;
        }
	}



    private void Move()
    {
        if (changeWayPointTimer >= timeUntilChangeWayPoint)
        {
            int nextWayPoint = currentWayPoint + 1;

            if (nextWayPoint >= wayPoints.Length) nextWayPoint = 0;

            agent.SetDestination(wayPoints[nextWayPoint]);

            currentWayPoint = nextWayPoint;

            changeWayPointTimer = 0f;

            timeUntilChangeWayPoint = Random.Range(minTimeAtWayPoint, maxTimeAtWayPoint);   
        }
        else
        {
            if (agent.remainingDistance == 0)
            {
                changeWayPointTimer += Time.deltaTime;
            }
        }

        capCollider.center = agentCollider.transform.localPosition;
    }



    private void PauseAgent()
    {
        lastAgentVelocity = agent.velocity;
        lastAgentPath = agent.path;

        agent.velocity = Vector3.zero;
        agent.ResetPath();
    }

    private void ResumeAgent()
    {
        agent.velocity = lastAgentVelocity;
        agent.SetPath(lastAgentPath);
    }
}
