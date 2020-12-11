﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Dictionary<Type, IGameManager> _startSequence = new Dictionary<Type, IGameManager>();
    public List<GameObject> managers = new List<GameObject>();
    void Awake()
    {
        foreach (GameObject gm in managers)
        {
            var manager = gm.GetComponent<IGameManager>();
            _startSequence.Add(manager.GetType(), manager);
        }
        StartCoroutine(StartupManagers());
    }


    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in _startSequence.Values)
        {
            manager.Startup();
        }

        yield return null;
        int numModules = _startSequence.Count;
        int numReady = 0;
        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;
            foreach (IGameManager manager in _startSequence.Values)
            {
                if (manager._Status == ManagerStatus.STARTED)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
            {
               //Debug.Log("Progress: " + numReady + " / " + numModules);
            }

            yield return null;
        }

        //Debug.Log("All managers started up");
    }

    public static T GetManager<T>() where T : class, IGameManager
    {
        IGameManager baseManager;
        _startSequence.TryGetValue(typeof(T), out baseManager);
        return baseManager as T;
    }
}