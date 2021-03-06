﻿using System;
using SQLite;

namespace LiveHTS.SharedKernel.Model
{
    public interface IEntity<TId> : IEquatable<Entity<TId>>
    {
        TId Id { get; set; }
        bool Voided { get; set; }
    }
   
    public abstract class Entity<TId> : IEntity<TId>
    {
        [PrimaryKey]
        public virtual TId Id { get; set; }
        public virtual bool Voided { get; set; }
        public Entity()
        {
            
        }
        public Entity(TId id)
        {
            Id = id;
        }
        public override bool Equals(object obj)
        {
            var entity = obj as Entity<TId>;
            if (entity != null)
            {
                return this.Equals(entity);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        #region IEquatable<Entity> Members
        public bool Equals(Entity<TId> other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }
        #endregion
    }
}