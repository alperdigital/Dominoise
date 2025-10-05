using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.UI.Themes;

namespace Game.UI.Debug
{
    /// <summary>
    /// Performance Test Suite - Sweet UI performans testleri
    /// </summary>
    public sealed class PerformanceTestSuite : MonoBehaviour
    {
        [Header("Performance Test Suite")]
        [SerializeField] private bool autoStartPerformanceTests = false;
        [SerializeField] private int buttonCount = 100;
        [SerializeField] private int animationCount = 50;
        [SerializeField] private float testDuration = 5f;

        private List<GameObject> _testObjects = new List<GameObject>();
        private bool _performanceTestsRunning = false;

        void Start()
        {
            if (autoStartPerformanceTests)
            {
                StartCoroutine(RunPerformanceTests());
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                StartCoroutine(RunPerformanceTests());
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                ClearPerformanceTests();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ShowPerformanceStats();
            }
        }

        [ContextMenu("Run Performance Tests")]
        public void RunPerformanceTestsMenu()
        {
            StartCoroutine(RunPerformanceTests());
        }

        IEnumerator RunPerformanceTests()
        {
            if (_performanceTestsRunning)
            {
                UnityEngine.Debug.Log("🍭 Performance tests already running!");
                yield break;
            }

            _performanceTestsRunning = true;
            UnityEngine.Debug.Log("🍭 === STARTING PERFORMANCE TESTS === 🍭");

            // Test 1: Button Creation Performance
            yield return StartCoroutine(TestButtonCreationPerformance());

            // Test 2: Animation Performance
            yield return StartCoroutine(TestAnimationPerformance());

            // Test 3: Memory Usage
            yield return StartCoroutine(TestMemoryUsage());

            // Test 4: Frame Rate
            yield return StartCoroutine(TestFrameRate());

            UnityEngine.Debug.Log("🍭 === PERFORMANCE TESTS COMPLETED === 🍭");
            _performanceTestsRunning = false;
        }

        IEnumerator TestButtonCreationPerformance()
        {
            UnityEngine.Debug.Log("🍭 Testing Button Creation Performance...");
            
            float startTime = Time.realtimeSinceStartup;
            
            // Create multiple sweet buttons
            for (int i = 0; i < buttonCount; i++)
            {
                if (EightBitTheme.Instance != null)
                {
                    GameObject button = EightBitTheme.Instance.CreateSweetButton(
                        $"🍭 Button {i}", 
                        new Vector2(Random.Range(-400, 400), Random.Range(-400, 400)), 
                        new Vector2(150, 50), 
                        () => UnityEngine.Debug.Log($"🍭 Button {i} clicked!")
                    );
                    
                    _testObjects.Add(button);
                }
            }
            
            float endTime = Time.realtimeSinceStartup;
            float duration = endTime - startTime;
            
            UnityEngine.Debug.Log($"✅ Created {buttonCount} buttons in {duration:F3} seconds");
            UnityEngine.Debug.Log($"✅ Average time per button: {(duration / buttonCount) * 1000:F2} ms");
            
            yield return new WaitForSeconds(1f);
        }

        IEnumerator TestAnimationPerformance()
        {
            UnityEngine.Debug.Log("🍭 Testing Animation Performance...");
            
            float startTime = Time.realtimeSinceStartup;
            
            // Create animated objects
            for (int i = 0; i < animationCount; i++)
            {
                GameObject animObj = new GameObject($"SweetAnim_{i}");
                animObj.transform.SetParent(transform, false);
                
                // Add sweet animation
                SweetTestAnimator animator = animObj.AddComponent<SweetTestAnimator>();
                animator.Initialize();
                
                _testObjects.Add(animObj);
            }
            
            float endTime = Time.realtimeSinceStartup;
            float duration = endTime - startTime;
            
            UnityEngine.Debug.Log($"✅ Created {animationCount} animated objects in {duration:F3} seconds");
            UnityEngine.Debug.Log($"✅ Average time per animation: {(duration / animationCount) * 1000:F2} ms");
            
            yield return new WaitForSeconds(testDuration);
        }

