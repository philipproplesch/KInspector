﻿using System;
using KInspector.Core;

namespace KInspector.Modules.Modules.Setup
{
    public class SitesSetupModule : IModule
    {
        public ModuleMetadata GetModuleMetadata()
        {
            return new ModuleMetadata
            {
                Name = "Stop all sites",
                Comment = "Stops all running sites by setting their Status to 'STOPPED'. The IsOffline is left intact.",
                SupportedVersions = new[] {
                    new Version("8.0"), 
                    new Version("8.1"), 
                    new Version("8.2") 
                },
                Category = "Setup",
            };
        }

        public ModuleResults GetResults(InstanceInfo instanceInfo)
        {
            var dbService = instanceInfo.DBService;
            var results = dbService.ExecuteAndGetDataSetFromFile("Setup/SitesSetupModule.sql");

            return new ModuleResults
            {
                Result = results
            };
        }
    }
}
