using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingTimerEvent : MonoBehaviour
{
    private TimerEvent timerEvent;

    void Start()
    {
        TimerEvent.Create(() => { Debug.Log("HelloWorld"); }, 2, "1");
        TimerEvent.Create(() => { Debug.Log("Hey... Whot!?!?"); }, 2, "2");
        TimerEvent.StopTimer("1");

    }
}
