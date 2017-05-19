﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {
    [Serializable]
    public class Count
    {
        // TODO ESTO APLICA PARA UN JUEGO 2D ESTILO ISAAC, HAY QUE CAMBIAR VARIOS VALORES DE VARIABLES PARA QUE FUNCIONE COMO SE QUIERE.
        public int minimum;
        public int maximum;
        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
        public int columns = 8;
        public int rows = 8;
        public Count wallCount = new Count(5, 9);
        public Count coinCount = new Count(1, 5);
        public GameObject exit;
        public GameObject[] limitTiles;
        public GameObject[] blockTiles;
        public GameObject[] coinTiles;
        public GameObject[] enemyTiles;
        public GameObject[] outerWallTiles;
        private Transform boardHolder;
        private List<Vector3> gridPositions = new List<Vector3>();

        void InitializeList()
        {
            gridPositions.Clear();
            for (int x = 0; x < columns - 1; x++)
            {
                for (int y = 0; y < rows -1; y++)
                {
                    gridPositions.Add(new Vector3(x, y, 0f));

                }
            }
        }
        void BoardSetup()
        {
            boardHolder = new GameObject("Board").transform;

            for (int x = -1; x < columns + 1; x++)
            {
                for (int y = -1; y < rows + 1; y++)
                {
                    GameObject toInstantiate = limitTiles[Random.Range(0, limitTiles.Length)];
                    if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                    }
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    instance.transform.SetParent(boardHolder);
                }
            }
        }
        Vector3 RandomPosition()
        {
            int randomIndex = Random.Range(0, gridPositions.Count);
            Vector3 randomPosition = gridPositions[randomIndex];
            gridPositions.RemoveAt(randomIndex);
            return randomPosition;
        }
        void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        {
            int objectCount = Random.Range(minimum, maximum + 1);
            for (int i = 0; i < objectCount; i++)
            {
                Vector3 randomPosition = RandomPosition();
                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
                Instantiate(tileChoice, randomPosition, Quaternion.identity);

            }
        }
        public void SetupScene(int level)
        {
            BoardSetup();
            InitializeList();
            LayoutObjectAtRandom(limitTiles, wallCount.minimum, wallCount.maximum);
            LayoutObjectAtRandom(coinTiles, coinCount.minimum, coinCount.maximum);
            int enemyCount = (int)Mathf.Log(level, 2f);
            LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
            Instantiate(exit, new Vector3(columns - 1, rows - 1, 0F), Quaternion.identity); 
        }
        public void llamar(int level) {
            SetupScene(level);
        }
    }

    public Count CountProperty { get; set; }
}
