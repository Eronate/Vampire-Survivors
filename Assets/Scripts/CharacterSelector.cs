using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Animations;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;
    public CharacterScriptableObject characterData;
    public Sprite characterSprite;
    public RuntimeAnimatorController characterAnimatorController;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Extra " + this + " deleted.");
            Destroy(gameObject);
        }

    }
    public static CharacterScriptableObject GetData()
    {
        return instance.characterData; 
    }  
    public static Sprite GetSprite()
    {
        return instance.characterSprite;
    }
    public static RuntimeAnimatorController GetAnimator()
    {
        return instance.characterAnimatorController;
    }
    public void SelectCharacter(CharacterScriptableObject character)
    {
        characterData = character;
    }
    public void SelectSprite(Sprite sprite)
    {
        characterSprite = sprite;
    }
    public void SelectAnimatorController(RuntimeAnimatorController controller)
    {
        characterAnimatorController = controller;
    }
    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
