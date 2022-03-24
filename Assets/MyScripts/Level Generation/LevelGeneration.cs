using Cysharp.Threading.Tasks;
using GameUtils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


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

    public GameObject[] PlayerSpawner;


    public float offset;
    public float MinX;
    public float MaxX;
    public float MinY;


    public Collider2D[] blocksForEnemy;
    public Collider2D[] blocksForPlayer;
    public Waves wave;


    public float _resetTime;
    //public GameObject LevelIsLoading;

    private int _direction;
    private bool _generateRoom = true;
    private GameObject CurrentRoom;
    private int DownCounter;

    private float _timeToGenerateRoom;

    public UnityEvent GeneratePlayer;
    public AudioClip LevelStartAudio;
    public AudioClip PlayerIdleAudio;



    void Awake()
    {

    }

    async void Start()
    {


        GenerateCity();
        AudioManager.Instance.PlayBGM(LevelStartAudio);
        await UniTask.Delay(TimeSpan.FromSeconds(LevelStartAudio.length - 2f), ignoreTimeScale: false);
        GeneratePlayerPosition();
        //AudioManager.Instance.PlayBackGroundSFX(PlayerIdleAudio);
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
        else if (_direction == 3 || _direction == 4)
            LeftMovement();
        else if (_direction == 5)
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

        GenerateRoom();
    }



    [ContextMenu("Generate Room")]
    async void GenerateRoom()
    {
        foreach (var room in AllRoomPoint)
        {
            var chunk=Instantiate(Rooms[Random.Range(0, Rooms.Length)], room.transform.position, Quaternion.identity);
            SpawnPoint chunkPoint = chunk.GetComponent<SpawnPoint>();
            chunkPoint.Initialize();


            await UniTask.Delay(TimeSpan.FromSeconds(.1f), ignoreTimeScale: false);
        }

        DestroyBlock();

    }

    [ContextMenu("Destroy Block")]
    void DestroyBlock()
    {
        for (int i = 0; i < StartingPosForEnemy.Length; i++)
        {
            blocksForEnemy = Physics2D.OverlapCircleAll(StartingPosForEnemy[i].position, .5f);

            if (blocksForEnemy != null)
            {
                foreach (var block in blocksForEnemy)
                {
                    if (!block.gameObject.CompareTag("barrier"))
                        Destroy(block.gameObject);
                }
            }
        }

        int sign = 1;

        for (int i = 0; i < 2; i++)
        {
            sign = -sign;
            blocksForPlayer = Physics2D.OverlapCircleAll(new Vector3(CityGenerationPoint.position.x - CityGenerationXOffset * sign,
                CityGenerationPoint.position.y, CityGenerationPoint.position.z), Random.Range(.5f, 2f));

            if (blocksForPlayer != null)
            {
                foreach (var block in blocksForPlayer)
                {
                    if (!block.gameObject.CompareTag("barrier") && !block.gameObject.CompareTag("Player"))
                        Destroy(block.gameObject);
                }
            }
        }

        
    }

    [ContextMenu("Generate Player")]

    async void GeneratePlayerPosition()
    {

        GeneratePlayer?.Invoke();

        /*foreach (var spawner in PlayerSpawner)
        {
            spawner.GetComponent<Spawner>().AnimationObject.SetActive(true);
        }

        await UniTask.Delay(TimeSpan.FromSeconds(1.5f), ignoreTimeScale: false);


        foreach (var spawner in PlayerSpawner)
        {
            spawner.GetComponent<Spawner>().AnimationObject.SetActive(false);
        }*/




        /*PlayerTransform1.transform.position = new Vector3(CityGenerationPoint.position.x - CityGenerationXOffset,
            CityGenerationPoint.position.y, CityGenerationPoint.position.z);

        PlayerTransform2.transform.position = new Vector3(CityGenerationPoint.position.x + CityGenerationXOffset,
            CityGenerationPoint.position.y, CityGenerationPoint.position.z);*/

        GenerateWave();

    }

    [ContextMenu("Generate Enemy Wave")]
    void GenerateWave()
    {
        wave.WaveGeneration = true;
    }


}
