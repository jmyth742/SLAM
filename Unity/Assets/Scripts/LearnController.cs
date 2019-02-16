using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LearnController : MonoBehaviour
{
    public Material p1mat;
    public Material p2mat;
    public Material linemat;
    public float time_between_animations;
    public GameObject mainPanel;
    public GameObject spaceButton;
    public Camera main_cam;

    private TMP_Text[] slides;
    private TMP_Text spaceButtonText;
    private int slideN;
    private int state;
    private GameObject p1;
    private GameObject p2;
    private float timer;
    private GameObject[][] connectors;
    private float button_color_timer;
    private bool button_color_increment;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        timer = 0;
        button_color_timer = 0;
        button_color_increment = true;

        p1 = new GameObject();
        p1.transform.position = new Vector3(-5, 0, 0);
        p1.transform.rotation = Quaternion.identity;
        p1.AddComponent<PointCloud1>();
        p1.GetComponent<PointCloud1>().main_material = p1mat;

        p2 = new GameObject();
        p2.transform.position = new Vector3(5, 0, 0);
        p2.transform.rotation = Quaternion.identity;
        p2.AddComponent<PointCloud2>();
        p2.GetComponent<PointCloud2>().main_material = p2mat;

        slides = mainPanel.GetComponentsInChildren<TMP_Text>();
        slideN = 0;
        ShowMenuItem(slideN);

        spaceButtonText = spaceButton.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > time_between_animations)
        {
            if (button_color_increment)
            {
                button_color_timer += Time.deltaTime/4;
            } else
            {
                button_color_timer -= Time.deltaTime/4;
            }
            if(button_color_timer > 0.5f)
            {
                button_color_increment = false;
            }
            if(button_color_timer < 0.01f)
            {
                button_color_increment = true;
            }
            if(state < 23)
            {
                spaceButton.SetActive(true);
            }            
            spaceButtonText.color = new Color(spaceButtonText.color.r, spaceButtonText.color.g, spaceButtonText.color.b,
                1-button_color_timer);
            
            if(Input.GetKeyDown("space") )
            {
                timer = 0;
                Next();
            }
        } else
        {
            spaceButton.SetActive(false);
            
        }

        if(state < 2)
        {
            main_cam.transform.position = new Vector3(0, 1, -10);
            main_cam.transform.rotation = Quaternion.Euler( new Vector3(0, 0, 0));
        }
        

    }

    public void Next()
    {
        state++;
        switch (state)
        {
            case 1:
                slideN++;
                break;
            case 2:
                slideN++;
                p1.GetComponent<PointCloud1>().Next();
                p2.GetComponent<PointCloud2>().Next();
                break;
            case 3:
                slideN++;   
                break;
            case 4:
                slideN++;
                p1.GetComponent<PointCloud1>().Next();
                p2.GetComponent<PointCloud2>().Next();
                break;
            case 5:
                slideN++;
                p1.GetComponent<PointCloud1>().Next();
                p2.GetComponent<PointCloud2>().Next();
                p1.GetComponent<PointCloud1>().Next();
                p2.GetComponent<PointCloud2>().Next();
                break;
            case 6:
                slideN++;
                p1.GetComponent<PointCloud1>().Next();
                p2.GetComponent<PointCloud2>().Next();
                break;

            case 7:
                slideN++;
                p1.GetComponent<PointCloud1>().Next();
                p2.GetComponent<PointCloud2>().Next();
                break;
            case 8:
                slideN++;
                p2.GetComponent<PointCloud2>().Next();
                CreateConnectors();
                break;
            case 9:
                DestroyConnectors();
                slideN++;
                break;
            case 10:
                p1.GetComponent<PointCloud1>().Next();
                slideN++;
                break;
            case 11:
                p1.GetComponent<PointCloud1>().Next();
                slideN++;
                break;
            case 12:
                slideN++;
                break;
            case 13:
                slideN++;
                break;
            case 14:
                slideN++;
                break;
            case 15:
                slideN++;
                break;
            case 16:
                slideN++;
                break;
            case 17:
                slideN++;
                CreateConnectors();
                break;
            case 18:
                DestroyConnectors();
                p1.GetComponent<PointCloud1>().Next();
                p2.GetComponent<PointCloud2>().Next();
                break;
            case 19:
                CreateConnectors();
                break;
            case 20:
                DestroyConnectors();
                p1.GetComponent<PointCloud1>().Next();
                p2.GetComponent<PointCloud2>().Next();
                break;
            case 21:
                CreateConnectors();
                break;
            case 22:
                DestroyConnectors();
                p1.GetComponent<PointCloud1>().Next();
                p2.GetComponent<PointCloud2>().Next();
                break;
            case 23:
                slideN++;
                break;
        }

        ShowMenuItem(slideN);

    }

    public void ShowMenuItem(int slideNumber)
    {
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].gameObject.SetActive(false);
        }

        slides[slideNumber].gameObject.SetActive(true);
    }

    public void CreateConnectors()
    {
        Vector3[][] pp1_pos = p1.GetComponent<PointCloud1>().GetSpheresPosition();
        Vector3[][] pp2_pos = p2.GetComponent<PointCloud2>().GetSpheresPosition();

        connectors = new GameObject[pp1_pos.Length][];

        for (int i = 0; i < pp1_pos.Length; i++)
        {
            connectors[i] = new GameObject[pp1_pos[i].Length];

            for (int j = 0; j < pp1_pos[i].Length; j++)
            {
                connectors[i][j] = new GameObject();
                LineRenderer lineRenderer = connectors[i][j].AddComponent<LineRenderer>();
                lineRenderer.material = linemat;
                lineRenderer.widthMultiplier = 0.1f;
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, pp1_pos[i][j]);
                lineRenderer.SetPosition(1, pp2_pos[i][j]); 
            }
        }

    }

    public void DestroyConnectors()
    {
        Vector3[][] pp1_pos = p1.GetComponent<PointCloud1>().GetSpheresPosition();
        for (int i = 0; i < pp1_pos.Length; i++)
        {
            for (int j = 0; j < pp1_pos[i].Length; j++)
            {
                Destroy(connectors[i][j]);
            }
        }
    }
}
