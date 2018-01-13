﻿namespace MvcForum.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Services;
    using Ioc;
    using Unity;

    public class ForumConfiguration
    {
        private string _mvcForumVersion;

        public string MvcForumVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_mvcForumVersion))
                {
                    _mvcForumVersion = GetConfig("MvcForumVersion");
                }
                return _mvcForumVersion;
            }
        }


        // This is just the initial standard role
        public string StandardMembers => GetConfig("StandardMembers");

        /// <summary>
        ///     Social Login Keys
        /// </summary>
        public string FacebookAppId => GetConfig("FacebookAppId");

        public string FacebookAppSecret => GetConfig("FacebookAppSecret");
        public string MicrosoftAppId => GetConfig("MicrosoftAppId");
        public string MicrosoftAppSecret => GetConfig("MicrosoftAppSecret");
        public string GooglePlusAppId => GetConfig("GooglePlusAppId");
        public string GooglePlusAppSecret => GetConfig("GooglePlusAppSecret");

        /// <summary>
        ///     File Upload Settings
        /// </summary>
        public string FileUploadAllowedExtensions => GetConfig("FileUploadAllowedExtensions");

        public string FileUploadMaximumFileSizeInBytes => GetConfig("FileUploadMaximumFileSizeInBytes");
        public string UploadFolderPath => GetConfig("UploadFolderPath");

        public int PrivateMessageWarningAmountLessThanAllowedSize => Convert.ToInt32(GetConfig("PrivateMessageWarningAmountLessThanAllowedSize"));

        /// <summary>
        ///     Paging options - Amount per page on different pages.
        /// </summary>
        public int PagingGroupSize => Convert.ToInt32(GetConfig("PagingGroupSize"));

        public int AdminListPageSize => Convert.ToInt32(GetConfig("AdminListPageSize"));
        public int ActiveTopicsListSize => Convert.ToInt32(GetConfig("ActiveTopicsListSize"));
        public int SearchListSize => Convert.ToInt32(GetConfig("SearchListSize"));
        public int MembersActivityListSize => Convert.ToInt32(GetConfig("MembersActivityListSize"));
        public int PrivateMessageListSize => Convert.ToInt32(GetConfig("PrivateMessageListSize"));
        public int SimilarTopicsListSize => Convert.ToInt32(GetConfig("SimilarTopicsListSize"));

        /// <summary>
        ///     Post Settings
        /// </summary>
        public bool IncludeFullPostInEmailNotifications =>
            Convert.ToBoolean(GetConfig("IncludeFullPostInEmailNotifications"));

        public int PostSecondsWaitBeforeNewPost => Convert.ToInt32(GetConfig("PostSecondsWaitBeforeNewPost"));

        /// <summary>
        ///     Registration Settings
        /// </summary>
        public bool AutoLoginAfterRegister => Convert.ToBoolean(GetConfig("AutoLoginAfterRegister"));

        /// <summary>
        ///     Social Gravatar size
        /// </summary>
        public int GravatarPostSize => Convert.ToInt32(GetConfig("GravatarPostSize"));

        public int GravatarTopicSize => Convert.ToInt32(GetConfig("GravatarTopicSize"));
        public int GravatarProfileSize => Convert.ToInt32(GetConfig("GravatarProfileSize"));
        public int GravatarLeaderboardSize => Convert.ToInt32(GetConfig("GravatarLeaderboardSize"));
        public int GravatarLikedBySize => Convert.ToInt32(GetConfig("GravatarLikedBySize"));
        public int GravatarLatestBySize => Convert.ToInt32(GetConfig("GravatarLatestBySize"));
        public int GravatarFooterSize => Convert.ToInt32(GetConfig("GravatarFooterSize"));

        // Url names
        public string CategoryUrlIdentifier => GetConfig("CategoryUrlIdentifier");

        public string TopicUrlIdentifier => GetConfig("TopicUrlIdentifier");
        public string TagsUrlIdentifier => GetConfig("TagsUrlIdentifier");
        public string MemberUrlIdentifier => GetConfig("MemberUrlIdentifier");

        /// <summary>
        ///     Which Editor the site should use
        /// </summary>
        public string ChosenEditor => GetConfig("EditorType");


        /// <summary>
        ///     These are the permission names used as keys to return them
        ///     So they must be the same as the database value
        /// </summary>

        // Category Permissions
        public string PermissionReadOnly => GetConfig("PermissionReadOnly");

        public string PermissionDeletePosts => GetConfig("PermissionDeletePosts");
        public string PermissionEditPosts => GetConfig("PermissionEditPosts");
        public string PermissionCreateStickyTopics => GetConfig("PermissionCreateStickyTopics");
        public string PermissionDenyAccess => GetConfig("PermissionDenyAccess");
        public string PermissionLockTopics => GetConfig("PermissionLockTopics");
        public string PermissionVoteInPolls => GetConfig("PermissionVoteInPolls");
        public string PermissionCreatePolls => GetConfig("PermissionCreatePolls");
        public string PermissionCreateTopics => GetConfig("PermissionCreateTopics");
        public string PermissionAttachFiles => GetConfig("PermissionAttachFiles");
        public string PermissionCreateTags => GetConfig("PermissionCreateTags");

        // Global Permissions
        public string PermissionEditMembers => GetConfig("PermissionEditMembers");

        public string PermissionInsertEditorImages => GetConfig("PermissionInsertEditorImages");

        // Database Connection Key
        public string MvcForumContext => GetConfig("MvcForumContext");

        // Default Theme folder
        public string ThemeRootFolder => GetConfig("ThemeRootFolder");

        // Themes
        public string ThemeRootFolderName => GetConfig("ThemeRootFolderName");

        /// <summary>
        ///     Show categories on home page instead of topics
        /// </summary>
        public bool CategoriesOnMain => Convert.ToBoolean(GetConfig("CategoriesOnMain"));


        /// <summary>
        /// Plugin locations
        /// </summary>
        private IList<string> _pluginSearchLocations;
        public IList<string> PluginSearchLocations
        {
            get
            {
                if (_pluginSearchLocations == null)
                {
                    var pluginStringLocations = GetConfig("PluginSearchLocations");
                    if (!string.IsNullOrWhiteSpace(pluginStringLocations))
                    {
                        _pluginSearchLocations = ConfigToListString(pluginStringLocations);
                    }
                }
                return _pluginSearchLocations;                
            }
        }

        /// <summary>
        /// Get the storage provider for the website
        /// </summary>
        public string StorageProviderType => GetPlugin("StorageProviderType");

        /// <summary>
        /// Get a list of badges
        /// </summary>
        private IList<string> _badges;
        public IList<string> Badges
        {
            get
            {
                if (_badges == null)
                {
                    var allBadges = GetPlugin("Badges");
                    if (!string.IsNullOrWhiteSpace(allBadges))
                    {
                        _badges = ConfigToListString(allBadges);
                    }
                }
                return _badges;
            }
        }

        /// <summary>
        /// Gets the User Create Pipes from the config
        /// </summary>
        private IList<string> _userCreatePipes;
        public IList<string> UserCreatePipes
        {
            get
            {
                if (_userCreatePipes == null)
                {
                    var pipes = GetPlugin("UserCreatePipes");
                    if (!string.IsNullOrWhiteSpace(pipes))
                    {
                        _userCreatePipes = ConfigToListString(pipes);
                    }
                }
                return _userCreatePipes;
            }
        }

        /// <summary>
        /// Turns a string config into a list
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private static IList<string> ConfigToListString(string config)
        {
            return config.TrimStart(',')
                        .TrimEnd(',')
                        .Split(',')
                        .Select(x => x.Trim())
                        .ToList();
        }

        #region Singleton

        private static ForumConfiguration _instance;
        private static readonly object InstanceLock = new object();
        private static IConfigService _configService;

        private ForumConfiguration(IConfigService configService)
        {
            _configService = configService;
        }

        public static ForumConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (_instance == null)
                        {
                            var configService = UnityHelper.Container.Resolve<IConfigService>();
                            _instance = new ForumConfiguration(configService);
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Generic Get

        /// <summary>
        ///     This is the generic get config method, you can use this to also get custom config items out
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConfig(string key)
        {
            var dict = _configService.GetForumConfig();
            if (!string.IsNullOrWhiteSpace(key) && dict.ContainsKey(key))
            {
                return dict[key];
            }
            return string.Empty;
        }

        public string GetPlugin(string key)
        {
            var dict = _configService.GetPlugins();
            if (!string.IsNullOrWhiteSpace(key) && dict.ContainsKey(key))
            {
                return dict[key];
            }
            return string.Empty;
        }

        #endregion
    }
}