using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoadTypes
{
    miami,
    street
}

public class MapController : MonoBehaviour
{
    [SerializeField] private float surrounding_difference_x = 4.36f;
    [SerializeField] private float roadTyles_difference_y = 3.2f;
    [SerializeField] private Vector2 mapStartCoordinates;
    [Space]
    [SerializeField] private GameObject[] RoadTyles_Miami;
    [SerializeField] private GameObject[] RoadTyles_Street;
    [Space]
    [SerializeField] private GameObject[] Obstacles;
    [SerializeField] private float[] obstacleCoords_x;
    [Space]
    [SerializeField] private GameObject[] Surrounding;
    [SerializeField] private int[] Surrounding_spawnChances;
    [Space]
    [SerializeField] private Transform ObstaclesContainer;
    [SerializeField] private Transform SurroundingContainer;
    [SerializeField] private Transform RoadContainer;

    private bool _lastSurroundingWasNotDouble = false;

    private delegate bool SurroundingSpawnCondition(GameObject element);

    private void Start()
    {
        GenerateMap(RoadTypes.miami);
    }

    /// <summary>
    /// function of map generation
    /// </summary>
    /// <param name="roadType">location type</param>
    /// <param name="length">roadTyles number</param>
    /// <param name="obstacles_opacity">roadTyles per obstacle</param>
    public void GenerateMap(RoadTypes roadType, int length = 50, int obstacles_opacity = 3)
    {
        GameObject[] roadTyles = null;
        int obstacles_opacity_multiplier = 1;

        //roadType indification
        switch (roadType)
        {
            case RoadTypes.miami:
                roadTyles = RoadTyles_Miami;
                break;
            case RoadTypes.street:
                roadTyles = RoadTyles_Street;
                break;
        }

        //generation cycle
        for (int i = 0; i < length; i++)
        {
            GameObject newTyle = Instantiate(roadTyles[Random.Range(0, roadTyles.Length)]);
            float coord_y = mapStartCoordinates.y + roadTyles_difference_y * i;
            newTyle.transform.position = new Vector2(mapStartCoordinates.x, coord_y);
            newTyle.GetComponent<Tyle_Initializator>().InitializateSetting(new Vector2(0, coord_y));
            newTyle.transform.parent = RoadContainer;

            //obstacles check
            if(i == (obstacles_opacity - 1) * obstacles_opacity_multiplier)
            {
                SpawnObstacles(coord_y, 3);
                obstacles_opacity_multiplier++;
            }

            //surrounding spawn
            SpawnSurrounding(coord_y);
        }
    }

    #region Surrounding
    private void SpawnSurrounding(float coord_y)
    {
        //left
        GameObject surroundingElement = null;

        if (_lastSurroundingWasNotDouble)
        {
            surroundingElement = RandomiseSurroundingElement((GameObject element) => { return true; });
            _lastSurroundingWasNotDouble = false;
        }
        else
        {
            surroundingElement = RandomiseSurroundingElement(SurroundingElement_SpawnCondition);
        }

        //right
        if (surroundingElement.GetComponent<SurroundingElemnt>().doubleElemnt)
        {
            Surrounding_Spawner(surroundingElement, -surrounding_difference_x, coord_y);
            Surrounding_Spawner(surroundingElement, surrounding_difference_x, coord_y);
        }
        else
        {
            surroundingElement = RandomiseSurroundingElement(SurroundingElement_SpawnCondition);

            if (Random.Range(0, 2) == 0)
            {
                Surrounding_Spawner(surroundingElement, -surrounding_difference_x, coord_y);
            }
            else
                Surrounding_Spawner(surroundingElement, surrounding_difference_x, coord_y);

            _lastSurroundingWasNotDouble = true;

            #region Tryed to make check for will object be only on one side or not
            //will surrounding be only on one side
            /*int oneSide = Random.Range(0, 2);

            if(oneSide == 0)//only on one side
            {
                if (Random.Range(0, 2) == 0)
                {
                    Surrounding_Spawner(surroundingElement, -surrounding_difference_x, coord_y);
                }
                else
                    Surrounding_Spawner(surroundingElement, surrounding_difference_x, coord_y);
            }
            else//on two sides
            {
                Surrounding_Spawner(surroundingElement, -surrounding_difference_x, coord_y);
                surroundingElement = RandomiseSurroundingElement(SurroundingElement_SpawnCondition);
                Surrounding_Spawner(surroundingElement, surrounding_difference_x, coord_y);
            }*/
            #endregion
        }
    }

    private void Surrounding_Spawner(GameObject surroundingElement, float difference_x, float coord_y)
    {
        GameObject newSurrounding = Instantiate(surroundingElement);
        newSurrounding.transform.position = new Vector2(mapStartCoordinates.x + difference_x, coord_y + 1);
        newSurrounding.transform.parent = SurroundingContainer;
    }

    /// <summary>
    /// returns true if object isnt double
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private bool SurroundingElement_SpawnCondition(GameObject element)
    {
        if (!element.GetComponent<SurroundingElemnt>().doubleElemnt)
        {
            return true;
        }

        return false;
    }

    private GameObject RandomiseSurroundingElement(SurroundingSpawnCondition spawnCondition)
    {
        GameObject surroundingElement = null;

        for (int j = 0; j < Surrounding.Length; j++)
        {
            if (!spawnCondition.Invoke(Surrounding[j]))
            {
                continue;
            }

            if (Random.Range(0, Surrounding_spawnChances[j]) == 0)
            {
                if (surroundingElement != null)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        surroundingElement = Surrounding[j];
                    }
                }
                else
                {
                    surroundingElement = Surrounding[j];
                }
            }
        }

        if (surroundingElement == null)
        {
            foreach(var j in Surrounding)
            {
                if (!j.GetComponent<SurroundingElemnt>().doubleElemnt)
                {
                    surroundingElement = j;
                    break;
                }
            }
        }


        return surroundingElement;
    }
    #endregion

    /// <summary>
    /// obstacles spawnner function
    /// </summary>
    /// <param name="coords_y"></param>
    /// <param name="obstSpawn_chance">chances to spawn obst = 1/(this number - 1)</param>
    private void SpawnObstacles(float coords_y, int obstSpawn_chance)
    {
        //randomize is it needed to spran obstacles
        if(Random.Range(0, obstSpawn_chance) != 0)
        {
            return;
        }

        int obstaclesNumber = Random.Range(0, obstacleCoords_x.Length);
        List<float> coordinates = new List<float>();
        List<float> coordinates_sorted = new List<float>();

        //add coords to dummy List
        foreach(var j in obstacleCoords_x)
        {
            coordinates.Add(j);
        }

        //determine coords with obstacles
        for (int j = 0; j < obstaclesNumber; j++)
        {
            int newCoord_index = Random.Range(0, coordinates.Count);
            coordinates_sorted.Add(coordinates[newCoord_index]);
            coordinates.RemoveAt(newCoord_index);
        }

        coordinates.Clear();

        //spawn obsts
        foreach(var coord in coordinates_sorted)
        {
            GameObject newObstacle = Instantiate(Obstacles[Random.Range(0, Obstacles.Length)]);
            coords_y = Random.Range(coords_y - roadTyles_difference_y / 2, coords_y + roadTyles_difference_y / 2);
            newObstacle.transform.position = new Vector2(coord, coords_y);
            newObstacle.transform.parent = ObstaclesContainer;
        }
    }
}
