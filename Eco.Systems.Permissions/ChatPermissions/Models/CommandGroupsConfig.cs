﻿using Eco.Shared.Serialization;
using System;

namespace Eco.Systems.Permissions.Permissions
{
    [Serializable]
    public class CommandGroupsConfig
    {
        [Serialized] public bool DefaultAdminBehaviour { get; set; } = true;
        [Serialized] public bool DefaultUserBehaviour { get; set; } = true;

        public CommandGroupsConfig()
        {
            DefaultAdminBehaviour = true;
            DefaultUserBehaviour = true;
        }
    }
}
