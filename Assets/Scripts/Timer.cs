using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    public int currentDay = 1;
    public float currentTime = 9f;
    public float timeIncrement = 0.5f;
    public float timeSpeed = 10f;

    private float timer = 0f;

    public TMP_Text timeText;
    public static Timer instance;

    [System.Serializable]
    public class TimedGameObject
    {
        public GameObject gameObject;
        public float enableTime;
        public float disableTime;
    }

    public List<TimedGameObject> timedGameObjects = new List<TimedGameObject>();
    private bool incrementAfterFrame = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        DisplayTime();
        timer += Time.deltaTime;

        if (timer >= timeSpeed)
        {
            timer = 0f;
            currentTime += timeIncrement;

            foreach (TimedGameObject timedObj in timedGameObjects)
            {
                if (timedObj.gameObject != null)
                {
                    if (currentTime >= timedObj.enableTime && currentTime < timedObj.disableTime)
                    {
                        timedObj.gameObject.SetActive(true);
                    }
                    else
                    {
                        timedObj.gameObject.SetActive(false);
                    }
                }
            }

            if (currentTime >= 20f)
            {
                currentDay++;
                timeSpeed = 10f; 
                if (currentDay >= 5)
                {
                    SceneManager.LoadScene("Bad Ending");
                    Destroy(gameObject);
                }
                else
                {
                    currentTime = 9f;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

        if (incrementAfterFrame)
        {
            timeSpeed = 0.1f;
            currentTime += 1f;
            incrementAfterFrame = false;
        }
    }

    public void DisplayTime()
    {
        if (timeText != null)
        {
            int hours = Mathf.FloorToInt(currentTime);
            int minutes = Mathf.FloorToInt((currentTime - hours) * 60);
            string amPm = (hours < 12 || hours == 24) ? "AM" : "PM";
            hours = (hours % 12 == 0) ? 12 : hours % 12;
            timeText.text = string.Format("{0:00}:{1:00} {2} - Day {3}", hours, minutes, amPm, currentDay);
        }
    }

    public void SetTimeAndIncrement(float newTime)
    {
        currentTime = newTime;
        incrementAfterFrame = true;
    }
}