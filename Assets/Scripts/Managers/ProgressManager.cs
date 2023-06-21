using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : Singleton<ProgressManager>
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
        unlockShelf,
        pcKabel, 
        partsCollected,
        motorPart4,
        motorPart5,
        motorPart6
    }

    public void AddProgress(Progress progress)
    {
        if (progress == Progress.None)
            return;
        
        checkpointList.Add(progress);
        OnProgressChanged?.Invoke(progress);
        
        if (checkpointList.Contains(Progress.motorPart1))
        {
            if (checkpointList.Contains(Progress.motorPart2))
            {
                if (checkpointList.Contains(Progress.motorpart3))
                {
                    if (checkpointList.Contains(Progress.motorPart4))
                    {
                        if (checkpointList.Contains(Progress.motorPart5))
                        {
                            if (checkpointList.Contains(Progress.motorPart6))
                            {
                                checkpointList.Add(Progress.partsCollected);
                            }
                        }
                    }
                }
            }
        }
    } 

    public bool ContainsProgress(Progress progress)
    {
        if (checkpointList.Contains(progress))
            return true;
        
        return false;
    }
}
