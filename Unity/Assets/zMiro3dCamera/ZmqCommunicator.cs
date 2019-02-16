using UnityEngine;
using System;
using NetMQ;
using NetMQ.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public enum ZmqCommunicatorType
{
    PUB, SUB
}

public class ZmqCommunicator : MonoBehaviour {

    private ZmqCommunicatorType _type;
    private SubscriberSocket _subSocket;
    private PublisherSocket _pubSocket;

    private string _ip;
    private Msg _receivedMsg = new Msg();
            
    static int INSTANCES = 0;

    private void OnDestroy()
    {
        Stop();

        INSTANCES--;

        if (INSTANCES == 0)
        {
            Debug.Log("NetMQConfig.Cleanup()");
            //NetMQConfig.Cleanup(true);
        }
    }


    private void Start()
    {
        INSTANCES++;
    }

    public void Init(ZmqCommunicatorType type, string ip) {
        //Debug.LogError("INITI ZMQ");
        _type = type;
        _ip = ip;

        if (_subSocket != null | _pubSocket != null)
        {
            Debug.LogWarning("ZmqCommunicator already started.");
            return;
        }


        if (_type == ZmqCommunicatorType.SUB)
        {
            try
            {
                AsyncIO.ForceDotNet.Force();
                _subSocket = new SubscriberSocket();
                _subSocket.Options.ReceiveHighWatermark = 100;
                _subSocket.Options.Linger = TimeSpan.Zero;
                _subSocket.SubscribeToAnyTopic();
                _subSocket.Connect(_ip);
                //Debug.Log("ZmqCommunicator Subscriber Connected to: " + _ip);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Got exception: " + ex);
            }
            
            _receivedMsg = new Msg();
            _receivedMsg.InitEmpty();

            return;
        }

        if (_type == ZmqCommunicatorType.PUB)
        {
            try
            {
                AsyncIO.ForceDotNet.Force();
                _pubSocket = new PublisherSocket();
                _pubSocket.Bind(_ip);
                //Debug.Log("ZmqCommunicator Publisher Binded to: " + _ip);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Got exception: " + ex);
            }
            return;
        }

    }

    public void Stop()
    {

        if (_subSocket != null)
        {
            try
            {
                _subSocket.Unsubscribe("");
                _subSocket.Disconnect(_ip);
                _subSocket.Close();
                _subSocket.Dispose();
                _subSocket = null;
                //Debug.Log("ZmqCommunicator Subscriber Disconnected from: " + _ip);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Got exception: " + ex);
            }
        }


        if (_pubSocket != null)
        {
            try
            {
                _pubSocket.Unbind(_ip);
                _pubSocket.Close();
                _pubSocket.Dispose();
                _pubSocket = null;
                //Debug.Log("ZmqCommunicator Publisher Unbinded from: " + _ip);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Got exception: " + ex);
            }
        }  




    }
    
    public bool TryGetLastMessage(ref byte[] msg)
    {
        if (_type != ZmqCommunicatorType.SUB)
        {
            Debug.LogWarning("Wrong Type of ZmqCommunicator notsub.");
            msg = null;
            return false;
        }

        if (_subSocket == null)
        {
            Debug.LogWarning("Subscriber not started.");
            msg = null;
            return false;
        }

        bool retVal = false;
        while (_subSocket.TryReceive(ref _receivedMsg, new TimeSpan(0, 0, 0)))
        {
            msg = _receivedMsg.Data;
            retVal = true;
        }
        
        return retVal;            
    }

    public bool Write(byte[] message)
    {
        if (_type != ZmqCommunicatorType.PUB)
        {
            Debug.LogWarning("Wrong Type of ZmqCommunicator.");
            return false;
        }

        if (_pubSocket == null)
        {
            Debug.LogWarning("Publisher not started.");
            return false;
        }

        _pubSocket.SendFrame(message);
        //Debug.Log("Writing message successful!");
        return true;
    }
    

}

namespace MiroZmq
{
    public struct ZmqPC
    {
        private const Byte TYPE = 16;
        public int _rows;
        public int _cols;
        public int _maxIter;
        public float _transformationEpsilon;
        public float _euclideanFitnessEpsilon;
        public float _filterSize;
        public float _tx;
        public float _ty;
        public float _tz;
        public float _rx;
        public float _ry;
        public float _rz;
        //public Vector3 _pts;
        public UInt64 pkgSize;
        public Vector3[] pointsRead;
        //public int[] colors;
        public int[] colorsRead;
        //public List<Color> color_list;


