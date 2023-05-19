using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    using HashCode = Int32;

    public class Engine
    {
        private Dictionary<HashCode, System> m_Systems;
        private Dictionary<HashCode, HashSet<Type>> m_Dependencies;

        private Queue<Entity> m_AvailableEntities;
        private long m_LivingEntityCount;

        private Dictionary<HashCode, IContainer> m_Components;

        public Engine()
        {
            m_Systems = new Dictionary<int, System>();
            m_Dependencies = new Dictionary<HashCode, HashSet<Type>>();

            m_AvailableEntities = new Queue<Entity>();
            m_LivingEntityCount = 0;

            for (int i = 0; i < Entity.MAX_ENTITIES; i++)
            {
                m_AvailableEntities.Enqueue(new Entity(i));
            }

            m_Components = new Dictionary<HashCode, IContainer>();
        }


        public void Update(GameTime aGameTime)
        {
            foreach (var (hashCode, system) in m_Systems)
            {
                if (system.IsEnabled)
                    system.Update(aGameTime);
            }
        }

        public void Render(SpriteBatch aRenderer)
        {
            foreach (var (hashCode, system) in m_Systems)
            {
                if (system.IsEnabled)
                    system.Render(aRenderer);
            }
        }

        // Entity
        public Entity CreateEntity()
        {
            if (Entity.MAX_ENTITIES <= m_LivingEntityCount)
            {
                throw new Exception("Max entities reached");
            }

            Entity entity = m_AvailableEntities.Dequeue();
            m_LivingEntityCount++;
            return entity;
        }
        public void DestroyEntity(Entity aEntity)
        {
            // remove entity
            if (aEntity.ID > m_LivingEntityCount)
            {
                throw new Exception("Entity does not exist");
            }

            m_AvailableEntities.Enqueue(aEntity);
            m_LivingEntityCount--;

            // remove components
            foreach (var (hashCode, container) in m_Components)
            {
                container.Unregister(aEntity);
            }

            // remove entity from systems
            foreach (var (hashCode, system) in m_Systems)
            {
                system.Unregister(aEntity);
            }
        }

        public void AddComponent<ComponentT>(Entity aEntity, ComponentT aComponent) where ComponentT : Component
        {
            var type = typeof(ComponentT);
            HashCode hashCode = type.GetHashCode();

            bool contains = m_Components.ContainsKey(hashCode);
            if (!contains)
            {
                m_Components.Add(hashCode, new Container<ComponentT>());
            }

            if (m_Components[hashCode] is not Container<ComponentT>)
            {
                throw new Exception("ComponentArray is not child of Component");
            }

            var container = m_Components[hashCode] as Container<ComponentT>;
            container.Register(aEntity, aComponent);

            UpdateDependencies(aEntity, type);
        }
        public void RemoveComponent<ComponentT>(Entity aEntity) where ComponentT : Component
        {
            var type = typeof(ComponentT);
            HashCode hashCode = type.GetHashCode();

            bool contains = m_Components.ContainsKey(hashCode);
            if (!contains)
            {
                throw new Exception("Component not registered");
            }

            var container = m_Components[hashCode] as Container<ComponentT>;
            container.Unregister(aEntity);

            UpdateDependencies(aEntity, type);
        }
        public ComponentT GetComponent<ComponentT>(Entity aEntity) where ComponentT : Component
        {
            var type = typeof(ComponentT);
            HashCode hashCode = type.GetHashCode();

            bool contains = m_Components.ContainsKey(hashCode);
            if (!contains)
            {
                throw new Exception("Component not registered");
            }

            var container = m_Components[hashCode] as Container<ComponentT>;
            return container.GetData(aEntity);
        }

        // Systems
        public void RegisterSystem<SystemT>() where SystemT : System
        {
            var type = typeof(SystemT);
            HashCode hashCode = type.GetHashCode();

            bool contains = m_Systems.ContainsKey(hashCode);
            if (contains)
            {
                throw new Exception("System already registered");
            }

            SystemT system = (SystemT)Activator.CreateInstance(type, this);
            m_Dependencies.Add(hashCode, system.Requirements); // add signatures 

            m_Systems.Add(hashCode, system);
        }

        public void UnregisterSystem<SystemT>() where SystemT : System
        {
            var type = typeof(SystemT);
            HashCode hashCode = type.GetHashCode();

            bool contains = m_Systems.ContainsKey(hashCode);
            if (!contains)
            {
                throw new Exception("System not registered");
            }

            m_Systems.Remove(hashCode);
        }
        public SystemT GetSystem<SystemT>() where SystemT : System
        {
            var type = typeof(SystemT);
            HashCode hashCode = type.GetHashCode();

            bool contains = m_Systems.ContainsKey(hashCode);
            if (!contains)
            {
                throw new Exception("System not registered");
            }

            return (SystemT)m_Systems[hashCode];
        }

        private Container<ComponentT> GetContainer<ComponentT>() where ComponentT : Component
        {
            var type = typeof(ComponentT);
            HashCode hashCode = type.GetHashCode();

            bool contains = m_Components.ContainsKey(hashCode);
            if (!contains)
            {
                throw new Exception("Component not registered");
            }

            return m_Components[hashCode] as Container<ComponentT>;
        }

        private void UpdateDependencies(Entity aEntity, Type aDependency)
        {
            foreach (var (hashCode, system) in m_Systems)
            {
                var dependencies = m_Dependencies[hashCode];

                bool contains = dependencies.Contains(aDependency);
                if (contains)
                {
                    system.Register(aEntity);
                }
                else
                {
                    system.Unregister(aEntity);
                }
            }
        }
    }
}