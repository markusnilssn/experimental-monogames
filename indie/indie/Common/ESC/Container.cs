using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public interface IContainer
    {
        public void Unregister(Entity aEntity);
    }

    public class Container<ComponentT> : IContainer where ComponentT : Component
    {
        private ComponentT[] m_Components;

        private Dictionary<Entity, int> m_EntityToIndex;
        private Dictionary<int, Entity> m_IndexToEntity;

        private int m_Length;

        public Container()
        {
            m_Components = new ComponentT[Entity.MAX_ENTITIES];
            m_EntityToIndex = new Dictionary<Entity, int>();
            m_IndexToEntity = new Dictionary<int, Entity>();
            m_Length = 0;
        }

        public void Register(Entity aEntity, ComponentT aComponent)
        {
            bool contains = m_EntityToIndex.ContainsKey(aEntity);
            if (contains)
            {
                throw new Exception("Entity already exists");
            }

            int newIndex = m_Length;
            m_EntityToIndex.Add(aEntity, newIndex);
            m_IndexToEntity.Add(newIndex, aEntity);
            m_Components[newIndex] = aComponent;
            m_Length++;
        }

        public void Unregister(Entity aEntity)
        {
            bool contains = m_EntityToIndex.ContainsKey(aEntity);
            if (!contains)
            {
                throw new Exception("Entity does not exist");
            }

            int indexOfRemovedEntity = m_EntityToIndex[aEntity];
            int indexOfLastElement = m_Length - 1;
            m_Components[indexOfRemovedEntity] = m_Components[indexOfLastElement];

            // Update map to point to moved spot
            Entity entityOfLastElement = m_IndexToEntity[indexOfLastElement];
            m_EntityToIndex[entityOfLastElement] = indexOfRemovedEntity;
            m_IndexToEntity[indexOfRemovedEntity] = entityOfLastElement;

            m_EntityToIndex.Remove(aEntity);
            m_IndexToEntity.Remove(indexOfLastElement);

            m_Length--;
        }

        public ComponentT GetData(Entity aEntity)
        {
            bool contains = m_EntityToIndex.ContainsKey(aEntity);
            if (!contains)
            {
                throw new Exception("Entity does not exist");
            }

            return m_Components[m_EntityToIndex[aEntity]];
        }
    }
}
