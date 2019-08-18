using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue 
{
    public static bool HitSavePoint = false;
    public static int SaveBullets;
    public static int SaveHearts;
    public static int SaveScore;
    public static int SaveStars;
    public static bool IsUsingJetPack = false;

    public static int WorldPlaying = 1;
    public static int LevelPlaying = 1;

    public static int WorldReached
    {
        get {
            return PlayerPrefs.GetInt("WorldReached", 1);
        }
        set {
            PlayerPrefs.SetInt("WorldReached",value);
        }
    }
}
