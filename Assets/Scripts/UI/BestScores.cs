using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Best scores", fileName = "New best scores")]
public class BestScores : ScriptableObject
{
    private List<BestScore> _bestScores = new List<BestScore>();
    public List<BestScore> Scores => _bestScores;
    public bool IsEmpty => _bestScores.Count == 0;

    readonly int MAX_SCORES = 3;

    public bool CheckScore(float time, int score)
    {
        BestScore newScore = new BestScore(time, score);

        if (_bestScores.Count < 3)
        {
            _bestScores.Add(newScore);
            return true;
        }
        else
        {
            for (int i = 0; i < MAX_SCORES; i++)
            {
                if (newScore.Time.IsBetter(_bestScores[i].Time))
                {
                    _bestScores.Insert(i, newScore);
                    _bestScores.Remove(_bestScores[MAX_SCORES]);

                    return true;
                }
            }
            return false;
        }
    }

    private void SortScores()
    {

    }
}

public struct BestScore
{
    public int Position;
    public GameTime Time;
    public int Score;

    public BestScore(float time, int score)
    {
        Time = new GameTime(time);
        Score = score;
        Position = 0;
    }
}

public struct GameTime
{
    private string _time;

    private int _days; // ???
    private int _hours;
    private int _minutes;
    private int _seconds;
    private int _milliseconds;

    readonly static int DAYS_DIVIDER = 86400;
    readonly static int HOURS_DIVIDER = 3600;
    readonly static int MINUTES_DIVIDER = 60;

    public GameTime(float timeInSec)
    {
        int time = (int)timeInSec;

        _milliseconds = (int)(timeInSec % time);

        _days = time / DAYS_DIVIDER;
        time = time % DAYS_DIVIDER;

        _hours = time / HOURS_DIVIDER;
        time = time % HOURS_DIVIDER;

        _minutes = time / MINUTES_DIVIDER;

        _seconds = time % MINUTES_DIVIDER;

        _time = (_days > 0 ? _days.ToString() + " days, " : "") +
            (_hours > 0 ? _hours.ToString() + " hours, " : "") +
            _minutes + ":" + _seconds + "." + _milliseconds;
    }

    public string GetTime() => _time;

    public bool IsBetter(GameTime time)
    {
        if (_days == time._days)
        {
            if (_hours == time._hours)
            {
                if (_minutes == time._minutes)
                {
                    if (_seconds == time._seconds)
                    {
                        if (_milliseconds == time._milliseconds)
                        {
                            Debug.Log("Times is equals");
                            return true;
                        }
                        else return _milliseconds > time._milliseconds;
                    }
                    else return _seconds > time._seconds;
                }
                else return _minutes > time._minutes;
            }
            else return _hours > time._hours;
        }
        else return _days > time._days;
        
    }
}
