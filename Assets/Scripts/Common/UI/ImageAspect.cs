//using Unity
using UnityEngine;

namespace Common.UI
{
    //Makes sure an image maintains a specific aspect based on a specified max and min
    //Uses method overloading where the params determine to use a texture or an AVPRO windows media movie
    public static class ImageAspect
    {

        public static Vector2 MaintainAspectRatio(Texture image, int maxWidth, int maxHeight)
        {
            return Ratio(image.height, image.width, maxWidth, maxHeight);
        }

        public static Vector2 MaintainAspectRatio(Vector2 rect, int maxWidth, int maxHeight)
        {
            return Ratio(rect.y, rect.x, maxWidth, maxHeight);
        }

        private static Vector2 Ratio(float height,float width,int maxWidth,int maxHeight)
        {
            if (((maxWidth / maxHeight) < (width / height)))
            {
                return new Vector2(maxWidth, maxWidth / (width / height));
            }
            else
            {
                return new Vector2(maxHeight / (height / width), maxHeight);
            }
        }

    }
}
