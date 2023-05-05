using UnityEngine;
using UnityEngine.UI;

public class playerPresenter : MonoBehaviour
{
   [SerializeField]
   private SpriteRenderer _spriteRenderer;

   [SerializeField] 
   private SkinTypeID _typeID;
   
   public SkinTypeID TypeID => 
       _typeID;
   
   public void Construct(Sprite sprite)
   {
       _spriteRenderer.sprite = sprite;
   }
}
