using UnityEngine;
using System.Collections;

public class AnimController : MonoBehaviour {


    private bool holderState, run, walk, aim, fire, scope, silencers, empty, melee, hide, holderkey, Jump;
    private float h, v;
    private int currentState;
    private Animator anim;
    private HashIDs hashs;
    private AudioSource SniperAudio;

    public AudioClip FireSound;
    public AudioClip ReloadSound;
    public AudioClip MeleeSound;
    public AudioClip SilencerSound;
    private Camera FpsCamera;
    const float zoom = 25f;
    const float unzoom = 55f;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        hashs = GetComponent<HashIDs>();
        SniperAudio = GetComponent<AudioSource>();
        FpsCamera = GetComponent<Camera>();
        }

    void GetButtons()
    {
        /*----------------------GET BUTTONS-----------------------------------------*/
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        run = Input.GetKey(KeyCode.LeftShift);
        Jump = Input.GetKey(KeyCode.Space);
        empty = Input.GetKeyDown(KeyCode.R);
        fire = Input.GetButtonDown("Fire1");
        aim = Input.GetButton("Fire2");
        melee = Input.GetKeyDown(KeyCode.F);
        holderkey = Input.GetKeyDown(KeyCode.T);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            hide = !hide;
        }

        if (holderkey) holderState = !holderState;

        if ((v == 0 && h == 0)) run = false;


        if ((v != 0 || h != 0) && !run) walk = true;
        else walk = false;

    }

    // Update is called once per frame
    void Update()
    {
        GetButtons();
        /*----------------------Linking  the Parameters-----------------------------------------*/
        anim.SetBool(hashs.runHash, run);
        anim.SetBool(hashs.walkHash, walk);
        anim.SetBool(hashs.aimHash, aim);
        anim.SetBool(hashs.emptyHash, empty);
        anim.SetBool(hashs.holderStateHash, holderState);
        anim.SetBool(hashs.hideHash, hide);
        anim.SetBool(hashs.fireHash, fire);
        anim.SetBool(hashs.meleeHash, melee);
        anim.SetBool(hashs.scopeHash, scope);
        anim.SetBool(hashs.jumpHash, Jump);

        if (holderkey) anim.SetTrigger(hashs.holderHash);

        if (aim && !scope)
        {
            var newZoom = Mathf.Lerp(FpsCamera.fieldOfView,zoom, Time.deltaTime * 5);
            FpsCamera.fieldOfView = newZoom;
        }
        else
        {
            var oldZoom = Mathf.Lerp(FpsCamera.fieldOfView, unzoom, Time.deltaTime * 7);
            FpsCamera.fieldOfView = oldZoom;
        }

        if (melee) SniperAudio.PlayOneShot(MeleeSound);
    }

    void HolderFn() {

        if (anim.GetBool("HolderState")) anim.SetLayerWeight(1, 1);

        else anim.SetLayerWeight(1, 0);
    }

    void ScopeFn()
    {
        scope = !scope;
            
    }

    void SilencerFn()
    {
        silencers = !silencers;
    }

    void SniperSound(int sounds)
    {

        if (sounds == 1)
        {
            if (!silencers) SniperAudio.PlayOneShot(FireSound);

            else SniperAudio.PlayOneShot(SilencerSound);
        }

        //if (sounds == 2) SniperAudio.PlayOneShot(MeleeSound);
        if (sounds == 3) SniperAudio.PlayOneShot(ReloadSound);
       

    }     
}
