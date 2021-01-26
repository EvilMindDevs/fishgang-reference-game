using HuaweiMobileServices.Base;
using HuaweiMobileServices.Game;
using HuaweiMobileServices.Id;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.HMS
{
    public class HMSGameService
    {
        private static string TAG = "HMSGameService";

        private IRankingsClient _rankingClient;
        private IAchievementsClient _achievementClient;

        //public AuthHuaweiId HuaweiId;

        public CommonAuthUser commonAuthUser = null;

        public void Init(AuthHuaweiId huaweiId)
        {
            //Load IJosAppClient for HMS
            HuaweiMobileServicesUtil.SetApplication();
            IJosAppsClient josAppsClient = JosApps.GetJosAppsClient(huaweiId);
            josAppsClient.Init();

            _rankingClient = Games.GetRankingsClient(huaweiId);
            _achievementClient = Games.GetAchievementsClient(huaweiId);
        }


        public void SendScore(int score, string boardId)
        {
            if (!HMSManager.Instance.accountKit.IsAuthenticated())
            {
                Debug.Log(TAG + ": SendScore failed! User is not authenticated!");
                return;
            }

            ITask<ScoreSubmissionInfo> task = _rankingClient.SubmitScoreWithResult(boardId, score);
            task.AddOnSuccessListener((scoreInfo) =>
            {
                Debug.Log(TAG + ": SendScore is succeeed. Leader board is " + boardId);
            }).AddOnFailureListener((error) =>
            {
                Debug.Log(TAG + ": SendScore is failed to leader board: " + boardId + " " + error.WrappedExceptionMessage);
            });
        }

        public void ShowLeaderBoard(string boardId = "")
        {
            if (!HMSManager.Instance.accountKit.IsAuthenticated())
            {
                Debug.Log(TAG + ": SendScore failed! User is not authenticated! Trying to login...");
                HMSManager.Instance.accountKit.AuthenticateUser(success =>
                {
                    if (success)
                    {
                        _rankingClient.ShowTotalRankings(() =>
                        {
                            Debug.Log(TAG + ": User is authenticated! Rankinsg are showed!");

                        }, (exception) =>
                        {
                            Debug.Log(TAG + ": ShowLeaderboards ERROR - " + exception.WrappedExceptionMessage);
                        });
                    }
                    else
                    {
                        Debug.Log(TAG + ": ShowLeaderboards connection is failed!");
                    }
                });
            }
            else
            {
                _rankingClient.ShowTotalRankings(() =>
                {
                    Debug.Log(TAG + ": User is authenticated! Rankinsg are showed!");
                }, (exception) =>
                {
                    Debug.Log(TAG + ": ShowLeaderboards ERROR - " + exception.WrappedExceptionMessage);
                });
            }
        }

        public void ShowAchievements()
        {
            if (!HMSManager.Instance.accountKit.IsAuthenticated())
            {
                Debug.Log(TAG + ": ShowAchievements failed! User is not authenticated! Trying to login...");
                HMSManager.Instance.accountKit.AuthenticateUser(success =>
                {
                    if (success)
                    {
                        _achievementClient.ShowAchievementList(() =>
                        {
                            Debug.Log(TAG + ": User is authenticated! ShowAchievementList are showed!");
                        },
                        (exception) =>
                        {
                            Debug.Log(TAG + ": ShowAchievementList ERROR - " + exception.WrappedExceptionMessage);
                        });
                    }
                    else
                    {
                        Debug.Log(TAG + ": ShowAchievementList connection is failed!");
                    }
                });
            }
            else
            {
                _achievementClient.ShowAchievementList(() =>
                {
                    Debug.Log(TAG + ": User is authenticated! ShowAchievementList are showed!");
                },
                (exception) =>
                {
                    Debug.Log(TAG + ": ShowAchievementList ERROR - " + exception.WrappedExceptionMessage);
                });
            }
        }

        public void UnlockAchievement(string key)
        {
            if (!HMSManager.Instance.accountKit.IsAuthenticated())
            {
                Debug.Log(TAG + ": UnlockAchievement is failed! User is not authenticated!");
                return;
            }

            ITask<HuaweiMobileServices.Utils.Void> task = _achievementClient.ReachWithResult(key);
            task.AddOnSuccessListener((result) =>
            {
                Debug.Log(TAG + ":achievements is unlocked successfully for " + key);

            }).AddOnFailureListener((exception) =>
            {
                Debug.Log(TAG + ": UnlockAchievements ERROR: " + exception.WrappedExceptionMessage);
            });
        }

        public CommonAuthUser GetUserInfo()
        {
            return commonAuthUser;
        }
    }
}