using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectilespellManager : MonoBehaviour
{
    public Dictionary<GameObject, ProjectileStats> projectilespells = new Dictionary<GameObject, ProjectileStats>();

    private void Start()
    {
        StartCoroutine(ClearDictionary());
    }

    IEnumerator ClearDictionary()
    {
        while (true)
        {
            if (projectilespells.Count == 0)
            {
                yield return new WaitForSeconds(5);
                continue;
            }
            
            List<GameObject> remove = new List<GameObject>();
            foreach (var index in projectilespells)
            {
                if (index.Key == null)
                {
                    remove.Add(index.Key);
                }
            }
            for (int i = 0; i < remove.Count; i++)
            {
                projectilespells.Remove(remove[i]);
            }

            yield return new WaitForSeconds(1);
        }
    }
}