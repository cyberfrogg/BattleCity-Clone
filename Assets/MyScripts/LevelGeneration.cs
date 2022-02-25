using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// ReSharper disable All

public class LevelGeneration : MonoBehaviour
{
    public List<GameObject> AllRoomPoint;
    public List<Transform> StartingPosForPlayer;
    public GameObject PlayerTransform;
    public Transform[] EnemyTransform;
    public Transform[] StartingPosForEnemy;
    public GameObject[] Rooms;
    public GameObject CityBlock;
    public float offset;
    public float MinX;
    public float MaxX;
    public float MinY;
    public float _resetTime;
    public GameObject LevelIsLoading;

    private int _direction;
    private bool _generateRoom = true;
    private GameObject CurrentRoom;
    private int DownCounter;

    private float _timeToGenerateRoom;



    void Awake()
    {
        //LevelIsLoading.SetActive(true);
    }


    void Start()
    {

        transform.position = StartingPosForEnemy[Random.Range(0, StartingPosForEnemy.Length)].position;


        for (int i = 0; i < EnemyTransform.Length; i++)
        {
            EnemyTransform[i].position = StartingPosForEnemy[Random.Range(0, StartingPosForEnemy.Length)].position;
        }



        _direction = Random.Range(1, 5);
    }

    void Update()
    {
        if (_timeToGenerateRoom < 0 && _generateRoom) 
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
        }
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


    private void CitySpawnPoint()
    {
        for (int i = 0; i < StartingPosForPlayer.Count; i++)
        {
            if (StartingPosForPlayer[i].position == transform.position)
            {
                StartingPosForPlayer.RemoveAt(i);
            }
        }

        int rand = Random.Range(0, StartingPosForPlayer.Count);

        StartingPosForPlayer[rand].gameObject.tag = "Untagged";

        Instantiate(CityBlock, StartingPosForPlayer[rand].position,
            Quaternion.identity);

    }

    private void GenerateOthers()
    {


        PlayerTransform.transform.position = transform.position;
        CitySpawnPoint();

        Debug.Log(AllRoomPoint.Count);
        foreach (var room in AllRoomPoint)
        {
            Instantiate(Rooms[Random.Range(0, Rooms.Length)], room.transform.position, Quaternion.identity);
        }

        LevelIsLoading.SetActive(false);

    }
}
