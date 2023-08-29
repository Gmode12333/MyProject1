using System.Text;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Items / Item")]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string ItemName;
    public Sprite Icon;
    [Range(1, 10000)]
    public int MaximumStack = 1;
    protected static readonly StringBuilder sb = new StringBuilder();

    public virtual Item GetCopy()
    {
        return this;
    }
    public virtual void Destroy()
    {

    }
    public virtual string GetItemType()
    {
        return "";
    }
    public virtual string GetDescription()
    {
        return "";
    }
}
