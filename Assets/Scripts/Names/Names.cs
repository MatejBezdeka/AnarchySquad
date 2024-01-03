using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Names : MonoBehaviour {
    static Random rn;
    static Random srn;
    static int firstNamesFileLength;
    static string firstNamesFilePath = "Assets/Scripts/Names/firstnames.txt";
    static int lastNamesFileLength;
    static string lastNamesFilePath = "Assets/Scripts/Names/surnames.txt";
    static int cityNamesFileLength;
    static string cityNamesFilePath = "Assets/Scripts/Names/places.txt";
    void Start() {
        rn = new Random();
        firstNamesFileLength = GetFileRowCount(firstNamesFilePath);
        lastNamesFileLength = GetFileRowCount(lastNamesFilePath);
        cityNamesFileLength = GetFileRowCount(cityNamesFilePath);
    }

    int GetFileRowCount(string filePath) {
        using StreamReader reader = new StreamReader(filePath);
        int count = 0;
        while (reader.ReadLine() != null)
        {
            count++;
        }
        return count;
    }

    public static string GetRandomName(int seed) {
        srn = new Random(seed);
        return GetName(srn.Next(0, firstNamesFileLength), srn.Next(0, lastNamesFileLength));
    }
    public static string GetRandomName() {
        return GetName(rn.Next(0, firstNamesFileLength), rn.Next(0, lastNamesFileLength));
    }
    public static string GetName(int firstNameId,int lastNameId) {
        Debug.Log(firstNameId + " " + lastNameId);
        if (firstNameId >= firstNamesFileLength || lastNameId >= lastNamesFileLength) {
            return "out of bound";
        }
        string name = "";
        StreamReader reader = new StreamReader(firstNamesFilePath);
        for (int i = 1; i < firstNameId; i++)
            if (reader.ReadLine() == null) return null;
        name += reader.ReadLine();
        reader = new StreamReader(lastNamesFilePath);
        for (int i = 1; i < lastNameId; i++)
            if (reader.ReadLine() == null) return null;
        name += " " + reader.ReadLine();
        Debug.Log(name);
        return name;
    }
    public static string GetRandomCityName(int seed) {
        srn = new Random(seed);
        return GetCityName(srn.Next(0,cityNamesFileLength));
    }
    public static string GetRandomCityName() {
        return GetCityName(rn.Next(0,cityNamesFileLength));
    }

    public static string GetCityName(int id) {
        if (id >= cityNamesFileLength) {
            return "out of bound";
        }
        string name;
        using StreamReader firstNameReader = new StreamReader(firstNamesFilePath);
        name = firstNameReader.ReadLine();
        return name;
    }
}