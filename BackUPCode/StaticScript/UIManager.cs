using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameoverUI; // 게임 오버시 활성화할 UI.
    [SerializeField] private GameObject textUI;
    [SerializeField] private GameObject ClearUI;
    [SerializeField] private GameObject TitleUI;
    [SerializeField] private GameObject BlackBack;
    public event System.Action RestartEvent;

    public static UIManager Instance { get; private set; }

    private bool evnet_flag = false;

    private bool maingame;

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

    public void GameStart()
    {
        SceneManager.LoadScene("MainGame");
        maingame = true;
        TitleUI.SetActive(false);
        Invoke("PlayerControll", 1f);
    }

    public void PlayerControll()
    {
        BlackBack.SetActive(false);
        GameManager.Instance.player_object.SetActive(maingame);
    }

    public void CallGameOver()
    {
        Invoke("GameOver", 1.5f);
    }

    public void GameClear()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        ClearUI.SetActive(true);
    }

    private void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        gameoverUI.SetActive(true);
    }

    public void Restart()
    {
        gameoverUI.SetActive(false);
        SceneManager.LoadScene("MainGame");
        Invoke("PlayerControll", 1f);
    }

    public void GameExit()
    {
        gameoverUI.SetActive(false);
        SceneManager.LoadScene("Title");
        BlackBack.SetActive(true);
        maingame = false;
        TitleUI.SetActive(true);
        Invoke("PlayerControll", 1f);
    }

    public void TextOn(string text, bool flag = false)
    {
        if (!evnet_flag)
        {
            textUI.SetActive(true);
            textUI.GetComponent<TextMeshProUGUI>().text = text;
        }
        if (flag)
        {
            evnet_flag = true;
            StartCoroutine(EventText());
        }
    }

    public void TextOff()
    {
        textUI.SetActive(false);
        textUI.GetComponent<TextMeshProUGUI>().text = "";
    }

    private IEnumerator EventText()
    {
        yield return new WaitForSeconds(2f);
        TextOff();
        evnet_flag = false;
    }
}
