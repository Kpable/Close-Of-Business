using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateMaintainer : MonoBehaviour {

    public int desiredFramerate = 60;

    const float fpsMeasurePeriod = 0.5f;
    private int m_FpsAccumulator = 0;
    private float m_FpsNextPeriod = 0;
    private int m_CurrentFps;

    // Use this for initialization
    void Start () {
            m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;

    }

    // Update is called once per frame
    void Update () {

        // measure average frames per second
        m_FpsAccumulator++;
        if (Time.realtimeSinceStartup > m_FpsNextPeriod)
        {
            m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
            if (m_CurrentFps <= desiredFramerate) AdjustTime();
            else if (Time.timeScale != 1) Time.captureFramerate = 0; //Time.timeScale = 1;
            m_FpsAccumulator = 0;
            m_FpsNextPeriod += fpsMeasurePeriod;
        }

    }

    void AdjustTime()
    {
        Time.captureFramerate = desiredFramerate;
        //Time.timeScale = m_CurrentFps / desiredFramerate;
        Debug.Log(Time.timeScale);
    }
}
