﻿using System;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Config;
using LiveHTS.Core.Model.Config;

namespace LiveHTS.Infrastructure.Repository.Config
{
    public class RelationshipTypeRepository : BaseRepository<RelationshipType,string>, IRelationshipTypeRepository
    {
        public RelationshipTypeRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
    }
}