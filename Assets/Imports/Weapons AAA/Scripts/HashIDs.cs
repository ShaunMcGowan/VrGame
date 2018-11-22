using UnityEngine;
using System.Collections;
public class HashIDs : MonoBehaviour
{
    [HideInInspector]    public int holderHash;
    [HideInInspector]    public int holderStateHash;
    [HideInInspector]    public int runHash;
    [HideInInspector]    public int walkHash;
    [HideInInspector]    public int scopeHash;
    [HideInInspector]    public int aimHash;
    [HideInInspector]    public int fireHash;
    [HideInInspector]    public int reloadHash;
    [HideInInspector]    public int meleeHash;
    [HideInInspector]    public int emptyHash;
    [HideInInspector]    public int hideHash;
    [HideInInspector]    public int jumpHash;

    void Awake()
    {
        holderHash = Animator.StringToHash("Holder");
        holderStateHash = Animator.StringToHash("HolderState");
        runHash = Animator.StringToHash("Run");
        walkHash = Animator.StringToHash("Walk");
        scopeHash = Animator.StringToHash("Scope");
        aimHash = Animator.StringToHash("AIM");
        fireHash = Animator.StringToHash("Fire");
        reloadHash = Animator.StringToHash("Reload");
        meleeHash = Animator.StringToHash("Melee");
        emptyHash = Animator.StringToHash("Empty");
        hideHash = Animator.StringToHash("Hide");
        jumpHash = Animator.StringToHash("Jump");

    }

}
  