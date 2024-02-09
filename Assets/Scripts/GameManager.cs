using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] magicCircle;
    [SerializeField] private GameObject pumpkin,pumpkinParent;

    [SerializeField] private float countdownTime;
    [SerializeField] private Text countdownText;

    [SerializeField] private Text remainingTime;
    [SerializeField] private Text DeadPumpkinCount;

    [SerializeField] private GameObject winningScene;

    public int deadPumpkinCounter;
    private PlayerManager player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        deadPumpkinCounter = 0;
    }

    void Update()
    {
        foreach (GameObject circle in magicCircle)
        {
            circle.transform.Rotate(new Vector3(0, 75 * Time.deltaTime, 0));
        }


        if (pumpkinParent.transform.childCount < 25)
        {
            Instantiate(pumpkin, magicCircle[Random.Range(0, magicCircle.Length)].transform.position, Quaternion.identity,pumpkinParent.transform);
        }

        Countdown();

        if(player.isWin)
        {
            remainingTime.text = "Remaining time : " + countdownText.text;
            DeadPumpkinCount.text = "Count of pumpkin killed : " + deadPumpkinCounter.ToString();

            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            winningScene.SetActive(true);
        }
        
    }


    void Countdown()
    {
        if (countdownTime > 0)
        {
            int minutes = Mathf.FloorToInt(countdownTime / 60);
            int seconds = Mathf.FloorToInt(countdownTime % 60);

            countdownText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            countdownTime -= Time.deltaTime;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().isDead = true;
        }
    }
}
