using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SystemManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] float volumn;

    void Update()
    {
        mixer.SetFloat("SE", volumn);

        // ������ �˾��� ����� ���� ������ �Ѵ�.
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();     // ���� ����.
            if(Application.platform == RuntimePlatform.Android)
            {
                //..
            }
            else if(Application.platform == RuntimePlatform.IPhonePlayer)
            {
                //...
            }
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        // ������ ��� ���� ȭ���� ��ȯ���� �� �Ͻ����� �뵵�� ���.
        Debug.Log("OnApplicationFocus : " + focus);
    }
    private void OnApplicationPause(bool pause)
    {
        Debug.Log("OnApplicationPause : " + pause);
    }
    private void OnApplicationQuit()
    {
        // ������ �������� �� ȣ��Ǵ� �̺�Ʈ.
        // ���� �߰� ���尰�� ��Ȳ�� �����.
        Debug.Log("OnApplicationQuit");
    }
}
