using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum EScene
    {
        Unknown,
        Title,
        StageSelect,
        Game,
    }

    public enum ESound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum EUIEvent
    {
        Click,
        Drag,
    }

    public enum ETurretType
    { 
        MachineGun,
        Rocket,
        Laser,
        PowerPole,
    }
}
