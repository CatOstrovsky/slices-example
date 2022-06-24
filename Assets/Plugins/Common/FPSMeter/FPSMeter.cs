using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Common
{
    public class FPSMeter : MonoBehaviour
    {
        private const int HistoryLimit = 200;

        public Text value;
        public Text details;
        [Range(0f, 1f)] public float hudRefreshRate = 1f;

        private float timer;
        private int minFPS = -1;
        private int maxFPS = -1;
        private int avg = 0;

        public readonly List<int> fpsHistory = new List<int>();

        private readonly Dictionary<int, Color> colorFPSOptions = new Dictionary<int, Color>
        {
            { 30, Color.green },
            { 15, Color.magenta },
            { 0, Color.red }
        };

        private Color GetFpsColor(int fps)
        {
            foreach (var kvp in colorFPSOptions)
                if (fps >= kvp.Key)
                    return kvp.Value;

            return Color.black;
        }

        private int GetFPS()
        {
            var fps = (int)(1f / Time.unscaledDeltaTime);

            if (minFPS == -1) minFPS = fps;
            if (maxFPS == -1) maxFPS = fps;

            fpsHistory.Add(fps);
            if (fpsHistory.Count > HistoryLimit)
                fpsHistory.RemoveAt(0);

            minFPS = fpsHistory.Min();
            maxFPS = fpsHistory.Max();
            avg = (int)fpsHistory.Average();

            return fps;
        }

        private void DrawGraph()
        {
            /*lineRenderer.positionCount = FPSMeter.HistoryLimit;
            for (int i = 0; i < fpsMeter.fpsHistory.Count; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(i * (cellSize), fpsMeter.fpsHistory[i], 0));
            }*/
        }

        private void Update()
        {
            if (Time.unscaledTime > timer)
            {
                var fps = GetFPS();

                value.text = fps.ToString();
                value.color = GetFpsColor(fps);

                details.text = $"{maxFPS} {minFPS} {avg}";

                timer = Time.unscaledTime + hudRefreshRate;
            }
        }
    }
}