using UnityEngine;

public class ChangeDancing : MonoBehaviour
{
    public static int current_dance_id = 0;
    private Animator char_anim;

    private void Start()
    {
        char_anim = GetComponent<Animator>();    
    }

    public void ChangeDanceID(int newId)
    {
        if(!current_dance_id.Equals(newId))
            char_anim.SetInteger("id_dance", newId);
        else
        {
            int animHashName = char_anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
            char_anim.Play(animHashName, 0, 0.0f);
            Debug.Log("Animation with hashname "+ animHashName +" replayed from first frame.");
        }

        current_dance_id = newId;

        this.transform.localPosition = new Vector3(0,0,0);
        this.transform.rotation = new Quaternion(0,0,0,0);
    }
}
