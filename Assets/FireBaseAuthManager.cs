using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
using System;

public class FireBaseAuthManager : MonoBehaviour
{
    FirebaseAuth auth_;
    FirebaseUser user_;
    
    public TMP_InputField email;
    public TMP_InputField password;
    // Start is called before the first frame update

    void Start()
    {
        auth_ = FirebaseAuth.DefaultInstance;
        auth_.StateChanged += OnChangeAuthState;
    }

    private void OnChangeAuthState(object sender, EventArgs e)
    {
        if (auth_.CurrentUser != user_)
        {
            bool signed = auth_.CurrentUser != user_ && auth_.CurrentUser != null;
            if (!signed && user_ != null)
            {
                Debug.Log("이벤트-로그아웃" + e.ToString());
            }

            user_ = auth_.CurrentUser;
            if (signed)
            {
                Debug.Log("이벤트-로그인 " + e.ToString());
            }
        }
    }


    public void Create()
    {
        auth_.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(t =>
        {
            if (t.IsCanceled)
            {
                Debug.Log("회원가입 취소");
                return;
            }
            if (t.IsFaulted)
            {
                Debug.Log("회원가입 실패");
                return;
            }

            FirebaseUser newUser = t.Result.User;
            Debug.Log("회원가입 완료");

        });
    }

    public void LogIn()
    {
        auth_.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(t =>
        {
            if (t.IsCanceled)
            {
                Debug.Log("로그인 취소");
                return;
            }
            if (t.IsFaulted)
            {
                Debug.Log("로그인 실패");
                return;
            }

            FirebaseUser newUser = t.Result.User;
            Debug.Log("로그인 완료");

        });
    }
    public void LogOut()
    {
        auth_.SignOut();
        Debug.Log("logout");
    }

    public void CreateGuest()
    {
    }
}
