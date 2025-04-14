using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TargetStaticController
{
    public static List<LookAt> pers = new List<LookAt>();

    public static void Add(LookAt p)
    {
        pers.Add(p);
    }
    public static void Remove(LookAt p)
    {
        pers.Remove(p);
    }
    public static void SetTarget(GameObject gm)
    {
        foreach (LookAt p in pers) 
        { 
            p.SetObjectTarget(gm);
        }
    }
}
