using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public float TimeSurvived { get; private set; }

    void Start()
    {
        TimeSurvived = 0f;
    }

    void Update()
    {
        TimeSurvived += Time.deltaTime;
    }

    public void ResetTimeSurvived()
    {
        TimeSurvived = 0f;
    }

    public string GetTimeSurvivedString()
    {
        int minutes = Mathf.FloorToInt(TimeSurvived / 60f);
        int seconds = Mathf.FloorToInt(TimeSurvived % 60f);

        return $"{minutes:00}:{seconds:00}";
    }
}

