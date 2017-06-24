using System;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.SharedKernel.Model
{
    public abstract class Entity
    {
        public virtual  Guid Id { get; set; }

        protected Entity():this(LiveGuid.NewGuid())
        {
        }

        protected Entity(Guid id)
        {
            Id = id;
        }
    }
}