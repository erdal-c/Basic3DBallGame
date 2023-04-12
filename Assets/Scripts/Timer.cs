using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    static Timer timerInstance;

    float currentTime;
    float totalTime = 0f;
    public Text timeText;
    bool timerActive = true;

    // Start is called before the first frame update
    void Start()
    {
        if (timerInstance != null)
        {
            Destroy(timerInstance);
            print("destroyed");
            print("Id : " + timerInstance.GetInstanceID());
            return;
        }
        print("Ýs Null? : "+timerInstance.IsUnityNull());
        timerInstance = this;
        print("Id : "+timerInstance.GetInstanceID());
        
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive==true) 
        {
            currentTime += Time.deltaTime;
            //print(currentTime);
            TimeSpan timeCurrent = TimeSpan.FromSeconds(currentTime);
            //timeText.text = time.Minutes.ToString() +":"+ time.Seconds.ToString();
            timeText.text = timeCurrent.ToString(@"mm\:ss\:ff");
        }
    }
    public void Starts()
    {
        Start();
    }

    public static Timer GetInstance() { return timerInstance; }
    
    public float GetTotalTime()
    {
        totalTime += currentTime;
        TimeStop();
        return totalTime;
    }

    public void TimeStart()
    {
        timerActive= true;
    }
    public void TimeStop()
    {
        timerActive = false;
        currentTime = 0f;
    }

    public void TimeZero()
    {
        currentTime= 0f;
    }

    public void TotalTimeCleaner()
    {
        totalTime= 0f;  
    }

    public bool ActiveCheck()
    {
        return timerActive;
    }


}
