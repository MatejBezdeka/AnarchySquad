using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Save {
    public int gameSeed;
    public int points;
    public int missionsDone;
    public bool inGame; /*true - in Game false - missionChoosing  */
    public int currentSeed;// (game) seed / current mission map seed;
    private List<SquadUnit> units;
    private List<EnemyUnit> enemyUnits;
    public string date;
    //units pos/stats + currentStats/weapon + currentWeaponStats/morale/secondWeapon
    //

    public Save(int gameSeed, int points, int missionsDone, bool inGame, int currentSeed, List<SquadUnit> units, List<EnemyUnit> enemyUnits, string date) {
        this.gameSeed = gameSeed;
        this.points = points;
        this.missionsDone = missionsDone;
        this.inGame = inGame;
        this.currentSeed = currentSeed;
        this.units = units;
        this.enemyUnits = enemyUnits;
        this.date = date;
    }

    public Save(int gameSeed, int points, int missionsDone, bool inGame, int currentSeed, string date) {
        this.gameSeed = gameSeed;
        this.points = points;
        this.missionsDone = missionsDone;
        this.inGame = inGame;
        this.currentSeed = currentSeed;
        this.date = date;
    }

    public void SaveData(int currentSeed) {
        PlayerPrefs.SetInt("GS", gameSeed);
        PlayerPrefs.SetInt("PT", points);
        PlayerPrefs.SetInt("MS", missionsDone);
        PlayerPrefs.SetString("DT", DateTime.Now.ToString("HH:mm dd.MM. yyyy"));
        PlayerPrefs.SetInt("CS", currentSeed);
        /*if (inGame) {
            PlayerPrefs.SetInt("IG", 1);
            for (int i = 0; i < units.Count; i++) {
                //JSON Units
            }

            for (int i = 0; i < enUnits.Count; i++) {
                
            }
        }
        else {
            PlayerPrefs.SetInt("IG", 0);
        }*/
    }

    public static Save GetSave(int saveId) {
        return new Save(
            PlayerPrefs.GetInt("GS"),
            PlayerPrefs.GetInt("PT"),
            PlayerPrefs.GetInt("MS"),
            PlayerPrefs.GetInt("IG") == 1,
            PlayerPrefs.GetInt("CS"),
            PlayerPrefs.GetString("DT")
            
            );
    }
    public static void NewSave(int saveId,int gameSeed) {
        PlayerPrefs.SetInt("GS", gameSeed);
        PlayerPrefs.SetInt("PT", 500);
        PlayerPrefs.SetInt("MS", 0);
        PlayerPrefs.SetString("DT", DateTime.Now.ToString("HH:mm dd.MM.yyyy"));
        Random rn = new Random(gameSeed);
        PlayerPrefs.SetInt("CS", rn.Next(int.MinValue + 64, int.MaxValue - 64));
        PlayerPrefs.SetInt("IG", 0);
    }

    public static void DeleteData() {
        PlayerPrefs.SetInt("MS", -1);
    }
}
