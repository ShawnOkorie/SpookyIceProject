using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : Singleton<ProgressManager>, IShouldForceAwake
{
    public delegate void ProgressChanged(Progress progress);
    public event ProgressChanged OnProgressChanged;
    
    public List<Progress> checkpointList = new List<Progress>();
    
    public enum Progress
    {
        None,
        healedSelf,
        activatedGenerator,
        radioSolved1,
        radioRepaired,
        radiosolved2,
        unlockedCreate,
        motorPart1,
        motorPart2,
        motorpart3,
        openedFloor,
        meltedIce,
        unlockedLab,
        unlockedSafe,
        snowmobileRepaired,
        UVLampUsed,
        unlockShelf
    }

    public void AddProgress(Progress progress)
    {
        if (progress == Progress.None)
            return;
        
        checkpointList.Add(progress);
        OnProgressChanged?.Invoke(progress);
    }

    public bool ContainsProgress(Progress progress)
    {
        if (checkpointList.Contains(progress))
            return true;
        
        return false;
    }
}
