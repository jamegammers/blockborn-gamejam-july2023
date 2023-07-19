namespace Util {

    public static class DebugUtil {

        public static string ToString<T>(this T[] array) {
            string result = "[";
            for (int i = 0; i < array.Length; i++) {
                result += array[i].ToString();
                if (i < array.Length - 1)
                    result += ", ";
            }
            result += "]";
            return result;
        }

    }

}