using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyList ListSmaple;
    [SerializeField] private GameObject m_player;
    public GameObject player_object { get => m_player; }
    [SerializeField] private Player playerscript;
    public Player player { get => playerscript; }

    private Enemy enemyscript;
    private GameObject enemy;

    public bool gameOver { get => !playerscript.Survive; }
    List<GameObject> enemyList;
    private int m_iStage;
    public int Stage { get => m_iStage; }

    public bool Act;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Act = true;
        m_iStage = 1;
        enemyList = ListSmaple.getlist;
        //SetEnemy();
    }

    private void Update()
    {
        if (gameOver)
        {
            m_player.SetActive(false);
        }
    }

    private void SetEnemy()
    {
        var ran = Random.Range(0, enemyList.Count);

        enemy = Instantiate(enemyList[ran]);
    }


    public void DeathPlayer()
    {
        playerscript.Survive = false;
        m_player.SetActive(false);

    }

    public void NotAtiveEnemy()
    {
        enemy.SetActive(false);
    }
}
