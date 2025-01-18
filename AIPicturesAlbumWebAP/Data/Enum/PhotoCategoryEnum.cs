using System.ComponentModel;

namespace AIPicturesAlbumWebAP.Data.Enum
{
    public enum PhotoCategoryEnum
    {
        [Description("AI繪圖")]
        AI = 0,

        [Description("人像攝影")]
        Human = 1,

        [Description("Cosplay")]
        Cosplay = 2,
    }
}
