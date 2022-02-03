using System.Globalization;
using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEvent
{
    public static List<TimerEvent> ActiveTimerEvent;
    public static GameObject initGameObject;
    private static void InitIfNeeded()
    {
        if (!initGameObject)
        {
            initGameObject = new GameObject("FunctionTimer_InitGameObject");
            ActiveTimerEvent = new List<TimerEvent>();
        }
    }
    public static TimerEvent Create(Action action, float timer, String timerName = null)
    {
        InitIfNeeded();

        GameObject gameObject = new GameObject("Timer Event", typeof(MonoBehaviourHood));
        TimerEvent timerEvent = new TimerEvent(action, timer, timerName, gameObject);
        gameObject.GetComponent<MonoBehaviourHood>().OnUpdate = timerEvent.Update;

        ActiveTimerEvent.Add(timerEvent);

        return timerEvent;
    }

    public static void StopTimer(String timerName)
    {
        for (var i = 0; i < ActiveTimerEvent.Count; i++)
        {
            if (ActiveTimerEvent[i].timerName == timerName)
            {
                ActiveTimerEvent[i].DestroySelf();
                i--;
            }
        }
    }
    private static void RemoveTimer(TimerEvent timerEvent)
    {
        InitIfNeeded();
        ActiveTimerEvent.Remove(timerEvent);
    }
    public class MonoBehaviourHood : MonoBehaviour
    {
        public Action OnUpdate;
        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }

    private Action action;
    private float timer;
    private string timerName;
    private GameObject gameObject;
    private bool isDestroyed;

    private TimerEvent(Action action, float timer, string timerName, GameObject gameObject)
    {
        this.action = action;
        this.timer = timer;
        this.timerName = timerName;
        this.gameObject = gameObject;
        isDestroyed = false;
    }

    public void Update()
    {
        if (!isDestroyed)
        {
            timer -= Time.deltaTime;
            if (timer < -0)
            {
                //trigger action
                action();
                DestroySelf();
            }
        }
    }

    private void DestroySelf()
    {
        isDestroyed = true;
        GameObject.Destroy(gameObject);
        RemoveTimer(this);
    }
}
