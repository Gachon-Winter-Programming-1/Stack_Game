using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection {
    
    public static IEnumerator WaitThenCallback(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
    public static IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
    
    public static IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove, Action callback)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        callback();
    }
    public static void Swap<T>(ref T a,ref T b)
    {
        (a, b) = (b, a);
    }
}