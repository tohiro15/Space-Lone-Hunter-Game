using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Logging;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Networking
{
    public static class YGRequestManager
    {
        private static Dictionary<string, CallbackInfo> callbackMap = new Dictionary<string, CallbackInfo>();

        public static string GenerateRequestId()
        {
            return Guid.NewGuid().ToString();
        }

        public static void RegisterCallback<T>(string requestId, Action<bool, T, string> callback)
        {
            callbackMap[requestId] = new CallbackInfo
            {
                Callback = callback,
                DataType = typeof(T)
            };
        }

        public static void HandleJSResponse(string jsonResponse)
        {
            try
            {
                YGLogger.Info($"Received JS response: {jsonResponse}");
                var responseWrapper = JsonConvert.DeserializeObject<ResponseWrapper>(jsonResponse);

                if (callbackMap.TryGetValue(responseWrapper.requestId, out var callbackInfo))
                {
                    var dataType = callbackInfo.DataType;
                    var callback = callbackInfo.Callback;

                    if (responseWrapper.status)
                    {
                        object data = null;

                        if (!string.IsNullOrEmpty(responseWrapper.data))
                        {
                            if (dataType == typeof(string))
                            {
                                data = responseWrapper.data;
                            }
                            else if (dataType.IsPrimitive)
                            {
                                data = Convert.ChangeType(responseWrapper.data, dataType);
                            }
                            else
                            {
                                data = JsonConvert.DeserializeObject(responseWrapper.data, dataType);
                            }

                            if (callbackInfo.Callback is Action<bool, object, string> objectCallback)
                            {
                                objectCallback.Invoke(responseWrapper.status, data, responseWrapper.error);
                            }
                            else
                            {
                                YGLogger.Warning("Callback is not of the expected type Action<bool, object, string>.");

                                callback.DynamicInvoke(responseWrapper.status, data, responseWrapper.error);
                            }
                        }
                    }
                    else
                    {
                        if (callbackInfo.Callback is Action<bool, object, string> objectCallback)
                        {
                            objectCallback.Invoke(responseWrapper.status, default, responseWrapper.error);
                        }
                        else
                        {
                            YGLogger.Warning("Callback is not of the expected type Action<bool, object, string>.");

                            callback.DynamicInvoke(responseWrapper.status, default, responseWrapper.error);
                        }
                    }
                }
                else
                {
                    YGLogger.Warning($"No callback found for requestId: {responseWrapper.requestId}");
                }
            }
            catch (Exception ex)
            {
                YGLogger.Error($"Error processing JS response: {ex.Message}\nStack Trace: {ex.StackTrace}");
            }
        }


        private static object DeserializeData(string dataJson, Type dataType)
        {
            if (string.IsNullOrEmpty(dataJson))
            {
                return dataType.IsValueType ? Activator.CreateInstance(dataType) : null;
            }

            return JsonConvert.DeserializeObject(dataJson, dataType);
        }


        private class CallbackInfo
        {
            public Delegate Callback;
            public Type DataType;
        }

        private class ResponseWrapper
        {
            public string requestId;
            public bool status;
            public string data;
            public string error;
        }
    }
}