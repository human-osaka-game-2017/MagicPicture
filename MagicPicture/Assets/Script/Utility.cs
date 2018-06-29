using System;
using System.Collections;
using UnityEngine;

class Utility
{
    public static IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}