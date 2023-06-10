// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("UUNvMFWPT0fMm//umaJAAkCeLX5d1z8ZYcfcKaI84plgyIajE7UoyaYlKyQUpiUuJqYlJSSESqSDGrLV0pX2bKD2ojy5hm/rDC7bioJn1y6r18Kydjvk2Ej7X3iQEEo7oNNAN0ZBZZEY7LflJVfSQbhTGEs9HSYZeFLEAwG/0zuzNC6wjIOIj6T1D6bqJ15f2nhegoD0qTyYUV48Xye+QvxCNtS8Xvtka+Mz+Ldhx1H+ndYe2OqG2FDJtcSy8jjONWEMDbGpY7icpRUW47kzwrO5uX4oT8CgsNc3Zt4GzoWFdgrLQzQZWQtyikYkrBs1FKYlBhQpIi0Oomyi0yklJSUhJCex6yTwXxxQvGMe0ERaX4cqbMdGphBpxFw8XekbVSYnJSQl");
        private static int[] order = new int[] { 10,11,11,5,9,12,9,7,13,11,12,11,12,13,14 };
        private static int key = 36;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
