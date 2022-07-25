using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCam : MonoBehaviour
{


 [Header("Phone")]
    [SerializeField] private RawImage img;
    bool shouldBeFrontfacing = false;
    private Texture2D temp;
    private WebCamTexture phoneCam;
    [SerializeField] private TextMeshProUGUI text;
    private bool isPhone = false;
    
    
    [Header("Editor")]
    [SerializeField] private Texture2D apple;
    void Awake()
    {
#if !UNITY_EDITOR
        isPhone = true;
#endif
        if (isPhone)
        {
            SetupCamera();
        }
        
    }
    
    
    private IEnumerator Start()
    {
        while (true)
        {
            try
            {
                if (isPhone)
                {
                    SendUDP();
                }
                else
                {
                    temp = new Texture2D(apple.width, apple.height);
                    TestSendUDP();
                }

                Display("[Sending...]"+Time.timeSinceLevelLoad);
                
            }
            catch (Exception e)
            {
                Display("[Error]"+e.Message);
//                Debug.LogError(e);
            }
            
            yield return new WaitForSeconds(1f);
        }
    }

    private void Display(string tex)
    {
        text.text = tex;
    }
    private void TestSendUDP()
    {
        //Debug.Log("Sending");
        temp.SetPixels(apple.GetPixels());
        UDP.Send(temp.GetRawTextureData());
    }
    
    private void SetupCamera()
    {
        foreach (var divice in WebCamTexture.devices)
        {
            if (divice.isFrontFacing == shouldBeFrontfacing)
            {
                phoneCam = new WebCamTexture(divice.name, Screen.height, Screen.height);
                phoneCam.Play();
                img.texture = phoneCam;
                //  foundCam = true;
                temp = new Texture2D(phoneCam.width, phoneCam.height);
            }
        }
    }

    
    private void SendUDP()
    {
       // Debug.Log("Sending");
      //  UDP.Send("hi");
        temp.SetPixels(phoneCam.GetPixels());
        UDP.Send(temp.GetRawTextureData());
       
    }
  private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        shouldBeFrontfacing = !shouldBeFrontfacing;
        SetupCamera();
    }

    public void TakePhoto()
    {
        Texture2D snap = new Texture2D(phoneCam.width, phoneCam.height);
        snap.SetPixels(phoneCam.GetPixels());
        snap.Apply();
        Debug.Log(Application.persistentDataPath + "//" + Time.deltaTime + ".png");
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "//" + Time.deltaTime + ".png",
            snap.EncodeToPNG());
    }
    

}