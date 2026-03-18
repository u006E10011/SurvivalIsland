namespace YTools
{
    public class Data
    {
        [System.Serializable]
        public struct MinMax
        {
            public MinMax(float min, float max)
            {
                Min = min;
                Max = max;
            }

            public float Min;
            public float Max;

        }
    }
}