        public Byte[] Serialize(int rows, int cols, int maxIter, float transformationEpsilon, float euclideanFitnessEpsilon, float filterSize, float tx, float ty, float tz, float rx, float ry, float rz, ref Vector3[] points, ref int[] colors)
        {
            //Debug.LogError("Serializing");
            Byte[] buffer = new Byte[sizeof(Byte) +
                                     sizeof(int) +
                                     sizeof(int) +
                                     sizeof(int) +
                                     9*sizeof(float) +
                                     3*points.Length*sizeof(float)+
                                     points.Length*sizeof(int)];
            int index = 0;

            Array.Copy(BitConverter.GetBytes(TYPE), 0, buffer, index, sizeof(Byte));
            index += sizeof(Byte);

            Array.Copy(BitConverter.GetBytes(rows), 0, buffer, index, sizeof(int));
            index += sizeof(int);

            Array.Copy(BitConverter.GetBytes(cols), 0, buffer, index, sizeof(int));
            index += sizeof(int);

            Array.Copy(BitConverter.GetBytes(maxIter), 0, buffer, index, sizeof(int));
            index += sizeof(int);

            Array.Copy(BitConverter.GetBytes(transformationEpsilon), 0, buffer, index, sizeof(float));
            index += sizeof(float);

            Array.Copy(BitConverter.GetBytes(euclideanFitnessEpsilon), 0, buffer, index, sizeof(float));
            index += sizeof(float);

            Array.Copy(BitConverter.GetBytes(filterSize), 0, buffer, index, sizeof(float));
            index += sizeof(float);

            Array.Copy(BitConverter.GetBytes(tx), 0, buffer, index, sizeof(float));
            index += sizeof(float);

            Array.Copy(BitConverter.GetBytes(ty), 0, buffer, index, sizeof(float));
            index += sizeof(float);

            Array.Copy(BitConverter.GetBytes(tz), 0, buffer, index, sizeof(float));
            index += sizeof(float);

            Array.Copy(BitConverter.GetBytes(rx), 0, buffer, index, sizeof(float));
            index += sizeof(float);

            Array.Copy(BitConverter.GetBytes(ry), 0, buffer, index, sizeof(float));
            index += sizeof(float);

            Array.Copy(BitConverter.GetBytes(rz), 0, buffer, index, sizeof(float));
            index += sizeof(float);

            for (int i=0;i < points.Length; i++)
            {
                Array.Copy(BitConverter.GetBytes((float)points[i].x), 0, buffer, index, sizeof(float));
                index += sizeof(float);
            }
            for (int i = 0; i < points.Length; i++)
            {
                Array.Copy(BitConverter.GetBytes((float)points[i].y), 0, buffer, index, sizeof(float));
                index += sizeof(float);
            }
            for (int i = 0; i < points.Length; i++)
            {
                Array.Copy(BitConverter.GetBytes((float)points[i].z), 0, buffer, index, sizeof(float));
                index += sizeof(float);
            }
            for (int i = 0; i < points.Length; i++)
            {
                Array.Copy(BitConverter.GetBytes((int)colors[i]), 0, buffer, index, sizeof(int));
                index += sizeof(int);
            }

            //Debug.Log("X: " + points[289].x);
            //Debug.Log("Y: " + points[289].y);

            //Debug.Log("colors[200]: " + colors[200]);


            return buffer;
        }

        public bool Deserialize(ref byte[] buffer)
        {
            //Debug.LogError("Deserializing");
            
            int i=0;
            Byte type = (byte) BitConverter.ToChar(buffer, i);
            i += sizeof(Byte);
            int row = (int) BitConverter.ToInt32(buffer, i);
            i += sizeof(int);
            int cols = (int) BitConverter.ToInt32(buffer, i);
            i += sizeof(int);

            int maxIter = (int)BitConverter.ToInt32(buffer, i);
            i += sizeof(int);
            float transformationEpsilon = (float)BitConverter.ToSingle(buffer, i);
            i += sizeof(float);
            float euclideanFitnessEpsilon = (float)BitConverter.ToSingle(buffer, i);
            i += sizeof(float);
            float filterSize = (float)BitConverter.ToSingle(buffer, i);
            i += sizeof(float);

            float tx = (float)BitConverter.ToSingle(buffer, i);
            i += sizeof(float);
            float ty = (float)BitConverter.ToSingle(buffer, i);
            i += sizeof(float);
            float tz = (float)BitConverter.ToSingle(buffer, i);
            i += sizeof(float);
            float rx = (float)BitConverter.ToSingle(buffer, i);
            i += sizeof(float);
            float ry = (float)BitConverter.ToSingle(buffer, i);
            i += sizeof(float);
            float rz = (float)BitConverter.ToSingle(buffer, i);
            i += sizeof(float);

            //Debug.Log("rx " + rx);

            pointsRead = new Vector3[row*cols];
            colorsRead = new int[row];


            for (int j=0;j<row*cols; j++)
            {
                pointsRead[j].x = (float) BitConverter.ToSingle(buffer, i);
                i += sizeof(float);
            }

            for (int j=0;j<row*cols; j++)
            {
                pointsRead[j].y = (float) BitConverter.ToSingle(buffer, i);
                i += sizeof(float);
            }
            for (int j=0;j<row*cols; j++)
            {
                pointsRead[j].z = (float) BitConverter.ToSingle(buffer, i);
                i += sizeof(float);
            }
            for (int j = 0; j < row*cols; j++)
            {
                colorsRead[j] = (int) BitConverter.ToUInt32(buffer, i);
                i += sizeof(int);
            }

            _rows = row;
            _cols = cols;
            _maxIter = maxIter;
            _transformationEpsilon = transformationEpsilon;
            _euclideanFitnessEpsilon = euclideanFitnessEpsilon;
            _filterSize = filterSize;
            _tx = tx;
            _ty = ty;
            _tz = tz;
            _rx = rx;
            _ry = ry;
            _rz = rz;

            //Debug.Log("Colors deserialized:");
            //Debug.Log(colorsRead[200]);

            //color_list = new List<Color>();
            
            return true;
        }

    }
        
}