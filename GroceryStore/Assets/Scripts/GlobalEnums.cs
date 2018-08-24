using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script Name: GlobalEnums.cs
// Written By: Laurence Valentini

public enum ItemType { None, Stock, Door, NPC, Freezer};

public enum DoorState { Locked, Unlocked};

public enum ObjectiveState { Start, ManagerOffice, FindKey, KeyFound, Finish};

public enum NPCMissionState { NotSet, None, HasMission, MissionStarted, MissionFinished, AfterMission};
