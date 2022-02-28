using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// ReSharper disable All

public class LevelGeneration : MonoBehaviour
{
    public List<GameObject> AllRoomPoint;
    public GameObject[] Rooms;

    //public List<Transform> StartingPosForPlayer;
    public GameObject PlayerTransform1;
    public GameObject PlayerTransform2;

    //public Transform[] EnemyTransform;
    public Transform[] StartingPosForEnemy;


    public GameObject CityBlock;
    public Transform CityGenerationPoint;
    public float CityGenerationXOffset;


    public float offset;
    public float MinX;
    public float MaxX;
    public float MinY;


    public float _resetTime;
    //public GameObject LevelIsLoading;

    private int _direction;
    private bool _generateRoom = true;
    private GameObject CurrentRoom;
    private int DownCounter;

    private float _timeToGenerateRoom;



    void Start()
    {
        //_direction = Random.Range(1, 5);
    }

    void Update()
    {
        /*if (_timeToGenerateRoom < 0 && _generateRoom) 
        {
            Generate();
            _timeToGenerateRoom = _resetTime;
        }
        else
        {
            _timeToGenerateRoom -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }*/
    }

    private void Generate()
    {
        if (_direction == 1 || _direction == 2)
            RightMovement();
        else if(_direction == 3 || _direction == 4)
            LeftMovement();
        else if(_direction == 5)
            DownMovement();

    }

    private void LeftMovement()
    {
        if (transform.position.x > MinX)
        {
            Vector2 movePos = new Vector2(transform.position.x - offset, transform.position.y);
            transform.position = movePos;
            GenerateTiles(false);

            _direction = Random.Range(3, 6);
        }
        else
        {
            _direction = 5;
        }

    }


    private void RightMovement()
    {
        if (transform.position.x < MaxX)
        {
            Vector2 movePos = new Vector2(transform.position.x + offset, transform.position.y);
            transform.position = movePos;
            GenerateTiles(false);

            _direction = Random.Range(1, 6);
            if (_direction == 3)
                _direction = 2;
            if (_direction == 4)
                _direction = 5;
        }
        else
        {
            _direction = 5;
        }
    }

    private void DownMovement()
    {
        if (transform.position.y > MinY)
        {
            DownCounter++;

            GenerateTiles(true);

            Vector2 movePos = new Vector2(transform.position.x, transform.position.y - offset);
            transform.position = movePos;
            CurrentRoom = Instantiate(Rooms[Random.Range(2, 4)], transform.position, Quaternion.identity);
            RemoveRoomSpawnPoint();

            _direction = Random.Range(1, 6);
        }
        else
        {

            _generateRoom = false;
            GenerateOthers();
        }
    }

    private void GenerateTiles(bool state)
    {

        RemoveRoomSpawnPoint();

        if (!state)
        {
            DownCounter = 0;
            CurrentRoom = Instantiate(Rooms[Random.Range(0, Rooms.Length)], transform.position, Quaternion.identity);
        }
            
        else
        {
            if (CurrentRoom != null)
            {
                Destroy(CurrentRoom);
            }

            if (DownCounter > 1)
            {
                CurrentRoom = Instantiate(Rooms[3], transform.position, Quaternion.identity);

            }
            else
            {


                int rand = Random.Range(1, 4);

                if (rand == 2)
                    rand = 1;

                CurrentRoom = Instantiate(Rooms[rand], transform.position, Quaternion.identity);
            }


        }
    }


    private void RemoveRoomSpawnPoint()
    {
        /*foreach (var room in AllRoomPoint)
        {
            if (room.transform.position == transform.position)
            {
                AllRoomPoint.Remove(room);
            }
        }*/

        for (int i = 0; i < AllRoomPoint.Count; i++)
        {
            if (AllRoomPoint[i].transform.position == transform.position)
            {
                AllRoomPoint.RemoveAt(i);
            }
        }
    }


    private void GenerateOthers()
    {


        
        GenerateCity();

        GeneratePlayerPosition();


        foreach (var room in AllRoomPoint)
        {
            Instantiate(Rooms[Random.Range(0, Rooms.Length)], room.transform.position, Quaternion.identity);
        }

    }


    [ContextMenu("Generate City")]
    void GenerateCity()
    {
        Instantiate(CityBlock, CityGenerationPoint.position, Quaternion.identity);
    }

    [ContextMenu("Generate Player")]

    void GeneratePlayerPosition()
    {
        PlayerTransform1.transform.position = new Vector3(CityGenerationPoint.position.x - CityGenerationXOffset,
            CityGenerationPoint.position.y, CityGenerationPoint.position.z);

        PlayerTransform2.transform.position = new Vector3(CityGenerationPoint.position.x + CityGenerationXOffset,
            CityGenerationPoint.position.y, CityGenerationPoint.position.z);

    }

    [ContextMenu("Generate Room")]
    void GenerateRoom()
    {
        foreach (var room in AllRoomPoint)
        {
            Instantiate(Rooms[Random.Range(0, Rooms.Length)], room.transform.position, Quaternion.identity);
        }
    }

    [ContextMenu("Generate Enemy")]
    void GenerateEnemy()
    {
        for (int i = 0; i < StartingPosForEnemy.Length; i++)
        {
            
        }
    }


}
