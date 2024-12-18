using System;
using System.Collections;
using System.Collections.Generic;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Logging;
using UnityEngine;
using UnityEngine.Networking;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Utilities
{
    /// <summary>
    /// Manages asynchronous loading and caching of remote images.
    /// Allows concurrent requests for the same URL to share callbacks.
    /// </summary>
    public static class YGRemoteFile
    {
        // Dictionary to store downloaded images by URL
        private static readonly Dictionary<string, Texture2D> _imageCache = new();

        // Dictionary to manage callbacks for URLs currently being downloaded
        private static readonly Dictionary<string, Action<Texture2D>> _pendingCallbacks = new();

        /// <summary>
        /// Loads an image from a URL asynchronously.
        /// If the image is cached, the callback is invoked immediately.
        /// If already downloading, the callback is queued.
        /// </summary>
        /// <param name="url">The URL of the image.</param>
        /// <param name="callback">Callback function to handle the downloaded Texture2D.</param>
        public static void LoadImage(string url, Action<Texture2D> callback)
        {
            if (string.IsNullOrEmpty(url))
            {
                YGLogger.Error("YGRemoteFile.LoadImage: URL is null or empty.");
                callback?.Invoke(null);
                return;
            }

            if (_imageCache.TryGetValue(url, out var cachedTexture))
            {
                callback?.Invoke(cachedTexture);
                return;
            }

            if (!_pendingCallbacks.TryAdd(url, callback))
            {
                _pendingCallbacks[url] += callback;
                return;
            }

            YGCoroutineManager.Instance.StartYGCoroutine(DownloadImageCoroutine(url));
        }

        /// <summary>
        /// Coroutine that downloads an image from the URL and caches it.
        /// Invokes all queued callbacks upon completion.
        /// </summary>
        /// <param name="url">The URL of the image to download.</param>
        /// <returns>IEnumerator for coroutine.</returns>
        private static IEnumerator DownloadImageCoroutine(string url)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                yield return request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
                if (request.result != UnityWebRequest.Result.Success)
#else
                if (request.isNetworkError || request.isHttpError)
#endif
                {
                    YGLogger.Error(
                        $"YGRemoteFile.DownloadImageCoroutine: Error downloading image from {url}: {request.error}");
                    _pendingCallbacks[url]?.Invoke(null);
                }
                else
                {
                    // Cache the downloaded texture
                    Texture2D texture = DownloadHandlerTexture.GetContent(request);
                    _imageCache[url] = texture;
                    _pendingCallbacks[url]?.Invoke(texture);
                }

                // Clean up callbacks for this URL
                _pendingCallbacks.Remove(url);
            }
        }

        /// <summary>
        /// Clears the image cache.
        /// </summary>
        public static void ClearCache()
        {
            _imageCache.Clear();
        }
    }
}