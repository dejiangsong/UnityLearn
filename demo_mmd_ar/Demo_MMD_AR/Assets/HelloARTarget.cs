using UnityEngine;
using EasyAR;

namespace EasyARSample
{
    public class HelloARTarget : MonoBehaviour
    {
        private const string title = "Please enter KEY first!";
        private const string boxtitle = "9BVupsbbTSjeqNYGlg1tXsaOE2OKQiKva786547bPgVn0BKiszc2vOwMm6AZx8ISC3HfxiAbw02pN0bQpSnpwtK3fV7wb3QKWVoZ5nEM6oOowTfaxGZwibVj9R87ZM7cQ6Nwb54Z9hguWKet20kvfDBuSHN2eUyf7bV4sfHHJufERqw9xpFP6pyqquZYfgXsQibv7IgI";
        private const string keyMessage = ""
            + "Steps to create the key for this sample:\n"
            + "  1. login www.easyar.com\n"
            + "  2. create app with\n"
            + "      Name: HelloARMultiTarget-SameImage (Unity)\n"
            + "      Bundle ID: cn.easyar.samples.unity.helloarmultitarget.si\n"
            + "  3. find the created item in the list and show key\n"
            + "  4. replace all text in TextArea with your key";

        private void Awake()
        {
            if (FindObjectOfType<EasyARBehaviour>().Key.Contains(boxtitle))
            {
#if UNITY_EDITOR
                UnityEditor.EditorUtility.DisplayDialog(title, keyMessage, "OK");
#endif
                Debug.LogError(title + " " + keyMessage);
            }
        }
    }
}
