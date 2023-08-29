using System.Data.Common;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System.Text.RegularExpressions;
using File = System.IO.File;
using System.Linq;
using System;
using UnityEngine.Windows;

public class LoginMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField FieldUser;
    [SerializeField] TMP_InputField FieldPassword;
    [SerializeField] TMP_InputField FieldRePassword;

    [SerializeField] TextMeshProUGUI InputUsername;
    [SerializeField] TextMeshProUGUI InputUserPassword;
    [SerializeField] TextMeshProUGUI InputUserrePassword;

    [SerializeField] TextMeshProUGUI UserNameError;
    [SerializeField] TextMeshProUGUI UserPasswordError;
    [SerializeField] TextMeshProUGUI UserRePasswordError;

    char c = '●';

    private void Start()
    {
        FieldPassword.asteriskChar = c;

        FieldRePassword.asteriskChar = c;
    }

    public bool Name = false;
    public bool Password = false;
    public bool RePassword = false;

    public void InputName()
    {
        if (InputUsername.text.Length < 8)
        {
            UserRePasswordError.color = Color.red;
            UserNameError.text = "User Name Too Short!";
        }
        else if (InputUsername.text.Length >= 32)
        {
            UserRePasswordError.color = Color.red;
            UserNameError.text = "User Name Too Long!";
        }
        else
        {
            UserNameError.color = Color.green;
            UserNameError.text = "User Name Correct!";
            Name = true;
        }
    }
    public void InputPassword()
    {
        var hasMiniMaxChars = new Regex(@".{8,15}");
        string temp = InputUserPassword.text;
        if (temp.Any(Char.IsLetter))
        {
            UserRePasswordError.color = Color.red;
            UserPasswordError.text = "Password should contain at least one lower case letter!";
            Password = false;
        }
        else if (InputUserPassword.text.Any(Char.IsUpper))
        {
            UserRePasswordError.color = Color.red;
            UserPasswordError.text = "Password should contain at least one upper case letter!";
            Password = false;
        }
        else if (!hasMiniMaxChars.IsMatch(InputUserPassword.text))
        {
            UserRePasswordError.color = Color.red;
            UserPasswordError.text = "Password should not be lesser than 8 or greater than 15 characters!";
            Password = false;
        }
        else if (InputUserPassword.text.Any(Char.IsDigit))
        {
            UserRePasswordError.color = Color.red;
            UserPasswordError.text = "Password should contain at least one numeric value!";
            Password = false;
        }
        else if (InputUserPassword.text.Any(Char.IsSymbol))
        {
            UserRePasswordError.color = Color.red;
            UserPasswordError.text = "Password should contain at least one special case character!";
            Password = false;
        }
        else
        {
            UserPasswordError.color = Color.green;
            UserPasswordError.text = "User Password Correct!";
            Password = true;
        }
    }
    public void InputRePassword()
    {
        if (InputUserrePassword.text != InputUserPassword.text)
        {
            UserRePasswordError.color = Color.red;
            UserRePasswordError.text = "User Password Incorrect!";
        }
        else if (InputUserrePassword.text == InputUserPassword.text)
        {
            UserRePasswordError.color = Color.green;
            UserRePasswordError.text = "User Password Ok!";
            RePassword = true;
        }
    }
    public void CheckRegister()
    {
        if (Name && Password && RePassword)
        {
            Debug.Log("User Finish Register!");
        } else
        {
            Debug.Log("Oops Something Wrong!");
        }
        Debug.Log(FieldUser.text);
        Debug.Log(FieldPassword.text);

    }
    public void SaveData()
    {
        string path = Path.Combine(Application.persistentDataPath, "LUUGAME");

        //string username = InputUsername.text;
        //string passwowrd = InputUserPassword.text;

        string username = "username";
        string password = "password";

        DataUser data = new DataUser(username, password);

        string jsonData = JsonUtility.ToJson(data);

        File.WriteAllText(path, jsonData);

    }
    public DataUser LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, "LUUGAME");

        if (!File.Exists(path))
        {
            return null;
        }
        string jsonData = File.ReadAllText(path);

        DataUser data = JsonUtility.FromJson<DataUser>(jsonData);

        return data;

    }
}

public class DataUser
{
    public string taikhoan;
    public string matkhau;
    

    public DataUser(string taikhoan, string matkhau)
    {
        this.taikhoan = taikhoan;
        this.matkhau = matkhau;
    }
}
