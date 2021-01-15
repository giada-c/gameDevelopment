﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private int _score;
    public int Score
    {
        get => _score;
    }

    public static ScoreManager Instance = null;

    private Dictionary<string, int> _scoreTable;

    void Awake()
    {
        _score = 0;
        _scoreTable = new Dictionary<string, int>();
        PopulateScoreTable();
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Messenger<string>.AddListener(GameEvent.CHARACTER_HIT, ChangeScore);
        Messenger<string>.AddListener(GameEvent.ENEMY_HIT, ChangeScore);
    }

    private void ChangeScore(string eventName)
    {
        try
        {
            var quantity = _scoreTable[eventName];
            _score += quantity;
            _score = Mathf.Clamp(_score, 0, 999999999);
        }
        catch (KeyNotFoundException keyEx)
        {
            // Inform in some way that an error happened
            Debug.LogError("Error " + this.name + ": " + keyEx.Message);
            return;
        }

        // Broadcast score changed event
        Messenger<int>.Broadcast(GameEvent.SCORE_CHANGED, _score, MessengerMode.DONT_REQUIRE_LISTENER);
    }

    void onDestroy()
    {
        Messenger<string>.RemoveListener(GameEvent.CHARACTER_HIT, ChangeScore);
        Messenger<string>.RemoveListener(GameEvent.ENEMY_HIT, ChangeScore);
    }

    void PopulateScoreTable()
    {
        // pro
        _scoreTable.Add(GameEvent.ENEMY_HIT, 5);

        // const
        _scoreTable.Add(GameEvent.CHARACTER_HIT, -5);
    }
}
