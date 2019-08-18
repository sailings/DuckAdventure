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

    public static int HightestLevel
    {
        get {
            return PlayerPrefs.GetInt("World" + WorldPlaying + "HighestLevel", 1);
        }
        set {
            PlayerPrefs.SetInt("World" + WorldPlaying + "HighestLevel", value);
        }
    }

    public static int BestStar
    {
        get {
            return PlayerPrefs.GetInt("World" + WorldPlaying + "-" + LevelPlaying + "BestStar", 0);
        }
        set {
            if (value > BestStar)
                PlayerPrefs.SetInt("World" + WorldPlaying + "-" + LevelPlaying + "BestStar", value);
        }
    }
}