        IEnumerator TestMemoryUsage()
        {
            UnityEngine.Debug.Log("🍭 Testing Memory Usage...");
            
            // Force garbage collection
            System.GC.Collect();
            
            long memoryBefore = System.GC.GetTotalMemory(false);
            
            // Create test objects
            for (int i = 0; i < 100; i++)
            {
                GameObject testObj = new GameObject($"MemoryTest_{i}");
                testObj.AddComponent<SweetTestComponent>();
                _testObjects.Add(testObj);
            }
            
            long memoryAfter = System.GC.GetTotalMemory(false);
            long memoryUsed = memoryAfter - memoryBefore;
            
            UnityEngine.Debug.Log($"✅ Memory before: {memoryBefore / 1024} KB");
            UnityEngine.Debug.Log($"✅ Memory after: {memoryAfter / 1024} KB");
            UnityEngine.Debug.Log($"✅ Memory used: {memoryUsed / 1024} KB");
            
            yield return new WaitForSeconds(1f);
        }

        IEnumerator TestFrameRate()
        {
            UnityEngine.Debug.Log("🍭 Testing Frame Rate...");
            
            float startTime = Time.realtimeSinceStartup;
            int frameCount = 0;
            
            while (Time.realtimeSinceStartup - startTime < testDuration)
            {
                frameCount++;
                yield return null;
            }
            
            float averageFPS = frameCount / testDuration;
            
            UnityEngine.Debug.Log($"✅ Average FPS: {averageFPS:F1}");
            UnityEngine.Debug.Log($"✅ Frame count: {frameCount}");
            
            if (averageFPS >= 60f)
            {
                UnityEngine.Debug.Log("✅ Performance: EXCELLENT");
            }
            else if (averageFPS >= 30f)
            {
                UnityEngine.Debug.Log("✅ Performance: GOOD");
            }
            else
            {
                UnityEngine.Debug.Log("⚠️ Performance: NEEDS OPTIMIZATION");
            }
        }

        [ContextMenu("Clear Performance Tests")]
        public void ClearPerformanceTests()
        {
            UnityEngine.Debug.Log("🍭 Clearing performance tests...");
            
            foreach (GameObject obj in _testObjects)
            {
                if (obj != null)
                {
                    DestroyImmediate(obj);
                }
            }
            
            _testObjects.Clear();
            
            // Force garbage collection
            System.GC.Collect();
            
            UnityEngine.Debug.Log("✅ Performance tests cleared");
        }

        [ContextMenu("Show Performance Stats")]
        public void ShowPerformanceStats()
        {
            UnityEngine.Debug.Log("🍭 === PERFORMANCE STATS === 🍭");
            UnityEngine.Debug.Log($"🍭 Test Objects: {_testObjects.Count}");
            UnityEngine.Debug.Log($"🍭 Memory Usage: {System.GC.GetTotalMemory(false) / 1024} KB");
            UnityEngine.Debug.Log($"🍭 FPS: {1f / Time.deltaTime:F1}");
            UnityEngine.Debug.Log("🍭 ================================ 🍭");
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 320, 400, 200));
            GUILayout.Box("🍭 Performance Test Suite 🍭");
            GUILayout.Space(10);
            
            GUILayout.Label("🎮 Performance Controls:");
            GUILayout.Label("P - Run Performance Tests");
            GUILayout.Label("C - Clear Tests");
            GUILayout.Label("S - Show Stats");
            
            GUILayout.Space(10);
            GUILayout.Label($"Test Objects: {_testObjects.Count}");
            GUILayout.Label($"FPS: {1f / Time.deltaTime:F1}");
            
            GUILayout.EndArea();
        }
    }

    /// <summary>
    /// Sweet test animator for performance testing
    /// </summary>
    public sealed class SweetTestAnimator : MonoBehaviour
    {
        private float rotationSpeed;
        private float scaleSpeed;
        private Vector3 originalScale;

        public void Initialize()
        {
            rotationSpeed = Random.Range(30f, 90f);
            scaleSpeed = Random.Range(1f, 3f);
            originalScale = transform.localScale;
        }

        void Update()
        {
            // Sweet rotation
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            
            // Sweet scale animation
            float scale = Mathf.Sin(Time.time * scaleSpeed) * 0.2f + 0.8f;
            transform.localScale = originalScale * scale;
        }
    }

    /// <summary>
    /// Sweet test component for memory testing
    /// </summary>
    public sealed class SweetTestComponent : MonoBehaviour
    {
        private float[] testData = new float[1000];
        
        void Start()
        {
            // Initialize test data
            for (int i = 0; i < testData.Length; i++)
            {
                testData[i] = Random.Range(0f, 1f);
            }
        }
    }
}
