using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour {
    public GameObject cube;
    public int generationRangeForward = 5;
    public int generationRangeBackward = 5;
    public int generationRangeSides = 5;
    public GameObject[] obstacle;
    private Transform _player;
    //private System.Predicate<GameObject> _cubesoutside;
    // array for cubes positions
    //private List<Vector2> _position_pool = new List<Vector2>();
    //private List<GameObject> _object_pool = new List<GameObject>();
    // Use this for initialization
    void Start () {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        generateStart();
    }
	
	// Update is called once per frame
	void Update () {
        generateObstacles();
    }
    void generateStart()
    {
        for (int x = -generationRangeSides; x < generationRangeSides; x++)
        {
            for (int z = 0; z < generationRangeForward; z++)
            {
                Instantiate(cube, new Vector3(x, 0, z), transform.rotation);
                //_object_pool.Add(c);
            }
        }
    }
    void generateObstacles()
    {
        Collider[] test = Physics.OverlapSphere(new Vector3(0, 0, _player.position.z + 85), 0.1f);
        if (test.Length>0 && test[0].GetComponent<Renderer>().material.GetFloat("_MKGlowPower")>=1)
        {
            //for (int x = -10;x<10; x++)
            //Instantiate(obstacle[(int)Random.Range(0, obstacle.Length)], new Vector3(x, 1, test[0].transform.position.z), obstacle[(int)Random.Range(0, obstacle.Length)].transform.rotation);
            float difficulty = Time.time/(5*60);
            int count = (int)(Random.Range(0,11)*difficulty);
            for (int i=0;i< count; i++)
            {
                Instantiate(obstacle[0], new Vector3(Random.Range(-9, 9), 1, test[0].transform.position.z), obstacle[0].transform.rotation);
            }
        }
    }
    bool isthereacubehere(Vector2 pos)
    {
        return Physics.OverlapSphere(new Vector3(pos.x,0,pos.y), 0.1f).Length>0;
    }
    /*bool isthereacubehere2(Vector2 v)
    {
        foreach (GameObject go in _object_pool)
        {
            if(Mathf.Approximately(go.transform.position.x, v.x * 10) && Mathf.Approximately(go.transform.position.z, v.y * 10) )
            {
                return true;
            }
        }
        return false;
    }*//*
    void generateTerrain2()
    {
        // if not enough terrain, generating
        for (int x = (int)_player.position.x /10- generationRangeBackward; x < (int)_player.position.x/10 + generationRangeForward; x++)
        {
            for (int z = (int)_player.position.z/10 - generationRangeSides; z < (int)_player.position.z/10 + generationRangeSides; z++)
            {
                Vector2 v = new Vector2(x, z);
                if (!isthereacubehere2(v))
                {
                    GameObject c = (GameObject)Instantiate(cube, new Vector3(x *10 , 0, z *10), transform.rotation);
                    c.transform.parent = transform;
                    _object_pool.Add(c);
                }
            }
        }
    }*/
    /*void generateTerrain()
    {
        int r,z,x;
        for (z = Mathf.FloorToInt(_player.transform.position.z)+generationRangeForward-3;z< Mathf.FloorToInt(_player.transform.position.z)+generationRangeForward;z++)
        {
            if (!isthereacubehere(new Vector2(Mathf.FloorToInt(_player.transform.position.x),z)))
            {
                r = Random.Range(-generationRangeSides, generationRangeSides);
                for (x = (Mathf.FloorToInt(-generationRangeSides)); x < (Mathf.FloorToInt(generationRangeSides)); x++)
                {
                    GameObject c = (GameObject)Instantiate(cube, new Vector3(x, 0, z), transform.rotation);
                    c.transform.parent = transform;
                    //_object_pool.Add(c);
                    if (x==r && (int)Random.Range(0,100)==1)
                    {
                        GameObject c2 = (GameObject)Instantiate(obstacle[0], new Vector3(r, 0, z), transform.rotation);
                        c2.transform.parent = transform;
                    }
                }
            }
        }
    }*/
    /*void destroyTerrain()
    {
        List<GameObject> cubes = _object_pool.FindAll(_cubesoutside);

        foreach (GameObject g in cubes)
        {
            Destroy(g);
        }
        _object_pool.RemoveAll(_cubesoutside);
    }*/
    /*List<GameObject> cubesOutsideRange()
    {
        return _object_pool.FindAll(_cubesoutside);
    }*/
}
