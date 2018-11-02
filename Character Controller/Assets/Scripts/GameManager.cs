using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    static GameManager instance;

    public Movement MyCharacter
    { get; set; }

    public static GameManager Instance
    { get { return instance ?? ((instance = new GameManager())); } }

    private GameManager()
    {
        Object.DontDestroyOnLoad(new GameObject("Updater", typeof(Updater)));
    }
    private void Update()
    {

    }
    class Updater : MonoBehaviour
    {
        private void Update()
        {
            instance.Update();
        }
    }
}  

