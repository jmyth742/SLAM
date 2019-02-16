using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Miro3DCamera))]
public class SendPointcloud : MonoBehaviour {
    
    ZmqCommunicator _pub;

    [Tooltip("frame per second (Hz), 0 mean each Update()")]
    public float fps;   // = 10.0f;

    [Tooltip("es: tcp://127.0.0.1:5401")]
    public string ZmqChannel;     // = "tcp://127.0.0.1:5401"
        
    private Coroutine _coroutine;
    Miro3DCamera camera3d;

    Vector3[] pointCloud;



    // Use this for initialization
    void Start () {
        camera3d = GetComponent<Miro3DCamera>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator Worker()
    {

        MiroZmq.ZmqPC _pc = new MiroZmq.ZmqPC();        
        for (; ; )
        {
            if (camera3d != null && camera3d.IsInitialized())
            {
                camera3d.CalcPointCloud(out pointCloud);
                RotatePC(ref pointCloud);
                //  .Write(_pc.Serialize(camera3d.GetPointcloudSize().rows, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, camera3d.GetPointcloudSize().cols, ref pointCloud));
            }
            

            if (fps > 0)
            {
                yield return new WaitForSeconds(1.0f / fps);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void Awake()
    {
        _pub = gameObject.AddComponent<ZmqCommunicator>();
    }

    private void OnEnable()
    {
        _pub.Init(ZmqCommunicatorType.PUB, ZmqChannel);
        _coroutine = StartCoroutine(Worker());        
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
        _pub.Stop();
    }
        

    void RotatePC(ref Vector3[] pts)
    {
        // si vuole:
        //  z si allontana dal sensore
        //  x verso il basso
        //  y verso sinistra
        // ma ora 
        //  z si allontana dal sensore
        //  x verso destra
        //  y verso l'alto

        for (int i=0; i < pts.Length; i++)
        {
            float appX = pts[i].x;
            pts[i].x = -pts[i].y;
            pts[i].y = -appX;
            pts[i].z = pts[i].z;
        }
    }

}
