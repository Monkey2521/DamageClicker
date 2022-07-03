using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Best scores", fileName = "New best scores")]
public class BestScores : ScriptableObject
{
    [SerializeField] private List<BestScore> _bestScores;
    public List<BestScore> Scores => _bestScores;
    public bool IsEmpty => _bestScores.Count == 0;

    readonly int MAX_SCORES = 3;

    public bool CheckScore(float time, int score)
    {
        BestScore newScore = new BestScore(time, score);

        if (_bestScores.Count < MAX_SCORES)
        {
            int i = 0;

            while (i < _bestScores.Count && _bestScores[i].Time.IsBetter(newScore.Time))
                i++;

            if (i == _bestScores.Count) _bestScores.Add(newScore);
            else _bestScores.Insert(i, newScore);

            SetPositions();

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

                    SetPositions();

                    return true;
                }
            }
            return false;
        }
    }

    private void SetPositions()
    {
        for(int i = 0; i < _bestScores.Count; i++)
        {
            _bestScores[i].Position = i + 1;
        }
    }
}

[System.Serializable]
public class BestScore
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

[System.Serializable]
public class GameTime
{
    [SerializeField] private string _time;

    private int _days; // ???
    private int _hours;
    private int _minutes;
    private int _seconds;
    private int _milliseconds;

    readonly static int DAYS_DIVIDER = 86400;
    readonly static int HOURS_DIVIDER = 3600;
    readonly static int MINUTES_DIVIDER = 60;
    readonly static char[] SEPARATOR = { ',' };

    public GameTime(float timeInSec)
    {
        int time = (int)timeInSec;

        _milliseconds = (timeInSec - time).ToString().Split(SEPARATOR)[1][0] - '0'; // округление дробной части до 1 знака

        _days = time / DAYS_DIVIDER;
        time = time % DAYS_DIVIDER;

        _hours = time / HOURS_DIVIDER;
        time = time % HOURS_DIVIDER;

        _minutes = time / MINUTES_DIVIDER;

        _seconds = time % MINUTES_DIVIDER;

        _time = (_days > 0 ? _days + " days, " : "") +
            (_hours > 0 ? _hours + " hours, " : "") +
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
