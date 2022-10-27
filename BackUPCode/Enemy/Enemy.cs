using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DK.FSM;
using SETTING_VALUE;

public class Enemy : Character
{
   // private Transform Target;
    public Vector3 prevTraget;
    public Vector3 CurTarget;
    private RaycastHit hit;
    [SerializeField] private RouteController R_controller;
    public RouteController controller { get => R_controller; }

    public Animator animePass { get => anime; }//�ٸ������� anime�� ������ ���� ������ ���� �ְ� �ϱ� ���ؼ� ���, 
    //���� ���� ���
    private StateMachine<Enemy> m_stateMachine;
    public StateMachine<Enemy> StateMachine { get => m_stateMachine; }

    //������
    [SerializeField] private EnemyData m_EnemyData;
    public EnemyData data { get => m_EnemyData; }

    //�ΰ�����
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    public UnityEngine.AI.NavMeshAgent navAgent { get => agent; }


    [SerializeField] private List<GameObject> SelectionList;

    [SerializeField] private Cinemachine.CinemachineVirtualCameraBase JumpScareCamera;

    [SerializeField] private int StateType;
    public int state { get => StateType; }
    private bool ChaseFlag;
    public bool chase_flag { get => ChaseFlag; }

    private Vector3 backupTarget;

    private bool stop;

    private void Start()
    {
        JumpScareCamera.gameObject.SetActive(false);
        rigid = GetComponent<Rigidbody>();
        stop = false;

        m_stateMachine = new StateMachine<Enemy>(this);
        m_stateMachine.SetCurrentState(m_EnemyData.StartState);
        m_stateMachine.CurrentState.Enter(this);
        StateType = (int)STATE_ID.PRODING;
        ChaseFlag = false;
        StartCoroutine(Search());
    }

    private void Update()
    {
        if(!stop)
        {
            m_stateMachine.Update();
        }
    }

    private IEnumerator Search()
    {
        while (true)
        {
            if(!stop)
            {
                var layerMask = 1 << LayerMask.NameToLayer("Object") | 1 << LayerMask.NameToLayer("Item");
                if (Physics.Raycast(transform.position + transform.up, transform.forward, out hit, m_EnemyData.AttackRange + 5))
                {
                    GameObject target = hit.transform.gameObject;
                    if (target.tag == "Door")
                    {
                        if (!target.GetComponent<Door>().Open && !target.GetComponent<Door>().getLock)
                        {
                            stopEnemy();
                            hit.transform.gameObject.GetComponent<Door>().OnAction();
                            Invoke("RE_Move", 8f);
                        }
                    }
                }

                bool check = false;
                var PlayerMask = 1 << LayerMask.NameToLayer("Player");

                Collider[] objects = Physics.OverlapSphere(transform.position, m_EnemyData.RecognizeRange, PlayerMask);//���� ������ ���ȿ� �ִ� ��� ������Ʈ Ȯ�� 
                for (int i = 0; i < objects.Length; i++)
                {
                    Transform target = objects[i].transform;

                    if (target.tag == "Player")
                    {
                        check = true;
                        FocusPlayer((target.position - transform.position).normalized);
                        break;
                    }

                }

                if (!check && ChaseFlag)
                {
                    ChaseFlag = false;
                    StartCoroutine(EndChase());
                }
                yield return new WaitForSeconds(0.25f);//0.25�� �Ŀ� ����
            } 
        }
    }

    private void FocusPlayer(Vector3 direction)
    {
        float angle = Vector3.Angle(direction, transform.forward);
        if (angle >= -m_EnemyData.RecognizeAngle * 0.5f && angle <= m_EnemyData.RecognizeAngle * 0.5f)
        {
            if (Physics.Raycast(transform.position + transform.up, direction, out hit, m_EnemyData.RecognizeRange))//�÷��̾ �þ߰��� ���� �ֱ� ������ �÷��̾� �������� Raycast�� ���� ��ֹ��� �ִ��� Ȯ��.
            {
                if (hit.transform.tag == "Player")
                {
                    StateType = (int)STATE_ID.CHASE;
                    ChaseFlag = true;
                }
                else
                {
                    if (ChaseFlag)
                    {
                        ChaseFlag = false;
                        StartCoroutine(EndChase());
                    }
                }
            }
        }
    }

    public bool CheckAttack()
    {
        if (hit.transform != null && hit.transform.tag == "Player")
        {
            if (Vector3.Distance(rigid.position, hit.transform.position) <= m_EnemyData.AttackRange)
                return true;
        }
        return false;
    }

    public void JumpScare()
    {
        JumpScareCamera.gameObject.SetActive(true);
    }

    private void RE_Move()
    {
        if (StateType != (int)STATE_ID.CHASE)
            agent.SetDestination(backupTarget);
    }

    private IEnumerator EndChase()
    {
        yield return new WaitForSeconds(5f);//5�� �Ŀ� ����
        if (!ChaseFlag)
            StateType = (int)STATE_ID.PRODING;
    }

    public void MoveEnemy(Vector3 target)
    {
        Debug.Log(target);
        backupTarget = target;
        agent.SetDestination(target);
    }

    public void ChangeState(StateBase<Enemy> state)
    {
        m_stateMachine.ChangeState(state);
    }

    public bool GoalCheck()
    {
        if (Vector3.Distance(rigid.position, backupTarget) <= 5)
            return true;

        return false;
    }

    public void stopEnemy()
    {
        agent.ResetPath();
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }

    public void Timestop()
    {
        stopEnemy();
        stop = true;
        ChaseFlag = false;
        StateType = (int)STATE_ID.PRODING;
        this.GetComponent<Collider>().enabled = false;

        StartCoroutine(IsMoveEnemy());
    }

    public IEnumerator IsMoveEnemy()
    {
        yield return new WaitForSeconds(10f);//10�� �Ŀ� ����
        stop = false;
        this.GetComponent<Collider>().enabled = true;
        RE_Move();
    }
}
