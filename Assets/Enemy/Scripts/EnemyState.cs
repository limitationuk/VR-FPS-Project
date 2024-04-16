using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour
{
    public enum State { E_Idle, E_Shooting, E_Hit, E_Die, E_Chase, E_Patrol, E_Rotating }

    [SerializeField] float range;
    [SerializeField] float angle;
    [SerializeField] float eyeHeight;
    [SerializeField] int currentWayPointIndex = 0;
    [SerializeField] float waitTime;


    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;



    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;



    public StateMachine<State> stateMachine;
    Collider[] colliders = new Collider[20];

    [SerializeField] List<Transform> wayPoints = new List<Transform>();


    public Animator Animator => animator;
    public List<Transform> WayPoints => wayPoints;
    public NavMeshAgent Agent => agent;
    public Transform Player { get => player; set => player = value; }
    public float WaitTime => waitTime;
    public int CurrentWayPointIndex { get => currentWayPointIndex; set => currentWayPointIndex = value; }




    private void Awake()
    {
        stateMachine = new StateMachine<State>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        stateMachine.AddState(State.E_Idle, new E_Idle(this));
        stateMachine.AddState(State.E_Shooting, new E_Shooting(this));
        stateMachine.AddState(State.E_Hit, new E_Hit(this));
        stateMachine.AddState(State.E_Die, new E_Die(this));
        stateMachine.AddState(State.E_Chase, new E_Chase(this));
        stateMachine.AddState(State.E_Patrol, new E_Patrol(this));
        stateMachine.AddState(State.E_Rotating, new E_Rotating(this));

        stateMachine.Start(State.E_Idle);
    }

    void Update()
    {
        stateMachine.Update();
        FindTarget();
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 lookDir = AngleToDir(transform.eulerAngles.y);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);

        Debug.DrawRay(transform.position, lookDir * range, Color.green);
        Debug.DrawRay(transform.position, rightDir * range, Color.blue);
        Debug.DrawRay(transform.position, leftDir * range, Color.blue);

    }

    public Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }

    public void FindTarget()
    {
        if (player == null)
        {
            Vector3 eyePosition = transform.position + Vector3.up * eyeHeight;

            int size = Physics.OverlapSphereNonAlloc(eyePosition, range, colliders, targetMask);
            for (int i = 0; i < size; i++)
            {
                Vector3 dirToTarget = (colliders[i].transform.position - eyePosition).normalized;
                if (Vector3.Dot(transform.forward, dirToTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
                    continue;

                float disToTarget = Vector3.Distance(eyePosition, colliders[i].transform.position);
                if (Physics.Raycast(eyePosition, dirToTarget, disToTarget, obstacleMask))
                    continue;

                stateMachine.ChangeState(EnemyState.State.E_Chase);
                player = colliders[i].GetComponent<Transform>();



                Debug.DrawRay(eyePosition, dirToTarget * disToTarget, Color.red);
            }
        }


    }



    public class EnemyStateData : BaseState<EnemyState.State>
    {
        protected EnemyState enemyState;
    }




}
