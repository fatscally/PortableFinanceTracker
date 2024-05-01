using PFT.Base;

namespace PFT.Interfaces
{
    interface IItems_Tags_ColData
    {
        Transaction_Tag_Col LoadAll();
        Transaction_Tag_Col LoadByItemId(int itemId);
        Transaction_Tag_Col LoadByTagId(int tagId);
    }
}
