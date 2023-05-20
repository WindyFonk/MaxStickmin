using System.Threading.Tasks;
using System;
using TMPro;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    // email, password
    [SerializeField] TMP_InputField email;
    [SerializeField] TMP_InputField password;
    [SerializeField] TextMeshProUGUI textFail;

    public async void onLogin()
    {
        var _email = email.text;
        var _password = password.text;
        // Gọi API
        // UnityWebRequest POST
        var url = "https://fpoly-hcm.herokuapp.com/api/auth/login";
        var form = new WWWForm();
        form.AddField("email", _email);
        form.AddField("password", _password);

        var result = await PostAPI(url, form);
        if (result)
        {
            // dang nhap khong thanh cong
            textFail.gameObject.SetActive(true);
        }
        else
        {
            // thanh cong  
            SceneManager.LoadScene(1);
            textFail.gameObject.SetActive(false);

        }

    }
    public void onCancel()
    {

    }

    public async Task<bool> PostAPI(string url, WWWForm form)
    {
        try
        {
            using var www = UnityWebRequest.Post(url, form);
            var operation = www.SendWebRequest();
            while (!operation.isDone) await Task.Yield();
            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log(www.error);
            /*var result = www.downloadHandler.text;
            Debug.Log(result);*/


            var result = www.downloadHandler.text;
            var model = LoginResponseModel.CreateFormJSON(result);
            return model.error;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return default;
        }
    }
}


[System.Serializable]
public class LoginResponseModel
{
    public bool error;
    public int statusCode;
    public static LoginResponseModel CreateFormJSON(string jsonString)
    {
        return JsonUtility.FromJson<LoginResponseModel>(jsonString);
    }


}