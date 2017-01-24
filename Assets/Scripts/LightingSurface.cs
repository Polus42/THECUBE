using UnityEngine;
using System.Collections;

public class LightingSurface : MonoBehaviour {
    public float lightingspeed = 0.1f;
    public float lightingtime = 0.1f; 
    private Material _mat;
    private Transform _player;
    private GameObject _bullet;
    private bool _illuminating = false;
    private bool _lerping = false;
    private Vector3 _lerpspeed = Vector3.zero;
    // Use this for initialization
    void Start () {
        _mat = GetComponent<Renderer>().material;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _bullet = _player.GetComponent<CustomController>().instanciatedBullet;
        if (gameObject.tag=="Obstacle")
        {
            _mat.SetFloat("_MKGlowTexStrength", 0);
        }
        glowWithTheBeat();
    }
	
	// Update is called once per frame
	void Update () {
        _bullet = _player.GetComponent<CustomController>().instanciatedBullet;
        glowWithTheBeat();
        //illuminateIfClose();
	}
    // if the last beat is not now, use lastbeattime
    /*IEnumerator glow(float glowpower)
    {
        yield return new WaitForSeconds(distancewithplayer()/100*lightingspeed);
        _mat.SetFloat("_MKGlowPower", glowpower);
        float step = lightingtime / glowpower;
        for (float i = lightingtime; i>0;i-= step)
        {
            _mat.SetFloat("_MKGlowPower", i*10);
            yield return new WaitForSeconds(step);
        }
        _mat.SetFloat("_MKGlowPower", 0);
    }*/
    void glowWithTheBeat()
    {
        if (_bullet != null && Vector3.Distance(_bullet.transform.position, transform.position) < 5)
        {
            _mat.SetColor("_MKGlowColor", new Color(1, 0, 0, 255));
        }
            _mat.SetFloat("_MKGlowPower", 2.5f / Mathf.Abs(transform.position.z - (SpectrumAnalyzer.timeSinceLastBeat(0) * lightingspeed + _player.position.z)));
            _mat.SetFloat("_MKGlowPower", _mat.GetFloat("_MKGlowPower") + 2.5f / Mathf.Abs(transform.position.z - (SpectrumAnalyzer.timeSinceLastBeat(1) * lightingspeed + _player.position.z)));
    }
    float distancewithplayer()
    {
        return Vector3.Distance(_player.position,transform.position);
    }
    void illuminateIfClose()
    {
            if (_bullet != null)
            {
                float v = Vector3.Distance(_bullet.transform.position, transform.position);
                if (v < 10)
                {
                    _mat.SetColor("_MKGlowColor", new Color(255, 0, 0, 255));
                }
            }
    }
    void illuminate()
    {
        _mat.SetColor("_MKGlowColor", new Color(0, 255, 0, 255));
        _illuminating = true;
        lerpDown();
    }
    void lerpDown()
    {
        _lerping = true;
    }
    void init()
    {
        _mat.SetFloat("_MKGlowPower", 0);
        StopAllCoroutines();
    }
    void resetColor()
    {
        _mat.SetColor("_MKGlowColor", new Color(0, 0.79f, 1, 1));
    }
}
