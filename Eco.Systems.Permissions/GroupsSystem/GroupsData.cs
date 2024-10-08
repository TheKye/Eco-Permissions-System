﻿using Eco.Systems.Permissions.Utils;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eco.Systems.Permissions.Groups
{
    [Serializable]
    public class GroupsData
    {
        public static event Action<string> GroupCreated;
        public static event Action<string> GroupDeleted;

        public HashSet<Group> Groups { get; private set; }
         public HashSet<SimpleGroupUser> AllUsers { get; private set; }

        public GroupsData() 
        {
            Groups = new HashSet<Group>();
            AllUsers = new HashSet<SimpleGroupUser>();
        }

        // May return null if not set to create, so make sure to protect for those cases.
        public Group GetOrAddGroup(string dirtyGroupName, bool create = false)
        {
            var cleanGroupName = StringUtils.Sanitize(dirtyGroupName);
            Group group = Groups.FirstOrDefault(g => g.GroupName == cleanGroupName);

            if (group == null && create)
            {
                group = new Group(cleanGroupName);
                Groups.Add(group);
            }

            GroupCreated?.Invoke(group.GroupName);

            return group;
        }

        public bool DeleteGroup(string dirtyGroupName)
        {
            var cleanGroupName = StringUtils.Sanitize(dirtyGroupName);

            Group group = Groups.FirstOrDefault(g => g.GroupName == cleanGroupName);

            if (group == null)
                return false;

            if (GroupDeleted != null) GroupDeleted.Invoke(group.GroupName);

            return Groups.Remove(group);
        }

        public SimpleGroupUser GetGroupUser(User user) =>

            AllUsers.FirstOrDefault(entry => entry.Name == user.Name && (entry.SlgID == user.StrangeId || entry.SteamID == user.SteamId));

        public SimpleGroupUser GetGroupUser(IChatClient chatClient)
        {
            return AllUsers.FirstOrDefault(entry => entry.Name == chatClient.Name);
        }
    }
}