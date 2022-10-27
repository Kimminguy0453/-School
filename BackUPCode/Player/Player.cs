using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class Player : Character
{
    public bool checkRun { get => playerInput.run; }

    [SerializeField] private PlayerInput playerInput;
    private float m_fIdle;
    private bool idelFlag;
    private float m_angle;

    [SerializeField] private Vector3 boxsize;
    [SerializeField] private float speed;

    [SerializeField] private Cinemachine.CinemachineVirtualCameraBase FirstCamera;
    [SerializeField] private PlayerData playerData;

    [SerializeField] private GameObject staminaBar;
    [SerializeField] private Stamina staminaScript;
    public bool Survive { get { return survive; } set { survive = value; } }
    private bool survive;
    public bool hide { get; private set; }

    private float stamina;

    private Dictionary<int, int> inventory;

    [SerializeField] private List<string> KeyList;

    private RaycastHit hit;

    private int layerMask
    {
        get => 1 << LayerMask.NameToLayer("Object") | 1 << LayerMask.NameToLayer("Item");
    }
    private bool ObjectFlag;


    Vector3 boxPos
    {
        get
        {
            return rigid.position + Vector3.up;
        }
    }

    Vector3 rayboxDir
    {
        get
        {
            return FirstCamera.transform.forward;//플레이어의 시야를 담당하는 카메라 방향
        }
    }
    
    public int CheckItem(int code)
    {
        if(inventory.ContainsKey(code))
            return inventory[code];
        return 0;
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        Cursor.lockState = CursorLockMode.Locked;
        survive = true;
        hide = false;
        idelFlag = true;
        stamina = 100f;
        m_angle = 0;
        m_fIdle = 0f;//서있는 애니메이션 갱신.
        rigid = this.GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        rigid.velocity = Vector3.zero;
        playerInput.Object_Event += CheckObject;
    }

    private void Update()
    {
        ViewItemText();
        Idel();
        StaminaCheck();
    }

    private void FixedUpdate()
    {
        RotationPlayer();
        MovePlayer();
    }

    public Vector3 GetPlayerPos()
    {
        return this.transform.position;
    }

    private void Idel()
    {
        if (idelFlag)
            m_fIdle += Time.deltaTime;
        else
            m_fIdle -= Time.deltaTime;

        if (m_fIdle >= 2)
        {
            m_fIdle = 2f;
            idelFlag = false;
        }

        if (m_fIdle <= 0)
        {
            m_fIdle = 0f;
            idelFlag = true;
        }
        anime.SetFloat("Timer", m_fIdle);
    }

    private void MovePlayer()
    {
        Vector3 velocity = Vector3.zero;

        if (playerInput.run && stamina > 0)
        {
            anime.SetFloat("Run", 1);
            speed = playerData.RunSpeed;
            staminacalculate(true);
        }
        else
        {
            anime.SetFloat("Run", 0);
            speed = playerData.WalkSpeed;
            staminacalculate();
        }
        velocity.x = playerInput.Move_Hor * speed;
        velocity.z = playerInput.Move_Ver * speed;
        velocity.y = rigid.velocity.y;
    
        rigid.velocity = (transform.forward * playerInput.Move_Ver + transform.right * playerInput.Move_Hor).normalized * speed + +rigid.velocity.y * Vector3.up;

        anime.SetFloat("MoveSetZ", velocity.normalized.z);
    }

    private void staminacalculate(bool run = false)
    {
        if (run || playerInput.run)
        {
            stamina -= Time.deltaTime * 10;
            if (stamina < 0)
                stamina = 0;
        }
        else
        {
            stamina += Time.deltaTime * (float)7.5;
            if (stamina > 100)
                stamina = 100;
        }

    }

    private void StaminaCheck()
    {
        if(playerInput.run || stamina < 100)
        {
            staminaBar.SetActive(true);
            staminaScript.UseStamina(stamina);
        }
        else
            staminaBar.SetActive(false);
    }




    private void RotationPlayer()
    {
        float angle = playerInput.rotate * playerData.rotateSpeed;
        rigid.rotation *= Quaternion.Euler(0, angle, 0);

        m_angle += playerInput.roteteY * 5;

        if (m_angle > 50)
            m_angle = 50;
        else if (m_angle < -50)
            m_angle = -50;

        var playerAngel = Quaternion.Euler(-m_angle, 0, 0);
        FirstCamera.transform.localRotation = playerAngel;
    }

    public void Teleport(Vector3 target)
    {
        this.gameObject.transform.position = target;
    }


    private void ViewItemText()
    {
        ObjectFlag = Physics.BoxCast(boxPos, boxsize, rayboxDir, out hit, FirstCamera.transform.rotation, 5, layerMask);
        if (ObjectFlag)
        {
            GameObject target = hit.transform.gameObject;
            switch (target.tag)
            {
                case "EscapeItem":
                    
                    break;
                case "Key":
                    //UI미구현
                    break;
                case "Door":
                    if (!target.GetComponent<Door>().getLock)
                        UIManager.Instance.TextOn(target.GetComponent<Door>().GetText());
                    break;
                case "CrystalBall":
                    //UI미구현
                    break;
                case "Mirror":
                    //UI미구현
                    break;
                case "Firecracker":
                    //UI미구현
                    break;
            }
        }
        else
            UIManager.Instance.TextOff();
    }

    private void CheckObject()
    {
        if (ObjectFlag)
        {
            GameObject target = hit.transform.gameObject;
            switch (target.tag)
            {
                case "Key":
                    KeyList.Add(target.name);
                    Destroy(target);
                    break;
                case "Door":
                    if (KeyList.Contains(target.GetComponent<Door>().getNeedItem))
                    {
                        target.GetComponent<Door>().Open = !target.GetComponent<Door>().Open;
                        target.GetComponent<Door>().OnAction();
                    }
                    break;
            }
        }
    }
}