using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsMenu : MonoBehaviour {
    public Miro3DCamera miroCamera;

    ZmqCommunicator zmq = new ZmqCommunicator();
    MiroZmq.ZmqPC _pc = new MiroZmq.ZmqPC();

    public GameObject captureMenu;
    public GameObject ICPMenu;

    private TMP_InputField[] inputFields;
    float verticalFOV;
    float horizontalFOV;
    int verticalRes;
    int horizontalRes;
    float maxRange;

    private TMP_InputField[] icpFields;
    int maxIter;
    float transformationEpsilon;
    float euclideanFitnessEpsilon;
    float filterSize;

    private void Start()
    {
        inputFields = captureMenu.GetComponentsInChildren<TMP_InputField>();
        icpFields = ICPMenu.GetComponentsInChildren<TMP_InputField>();
    }

    public void ValueChanged () {

        if (float.TryParse(inputFields[0].text, out verticalFOV))
        {
            miroCamera.FOV.v = verticalFOV;
        }
        if (float.TryParse(inputFields[1].text, out horizontalFOV))
        {
            miroCamera.FOV.h = horizontalFOV;
        }
        if (int.TryParse(inputFields[2].text, out verticalRes))
        {
            miroCamera.Size.cols = verticalRes;
        }
        if (int.TryParse(inputFields[3].text, out horizontalRes))
        {
            miroCamera.Size.rows = horizontalRes;
        }
        if (float.TryParse(inputFields[4].text, out maxRange))
        {
            miroCamera.MaxRange = maxRange;
        }

        if (int.TryParse(inputFields[5].text, out maxIter))
        {
            _pc._maxIter = maxIter;
        }
        if (float.TryParse(inputFields[6].text, out transformationEpsilon))
        {
            _pc._transformationEpsilon = transformationEpsilon;
        }
        if (float.TryParse(inputFields[7].text, out euclideanFitnessEpsilon))
        {
            _pc._euclideanFitnessEpsilon = euclideanFitnessEpsilon;
        }
        if (float.TryParse(inputFields[8].text, out filterSize))
        {
            _pc._filterSize = filterSize;
        }

        miroCamera.Reload();
    }

}
