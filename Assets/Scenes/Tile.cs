﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Tile : MonoBehaviour {

    public const int GRASS = 0;
    public const int ROCK = 1;

    public TileMap tileMap;

    public bool isValidMovement = false;
    public bool isValidAttack = false;

    public int type; 
    public int x;
    public int y;

    public const int MAT_GRASS_INVALID = 0;
    public const int MAT_GRASS_VALID_MOVEMENT = 1;
    public const int MAT_ROCK_INVALID = 2;
    public const int MAT_GRASS_VALID_ATTACK = 3;
    public const int MAT_ROCK_VALID = 4;

    [SerializeField] // So we can add in the mats in the inspector
    public Material[] mats;

    public void setTile(int type, int x, int y, TileMap tileMap) {
        this.type = type;
        this.x = x;
        this.y = y;
        this.tileMap = tileMap;

        if (type == GRASS) {
            gameObject.GetComponent<Renderer>().material = mats[MAT_GRASS_INVALID];
        }
        else if (type == ROCK) {
            gameObject.GetComponent<Renderer>().material = mats[MAT_ROCK_INVALID];
        }
    }

    // Assertion: isValidAttack and isValidMovement can never be true at the same time for any tile
    public void setAsValidMovement() {
        Assert.IsTrue(isValidAttack == false);
        if (type != GRASS) {
            return; // only grass can be a valid tile
        }
        isValidMovement = true;
        gameObject.GetComponent<Renderer>().material = mats[MAT_GRASS_VALID_MOVEMENT];
    }

    public void setAsValidAttack() {
        Assert.IsTrue(isValidMovement == false);
        isValidAttack = true;
        if (type == GRASS) {
            gameObject.GetComponent<Renderer>().material = mats[MAT_GRASS_VALID_ATTACK];
        }
        else if (type == ROCK) {
            gameObject.GetComponent<Renderer>().material = mats[MAT_ROCK_VALID];
        }
    }

    public void setAsInvalid() {
        Assert.IsTrue(!(isValidAttack && isValidMovement));
        isValidMovement = false;
        isValidAttack = false;
        
        if (type == GRASS) {
            gameObject.GetComponent<Renderer>().material = mats[MAT_GRASS_INVALID];
        }
        else if (type == ROCK) {
            gameObject.GetComponent<Renderer>().material = mats[MAT_ROCK_INVALID];
        }
       
    }


    void OnMouseUp() {
        tileMap.processTilePress(x, y);
    }
}
