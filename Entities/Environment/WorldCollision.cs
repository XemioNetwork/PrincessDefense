using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Events;
using Xemio.PrincessDefense.Entities.Events;
using Xemio.PrincessDefense.Entities.Components;

namespace Xemio.PrincessDefense.Entities.Environment
{
    public class WorldCollision
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldCollision"/> class.
        /// </summary>
        public WorldCollision(World world)
        {
            this.World = world;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the world.
        /// </summary>
        public World World { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            EventManager eventManager = XGL.GetComponent<EventManager>();
            List<Entity> entities = this.World.ToList();

            for (int i = 0; i < entities.Count; i++)
            {
                for (int j = i; j < entities.Count; j++)
                {
                    BaseEntity a = entities[i] as BaseEntity;
                    BaseEntity b = entities[j] as BaseEntity;

                    if (a == b) continue;
                    if (a.Team == Team.Princess && b.Team == Team.Princess) continue;
                    if (!a.IsDirty && !b.IsDirty) continue;

                    CollidableComponent collisionA = a.GetComponent<CollidableComponent>();
                    CollidableComponent collisionB = b.GetComponent<CollidableComponent>();

                    if (collisionA == null) continue;
                    if (collisionB == null) continue;
                    
                    float totalRadius = collisionA.Radius + collisionB.Radius;
                    float radiusSquared = totalRadius * totalRadius;

                    Vector2 aPosition = a.Position + collisionA.Offset;
                    Vector2 bPosition = b.Position + collisionB.Offset;

                    Vector2 distance =  aPosition - bPosition;

                    if (distance.LengthSquared < radiusSquared)
                    {
                        Vector2 translation = Vector2.Normalize(distance) * (totalRadius - distance.Length);

                        Type typeA = a.GetType();
                        Type typeB = b.GetType();

                        if (collisionA.IsSeperating && collisionB.IsSeperating || typeA == typeB)
                        {
                            if (!collisionA.IsStatic && !collisionB.IsStatic)
                            {
                                a.Position += translation * 0.5f;
                                b.Position -= translation * 0.5f;
                            }
                            else if (!collisionA.IsStatic && collisionB.IsStatic)
                            {
                                a.Position += translation;
                            }
                            else if (collisionA.IsStatic && !collisionB.IsStatic)
                            {
                                b.Position -= translation;
                            }
                        }

                        eventManager.Send(new CollisionEvent(a, b, translation));
                    }
                }
            }
        }
        #endregion
    }
}
