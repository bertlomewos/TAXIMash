﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class RemoveData  
{
    [MenuItem("BBGames/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }
}
