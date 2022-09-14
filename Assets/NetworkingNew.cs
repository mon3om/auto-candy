using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkingNew : MonoBehaviour
{
    public static string URL = "https://candy-server.herokuapp.com";

    public delegate void Callback();
    public static IEnumerator CreatUser(User user, Callback callback = null)
    {
        UnityWebRequest request = UnityWebRequest.Put(URL + "/", JsonUtility.ToJson(user).ToString());
        request.method = "POST";
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.result);
            MessagePopUp.Instance.EnablePopup(request.result.ToString());
        }
        else
        {
            MessagePopUp.Instance.EnablePopup(request.downloadHandler.text);
            if (callback != null) callback();
        }

        Debug.Log(request.result);
        Debug.Log(request.responseCode);
    }

    public static IEnumerator UpdateUser(User user, Callback callback = null)
    {
        UnityWebRequest request = UnityWebRequest.Put(URL + "/update", JsonUtility.ToJson(user).ToString());
        request.method = "POST";
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.result);
            MessagePopUp.Instance.EnablePopup(request.result.ToString());
        }
        else
        {
            if (callback != null) callback();
        }
    }


    public delegate void GetAllUsersCallback(User[] users);
    public static IEnumerator GetAllUsers(GetAllUsersCallback callback = null)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL + "/all");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.result);
            MessagePopUp.Instance.EnablePopup(request.result.ToString());
        }
        else
        {
            User[] users = JsonHelper.getJsonArray<User>(request.downloadHandler.text);

            if (callback != null) callback(users);
        }
    }

    public delegate void GetSearchResultCallback(SearchResult[] results);
    public static IEnumerator GetSearchResult(string search_string, GetSearchResultCallback callback)
    {
        string jsonString = "{\"search_string\": \"" + search_string + "\"}";

        UnityWebRequest request = UnityWebRequest.Put(URL + "/search", jsonString);
        request.method = "POST";
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.result);
            MessagePopUp.Instance.EnablePopup(request.result.ToString());
        }
        else
        {
            SearchResult[] results = JsonHelper.getJsonArray<SearchResult>(request.downloadHandler.text);
            callback(results);
        }
    }
}

[System.Serializable]
public class User
{
    public string office_name = "";
    public string office_instagram = "";
    public string email = "";
    public string score = "";
    public string website = "";
    public string rank = "";

    public static string[] KEYS = new string[] { "website", "office_name", "email", "office_instagram" };

    public User() { }

    public User(string website, string office_name, string email, string score, string office_instagram)
    {
        this.email = email;
        this.website = website;
        this.office_name = office_name;
        this.score = score;
        this.office_instagram = office_instagram;
    }

    public static void SaveUser(User user)
    {

        foreach (string key in KEYS)
        {
            PlayerPrefs.SetString(key, user.GetType().GetField(key).GetValue(user).ToString());
        }
        PlayerPrefs.Save();
    }

    public static User GetSavedUser()
    {

        User user = new User();
        foreach (string key in KEYS)
        {
            user.GetType().GetField(key).SetValue(user, PlayerPrefs.GetString(key));
        }

        return user;
    }

    // public static void DeserializeJson(string json)
    // {
    //     foreach (string key in KEYS)
    //     {
    //         string value = "";
    //         string temp = json;
    //         string keyIndex = "\"" + key + "\":";
    //         string comaIndex = "\",";
    //         temp = temp.Substring(temp.IndexOf(keyIndex) + keyIndex.Length);
    //         temp = temp.Substring(0, temp.IndexOf(comaIndex));

    //         Debug.Log(temp);
    //         value = temp;

    //     }
    // }
}

public class JsonHelper
{
    //Usage:
    //YouObject[] objects = JsonHelper.getJsonArray<YouObject> (jsonString);
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    //Usage:
    //string jsonString = JsonHelper.arrayToJson<YouObject>(objects);
    public static string arrayToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.array = array;
        return JsonUtility.ToJson(wrapper);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}

[System.Serializable]
public class SearchResult
{
    public string result;

    public SearchResult(string result)
    {
        this.result = result;
    }
}