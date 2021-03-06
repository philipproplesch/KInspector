﻿using System;
using System.Data.SqlClient;
using System.IO;

namespace KInspector.Core
{
    public class InstanceInfo
    {
        private Lazy<DatabaseService> dbService;
        private Lazy<Version> version;
        private Lazy<Uri> uri;
        private Lazy<DirectoryInfo> directory;


        /// <summary>
        /// URI of the application instance
        /// </summary>
        public Uri Uri
        {
            get
            {
                return uri.Value;
            }
        }


        /// <summary>
        /// Directory of the application instance
        /// </summary>
        public DirectoryInfo Directory
        {
            get
            {
                return directory.Value;
            }
        }


        /// <summary>
        /// Version of the intance based on the database setting key.
        /// </summary>
        public Version Version
        {
            get
            {
                return version.Value;
            }
        }


        /// <summary>
        /// Configuration with instance information.
        /// </summary>
        public InstanceConfig Config { get; private set; }


        /// <summary>
        /// Database service to communicate with the instance database.
        /// </summary>
        public DatabaseService DBService
        {
            get
            {
                return dbService.Value;
            }
        }


        /// <summary>
        /// Creates instance information based on configuration.
        /// </summary>
        /// <param name="config">Instance configuration</param>
        public InstanceInfo(InstanceConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("version");
            }

            Config = config;

            dbService = new Lazy<DatabaseService>(() => new DatabaseService(Config));
            version = new Lazy<Version>(() => GetKenticoVersion());
            uri = new Lazy<Uri>(() => new Uri(Config.Url));
            directory = new Lazy<DirectoryInfo>(() => new DirectoryInfo(Config.Path));
        }


        /// <summary>
        /// Gets the version of Kentico.
        /// </summary>
        private Version GetKenticoVersion()
        {
            string version = DBService.ExecuteAndGetScalar<string>("SELECT KeyValue FROM CMS_SettingsKey WHERE KeyName = 'CMSDBVersion'");
            return new Version(version);
        }
    }
}
