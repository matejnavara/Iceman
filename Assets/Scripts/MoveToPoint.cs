using UnityEngine;
using UnityEngine.AI;

public class MoveToPoint : MonoBehaviour
{
    NavMeshAgent agent;
    GameManager gm;
    public GameObject cross;

    void Start()
    {
        agent =  gameObject.GetComponent<NavMeshAgent>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!gm.isGameOver() && !gm.countDown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    agent.destination = hit.point;
                }
            }
        }
        else
        {
            agent.ResetPath();
        }

        if (agent.hasPath)
        {
            if(cross == null)
            {
                cross = (GameObject)Instantiate(Resources.Load("Prefabs/Cross"), agent.destination, agent.transform.rotation);
            }
            else
            {
                cross.transform.position = agent.destination;
            } 
        }
        else
        {
            Destroy(cross);
        }
    }
}
