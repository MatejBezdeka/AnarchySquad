using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save {
    int gameSeed;
    public int points;
    //public bool inGame;
    //public int currentMap;

    public Save(int gameSeed) {
        this.gameSeed = gameSeed;
        points = 0;
        //inGame = false;
        //currentMap = -1;
    }
    public void SaveDate(List<Unit> units) {
        PlayerPrefs.SetInt("GS", gameSeed);
        PlayerPrefs.SetInt("PT", points);
        PlayerPrefs.SetString("DT", "save time");
        /*PlayerPrefs.SetInt("IG", inGame? 1 : 0);
        if (inGame) {
            PlayerPrefs.SetInt("CM", currentMap);
            foreach (var  in units) {
                
            }
        }*/ }
}
